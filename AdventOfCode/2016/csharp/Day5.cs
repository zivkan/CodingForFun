using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using Xunit;
using Xunit.Abstractions;

namespace csharp
{
    public class Day5
    {
        private readonly ITestOutputHelper _output;

        public Day5(ITestOutputHelper output)
        {
            this._output = output;
        }

        [Fact]
        public void Part1SampleSingleThread()
        {
            char[] code = new char[8];
            int codeIndex = 0;
            string doorId = "abc";

            using (var md5 = MD5.Create())
            {
                for (int i = 0;; i++)
                {
                    var hash = md5.ComputeHash(Encoding.ASCII.GetBytes(doorId + i));
                    if (hash[0] == 0 && hash[1] == 0 && (hash[2] & 0xF0) == 0)
                    {
                        char c = (hash[2] & 0x0F).ToString("x")[0];
                        code[codeIndex] = c;
                        codeIndex++;
                        if (codeIndex == code.Length)
                        {
                            break;
                        }
                    }
                }
            }

            string finalCode = new string(code);
            Assert.Equal("18f47a30", finalCode);
        }

        [Fact]
        public void Part1Sample()
        {
            var doorId = "abc";
            var finalCode = GetPart1Code(doorId);
            Assert.Equal("18f47a30", finalCode);
        }

        [Fact]
        public void Part1()
        {
            var doorId = GetPuzzleInput.Day(5).ReadToEnd();
            var code = GetPart1Code(doorId);
            _output.WriteLine(code);
        }

        [Fact]
        public void Part2Sample()
        {
            var doorId = "abc";
            var finalCode = GetPart2Code(doorId);
            Assert.Equal("05ace8e3", finalCode);
        }

        [Fact]
        public void Part2()
        {
            var doorId = GetPuzzleInput.Day(5).ReadToEnd();
            var code = GetPart2Code(doorId);
            _output.WriteLine(code);
        }

        private string GetPart1Code(string doorId)
        {
            var state = new Part1State
            {
                DoorId = doorId,
                BatchSize = 10000,
                Next = 0,
                Found = new ConcurrentBag<KeyValuePair<int, char>>(),
                Complete = false
            };

            Thread[] threads = new Thread[Environment.ProcessorCount];
            for (int i = 0; i < threads.Length; i++)
            {
                threads[i] = new Thread(Part1ThreadStart);
                threads[i].Start(state);
            }

            for (int i = 0; i < threads.Length; i++)
            {
                threads[i].Join();
            }

            string finalCode =
                new string(
                    state.Found.OrderBy(kvp => kvp.Key)
                        .Select(kvp => kvp.Value)
                        .Take(8)
                        .ToArray());
            return finalCode;
        }

        private void Part1ThreadStart(object obj)
        {
            Part1State state = (Part1State) obj;
            using (var md5 = MD5.Create())
            {
                while (!state.Complete)
                {
                    int start;
                    lock (state)
                    {
                        start = state.Next;
                        state.Next += state.BatchSize;
                    }

                    for (int i = 0; i < state.BatchSize; i++)
                    {
                        var hash = md5.ComputeHash(Encoding.ASCII.GetBytes(state.DoorId + (i + start)));
                        if (hash[0] == 0 && hash[1] == 0 && (hash[2] & 0xF0) == 0)
                        {
                            char c = (hash[2] & 0x0F).ToString("x")[0];
                            state.Found.Add(new KeyValuePair<int, char>(start+i, c));

                            if (state.Found.Count >= 8)
                            {
                                state.Complete = true;
                            }
                        }
                    }
                }
            }
        }


        private class Part1State
        {
            public string DoorId { get; set; }
            public int BatchSize { get; set; }
            public int Next { get; set; }
            public ConcurrentBag<KeyValuePair<int, char>> Found { get; set; }
            public bool Complete { get; set; }
        }

        private string GetPart2Code(string doorId)
        {
            var state = new Part2State
            {
                DoorId = doorId,
                BatchSize = 10000,
                Next = 0,
                Found = new ConcurrentBag<KeyValuePair<int, char>>[8],
                Complete = 0
            };
            for (int i = 0; i < state.Found.Length; i++)
            {
                state.Found[i] = new ConcurrentBag<KeyValuePair<int, char>>();
            }

            Thread[] threads = new Thread[Environment.ProcessorCount];
            for (int i = 0; i < threads.Length; i++)
            {
                threads[i] = new Thread(Part2ThreadStart);
                threads[i].Start(state);
            }

            for (int i = 0; i < threads.Length; i++)
            {
                threads[i].Join();
            }

            char[] codeChars = new char[state.Found.Length];
            for (int i = 0; i < codeChars.Length; i++)
            {
                codeChars[i] = state.Found[i].OrderBy(kvp => kvp.Key)
                    .Select(kvp => kvp.Value)
                    .First();
            }

            return new string(codeChars);
        }

        private void Part2ThreadStart(object obj)
        {
            var state = (Part2State)obj;
            using (var md5 = MD5.Create())
            {
                while (state.Complete < state.Found.Length)
                {
                    int start;
                    lock (state)
                    {
                        start = state.Next;
                        state.Next += state.BatchSize;
                    }

                    for (int i = 0; i < state.BatchSize; i++)
                    {
                        var hash = md5.ComputeHash(Encoding.ASCII.GetBytes(state.DoorId + (i + start)));
                        if (hash[0] == 0 && hash[1] == 0 && (hash[2] & 0xF0) == 0)
                        {
                            int position = (hash[2] & 0x0F);
                            if (position >= 0 && position < state.Found.Length)
                            {
                                char c = ((hash[3] & 0xF0) >> 4).ToString("x")[0];
                                state.Found[position].Add(new KeyValuePair<int, char>(start + i, c));

                                if (state.Found[position].Count == 1)
                                {
                                    lock (state)
                                    {
                                        state.Complete++;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }


        private class Part2State
        {
            public string DoorId { get; set; }
            public int BatchSize { get; set; }
            public int Next { get; set; }
            public ConcurrentBag<KeyValuePair<int, char>>[] Found { get; set; }
            public int Complete { get; set; }
        }

    }
}

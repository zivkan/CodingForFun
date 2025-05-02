using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;

namespace aoc.csharp._2016
{
    public class Day05 : ISolver
    {
        public (string Part1, string Part2) GetSolution(TextReader input)
        {
            return GetAnswer(input);
        }

        public static (string Part1, string Part2) GetAnswer(TextReader input)
        {
            var doorId = input.ReadLine();

            var part1 = GetPart1Code(doorId);
            var part2 = GetPart2Code(doorId);

            return (part1, part2);
        }

        public static string GetPart1Code(string doorId)
        {
            var state = new Part1State
            {
                DoorId = doorId,
                BatchSize = 10000,
                Next = 0,
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

        private static void Part1ThreadStart(object? obj)
        {
            if (obj == null) throw new ArgumentNullException(nameof(obj));

            Part1State state = (Part1State)obj;
            using var md5 = MD5.Create();
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
                        state.Found.Add(new KeyValuePair<int, char>(start + i, c));

                        if (state.Found.Count >= 8)
                        {
                            state.Complete = true;
                        }
                    }
                }
            }
        }

        private class Part1State
        {
            public string? DoorId { get; set; }
            public int BatchSize { get; set; }
            public int Next { get; set; }
            public ConcurrentBag<KeyValuePair<int, char>> Found { get; } = new ConcurrentBag<KeyValuePair<int, char>>();
            public bool Complete { get; set; }
        }

        public static string GetPart2Code(string doorId)
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

        private static void Part2ThreadStart(object? obj)
        {
            if (obj == null) throw new ArgumentNullException(nameof(obj));

            var state = (Part2State)obj;
            if (state.Found == null) throw new Exception();

            using var md5 = MD5.Create();
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

        private class Part2State
        {
            public string? DoorId { get; set; }
            public int BatchSize { get; set; }
            public int Next { get; set; }
            public ConcurrentBag<KeyValuePair<int, char>>[]? Found { get; set; }
            public int Complete { get; set; }
        }
    }
}

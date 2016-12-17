using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xunit;
using Xunit.Abstractions;

namespace csharp
{
    public class Day6
    {
        private readonly ITestOutputHelper _output;

        private const string SampleInput = "eedadn\ndrvtee\neandsr\nraavrd\natevrs\ntsrnev\nsdttsa\nrasrtv\nnssdts\nntnada\nsvetve\ntesnvt\nvntsnd\nvrdear\ndvrsen\nenarar";

        public Day6(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void Part1Sample()
        {
            List<string> messages;
            using (var reader = new StringReader(SampleInput))
            {
                messages = GetMessages(reader);
            }
            var code = GetMostRepeatedCode(messages);
            Assert.Equal("easter", code);
        }

        [Fact]
        public void Part1()
        {
            List<string> messages;
            using (var reader = GetPuzzleInput.Day(6))
            {
                messages = GetMessages(reader);
            }
            var code = GetMostRepeatedCode(messages);
            _output.WriteLine(code);
        }

        [Fact]
        public void Part2Sample()
        {
            List<string> messages;
            using (var reader = new StringReader(SampleInput))
            {
                messages = GetMessages(reader);
            }
            var code = GetLeastRepeatedCode(messages);
            Assert.Equal("advent", code);
        }

        [Fact]
        public void Part2()
        {
            List<string> messages;
            using (var reader = GetPuzzleInput.Day(6))
            {
                messages = GetMessages(reader);
            }
            var code = GetLeastRepeatedCode(messages);
            _output.WriteLine(code);
        }

        private static string GetMostRepeatedCode(List<string> messages)
        {
            char[] code = new char[messages[0].Length];

            for (int i = 0; i < messages[0].Length; i++)
            {
                var charCount = GetCharCount(messages, i);

                code[i] = charCount.OrderByDescending(cc => cc.Value).Select(cc => cc.Key).First();
            }

            return new string(code);
        }

        private static string GetLeastRepeatedCode(List<string> messages)
        {
            char[] code = new char[messages[0].Length];

            for (int i = 0; i < messages[0].Length; i++)
            {
                var charCount = GetCharCount(messages, i);

                code[i] = charCount.OrderBy(cc => cc.Value).Select(cc => cc.Key).First();
            }

            return new string(code);
        }

        private static Dictionary<char, int> GetCharCount(List<string> messages, int position)
        {
            Dictionary<char, int> charCount = new Dictionary<char, int>();
            foreach (string message in messages)
            {
                char c = message[position];
                int count;
                if (!charCount.TryGetValue(c, out count))
                {
                    count = 0;
                }
                count++;
                charCount[c] = count;
            }
            return charCount;
        }

        private static List<string> GetMessages(TextReader input)
        {
            List<string> messages = new List<string>();
            string line;
            while ((line = input.ReadLine()) != null)
            {
                messages.Add(line);
            }
            return messages;
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Xunit;
using Xunit.Abstractions;

namespace csharp
{
    public class Day4
    {
        private readonly ITestOutputHelper _output;

        private static readonly Regex Regex = new Regex(@"^([a-z-]+)\-(\d+)\[([a-z]+)\]$");

        public Day4(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void Part1Sample()
        {
            string input = "aaaaa-bbb-z-y-x-123[abxyz]\n" +
                           "a-b-c-d-e-f-g-h-987[abcde]\n" +
                           "not-a-real-room-404[oarel]\n" +
                           "totally-real-room-200[decoy]";

            int sum;
            using (var reader = new StringReader(input))
            {
                sum = GetSumOfRealRooms(reader);
            }

            Assert.Equal(1514, sum);
        }

        [Fact]
        public void Part1()
        {
            int sum;
            using (var reader = GetPuzzleInput.Day(4))
            {
                sum = GetSumOfRealRooms(reader);
            }

            _output.WriteLine("{0}", sum);
        }

        [Fact]
        public void Part2Sample()
        {
            var room = new RoomInformation("qzmt-zixmtkozy-ivhz", 343, null);

            string result = ShiftCypher(room.Name, room.SectorId);

            Assert.Equal("very encrypted name", result);
        }

        [Fact]
        public void Part2()
        {
            var matches = new List<KeyValuePair<string, int>>();
            using (var reader = GetPuzzleInput.Day(4))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    var room = ParseRoom(line);
                    var decrypted = ShiftCypher(room.Name, room.SectorId);
                    if (decrypted.IndexOf("north") != -1 && decrypted.IndexOf("pole") != -1)
                    {
                        matches.Add(new KeyValuePair<string, int>(decrypted, room.SectorId));
                    }
                }
            }

            foreach (var match in matches)
            {
                _output.WriteLine("{0}: {1}", match.Key, match.Value);
            }
        }

        private int GetSumOfRealRooms(TextReader reader)
        {
            int sum = 0;
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                var room = ParseRoom(line);
                if (IsRealRoom(room))
                {
                    sum += room.SectorId;
                }
            }

            return sum;
        }

        private RoomInformation ParseRoom(string input)
        {
            var match = Regex.Match(input);
            if (!match.Success)
            {
                throw new ArgumentException("Input doesn't match expected format");
            }

            string name = match.Groups[1].Value;
            int sectorId = int.Parse(match.Groups[2].Value);
            string checksum = match.Groups[3].Value;

            return new RoomInformation(name, sectorId, checksum);
        }

        private bool IsRealRoom(RoomInformation room)
        {
            var checksum = CalculateChecksum(room.Name);
            return ChecksumsAreEqual(room.Checksum, checksum);
        }

        private static string CalculateChecksum(string name)
        {
            var charCounts = name.Where(c => c != '-')
                .GroupBy(c => c)
                .ToDictionary(c => c.Key, c => c.Count())
                .ToList();
            var top5Chars = charCounts.OrderByDescending(cc => cc.Value)
                .ThenBy(cc => cc.Key)
                .Take(5)
                .Select(cc=>cc.Key)
                .ToArray();
            return new string(top5Chars);
        }

        private static bool ChecksumsAreEqual(string expected, string calculated)
        {
            if (expected.Length != 5)
            {
                throw new ArgumentException("Checksum is the wrong length", nameof(expected));
            }

            if (calculated.Length != 5)
            {
                throw new ArgumentException("Checksum is the wrong length", nameof(calculated));
            }

            return expected.Intersect(calculated).Count() == expected.Length;
        }

        private string ShiftCypher(string roomName, int amount)
        {
            char[] buffer = new char[roomName.Length];

            for (int i = 0; i < roomName.Length; i++)
            {
                var c = roomName[i];
                if (c == '-')
                {
                    buffer[i] = ' ';
                }
                else if (c >= 'a' && c <= 'z')
                {
                    int step1 = c - 'a';
                    int step2 = step1 + amount;
                    int step3 = step2%26;
                    int step4 = step3 + 'a';
                    buffer[i] = (char)step4;
                }
                else
                {
                    throw new Exception("Unsupported character");
                }
            }

            return new string(buffer);
        }

        private class RoomInformation
        {
            public string Name { get; }
            public int SectorId { get; }
            public string Checksum { get; }

            public RoomInformation(string name, int sectorId, string checksum)
            {
                Name = name;
                SectorId = sectorId;
                Checksum = checksum;
            }
        }
    }
}

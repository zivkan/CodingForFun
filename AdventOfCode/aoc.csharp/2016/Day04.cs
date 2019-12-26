using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace aoc.csharp._2016
{
    public class Day04 : ISolver
    {
        public (string Part1, string Part2) GetSolution(TextReader input)
        {
            return GetAnswer(input);
        }

        public static (string Part1, string Part2) GetAnswer(TextReader input)
        {
            int part1 = 0;
            var matches = new List<KeyValuePair<string, int>>();
            string? line;
            while ((line = input.ReadLine()) != null)
            {
                var room = ParseRoom(line);
                if (IsRealRoom(room))
                {
                    part1 += room.SectorId;
                }

                var decrypted = ShiftCypher(room.Name, room.SectorId);
                if (decrypted.IndexOf("north") != -1 && decrypted.IndexOf("pole") != -1)
                {
                    matches.Add(new KeyValuePair<string, int>(decrypted, room.SectorId));
                }
            }

            var part2 = matches.Single().Value;

            return (part1.ToString(), part2.ToString());
        }

        private static readonly Regex Regex = new Regex(@"^([a-z-]+)\-(\d+)\[([a-z]+)\]$");

        public static RoomInformation ParseRoom(string input)
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

        public static bool IsRealRoom(RoomInformation room)
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
                .Select(cc => cc.Key)
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

        public static string ShiftCypher(string roomName, int amount)
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
                    int step3 = step2 % 26;
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

        public class RoomInformation
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

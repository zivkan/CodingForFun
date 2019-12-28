using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace aoc.csharp._2018
{
    public class Day09 : ISolver
    {
        public (string Part1, string Part2) GetSolution(TextReader input)
        {
            return GetAnswer(input);
        }

        public static (string Part1, string Part2) GetAnswer(TextReader input)
        {
            var text = input.ReadToEnd();
            var (players, maxPoints) = ParseInput(text);
            var part1 = PlayGame(players, maxPoints);
            var part2 = PlayGame(players, maxPoints * 100);
            return (part1.ToString(), part2.ToString());
        }

        private static readonly Regex _regex = new Regex(@"^(?<players>\d+) players; last marble is worth (?<points>\d+) points$");

        public static long PlayGame(int players, int maxPoints)
        {
            var board = new LinkedList<int>();
            LinkedListNode<int>? current = board.AddFirst(0);
            var playerScores = new long[players];

            for (int i = 1; i <= maxPoints; i++)
            {
                if (i % 23 == 0)
                {
                    for (int j = 1; j < 7; j++)
                    {
                        current = current.Previous ?? board.Last;
                        if (current == null)
                            throw new Exception();
                    }
                    LinkedListNode<int>? toRemove = current.Previous ?? board.Last;
                    if (toRemove == null) throw new Exception();
                    playerScores[i % players] += i + toRemove.Value;
                    board.Remove(toRemove);
                }
                else
                {
                    current = current.Next ?? board.First;
                    if (current == null) throw new Exception();
                    current = board.AddAfter(current, i);
                }
            }

            return playerScores.Max();
        }

        public static (int players, int points) ParseInput(string input)
        {
            var match = _regex.Match(input);
            if (!match.Success) throw new Exception();
            int players = int.Parse(match.Groups["players"].Value);
            int points = int.Parse(match.Groups["points"].Value);

            return (players, points);
        }
    }
}

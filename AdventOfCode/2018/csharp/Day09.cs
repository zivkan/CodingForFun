using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using input;
using Xunit;
using Xunit.Abstractions;

namespace csharp
{
    public class Day09
    {
        private ITestOutputHelper _output;
        private string _input;
        private static readonly Regex _regex = new Regex(@"^(?<players>\d+) players; last marble is worth (?<points>\d+) points$");

        public Day09(ITestOutputHelper output)
        {
            _output = output;
            _input = GetInput.Day(9);
        }

        [Theory]
        [InlineData("9 players; last marble is worth 25 points", 32)]
        [InlineData("10 players; last marble is worth 1618 points", 8317)]
        [InlineData("13 players; last marble is worth 7999 points", 146373)]
        [InlineData("17 players; last marble is worth 1104 points", 2764)]
        [InlineData("21 players; last marble is worth 6111 points", 54718)]
        [InlineData("30 players; last marble is worth 5807 points", 37305)]
        public void Part1Sample(string input, int expected)
        {
            var (players, maxPoints) = ParseInput(input);
            var result = PlayGame(players, maxPoints);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void Part1()
        {
            var (players, maxPoints) = ParseInput(_input);
            var result = PlayGame(players, maxPoints);
            _output.WriteLine("{0}", result);
        }

        [Fact]
        public void Part2()
        {
            var (players, maxPoints) = ParseInput(_input);
            var result = PlayGame(players, maxPoints * 100);
            _output.WriteLine("{0}", result);
        }

        private long PlayGame(int players, int maxPoints)
        {
            var board = new LinkedList<int>();
            var current = board.AddFirst(0);
            var playerScores = new long[players];

            for (int i = 1; i <= maxPoints; i++)
            {
                if (i % 23 == 0)
                {
                    for (int j = 1; j < 7; j++)
                    {
                        current = current.Previous == null ? board.Last : current.Previous;
                    }
                    var toRemove = current.Previous == null ? board.Last : current.Previous;
                    playerScores[i % players] += i + toRemove.Value;
                    board.Remove(toRemove);
                }
                else
                {
                    current = current.Next == null ? board.First : current.Next;
                    current = board.AddAfter(current, i);
                }
            }

            return playerScores.Max();
        }

        private (int players, int points) ParseInput(string input)
        {
            var match = _regex.Match(input);
            if (!match.Success) throw new Exception();
            int players = int.Parse(match.Groups["players"].Value);
            int points = int.Parse(match.Groups["points"].Value);

            return (players, points);
        }
    }
}

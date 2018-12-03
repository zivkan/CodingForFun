using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using input;
using Xunit;
using Xunit.Abstractions;

namespace csharp
{
    public class Day03
    {
        private ITestOutputHelper _output;
        private string _input;
        private static readonly Regex _regex = new Regex(@"#(?<id>\d+) @ (?<x>\d+),(?<y>\d+): (?<w>\d+)x(?<h>\d+)");

        public Day03(ITestOutputHelper output)
        {
            _output = output;
            _input = GetInput.Day(3);
        }

        [Fact]
        public void Sample()
        {
            string input =
                "#1 @ 1,3: 4x4\n" +
                "#2 @ 3,1: 4x4\n" +
                "#3 @ 5,5: 2x2";
            var result = CountOverlappedSquares(input);
            Assert.Equal(4, result.overlaps);
            Assert.Equal(3, result.intact);
        }

        [Fact]
        public void Parts()
        {
            var result = CountOverlappedSquares(_input);
            _output.WriteLine("part 1 = {0}", result.overlaps);
            _output.WriteLine("part 2 = {0}", result.intact);
        }

        private (int overlaps, int intact) CountOverlappedSquares(string input)
        {
            var matches = _regex.Matches(input);
            var points = new Dictionary<Point, List<int>>();
            var intactClaims = new HashSet<int>();
            foreach (Match match in matches)
            {
                var id = int.Parse(match.Groups["id"].Value);
                intactClaims.Add(id);
                var left = int.Parse(match.Groups["x"].Value);
                var top = int.Parse(match.Groups["y"].Value);
                var right = left + int.Parse(match.Groups["w"].Value);
                var bottom = top + int.Parse(match.Groups["h"].Value);

                for (int y = top; y < bottom; y++)
                {
                    for (int x = left; x < right; x++)
                    {
                        var p = new Point { X = x, Y = y };
                        List<int> ids;
                        if (points.TryGetValue(p, out ids))
                        {
                            ids.Add(id);
                            intactClaims.Remove(id);
                            intactClaims.Remove(ids[0]);
                        }
                        else
                        {
                            ids = new List<int>();
                            ids.Add(id);
                            points[p] = ids;
                        }
                    }
                }
            }

            return (points.Count(p => p.Value.Count > 1), intactClaims.Single());
        }

        private struct Point
        {
            public int X { get; set; }
            public int Y { get; set; }
        }
    }
}

using System.Numerics;
using System.Text;
using System.Text.RegularExpressions;
using input;
using Xunit;
using Xunit.Abstractions;

namespace csharp
{
    public class Day10
    {
        private ITestOutputHelper _output;
        private string _input;
        private static readonly Regex _regex = new Regex(@"position=<\s*(?<px>-?\d+),\s*(?<py>-?\d+)> velocity=<\s*(?<vx>-?\d+),\s*(?<vy>-?\d+)>");
        private static readonly string _sampleInput = @"position=< 9,  1> velocity=< 0,  2>
position=< 7,  0> velocity=<-1,  0>
position=< 3, -2> velocity=<-1,  1>
position=< 6, 10> velocity=<-2, -1>
position=< 2, -4> velocity=< 2,  2>
position=<-6, 10> velocity=< 2, -2>
position=< 1,  8> velocity=< 1, -1>
position=< 1,  7> velocity=< 1,  0>
position=<-3, 11> velocity=< 1, -2>
position=< 7,  6> velocity=<-1, -1>
position=<-2,  3> velocity=< 1,  0>
position=<-4,  3> velocity=< 2,  0>
position=<10, -3> velocity=<-1,  1>
position=< 5, 11> velocity=< 1, -2>
position=< 4,  7> velocity=< 0, -1>
position=< 8, -2> velocity=< 0,  1>
position=<15,  0> velocity=<-2,  0>
position=< 1,  6> velocity=< 1,  0>
position=< 8,  9> velocity=< 0, -1>
position=< 3,  3> velocity=<-1,  1>
position=< 0,  5> velocity=< 0, -1>
position=<-2,  2> velocity=< 2,  0>
position=< 5, -2> velocity=< 1,  2>
position=< 1,  4> velocity=< 2,  1>
position=<-2,  7> velocity=< 2, -2>
position=< 3,  6> velocity=<-1, -1>
position=< 5,  0> velocity=< 1,  0>
position=<-6,  0> velocity=< 2,  0>
position=< 5,  9> velocity=< 1, -2>
position=<14,  7> velocity=<-2,  0>
position=<-3,  6> velocity=< 2, -1>";

        public Day10(ITestOutputHelper output)
        {
            _output = output;
            _input = GetInput.Day(10);
        }

        [Fact]
        public void Part1Sample()
        {
            var result = FindMessage(_sampleInput);
            var expected = new StringBuilder();
            expected.AppendLine("#...#..###");
            expected.AppendLine("#...#...#.");
            expected.AppendLine("#...#...#.");
            expected.AppendLine("#####...#.");
            expected.AppendLine("#...#...#.");
            expected.AppendLine("#...#...#.");
            expected.AppendLine("#...#...#.");
            expected.AppendLine("#...#..###");
            Assert.Equal(expected.ToString(), result.message);
            Assert.Equal(3, result.seconds);
        }

        [Fact]
        public void Part1()
        {
            var result = FindMessage(_input);
            _output.WriteLine($"Result after {result.seconds} seconds");
            _output.WriteLine("{0}", result.message);
        }

        private (string message, int seconds) FindMessage(string input)
        {
            var (positions, velocities) = ParseInput(input);
            long volume = GetVolume(positions);
            long newVolume = volume;
            var newPositions = positions;
            int iteration = 0;
            string message;
            do
            {
                iteration++;
                volume = newVolume;
                positions = newPositions;

                newPositions = Increment(positions, velocities);
                newVolume = GetVolume(newPositions);
                if (volume < 100000)
                {
                    message = GenerateResult(newPositions);
                }

            } while (newVolume < volume);

            var result = GenerateResult(positions);
            return (result, iteration - 1);
        }

        private string GenerateResult(Vector<int>[] positions)
        {
            int left = int.MaxValue;
            int right = int.MinValue;
            int top = int.MaxValue;
            int bottom = int.MinValue;
            foreach (var position in positions)
            {
                int x = position[0];
                if (x < left) left = x;
                if (x > right) right = x;

                int y = position[1];
                if (y < top) top = y;
                if (y > bottom) bottom = y;
            }
            int width = right - left + 1;
            int height = bottom - top + 1;

            var result = new char[height][];
            for (int y = 0; y < height; y++)
            {
                result[y] = new char[width];
                for (int x = 0; x < width; x++)
                {
                    result[y][x] = '.';
                }
            }

            foreach(var p in positions)
            {
                int x = p[0] - left;
                int y = p[1] - top;

                result[y][x] = '#';
            }

            var sb = new StringBuilder();
            for (var y = 0; y < height; y++)
            {
                sb.AppendLine(new string(result[y]));
            }

            return sb.ToString();
        }

        private long GetVolume(Vector<int>[] positions)
        {
            int left = int.MaxValue;
            int right = int.MinValue;
            int top = int.MaxValue;
            int bottom = int.MinValue;
            foreach (var position in positions)
            {
                int x = position[0];
                if (x < left) left = x;
                if (x > right) right = x;

                int y = position[1];
                if (y < top) top = y;
                if (y > bottom) bottom = y;
            }

            long width = right - left + 1;
            long height = bottom - top + 1;

            return width * height;
        }

        private Vector<int>[] Increment(Vector<int>[] positions, Vector<int>[] velocities)
        {
            var newPositions = new Vector<int>[positions.Length];

            for (int i = 0; i < positions.Length; i++)
            {
                newPositions[i] = positions[i] + velocities[i];
            }

            return newPositions;
        }

        private (Vector<int>[] positions, Vector<int>[] velocities) ParseInput(string input)
        {
            var matches = _regex.Matches(input);
            var positions = new Vector<int>[matches.Count];
            var velocities = new Vector<int>[matches.Count];

            var values = new int[Vector<int>.Count];

            for (int i = 0; i < matches.Count; i++)
            {
                var match = matches[i];
                var px = int.Parse(match.Groups["px"].Value);
                var py = int.Parse(match.Groups["py"].Value);
                var vx = int.Parse(match.Groups["vx"].Value);
                var vy = int.Parse(match.Groups["vy"].Value);
                values[0] = px;
                values[1] = py;
                var position = new Vector<int>(values);
                positions[i] = position;
                values[0] = vx;
                values[1] = vy;
                var velocity = new Vector<int>(values);
                velocities[i] = velocity;
            }

            return (positions, velocities);
        }
    }
}

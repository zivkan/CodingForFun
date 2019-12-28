using System.IO;
using System.Numerics;
using System.Text;
using System.Text.RegularExpressions;

namespace aoc.csharp._2018
{
    public class Day10 : ISolver
    {
        public (string Part1, string Part2) GetSolution(TextReader input)
        {
            return GetAnswer(input);
        }

        public static (string Part1, string Part2) GetAnswer(TextReader input)
        {
            var text = input.ReadToEnd();
            var (message, seconds) = FindMessage(text);
            return (message, seconds.ToString());
        }

        private static readonly Regex _regex = new Regex(@"position=<\s*(?<px>-?\d+),\s*(?<py>-?\d+)> velocity=<\s*(?<vx>-?\d+),\s*(?<vy>-?\d+)>");

        public static (string message, int seconds) FindMessage(string input)
        {
            var (positions, velocities) = ParseInput(input);
            long volume = GetVolume(positions);
            long newVolume = volume;
            var newPositions = positions;
            int iteration = 0;
            do
            {
                iteration++;
                volume = newVolume;
                positions = newPositions;

                newPositions = Increment(positions, velocities);
                newVolume = GetVolume(newPositions);
            } while (newVolume < volume);

            var result = GenerateResult(positions);
            return (result, iteration - 1);
        }

        private static string GenerateResult(Vector<int>[] positions)
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

            foreach (var p in positions)
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

        private static long GetVolume(Vector<int>[] positions)
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

        private static Vector<int>[] Increment(Vector<int>[] positions, Vector<int>[] velocities)
        {
            var newPositions = new Vector<int>[positions.Length];

            for (int i = 0; i < positions.Length; i++)
            {
                newPositions[i] = positions[i] + velocities[i];
            }

            return newPositions;
        }

        private static (Vector<int>[] positions, Vector<int>[] velocities) ParseInput(string input)
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

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace aoc.csharp._2016
{
    public class Day03 : ISolver
    {
        public (string Part1, string Part2) GetSolution(TextReader input)
        {
            return GetAnswer(input);
        }

        public static (string Part1, string Part2) GetAnswer(TextReader input)
        {
            var intStream = Input.GetLines(input).SelectMany(ToIntStream);
            var part1 = ToTriangleLengths(intStream)
                .Where(IsValidTriangle)
                .Count();

            var transposedInput = TransposeRowsAndColumns(ToTriangleLengths(intStream));
            var part2 = transposedInput
                .Where(IsValidTriangle)
                .Count();

            return (part1.ToString(), part2.ToString());
        }

        private static readonly char[] Separator = { ' ' };

        private static IEnumerable<int> ToIntStream(string line)
        {
            string[] split = line.Split(Separator, StringSplitOptions.RemoveEmptyEntries);
            if (split.Length != 3)
            {
                throw new ArgumentException("Didn't get 3 values on the line");
            }

            yield return int.Parse(split[0]);
            yield return int.Parse(split[1]);
            yield return int.Parse(split[2]);
        }

        private static IEnumerable<Tuple<int, int, int>> ToTriangleLengths(IEnumerable<int> arg)
        {
            var ints = new List<int>(3);

            foreach (int i in arg)
            {
                ints.Add(i);

                if (ints.Count == 3)
                {
                    yield return new Tuple<int, int, int>(ints[0], ints[1], ints[2]);
                    ints.Clear();
                }
            }

            if (ints.Count != 0)
            {
                throw new ArgumentException("Number of inputs was not multiple of 3");
            }
        }

        private static bool IsValidTriangle(Tuple<int, int, int> lengths)
        {
            return lengths.Item1 + lengths.Item2 > lengths.Item3
                   && lengths.Item1 + lengths.Item3 > lengths.Item2
                   && lengths.Item2 + lengths.Item3 > lengths.Item1;
        }

        private static IEnumerable<Tuple<int, int, int>> TransposeRowsAndColumns(IEnumerable<Tuple<int, int, int>> triangles)
        {
            var triplet = new List<Tuple<int, int, int>>(3);

            foreach (var triangle in triangles)
            {
                triplet.Add(triangle);

                if (triplet.Count == 3)
                {
                    yield return new Tuple<int, int, int>(triplet[0].Item1, triplet[1].Item1, triplet[2].Item1);
                    yield return new Tuple<int, int, int>(triplet[0].Item2, triplet[1].Item2, triplet[2].Item2);
                    yield return new Tuple<int, int, int>(triplet[0].Item3, triplet[1].Item3, triplet[2].Item3);

                    triplet.Clear();
                }
            }

            if (triplet.Count != 0)
            {
                throw new ArgumentException("Input did not contain multiple of 3 items.");
            }
        }
    }
}

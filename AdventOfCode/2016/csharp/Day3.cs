using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xunit;
using Xunit.Abstractions;

namespace csharp
{
    public class Day3
    {
        private static readonly char[] Separator = {' '};

        private readonly ITestOutputHelper _output;

        public Day3(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void Part1()
        {
            var input = GetPuzzleInput.Day(3);
            var intStream = ReadLines(input).SelectMany(ToIntStream);
            var validTriangles = ToTriangleLengths(intStream)
                .Where(IsValidTriangle)
                .Count();

            _output.WriteLine("{0}", validTriangles);
        }

        [Fact]
        public void Part2()
        {
            var input = GetPuzzleInput.Day(3);
            var intStream = ReadLines(input).SelectMany(ToIntStream);
            var transposedInput = TransposeRowsAndColumns(ToTriangleLengths(intStream));
            var validTriangles = transposedInput
                .Where(IsValidTriangle)
                .Count();

            _output.WriteLine("{0}", validTriangles);
        }

        private IEnumerable<string> ReadLines(TextReader input)
        {
            string line;
            while ((line = input.ReadLine()) != null)
            {
                yield return line;
            }
        }

        private IEnumerable<int> ToIntStream(string line)
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

        private IEnumerable<Tuple<int,int,int>> ToTriangleLengths(IEnumerable<int> arg)
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

        private bool IsValidTriangle(Tuple<int, int, int> lengths)
        {
            return lengths.Item1 + lengths.Item2 > lengths.Item3
                   && lengths.Item1 + lengths.Item3 > lengths.Item2
                   && lengths.Item2 + lengths.Item3 > lengths.Item1;
        }

        private IEnumerable<Tuple<int, int, int>> TransposeRowsAndColumns(IEnumerable<Tuple<int, int, int>> triangles)
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

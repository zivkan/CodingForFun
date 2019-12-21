using aoc.csharp.Geometry;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace aoc.csharp._2019
{
    public class Day11 : ISolver
    {
        public (string Part1, string Part2) GetSolution(TextReader input)
        {
            return GetAnswer(input);
        }

        public static (string Part1, string Part2) GetAnswer(TextReader input)
        {
            var program = Input.ToList<long>(input).ToArray();

            var part1 = GetPaintedPanels(program, false);

            var part2 = GetPaintedPanels(program, true);

            return (part1.Count.ToString(), ToString(part2));
        }

        private static string ToString(Dictionary<Point2D, bool> paint)
        {
            int top, bottom, left, right;
            top = bottom = 0;
            left = right = 0;

            foreach (var position in paint.Keys)
            {
                if (left > position.X) left = position.X;
                if (right < position.X) right = position.X;
                if (top < position.Y) top = position.Y;
                if (bottom > position.Y) bottom = position.Y;
            }

            var sb = new StringBuilder();

            for (int y = top; y >= bottom; y--)
            {
                if (y != top)
                {
                    sb.AppendLine();
                }

                for (int x = left; x <= right; x++)
                {
                    var value = paint.GetValueOrDefault(new Point2D(x, y), defaultValue: false);
                    sb.Append(value ? '#' : ' ');
                }
            }

            return sb.ToString();
        }

        private static Dictionary<Point2D, bool> GetPaintedPanels(long[] program, bool startOnWhite)
        {
            var vm = new IntcodeVm(program);
            var position = new Point2D(0, 0);
            var direction = new Point2D(0, 1);
            var painted = new Dictionary<Point2D, bool>();

            vm.Input.Enqueue(startOnWhite ? 1 : 0);
            while (vm.Step())
            {
                if (vm.Output.Count == 2)
                {
                    var encodedColour = vm.Output.Dequeue();
                    var encodedTurn = vm.Output.Dequeue();

                    bool white = encodedColour == 1 ? true : encodedColour == 0 ? false : throw new Exception("Unsupported paint value " + encodedColour);
                    if (encodedTurn == 0)
                    {
                        direction = new Point2D(-direction.Y, direction.X);
                    }
                    else if (encodedTurn == 1)
                    {
                        direction = new Point2D(direction.Y, -direction.X);
                    }
                    else
                    {
                        throw new Exception("Unsupported turn " + encodedTurn);
                    }

                    painted[position] = white;
                    position += direction;

                    white = painted.GetValueOrDefault(position, defaultValue: false);
                    vm.Input.Enqueue(white ? 1 : 0);
                }
            };

            return painted;
        }
    }
}

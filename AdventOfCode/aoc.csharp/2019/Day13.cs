using aoc.csharp.Geometry;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;

namespace aoc.csharp._2019;

public class Day13 : ISolver
{
    public (string Part1, string Part2) GetSolution(TextReader input)
    {
        return GetAnswer(input);
    }

    public static (string Part1, string Part2) GetAnswer(TextReader input)
    {
        var program = Input.ToList<long>(input);

        var part1 = RunGame(program).Screen.Count(v => v.Value == 2);

        program[0] = 2;
        var part2 = RunGame(program).Score;

        return (part1.ToString(), part2.ToString());
    }

    private static State RunGame(List<long> program)
    {
        var state = new State();
        var vm = new IntcodeVm(program, new Player(state), new StandardQueue<long>());

        while (vm.Step())
        {
            if (vm.Output.Count >= 3)
            {
                var x = vm.Output.Dequeue();
                var y = vm.Output.Dequeue();
                var tileCode = vm.Output.Dequeue();

                //if (x >= 0 && y >= 0)
                //{
                //    var tile = tileCode switch
                //    {
                //        0 => ' ',
                //        1 => '#',
                //        2 => '?',
                //        3 => '-',
                //        4 => 'o',
                //        _ => throw new Exception(),
                //    };

                //    Console.SetCursorPosition((int)x, (int)y);
                //    Console.Write(tile);
                //}

                state.Set(x, y, tileCode);
            }
        }

        return state;
    }

    private class State
    {
        public Dictionary<Point2D, long> Screen { get; }
        public Point2D Paddle { get; private set; }
        public Point2D Ball { get; private set; }
        public long Score { get; private set; }

        public State()
        {
            Screen = new Dictionary<Point2D, long>();
            Paddle = new Point2D(-1, -1);
            Ball = new Point2D(-1, -1);
            Score = 0;
        }

        public void Set(long x, long y, long value)
        {
            var point = new Point2D((int)x, (int)y);
            Screen[point] = value;

            if (x == -1 && y == 0)
            {
                Score = value;
            }
            else if (value == 3)
            {
                Paddle = point;
            }
            else if (value == 4)
            {
                Ball = point;
            }
        }
    }

    private class Player : IQueue<long>
    {
        private readonly State _state;

        public Player(State state)
        {
            _state = state;
        }

        public int Count => throw new System.NotImplementedException();

        public long Dequeue()
        {
            throw new System.NotImplementedException();
        }

        public void Enqueue(long value)
        {
            throw new System.NotImplementedException();
        }

        public bool TryDequeue([MaybeNullWhen(false)] out long value)
        {
            var direction = _state.Ball.X.CompareTo(_state.Paddle.X);

            if (direction < 0)
            {
                value = -1;
            }
            else if (direction > 0)
            {
                value = 1;
            }
            else
            {
                value = 0;
            }

            return true;
        }
    }
}

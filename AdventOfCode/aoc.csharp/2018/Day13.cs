using System;
using System.Collections.Generic;
using System.IO;

namespace aoc.csharp._2018
{
    public class Day13 : ISolver
    {
        public (string Part1, string Part2) GetSolution(TextReader input)
        {
            return GetAnswer(input);
        }

        public static (string Part1, string Part2) GetAnswer(TextReader input)
        {
            var text = input.ReadToEnd();
            var part1 = FirstCrashCoordinates(text);
            var part2 = LastCartCoordinates(text);
            return (part1, part2);
        }

        private static readonly IComparer<Cart> _cartComparer = new CartComparer();

        public static string FirstCrashCoordinates(string input)
        {
            var (map, carts) = ParseInput(input);

            int x, y;
            bool crash;
#if DEBUG
            int tickNumber = 0;
#endif
            do
            {
                (crash, x, y) = Tick(map, carts);
#if DEBUG
                if (tickNumber++ > 100_000)
                {
                    throw new Exception("Infinite loop");
                }
#endif
            } while (!crash);

            return $"{x},{y}";
        }

        public static string LastCartCoordinates(string input)
        {
            var (map, carts) = ParseInput(input);

#if DEBUG
            int tickNumber = 0;
#endif
            do
            {
                Tick(map, carts);

#if DEBUG
                if (tickNumber++ > 100_000)
                {
                    throw new Exception("Infinite loop");
                }
#endif
            } while (carts.Count > 1);

            return $"{carts[0].X},{carts[0].Y}";
        }

        private static (char[,] map, List<Cart> carts) ParseInput(string input)
        {
            var lines = new List<string>();
            using (var reader = new StringReader(input))
            {
                int lineLength = -1;
                string? line;
                while ((line = reader.ReadLine()) != null)
                {
                    if (lineLength == -1)
                    {
                        lineLength = line.Length;
                    }
                    if (lineLength != line.Length)
                    {
                        throw new Exception("Input line lengths do not match");
                    }
                    lines.Add(line);
                }
            }

            var map = new char[lines[0].Length, lines.Count];
            var carts = new List<Cart>();
            for (int y = 0; y < lines.Count; y++)
            {
                string line = lines[y];
                for (int x = 0; x < line.Length; x++)
                {
                    var c = line[x];
                    switch (c)
                    {
                        case ' ':
                            break;

                        case '-':
                        case '|':
                        case '+':
                        case '/':
                        case '\\':
                            map[x, y] = c;
                            break;

                        case '>':
                        case '<':
                            // unsafe assumption that starting point is not on a turn or intersection
                            map[x, y] = '-';
                            carts.Add(new Cart(x, y, c == '>' ? TravelDirection.East : TravelDirection.West));
                            break;

                        case 'v':
                        case '^':
                            // unsafe assumption that starting point is not on a turn or intersection
                            map[x, y] = '|';
                            carts.Add(new Cart(x, y, c == 'v' ? TravelDirection.South : TravelDirection.North));
                            break;

                        default:
                            throw new Exception($"Unknown map tile '{c}'");
                    }
                }
            }

            return (map, carts);
        }

        private static (bool crash, int x, int y) Tick(char[,] map, List<Cart> carts)
        {
            carts.Sort(_cartComparer);

            bool crash = false;
            int x = -1, y = -1;

            for (int i = 0; i < carts.Count; i++)
            {
                var cart = carts[i];
                // move cart
                switch (cart.Direction)
                {
                    case TravelDirection.North:
                        cart.Y--;
                        break;

                    case TravelDirection.South:
                        cart.Y++;
                        break;

                    case TravelDirection.West:
                        cart.X--;
                        break;

                    case TravelDirection.East:
                        cart.X++;
                        break;

                    default:
                        throw new Exception();
                }

                for (int j = 0; j < carts.Count; j++)
                {
                    if (i == j) continue;

                    var otherCart = carts[j];
                    if (cart.X == otherCart.X && cart.Y == otherCart.Y)
                    {
                        crash = true;
                        x = cart.X;
                        y = cart.Y;

                        if (i > j)
                        {
                            carts.RemoveAt(i);
                            carts.RemoveAt(j);
                            i -= 2;
                        }
                        else
                        {
                            carts.RemoveAt(j);
                            carts.RemoveAt(i);
                            i -= 1;
                        }
                    }
                }

                var newPos = map[cart.X, cart.Y];
                switch (newPos)
                {
                    case '-':
                    case '|':
                        break;

                    case '\\':
                        cart.Direction = cart.Direction switch
                        {
                            TravelDirection.North => TravelDirection.West,
                            TravelDirection.West => TravelDirection.North,
                            TravelDirection.East => TravelDirection.South,
                            TravelDirection.South => TravelDirection.East,
                            _ => throw new Exception($"invalid direction {cart.Direction} for turn '\\'"),
                        };
                        break;

                    case '/':
                        cart.Direction = cart.Direction switch
                        {
                            TravelDirection.East => TravelDirection.North,
                            TravelDirection.North => TravelDirection.East,
                            TravelDirection.South => TravelDirection.West,
                            TravelDirection.West => TravelDirection.South,
                            _ => throw new Exception($"invalid direct {cart.Direction} for turn '/'"),
                        };
                        break;

                    case '+':
                        switch (cart.NextTurn)
                        {
                            case TurnDirection.Left:
                                cart.NextTurn = TurnDirection.Straight;
                                cart.Direction = cart.Direction switch
                                {
                                    TravelDirection.East => TravelDirection.North,
                                    TravelDirection.North => TravelDirection.West,
                                    TravelDirection.West => TravelDirection.South,
                                    TravelDirection.South => TravelDirection.East,
                                    _ => throw new NotImplementedException($"{cart.Direction} not implemented for left turn"),
                                };
                                break;

                            case TurnDirection.Straight:
                                cart.NextTurn = TurnDirection.Right;
                                break;

                            case TurnDirection.Right:
                                cart.NextTurn = TurnDirection.Left;
                                cart.Direction = cart.Direction switch
                                {
                                    TravelDirection.North => TravelDirection.East,
                                    TravelDirection.West => TravelDirection.North,
                                    TravelDirection.East => TravelDirection.South,
                                    TravelDirection.South => TravelDirection.West,
                                    _ => throw new Exception($"{cart.Direction} not implemented for right turn"),
                                };
                                break;

                            default:
                                throw new NotImplementedException($"Next turn {cart.NextTurn} not implemented");
                        }
                        break;

                    default:
                        throw new Exception($"Invalid track '{newPos}'");
                }
            }

            return (crash, x, y);
        }

        private class Cart
        {
            public int X { get; set; }
            public int Y { get; set; }
            public TravelDirection Direction { get; set; }
            public TurnDirection NextTurn { get; set; }

            public Cart(int x, int y, TravelDirection direction)
            {
                X = x;
                Y = y;
                Direction = direction;
                NextTurn = TurnDirection.Left;
            }
        }

        private enum TravelDirection
        {
            North, East, South, West
        }

        private enum TurnDirection
        {
            Left, Straight, Right
        }

        private class CartComparer : IComparer<Cart>
        {
            private static readonly IComparer<int> _intComparer = Comparer<int>.Default;

            public int Compare(Cart a, Cart b)
            {
                var result = _intComparer.Compare(a.Y, b.Y);
                if (result == 0)
                {
                    result = _intComparer.Compare(a.X, b.X);
                }
                return result;
            }
        }
    }
}

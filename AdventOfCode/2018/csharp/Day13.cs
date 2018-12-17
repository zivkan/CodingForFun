using System;
using System.Collections.Generic;
using System.IO;
using input;
using Xunit;
using Xunit.Abstractions;

namespace csharp
{
    public class Day13
    {
        private ITestOutputHelper _output;
        private static readonly string _input = GetInput.Day(13);
        private static readonly IComparer<Cart> _cartComparer = new CartComparer();

        public Day13(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void Part1Sample()
        {
            var input = @"/->-\        
|   |  /----\
| /-+--+-\  |
| | |  | v  |
\-+-/  \-+--/
  \------/   ";
            var coordinates = FirstCrashCoordinates(input);
            Assert.Equal("7,3", coordinates);
        }

        [Fact]
        public void Part1()
        {
            var coordinates = FirstCrashCoordinates(_input);
            Assert.Equal("118,112", coordinates);
        }

        [Fact]
        public void Part2Sample()
        {
            var input = @"/>-<\  
|   |  
| /<+-\
| | | v
\>+</ |
  |   ^
  \<->/";
            var coordinates = LastCartCoordinates(input);
            Assert.Equal("6,4", coordinates);
        }

        [Fact]
        public void Part2()
        {
            var coordinates = LastCartCoordinates(_input);
            Assert.Equal("50,21", coordinates);
        }

        private string FirstCrashCoordinates(string input)
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

        private string LastCartCoordinates(string input)
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

        private (char[,] map, List<Cart> carts) ParseInput(string input)
        {
            var lines = new List<string>();
            using (var reader = new StringReader(input))
            {
                int lineLength = -1;
                string line;
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

        private (bool crash, int x, int y) Tick(char[,] map, List<Cart> carts)
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
                        switch (cart.Direction)
                        {
                            case TravelDirection.North:
                                cart.Direction = TravelDirection.West;
                                break;

                            case TravelDirection.West:
                                cart.Direction = TravelDirection.North;
                                break;

                            case TravelDirection.East:
                                cart.Direction = TravelDirection.South;
                                break;

                            case TravelDirection.South:
                                cart.Direction = TravelDirection.East;
                                break;

                            default:
                                throw new Exception($"invalid direction {cart.Direction} for turn '\\'");
                        }
                        break;

                    case '/':
                        switch (cart.Direction)
                        {
                            case TravelDirection.East:
                                cart.Direction = TravelDirection.North;
                                break;

                            case TravelDirection.North:
                                cart.Direction = TravelDirection.East;
                                break;

                            case TravelDirection.South:
                                cart.Direction = TravelDirection.West;
                                break;

                            case TravelDirection.West:
                                cart.Direction = TravelDirection.South;
                                break;

                            default:
                                throw new Exception($"invalid direct {cart.Direction} for turn '/'");
                        }
                        break;

                    case '+':
                        switch (cart.NextTurn)
                        {
                            case TurnDirection.Left:
                                cart.NextTurn = TurnDirection.Straight;
                                switch (cart.Direction)
                                {
                                    case TravelDirection.East:
                                        cart.Direction = TravelDirection.North;
                                        break;

                                    case TravelDirection.North:
                                        cart.Direction = TravelDirection.West;
                                        break;

                                    case TravelDirection.West:
                                        cart.Direction = TravelDirection.South;
                                        break;

                                    case TravelDirection.South:
                                        cart.Direction = TravelDirection.East;
                                        break;

                                    default:
                                        throw new NotImplementedException($"{cart.Direction} not implemented for left turn");
                                }
                                break;

                            case TurnDirection.Straight:
                                cart.NextTurn = TurnDirection.Right;
                                break;

                            case TurnDirection.Right:
                                cart.NextTurn = TurnDirection.Left;
                                switch (cart.Direction)
                                {
                                    case TravelDirection.North:
                                        cart.Direction = TravelDirection.East;
                                        break;

                                    case TravelDirection.West:
                                        cart.Direction = TravelDirection.North;
                                        break;

                                    case TravelDirection.East:
                                        cart.Direction = TravelDirection.South;
                                        break;

                                    case TravelDirection.South:
                                        cart.Direction = TravelDirection.West;
                                        break;

                                    default:
                                        throw new Exception($"{cart.Direction} not implemented for right turn");
                                }
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
            private static IComparer<int> _intComparer = Comparer<int>.Default;

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

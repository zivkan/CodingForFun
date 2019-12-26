using aoc.csharp.Geometry;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace aoc.csharp._2019
{
    public class Day12 : ISolver
    {
        private static readonly Regex regex = new Regex(@"<x=(?<x>-?\d+), y=(?<y>-?\d+), z=(?<z>-?\d+)>", RegexOptions.Multiline);

        public (string Part1, string Part2) GetSolution(TextReader input)
        {
            return GetAnswer(input);
        }

        public static (string Part1, string Part2) GetAnswer(TextReader input)
        {
            var initialState = ParseInitialState(input);

            var state = initialState;
            for (int i = 0; i < 1000; i++)
            {
                state = Step(state);
            }

            var part1 = GetEnergy(state);

            var cycleLength = GetCycleLength(initialState);

            return (part1.ToString(), cycleLength.ToString());
        }

        public static long GetCycleLength(Moons initialState)
        {
            if (initialState.Any(m=>m.Velocity != Point3D.Zero))
            {
                throw new ArgumentException("Initial state must have all moon velocities equal to zero");
            }

            int? x = null, y = null, z = null;
            var state = initialState;
            int step = 0;
            do
            {
                step++;

                state = Step(state);

                static bool Same(Moons a, Moons b, Func<Moon, (int pos, int vel)> func)
                {
                    var count = a.Count;
                    if (b.Count != count) throw new ArgumentException();

                    for (int i = 0; i < count; i++)
                    {
                        var (posA, velA) = func(a[i]);
                        var (posB, velB) = func(b[i]);
                        if (posA != posB || velA != velB)
                        {
                            return false;
                        }
                    }

                    return true;
                }
                if (x == null && Same(initialState, state, m => (m.Position.X, m.Velocity.X)))
                {
                    x = step;
                }
                if (y == null && Same(initialState, state, m => (m.Position.Y, m.Velocity.Y)))
                {
                    y = step;
                }
                if (z == null && Same(initialState, state, m => (m.Position.Z, m.Velocity.Z)))
                {
                    z = step;
                }
            } while (x == null || y == null || z == null);

            var lcm = GetLowestCommonMultiple(x.Value, y.Value, z.Value);

            return lcm;
        }

        private static long GetLowestCommonMultiple(int value1, int value2, int value3)
        {
            var lcm1 = GetLowestCommonMultiple(value1, value2);
            var lcm2 = GetLowestCommonMultiple(lcm1, value3);
            return lcm2;
        }

        private static long GetLowestCommonMultiple(long value1, long value2)
        {
            var v1 = value1;
            var v2 = value2;

            while (v1 != v2)
            {
                if (v1 < v2)
                {
                    v1 += value1;
                }
                else
                {
                    v2 += value2;
                }
            }

            return v1;
        }

        public static int GetEnergy(Moons state)
        {
            int energy = 0;

            static int Abs(Point3D point)
            {
                return Math.Abs(point.X) + Math.Abs(point.Y) + Math.Abs(point.Z);
            }

            for (int i = 0; i < state.Count; i++)
            {
                var moon = state[i];
                int pot = Abs(moon.Position);
                int kin = Abs(moon.Velocity);
                energy += pot * kin;
            }

            return energy;
        }

        public static Moons ParseInitialState(TextReader input)
        {
            var text = input.ReadToEnd();
            var matches = regex.Matches(text);

            var moons = new List<Moon>();
            for (int i = 0; i < matches.Count; i++)
            {
                var match = matches[i];
                if (!match.Success) throw new Exception();

                var x = int.Parse(match.Groups["x"].Value);
                var y = int.Parse(match.Groups["y"].Value);
                var z = int.Parse(match.Groups["z"].Value);

                var position = new Point3D(x, y, z);
                var moon = new Moon(position, Point3D.Zero);
                moons.Add(moon);
            }

            return new Moons(moons);
        }

        public static Moons Step(Moons initial)
        {
            var gravity = new Point3D[initial.Count];
            for (int i = 0; i < gravity.Length; i++)
            {
                gravity[i] = Point3D.Zero;
            }

            for (int i = gravity.Length-1; i > 0; i--)
            {
                var m1 = initial[i];
                for (int j = 0; j < i; j++)
                {
                    var m2 = initial[j];
                    var direction = new Point3D(
                        GetGravity(m1.Position.X, m2.Position.X),
                        GetGravity(m1.Position.Y, m2.Position.Y),
                        GetGravity(m1.Position.Z, m2.Position.Z));
                    gravity[i] += direction;
                    gravity[j] -= direction;
                }
            }

            List<Moon> result = new List<Moon>(gravity.Length);
            for (int i = 0; i < gravity.Length; i++)
            {
                var velocity = initial[i].Velocity + gravity[i];
                var position = initial[i].Position + velocity;
                var moon = new Moon(position, velocity);
                result.Add(moon);
            }

            return new Moons(result);
        }

        private static int GetGravity(int x, int y)
        {
            if (x == y) return 0;
            if (x < y) return 1;
            return -1;
        }

        public class Moon
        {
            public Point3D Position { get; }
            public Point3D Velocity { get; }

            public Moon(Point3D position, Point3D velocity)
            {
                Position = position;
                Velocity = velocity;
            }

            public static bool operator ==(Moon a, Moon b)
            {
                if (a.Position != b.Position)
                {
                    return false;
                }

                if (a.Velocity != b.Velocity)
                {
                    return false;
                }

                return true;
            }

            public static bool operator !=(Moon a, Moon b)
            {
                return !(a == b);
            }

            public override bool Equals(object? obj)
            {
                if (obj is Moon m)
                {
                    return this == m;
                }
                else
                {
                    return false;
                }
            }

            public override int GetHashCode()
            {
                var posHash = Position.GetHashCode();
                var velHash = Velocity.GetHashCode();

                var posHashRot = (posHash << 16) | (posHash >> 16);
                return posHashRot ^ velHash;
            }

            public override string ToString()
            {
                return $"{Position} {Velocity}";
            }
        }

        public class Moons : IEquatable<Moons>, IEnumerable<Moon>
        {
            private readonly IReadOnlyList<Moon> _moons;

            public Moons(IReadOnlyList<Moon> moons)
            {
                _moons = moons;
            }

            public int Count => _moons.Count;

            public Moon this[int i] => _moons[i];

            public bool Equals([AllowNull] Moons other)
            {
                if (ReferenceEquals(this, other))
                {
                    return true;
                }

                if (this is null || other is null)
                {
                    return false;
                }

                if (Count != other.Count)
                {
                    return false;
                }

                for (int i = 0; i < Count; i++)
                {
                    if (this[i] != other[i])
                    {
                        return false;
                    }
                }

                return true;
            }

            public override bool Equals(object? obj)
            {
                return Equals(obj as Moons);
            }

            public IEnumerator<Moon> GetEnumerator()
            {
                return _moons.GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return _moons.GetEnumerator();
            }

            public static bool operator ==(Moons a, Moons b)
            {
                return a.Equals(b);
            }

            public static bool operator !=(Moons a, Moons b)
            {
                return !a.Equals(b);
            }

            public override int GetHashCode()
            {
                var hash = 0;

                for (int i = 0; i < _moons.Count; i++)
                {
                    hash = ((hash << 3) | (hash >> 29)) ^ _moons[i].GetHashCode();
                }

                return hash;
            }
        }
    }
}

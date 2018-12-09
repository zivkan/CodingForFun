using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using input;
using Xunit;
using Xunit.Abstractions;

namespace csharp
{
    public class Day20
    {
        private ITestOutputHelper _output;
        private string _input;
        private static readonly Regex _regex = new Regex(@"p=<(?<px>-?\d+),(?<py>-?\d+),(?<pz>-?\d+)>, v=<(?<vx>-?\d+),(?<vy>-?\d+),(?<vz>-?\d+)>, a=<(?<ax>-?\d+),(?<ay>-?\d+),(?<az>-?\d+)>");
        private static readonly int[] _squares = GetSquares();

        private static int[] GetSquares()
        {
            var result = new List<int>();

            checked
            {
                try
                {
                    for (int i = 0; i < int.MaxValue; i++)
                    {
                        result.Add(i * i);
                    }
                }
                catch (OverflowException)
                {
                    // expected loop termination
                }
            }

            return result.ToArray();
        }

        public Day20(ITestOutputHelper output)
        {
            _output = output;
            _input = GetInput.Day(20);
        }

        [Fact]
        public void Part1Sample()
        {
            const string input =
                "p=<3,0,0>, v=<2,0,0>, a=<-1,0,0>\n" +
                "p=<4,0,0>, v=<0,0,0>, a=<-2,0,0>";

            var actual = FindParticleThatStaysClosestToOrigin(input);
            Assert.Equal(0, actual);
        }

        [Fact]
        public void Part1()
        {
            var result = FindParticleThatStaysClosestToOrigin(_input);
            _output.WriteLine("{0}", result);
        }

        [Fact]
        public void Part2Sample()
        {
            const string input =
                "p=<-6,0,0>, v=<3,0,0>, a=<0,0,0>\n" +
                "p=<-4,0,0>, v=<2,0,0>, a=<0,0,0>\n" +
                "p=<-2,0,0>, v=<1,0,0>, a=<0,0,0>\n" +
                "p=<3,0,0>, v=<-1,0,0>, a=<0,0,0>";

            var actual = FindParticleCountAfterCollisions(input);
            Assert.Equal(1, actual);
        }

        [Fact]
        public void Part2()
        {
            var result = FindParticleCountAfterCollisions(_input);
            _output.WriteLine("{0}", result);
        }

        private int FindParticleThatStaysClosestToOrigin(string input)
        {
            var particles = ParseInput(input);

            // As time goes to infinity, given constant acceleration, all particles will be moving away from the origin
            // and the particle with the lowest acceleration will be travelling the slowest, hence closest to the origin.
            // Therefore, no need to simulate.
            int id = -1;
            int max = int.MaxValue;
            foreach (var particle in particles)
            {
                var accel = ManhattenDistance(particle.Acceleration);
                if (accel < max)
                {
                    max = accel;
                    id = particle.Id;
                }
            }

            return id;
        }

        private int FindParticleCountAfterCollisions(string input)
        {
            var particles = ParseInput(input);
            RemoveCollisions(particles);

            for (int time = 0; time < 500; time++)
            {
                ApplyOneTimeUnit(particles);
                RemoveCollisions(particles);
            }

            return particles.Count;
        }

        private void ApplyOneTimeUnit(List<Particle> particles)
        {
            foreach (var particle in particles)
            {
                ApplyIncrement(particle.Acceleration, particle.Velocity);
                ApplyIncrement(particle.Velocity, particle.Position);
            }

            void ApplyIncrement(Vector3 src, Vector3 dest)
            {
                dest.X += src.X;
                dest.Y += src.Y;
                dest.Z += src.Z;
            }
        }

        private void RemoveCollisions(List<Particle> particles)
        {
            var points = new Dictionary<Vector3, List<Particle>>(new Vector3Comparer());
            var pointsWithCollision = new HashSet<List<Particle>>();

            foreach (var particle in particles)
            {
                if (points.TryGetValue(particle.Position, out var p))
                {
                    p.Add(particle);
                    pointsWithCollision.Add(p);
                }
                else
                {
                    p = new List<Particle>();
                    points.Add(particle.Position, p);
                    p.Add(particle);
                }
            }

            foreach (var point in pointsWithCollision)
            {
                foreach (var particle in point)
                {
                    particles.Remove(particle);
                }
            }
        }

        private List<Particle> ParseInput(string input)
        {
            var particles = new List<Particle>();

            var matches = _regex.Matches(input);
            int id = 0;
            foreach (Match match in matches)
            {
                var particle = new Particle(id++);

                particle.Position = new Vector3
                {
                    X = int.Parse(match.Groups["px"].Value),
                    Y = int.Parse(match.Groups["py"].Value),
                    Z = int.Parse(match.Groups["pz"].Value)
                };

                particle.Velocity = new Vector3
                {
                    X = int.Parse(match.Groups["vx"].Value),
                    Y = int.Parse(match.Groups["vy"].Value),
                    Z = int.Parse(match.Groups["vz"].Value)
                };

                particle.Acceleration = new Vector3
                {
                    X = int.Parse(match.Groups["ax"].Value),
                    Y = int.Parse(match.Groups["ay"].Value),
                    Z = int.Parse(match.Groups["az"].Value)
                };

                particles.Add(particle);
            }

            return particles;
        }

        private int ManhattenDistance(Vector3 vector)
        {
            return Abs(vector.X) + Abs(vector.Y) + Abs(vector.Z);
        }

        private int Abs(int i)
        {
            return i >= 0 ? i : -i;
        }

        [DebuggerDisplay("p=<{Position.X},{Position.Y},{Position.Z}>, v=<{Velocity.X},{Velocity.Y},{Velocity.Z}>, a=<{Acceleration.X},{Acceleration.Y},{Acceleration.Z}>")]
        private class Particle
        {
            public int Id { get; }
            public Vector3 Position { get; set; }
            public Vector3 Velocity { get; set; }
            public Vector3 Acceleration { get; set; }

            public Particle(int id)
            {
                Id = id;
            }
        }

        [DebuggerDisplay("<{X},{Y},{Z}>")]
        private class Vector3
        {
            public int X { get; set; }
            public int Y { get; set; }
            public int Z { get; set; }
        }

        private class Vector3Comparer : IEqualityComparer<Vector3>
        {
            public bool Equals(Vector3 a, Vector3 b)
            {
                return a.X == b.X &&
                    a.Y == b.Y &&
                    a.Z == b.Z;
            }

            public int GetHashCode(Vector3 obj)
            {
                return obj.X & 0xFF + ((obj.Y & 0xFF) << 8) + ((obj.Z & 0xFF) << 16);
            }
        }
    }
}

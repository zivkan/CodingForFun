using aoc.csharp.Geometry;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;

namespace aoc.csharp._2017
{
    public class Day20 : ISolver
    {
        public (string Part1, string Part2) GetSolution(TextReader input)
        {
            return GetAnswer(input);
        }

        public static (string Part1, string Part2) GetAnswer(TextReader input)
        {
            var text = input.ReadToEnd();
            var part1 = FindParticleThatStaysClosestToOrigin(text);
            var part2 = FindParticleCountAfterCollisions(text);
            return (part1.ToString(), part2.ToString());
        }

        private static readonly Regex _regex = new Regex(@"p=<(?<px>-?\d+),(?<py>-?\d+),(?<pz>-?\d+)>, v=<(?<vx>-?\d+),(?<vy>-?\d+),(?<vz>-?\d+)>, a=<(?<ax>-?\d+),(?<ay>-?\d+),(?<az>-?\d+)>");

        public static int FindParticleThatStaysClosestToOrigin(string input)
        {
            var particles = ParseInput(input);

            // As time goes to infinity, given constant acceleration, all particles will be moving away from the origin
            // and the particle with the lowest acceleration will be travelling the slowest, hence closest to the origin.
            // Therefore, no need to simulate.
            int id = -1;
            uint max = uint.MaxValue;
            foreach (var particle in particles)
            {
                var accel = particle.Acceleration.GetManhattenDistance();
                if (accel < max)
                {
                    max = accel;
                    id = particle.Id;
                }
            }

            return id;
        }

        public static int FindParticleCountAfterCollisions(string input)
        {
            IReadOnlyList<Particle> particles = ParseInput(input);
            RemoveCollisions(particles);

            for (int time = 0; time < 500; time++)
            {
                particles = ApplyOneTimeUnit(particles);
                particles = RemoveCollisions(particles);
            }

            return particles.Count;
        }

        private static IReadOnlyList<Particle> ApplyOneTimeUnit(IReadOnlyList<Particle> particles)
        {
            var updatedParticles = new List<Particle>(particles.Count);

            for (int i = 0; i < particles.Count; i++)
            {
                var particle = particles[i];
                var newVelocity = particle.Velocity + particle.Acceleration;
                var newPosition = particle.Position + newVelocity;
                var updatedParticle = new Particle(
                    particle.Id,
                    newPosition,
                    newVelocity,
                    particle.Acceleration);
                updatedParticles.Add(updatedParticle);
            }

            return updatedParticles;
        }

        private static IReadOnlyList<Particle> RemoveCollisions(IReadOnlyList<Particle> particles)
        {
            var points = new Dictionary<Point3D, Particle?>(particles.Count, Point3DComparer.Instance);

            foreach (var particle in particles)
            {
                if (points.TryGetValue(particle.Position, out _))
                {
                    points[particle.Position] = null;
                }
                else
                {
                    points.Add(particle.Position, particle);
                }
            }

            var particlesRemaining = new List<Particle>(points.Count);

            foreach (var point in points)
            {
                if (point.Value != null)
                {
                    particlesRemaining.Add(point.Value);
                }
            }

            return particlesRemaining;
        }

        private static List<Particle> ParseInput(string input)
        {
            var particles = new List<Particle>();

            var matches = _regex.Matches(input);
            int id = 0;
            foreach (Match? match in matches)
            {
                if (match == null) throw new Exception();

                var position = new Point3D(
                    int.Parse(match.Groups["px"].Value),
                    int.Parse(match.Groups["py"].Value),
                    int.Parse(match.Groups["pz"].Value));

                var velocity = new Point3D(
                    int.Parse(match.Groups["vx"].Value),
                    int.Parse(match.Groups["vy"].Value),
                    int.Parse(match.Groups["vz"].Value));

                var acceleration = new Point3D(
                    int.Parse(match.Groups["ax"].Value),
                    int.Parse(match.Groups["ay"].Value),
                    int.Parse(match.Groups["az"].Value));

                var particle = new Particle(id++, position, velocity, acceleration);

                particles.Add(particle);
            }

            return particles;
        }

        [DebuggerDisplay("p=<{Position.X},{Position.Y},{Position.Z}>, v=<{Velocity.X},{Velocity.Y},{Velocity.Z}>, a=<{Acceleration.X},{Acceleration.Y},{Acceleration.Z}>")]
        private class Particle
        {
            public int Id { get; }
            public Point3D Position { get; set; }
            public Point3D Velocity { get; set; }
            public Point3D Acceleration { get; set; }

            public Particle(int id, Point3D position, Point3D velocity, Point3D acceleration)
            {
                Id = id;
                Position = position;
                Velocity = velocity;
                Acceleration = acceleration;
            }
        }
    }
}

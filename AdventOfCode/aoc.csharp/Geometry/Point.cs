using System;
using System.Diagnostics.CodeAnalysis;

namespace aoc.csharp.Geometry
{
    public class Point : IEquatable<Point>
    {
        public int X { get; }
        public int Y { get; }

        public static Point Zero { get; } = new Point(0, 0);

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public static Point operator +(Point a, Point b)
        {
            return new Point(a.X + b.X, a.Y + b.Y);
        }

        public static Point operator -(Point a, Point b)
        {
            return new Point(a.X - b.X, a.Y - b.Y);
        }

        public override string ToString()
        {
            return $"({X},{Y})";
        }

        public static bool operator ==(Point a, Point b)
        {
            return PointComparer.Instance.Equals(a, b);
        }

        public static bool operator !=(Point a, Point b)
        {
            return !PointComparer.Instance.Equals(a, b);
        }

        public bool Equals([AllowNull] Point other)
        {
            return PointComparer.Instance.Equals(this, other);
        }

        public override bool Equals(object? obj)
        {
            return Equals(obj as Point);
        }

        public override int GetHashCode()
        {
            return PointComparer.Instance.GetHashCode(this);
        }

        public uint GetManhattenDistance()
        {
            uint x = X >= 0 ? (uint)X : (uint)-X;
            uint y = Y >= 0 ? (uint)Y : (uint)-Y;
            var distance = x + y;
            return distance;
        }
    }
}

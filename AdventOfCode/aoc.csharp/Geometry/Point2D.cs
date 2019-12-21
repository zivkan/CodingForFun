using System;
using System.Diagnostics.CodeAnalysis;

namespace aoc.csharp.Geometry
{
    public class Point2D : IEquatable<Point2D>
    {
        public int X { get; }
        public int Y { get; }

        public static Point2D Zero { get; } = new Point2D(0, 0);

        public Point2D(int x, int y)
        {
            X = x;
            Y = y;
        }

        public static Point2D operator +(Point2D a, Point2D b)
        {
            return new Point2D(a.X + b.X, a.Y + b.Y);
        }

        public static Point2D operator -(Point2D a, Point2D b)
        {
            return new Point2D(a.X - b.X, a.Y - b.Y);
        }

        public override string ToString()
        {
            return $"({X},{Y})";
        }

        public static bool operator ==(Point2D a, Point2D b)
        {
            return Point2DComparer.Instance.Equals(a, b);
        }

        public static bool operator !=(Point2D a, Point2D b)
        {
            return !Point2DComparer.Instance.Equals(a, b);
        }

        public bool Equals([AllowNull] Point2D other)
        {
            return Point2DComparer.Instance.Equals(this, other);
        }

        public override bool Equals(object? obj)
        {
            return Equals(obj as Point2D);
        }

        public override int GetHashCode()
        {
            return Point2DComparer.Instance.GetHashCode(this);
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

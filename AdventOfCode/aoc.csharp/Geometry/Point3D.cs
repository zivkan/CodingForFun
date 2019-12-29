using System;
using System.Diagnostics.CodeAnalysis;

namespace aoc.csharp.Geometry
{
    public class Point3D : IEquatable<Point3D>
    {
        public int X { get; }
        public int Y { get; }
        public int Z { get; }

        public Point3D(int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public static Point3D Zero { get; } = new Point3D(0, 0, 0);

        public static Point3D operator +(Point3D a, Point3D b)
        {
            return new Point3D(
                a.X + b.X,
                a.Y + b.Y,
                a.Z + b.Z);
        }

        public static Point3D operator -(Point3D a, Point3D b)
        {
            return new Point3D(
                a.X - b.X,
                a.Y - b.Y,
                a.Z - b.Z);
        }

        public bool Equals([AllowNull] Point3D other)
        {
            return Point3DComparer.Instance.Equals(this, other);
        }

        public override bool Equals(object? obj)
        {
            return Point3DComparer.Instance.Equals(this, obj as Point3D);
        }

        public static bool operator ==(Point3D a, Point3D b)
        {
            return Point3DComparer.Instance.Equals(a, b);
        }

        public static bool operator !=(Point3D a, Point3D b)
        {
            return !Point3DComparer.Instance.Equals(a, b);
        }

        public override string ToString()
        {
            return $"({X}, {Y}, {Z})";
        }

        public override int GetHashCode()
        {
            return Point3DComparer.Instance.GetHashCode(this);
        }

        internal uint GetManhattenDistance()
        {
            return (uint)Math.Abs(X) + (uint)Math.Abs(Y) + (uint)Math.Abs(Z);
        }
    }
}

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace aoc.csharp.Geometry;

public class Point2DComparer : IEqualityComparer<Point2D>
{
    public static Point2DComparer Instance { get; } = new Point2DComparer();

    public bool Equals([AllowNull] Point2D x, [AllowNull] Point2D y)
    {
        if (x is null || y is null)
        {
            return ReferenceEquals(x, y);
        }

        if (ReferenceEquals(x, y))
        {
            return true;
        }

        return x.X == y.X && x.Y == y.Y;
    }

    public int GetHashCode([DisallowNull] Point2D obj)
    {
        unchecked
        {
            var hash = (obj.X << 16 | obj.X >> 16) ^ obj.Y;
            return hash;
        }
    }
}

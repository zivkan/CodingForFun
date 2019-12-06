using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace aoc.csharp.Geometry
{
    public class PointComparer : IEqualityComparer<Point>
    {
        public static PointComparer Instance { get; } = new PointComparer();

        public bool Equals([AllowNull] Point x, [AllowNull] Point y)
        {
            if (x == null || y == null)
            {
                return x == y;
            }

            if (ReferenceEquals(x, y))
            {
                return true;
            }

            return x.X == y.X && x.Y == y.Y;
        }

        public int GetHashCode([DisallowNull] Point obj)
        {
            unchecked
            {
                var hash = (obj.X << 16 | obj.X >> 16) ^ obj.Y;
                return hash;
            }
        }
    }
}

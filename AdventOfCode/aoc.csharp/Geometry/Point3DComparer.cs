using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace aoc.csharp.Geometry;

public class Point3DComparer : IEqualityComparer<Point3D>
{
    public static Point3DComparer Instance { get; } = new Point3DComparer();

    public bool Equals([AllowNull] Point3D a, [AllowNull] Point3D b)
    {
        if (ReferenceEquals(a, b))
        {
            return true;
        }

        if (a is null || b is null)
        {
            return false;
        }

        return a.X == b.X
            && a.Y == b.Y
            && a.Z == b.Z;
    }

    public int GetHashCode([DisallowNull] Point3D obj)
    {
        int rotX = obj.X << 20 | obj.X >> 12;
        int rotY = obj.Y << 10 | obj.Y >> 22;
        int hash = rotX ^ rotY ^ obj.Z;
        return hash;
    }
}

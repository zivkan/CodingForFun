using System;
using System.Collections.Generic;

namespace aoc.csharp;

public static class EnumerableExtensions
{
    public static T1 Smallest<T1, T2>(this IEnumerable<T1> enumerable, Func<T1, T2> predicate) where T2 : IComparable
    {
        var enumerator = enumerable.GetEnumerator();
        if (!enumerator.MoveNext())
        {
            throw new InvalidOperationException();
        }

        var smallest = enumerator.Current;
        var value = predicate(smallest);

        while (enumerator.MoveNext())
        {
            var obj = enumerator.Current;
            var objValue = predicate(obj);
            if (value.CompareTo(objValue) > 0)
            {
                smallest = obj;
                value = objValue;
            }
        }

        return smallest;
    }
}

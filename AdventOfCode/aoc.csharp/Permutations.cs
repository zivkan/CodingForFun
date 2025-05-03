using System.Collections.Generic;

namespace aoc.csharp;

public static class Permutations
{
    public static IEnumerable<IReadOnlyList<T>> Generate<T>(IReadOnlyList<T> input)
    {
        var indexes = new int[input.Count];
        for (int i = 0; i < indexes.Length; i++)
        {
            indexes[i] = i;
        }

        yield return input;

        while (true)
        {
            for (int i = indexes.Length - 1; i >= 0; i--)
            {
                var value = ++indexes[i];
                if (value < indexes.Length)
                {
                    break;
                }
                else
                {
                    if (i > 0)
                    {
                        indexes[i] = 0;
                    }
                    else
                    {
                        yield break;
                    }
                }
            }

            bool valid = true;
            for (int i = 1; valid && i < indexes.Length; i++)
            {
                var value = indexes[i];
                for (int j = 0; j < i; j++)
                {
                    if (indexes[j] == value)
                    {
                        valid = false;
                        break;
                    }
                }
            }

            if (valid)
            {
                var value = new T[indexes.Length];
                for (int i = 0; i < indexes.Length; i++)
                {
                    value[i] = input[indexes[i]];
                }
                yield return value;
            }
        }
    }
}

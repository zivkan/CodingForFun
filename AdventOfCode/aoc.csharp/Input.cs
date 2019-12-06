using System;
using System.Collections.Generic;
using System.IO;

namespace aoc.csharp
{
    internal static class Input
    {
        private static readonly Type intListType = typeof(List<int>);
        private static readonly Type intIListType = typeof(IList<int>);
        private static readonly Type intIEnumerableType = typeof(IEnumerable<int>);
        private static readonly Type intArray = typeof(int[]);

        internal static T To<T>(TextReader reader)
        {
            var type = typeof(T);
            if (type == intListType || type == intIListType || type == intIEnumerableType)
            {
                return (T)ToIntList(reader);
            }
            else if (type == intArray)
            {
                return (T)(object)((List<int>)ToIntList(reader)).ToArray();
            }
            else
            {
                throw new NotSupportedException();
            }
        }

        private static object ToIntList(TextReader reader)
        {
            var list = new List<int>();

            int value = 0;
            bool inValue = false;

            int read;
            while ((read = reader.Read()) >= 0)
            {
                char c = (char)read;
                if (c >= '0' && c <= '9')
                {
                    if (inValue)
                    {
                        value = value * 10 + (c - '0');
                    }
                    else
                    {
                        value = c - '0';
                        inValue = true;
                    }
                }
                else if (c == ',' || c == ' ' || c == '\n' || c == '\r')
                {
                    if (inValue)
                    {
                        list.Add(value);
                        inValue = false;
                    }
                }
            }

            if (inValue)
            {
                list.Add(value);
            }

            return list;
        }
    }
}

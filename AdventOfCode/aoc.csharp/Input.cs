using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

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

        internal static string[] GetLines(TextReader input)
        {
            var lines = new List<string>();
            string? line;
            while ((line = input.ReadLine()) != null)
            {
                lines.Add(line);
            }

            return lines.ToArray();
        }

        private static object ToIntList(TextReader reader)
        {
            var list = new List<int>();
            var sb = new StringBuilder();

            int read;
            while ((read = reader.Read()) >= 0)
            {
                char c = (char)read;
                if ((c >= '0' && c <= '9') || c == '-')
                {
                    sb.Append(c);
                }
                else if (c == ',' || c == ' ' || c == '\n' || c == '\r')
                {
                    if (sb.Length > 0)
                    {
                        list.Add(int.Parse(sb.ToString()));
                        sb.Clear();
                    }
                }
                else
                {
                    throw new Exception("Unexpected character");
                }
            }

            if (sb.Length > 0)
            {
                list.Add(int.Parse(sb.ToString()));
            }

            return list;
        }
    }
}

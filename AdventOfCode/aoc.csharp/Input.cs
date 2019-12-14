using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace aoc.csharp
{
    internal static class Input
    {
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

        internal static List<T> ToList<T>(TextReader reader) where T : IConvertible
        {
            var list = new List<T>();
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
                        list.Add((T)Convert.ChangeType(sb.ToString(), typeof(T)));
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
                list.Add((T)Convert.ChangeType(sb.ToString(), typeof(T)));
            }

            return list;
        }
    }
}

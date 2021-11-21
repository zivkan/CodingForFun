using System;
using System.IO;
using System.Text;

namespace aoc.csharp._2015
{
    public class Day08 : ISolver
    {
        public (string Part1, string Part2) GetSolution(TextReader input)
        {
            return GetAnswer(input);
        }

        public static (string Part1, string Part2) GetAnswer(TextReader input)
        {
            string[] lines = Input.GetLines(input);
            string[] parsed = ParseLines(lines);
            string[] encoded = EncodeLines(lines);

            long part1 = 0;
            long part2 = 0;
            for (int i =0; i <parsed.Length; i++)
            {
                part1 += lines[i].Length - parsed[i].Length;
                part2 += encoded[i].Length - lines[i].Length;
            }

            return (part1.ToString(), part2.ToString());
        }

        public static string[] ParseLines(string[] lines)
        {
            string[] result = new string[lines.Length];

            for (int i = 0; i < lines.Length; i++)
            {
                result[i] = ParseLine(lines[i]);
            }

            return result;
        }

        public static string ParseLine(string input)
        {
            StringBuilder sb = new();
            if (input[0] != '"' || input[input.Length - 1] != '"')
            {
                throw new ArgumentException();
            }

            int end = input.Length - 1;
            for (int i = 1; i < end; i++)
            {
                char c = input[i];
                if (c == '\\')
                {
                    char next = input[i + 1];
                    if (next == 'x')
                    {
                        byte GetValue(char e)
                        {
                            if (e >= '0' && e <= '9')
                            {
                                return (byte)(e - '0');
                            }
                            else if (e >= 'a' && e <= 'f')
                            {
                                return (byte)(e - 'a' + 10);
                            }
                            else
                            {
                                throw new ArgumentException();
                            }
                        }

                        var p1 = GetValue(input[i + 2]);
                        var p2 = GetValue(input[i + 3]);
                        var value = (char)((p1 << 4) + p2);
                        sb.Append(value);
                        i += 3;
                    }
                    else
                    {
                        sb.Append(next);
                        i++;
                    }
                }
                else
                {
                    sb.Append(c);
                }
            }

            return sb.ToString();
        }

        public static string[] EncodeLines(string[] lines)
        {
            var result = new string[lines.Length];

            for (int i = 0; i < result.Length; i++)
            {
                result[i] = EncodeLine(lines[i]);
            }

            return result;
        }

        public static string EncodeLine(string line)
        {
            StringBuilder sb = new();

            sb.Append('"');
            for (int i = 0; i < line.Length; i++)
            {
                char c = line[i];
                if (c == '"')
                {
                    sb.Append("\\\"");
                }
                else if (c == '\\')
                {
                    sb.Append("\\\\");
                }
                else
                {
                    sb.Append(c);
                }
            }
            sb.Append('"');

            return sb.ToString();
        }
    }
}

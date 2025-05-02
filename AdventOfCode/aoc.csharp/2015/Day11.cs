using System;
using System.IO;

namespace aoc.csharp._2015
{
    public class Day11 : ISolver
    {
        public (string Part1, string Part2) GetSolution(TextReader input)
        {
            return GetAnswer(input);
        }

        public static (string Part1, string Part2) GetAnswer(TextReader input)
        {
            var text = input.ReadLine();
            var part1 = Next(text);
            var part2 = Next(part1);

            return (part1, part2);
        }

        public static string Next(string text)
        {
            if (text.Length != 8)
            {
                throw new ArgumentException();
            }

            char[] password = new char[text.Length];
            for (int i =0; i < text.Length; i++)
            {
                password[i] = text[i];
            }

            int index = password.Length - 1;
            while (index >= 0)
            {
                password[index]++;
                if (password[index] > 'z')
                {
                    password[index] = 'a';
                    index--;
                }
                else
                {
                    if (IsValid(password))
                    {
                        return new string(password);
                    }
                    else
                    {
                        index = password.Length - 1;
                    }
                }
            }

            throw new Exception("Unable to find next password");
        }

        public static bool IsValid(ReadOnlySpan<char> password)
        {
            if (password.Length != 8)
            {
                throw new ArgumentException();
            }

            return R1(password) && R2(password) && R3(password);

            static bool R1(ReadOnlySpan<char> password)
            {
                for (int i = 2; i < password.Length; i++)
                {
                    char c1 = password[i - 2];
                    char c2 = password[i - 1];
                    char c3 = password[i];

                    if ((c1 + 1) == c2 && c2 == (c3 - 1))
                    {
                        return true;
                    }
                }

                return false;
            }

            static bool R2(ReadOnlySpan<char> password)
            {
                for (int i =0; i < password.Length; i++)
                {
                    char c = password[i];
                    if (c == 'i' || c == 'o' || c== 'l')
                    {
                        return false;
                    }
                }

                return true;
            }

            static bool R3(ReadOnlySpan<char> password)
            {
                int pairs = 0;

                for (int i = 1; i < password.Length; i++)
                {
                    if (password[i-1] == password[i])
                    {
                        pairs++;
                        i++;
                    }
                }

                return pairs >= 2;
            }
        }
    }
}

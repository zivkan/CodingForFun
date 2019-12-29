using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace aoc.csharp._2017
{
    public class Day19 : ISolver
    {
        public (string Part1, string Part2) GetSolution(TextReader input)
        {
            return GetAnswer(input);
        }

        public static (string Part1, string Part2) GetAnswer(TextReader input)
        {
            var text = input.ReadToEnd();
            var (path, steps) = FindPath(text);
            return (path, steps.ToString());
        }

        public static (string path, int steps) FindPath(string input)
        {
            var diagram = Parse(input);
            var width = diagram.GetLength(0);

            int y = 0;
            int x;
            for (x = width - 1; x >= 0 && diagram[x, 0] != '|'; x--) ;
            if (x == -1)
            {
                throw new Exception();
            }

            int dx = 0;
            int dy = 1;
            int steps = 0;
            var path = new StringBuilder();
            char current = '|';

            while (current != ' ')
            {
                x += dx;
                y += dy;

                steps++;
                current = diagram[x, y];
                if (current >= 'A' && current <= 'Z')
                {
                    path.Append(current);
                }
                else if (current == '+')
                {
                    if (diagram[x + dy, y + dx] != ' ')
                    {
                        int tmp = dx;
                        dx = dy;
                        dy = tmp;
                    }
                    else if (diagram[x - dy, y - dx] != ' ')
                    {
                        int tmp = dx;
                        dx = -dy;
                        dy = -tmp;
                    }
                    else
                    {
                        throw new Exception();
                    }
                }
            }

            return (path.ToString(), steps);
        }

        private static char[,] Parse(string input)
        {
            List<string> lines = new List<string>();
            using (var reader = new StringReader(input))
            {
                string? line;
                while ((line = reader.ReadLine()) != null)
                {
                    lines.Add(line);
                }
            }

            int width = lines[0].Length;
            int height = lines.Count;

            var diagram = new char[width, height];
            for (int y = 0; y < height; y++)
            {
                string line = lines[y];
                if (line.Length != width)
                {
                    throw new ArgumentException();
                }

                for (int x = 0; x < width; x++)
                {
                    diagram[x, y] = line[x];
                }
            }

            return diagram;
        }
    }
}

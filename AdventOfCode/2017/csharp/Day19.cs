using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using input;
using Xunit;
using Xunit.Abstractions;

namespace csharp
{
    public class Day19
    {
        private ITestOutputHelper _output;
        private string _input;

        public Day19(ITestOutputHelper output)
        {
            _output = output;
            _input = GetInput.Day(19);
        }

        [Fact]
        public void Sample()
        {
            const string input = @"     |          
     |  +--+    
     A  |  C    
 F---|----E|--+ 
     |  |  |  D 
     +B-+  +--+ 
                ";

            var (path,steps) = FindPath(input);
            Assert.Equal("ABCDEF", path);
            Assert.Equal(38, steps);
        }

        [Fact]
        public void Puzzle()
        {
            var (path, steps) = FindPath(_input);
            _output.WriteLine("part 1 = {0}", path);
            _output.WriteLine("part 2 = {0}", steps);
        }

        private (string path, int steps) FindPath(string input)
        {
            var diagram = Parse(input);
            var width = diagram.GetLength(0);
            var height = diagram.GetLength(1);

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

        private char[,] Parse(string input)
        {
            List<string> lines = new List<string>();
            using (var reader = new StringReader(input))
            {
                string line;
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

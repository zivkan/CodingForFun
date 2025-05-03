using System.IO;

namespace aoc.csharp;

interface ISolver
{
    (string Part1, string Part2) GetSolution(TextReader input);
}

using System;
using System.CommandLine;
using System.CommandLine.Invocation;

namespace aoc.csharp
{
    class Program
    {
        static void Main(string[] args)
        {
            var rootCommand = new Command("aoc.csharp")
            {
                new Argument<int>("year"),
                new Argument<int>("day")
            };

            rootCommand.Handler =
                CommandHandler.Create(async (int year, int day) =>
                {
                    var type = typeof(Program).Assembly.GetType($"aoc.csharp._{year}.Day{day:D2}");
                    if (type == null)
                    {
                        Console.WriteLine($"Solution for {year} day {day} not implemented");
                        return;
                    }

                    var instance = Activator.CreateInstance(type);
                    if (instance is ISolver solver)
                    {
                        var input = await Input.GetAsync(year, day);

                        var (part1, part2) = solver.GetSolution(input);

                        Console.WriteLine("Part 1 solution:");
                        Console.WriteLine(part1);
                        Console.WriteLine();
                        Console.WriteLine("Part 2 solution:");
                        Console.WriteLine(part2);
                    }
                    else
                    {
                        Console.WriteLine("Unable to run " + type.FullName);
                    }
                });

            rootCommand.Invoke(args);
        }
    }
}

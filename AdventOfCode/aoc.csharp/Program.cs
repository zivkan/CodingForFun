using System;
using System.CommandLine;
using System.CommandLine.Invocation;

namespace aoc.csharp
{
    class Program
    {
        static void Main(string[] args)
        {
            var rootCommand = new Command("aoc.csharp");
            rootCommand.Add(new Argument<int>("year")
            {
                
            });
            rootCommand.Add(new Argument<int>("day"));

            rootCommand.Handler = CommandHandler.Create<int, int>((int year, int day) =>
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
                    var result = solver.GetSolution(Console.In);
                    Console.WriteLine("Part 1 solution: " + result.Part1);
                    Console.WriteLine("Part 2 solution: " + result.Part2);
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

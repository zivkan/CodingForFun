using System;
using System.CommandLine;
using System.CommandLine.Invocation;

#if DEBUG
using System.IO;
#endif

namespace aoc.csharp
{
    class Program
    {
        static void Main(string[] args)
        {
            var rootCommand = new Command("aoc.csharp")
            {
                new Argument<int>("year")
                {
                },
                new Argument<int>("day")
#if DEBUG
                , new Argument<FileInfo>("input")
                {
                    Arity = ArgumentArity.ZeroOrOne
                }
#endif
            };

            rootCommand.Handler =
#if DEBUG
                CommandHandler.Create<int, int, FileInfo>((int year, int day, FileInfo input) =>
#else
                CommandHandler.Create<int, int>((int year, int day) =>
#endif
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
                        var inputReader = Console.In;
#if DEBUG
                        if (input != null)
                        {
                            inputReader = input.OpenText();
                        }
#endif
                        var (part1, part2) = solver.GetSolution(inputReader);

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

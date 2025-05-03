using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace aoc.csharp._2019;

public class Day07 : ISolver
{
    public (string Part1, string Part2) GetSolution(TextReader input)
    {
        return GetAnswer(input);
    }

    public static (string Part1, string Part2) GetAnswer(TextReader input)
    {
        var program = Input.ToList<long>(input).ToArray();
        (var part1, _) = FindHighestSignal(program, new long[] { 0, 1, 2, 3, 4 });
        (var part2, _) = FindHighestSignal(program, new long[] { 5, 6, 7, 8, 9 });
        return (part1.ToString(), part2.ToString());
    }

    public static (long value, long[] phaseConfiguration) FindHighestSignal(long[] program, long[] phases)
    {
        long max = 0;
        long[]? phaseConfiguration = null;

        foreach (var phase in Permutations.Generate(phases))
        {
            var output = RunAmps(program, phase);
            if (output > max)
            {
                max = output;
                phaseConfiguration = phase.ToArray();
            }
        }

        if (phaseConfiguration == null)
        {
            throw new Exception();
        }

        return (max, phaseConfiguration);
    }

    private static long RunAmps(long[]  program, IReadOnlyList<long> phase)
    {
        var amps = new IntcodeVm[phase.Count];
        int amp;
        for (amp = 0; amp < amps.Length; amp++)
        {
            amps[amp] = new IntcodeVm(program);
            amps[amp].Input.Enqueue(phase[amp]);
        }

        amps[0].Input.Enqueue(0);

        amp = 0;
        long? thrust = null;
        while (true)
        {
            if (!amps[amp].Step())
            {
                break;
            }

            if (amps[amp].Output.Count > 0)
            {
                var value = amps[amp].Output.Dequeue();
                amp++;
                if (amp >= amps.Length)
                {
                    thrust = value;
                    amp = 0;
                }

                amps[amp].Input.Enqueue(value);
            }
        }

        if (thrust == null)
        {
            throw new Exception();
        }

        return thrust.Value;
    }
}

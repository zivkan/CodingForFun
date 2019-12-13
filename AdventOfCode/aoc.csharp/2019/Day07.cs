using System;
using System.Collections.Generic;
using System.IO;

namespace aoc.csharp._2019
{
    public class Day07 : ISolver
    {
        public (string Part1, string Part2) GetSolution(TextReader input)
        {
            return GetAnswer(input);
        }

        public static (string Part1, string Part2) GetAnswer(TextReader input)
        {
            var program = Input.To<int[]>(input);
            (var part1, _) = FindHighestSignal(program, new[] { 0, 1, 2, 3, 4 });
            (var part2, _) = FindHighestSignal(program, new[] { 5, 6, 7, 8, 9 });
            return (part1.ToString(), part2.ToString());
        }

        public static (int value, int[] phaseConfiguration) FindHighestSignal(int[] program, int[] phases)
        {
            var max = 0;
            int[]? phaseConfiguration = null;

            foreach (var phase in GetPermutations(phases))
            {
                var output = RunAmps(program, phase);
                if (output > max)
                {
                    max = output;
                    phaseConfiguration = phase;
                }
            }

            if (phaseConfiguration == null)
            {
                throw new Exception();
            }

            return (max, phaseConfiguration);
        }

        private static int RunAmps(int[]  program, int[] phase)
        {
            var amps = new IntcodeVm[phase.Length];
            int amp;
            for (amp = 0; amp < amps.Length; amp++)
            {
                amps[amp] = new IntcodeVm(program);
                amps[amp].Input.Enqueue(phase[amp]);
            }

            amps[0].Input.Enqueue(0);

            amp = 0;
            int? thrust = null;
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

        public static IEnumerable<T[]> GetPermutations<T>(T[] collection)
        {
            var indexes = new int[collection.Length];
            for (int i = 0; i < indexes.Length; i++)
            {
                indexes[i] = i;
            }

            yield return collection;

            while (true)
            {
                for (int i = indexes.Length - 1; i >= 0; i--)
                {
                    var value = ++indexes[i];
                    if (value < indexes.Length)
                    {
                        break;
                    }
                    else
                    {
                        if (i > 0)
                        {
                            indexes[i] = 0;
                        }
                        else
                        {
                            yield break;
                        }
                    }
                }

                bool valid = true;
                for (int i = 1; valid && i < indexes.Length; i++)
                {
                    var value = indexes[i];
                    for (int j = 0; j < i; j++)
                    {
                        if (indexes[j] == value)
                        {
                            valid = false;
                            break;
                        }
                    }
                }

                if (valid)
                {
                    var value = new T[indexes.Length];
                    for (int i = 0; i < indexes.Length; i++)
                    {
                        value[i] = collection[indexes[i]];
                    }
                    yield return value;
                }
            }
        }
    }
}

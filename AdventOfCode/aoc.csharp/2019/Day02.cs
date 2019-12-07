﻿using System;
using System.Collections.Generic;
using System.IO;

namespace aoc.csharp._2019
{
    public class Day02 : ISolver
    {
        public (string Part1, string Part2) GetSolution(TextReader input)
        {
            return GetAnswer(input);
        }

        public static (string Part1, string Part2) GetAnswer(TextReader input)
        {
            var list = Input.To<int[]>(input);

            var part1 = (int[])list.Clone();
            part1[1] = 12;
            part1[2] = 2;
            IntcodeVm.RunProgram(part1, new Queue<int>());

            var part2 = new int[list.Length];
            for (int verb = 0; verb < 100; verb++)
            {
                for (int noun = 0; noun < 100; noun++)
                {
                    Array.Copy(list, part2, list.Length);
                    part2[1] = noun;
                    part2[2] = verb;
                    IntcodeVm.RunProgram(part2, new Queue<int>());

                    if (part2[0] == 19690720)
                    {
                        return (part1[0].ToString(), (100 * noun + verb).ToString());
                    }
                }
            }

            throw new Exception("Couldn't find part2 answer");
        }
    }
}

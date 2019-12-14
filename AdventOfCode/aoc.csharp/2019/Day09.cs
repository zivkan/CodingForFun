﻿using System;
using System.IO;

namespace aoc.csharp._2019
{
    public class Day09 : ISolver
    {
        public (string Part1, string Part2) GetSolution(TextReader input)
        {
            return GetAnswer(input);
        }

        public static (string Part1, string Part2) GetAnswer(TextReader input)
        {
            var program = Input.ToList<long>(input).ToArray();

            var vm = new IntcodeVm(program);
            vm.Input.Enqueue(1);

            while (vm.Step()) ;

            if (vm.Output.Count != 1)
            {
                throw new Exception("Expected 1 output, got " + vm.Output.Count);
            }

            var part1 = vm.Output.Dequeue();

            vm = new IntcodeVm(program);
            vm.Input.Enqueue(2);
            while (vm.Step()) ;

            var part2 = string.Join(",", vm.Output.ToArray());

            return (part1.ToString(), part2);
        }
    }
}

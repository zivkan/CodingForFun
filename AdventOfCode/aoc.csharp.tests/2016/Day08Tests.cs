﻿using aoc.csharp._2016;
using System;
using System.Collections.Generic;
using System.IO;
using Xunit;

namespace aoc.csharp.tests._2016
{
    public class Day08Tests
    {
        [Fact]
        public void Answer()
        {
            using var input = Input.Get(2016, 08);
            var (part1, part2) = Day08.GetAnswer(input);

            Assert.Equal("106", part1);
            Assert.Equal(
                " ##  #### #    #### #     ##  #   #####  ##   ### " + Environment.NewLine +
                "#  # #    #    #    #    #  # #   ##    #  # #    " + Environment.NewLine +
                "#    ###  #    ###  #    #  #  # # ###  #    #    " + Environment.NewLine +
                "#    #    #    #    #    #  #   #  #    #     ##  " + Environment.NewLine +
                "#  # #    #    #    #    #  #   #  #    #  #    # " + Environment.NewLine +
                " ##  #    #### #### ####  ##    #  #     ##  ###  " + Environment.NewLine,
                part2);
        }

        [Fact]
        public void SampleData()
        {
            string input = "rect 3x2\n" +
                           "rotate column x=1 by 1\n" +
                           "rotate row y=0 by 4\n" +
                           "rotate column x=1 by 1\n";
            List<Day08.IInstruction> instructions;
            using (var reader = new StringReader(input))
            {
                instructions = Day08.ParseInstructions(reader);
            }

            var screen = new bool[7, 3];
            Day08.InitialiseScreen(screen);

            foreach (var instruction in instructions)
            {
                instruction.Execute(screen);
            }

            var result = Day08.DisplayScreen(screen);
            var expected =
                " #  # #" + Environment.NewLine +
                "# #    " + Environment.NewLine +
                " #     " + Environment.NewLine;
            Assert.Equal(expected, result);
        }
    }
}

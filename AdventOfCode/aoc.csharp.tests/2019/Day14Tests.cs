﻿using aoc.csharp._2019;
using System;
using System.IO;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace aoc.csharp.tests._2019;

public class Day14Tests(ITestOutputHelper _output)
{
    [Fact]
    public async Task Answer()
    {
        using var input = await Input.GetAsync(2019, 14);
        var (part1, part2) = Day14.GetAnswer(input);

        _output.WriteLine($"Part 1: {part1}");
        _output.WriteLine($"Part 2: {part2}");
    }

    [Fact]
    public void Sample1()
    {
        var input =
            "9 ORE => 2 A" + Environment.NewLine +
            "8 ORE => 3 B" + Environment.NewLine +
            "7 ORE => 5 C" + Environment.NewLine +
            "3 A, 4 B => 1 AB" + Environment.NewLine +
            "5 B, 7 C => 1 BC" + Environment.NewLine +
            "4 C, 1 A => 1 CA" + Environment.NewLine +
            "2 AB, 3 BC, 4 CA => 1 FUEL";
        string part1;
        using (var reader = new StringReader(input))
        {
            (part1, _) = Day14.GetAnswer(reader);
        }

        Assert.Equal("165", part1);
    }

    [Fact]
    public void Sample2()
    {
        var input =
            "157 ORE => 5 NZVS" + Environment.NewLine +
            "165 ORE => 6 DCFZ" + Environment.NewLine +
            "44 XJWVT, 5 KHKGT, 1 QDVJ, 29 NZVS, 9 GPVTF, 48 HKGWZ => 1 FUEL" + Environment.NewLine +
            "12 HKGWZ, 1 GPVTF, 8 PSHF => 9 QDVJ" + Environment.NewLine +
            "179 ORE => 7 PSHF" + Environment.NewLine +
            "177 ORE => 5 HKGWZ" + Environment.NewLine +
            "7 DCFZ, 7 PSHF => 2 XJWVT" + Environment.NewLine +
            "165 ORE => 2 GPVTF" + Environment.NewLine +
            "3 DCFZ, 7 NZVS, 5 HKGWZ, 10 PSHF => 8 KHKGT";
        string part1, part2;
        using (var reader = new StringReader(input))
        {
            (part1, part2) = Day14.GetAnswer(reader);
        }

        Assert.Equal("13312", part1);
        Assert.Equal("82892753", part2);
    }

    [Fact]
    public void Sample3()
    {
        var input = @"2 VPVL, 7 FWMGM, 2 CXFTF, 11 MNCFX => 1 STKFG
17 NVRVD, 3 JNWZP => 8 VPVL
53 STKFG, 6 MNCFX, 46 VJHF, 81 HVMC, 68 CXFTF, 25 GNMV => 1 FUEL
22 VJHF, 37 MNCFX => 5 FWMGM
139 ORE => 4 NVRVD
144 ORE => 7 JNWZP
5 MNCFX, 7 RFSQX, 2 FWMGM, 2 VPVL, 19 CXFTF => 3 HVMC
5 VJHF, 7 MNCFX, 9 VPVL, 37 CXFTF => 6 GNMV
145 ORE => 6 MNCFX
1 NVRVD => 8 CXFTF
1 VJHF, 6 MNCFX => 4 RFSQX
176 ORE => 6 VJHF";
        string part1, part2;
        using (var reader = new StringReader(input))
        {
            (part1, part2) = Day14.GetAnswer(reader);
        }

        Assert.Equal("180697", part1);
        Assert.Equal("5586022", part2);
    }

    [Fact]
    public void Sample4()
    {
        var input = @"171 ORE => 8 CNZTR
7 ZLQW, 3 BMBT, 9 XCVML, 26 XMNCP, 1 WPTQ, 2 MZWV, 1 RJRHP => 4 PLWSL
114 ORE => 4 BHXH
14 VRPVC => 6 BMBT
6 BHXH, 18 KTJDG, 12 WPTQ, 7 PLWSL, 31 FHTLT, 37 ZDVW => 1 FUEL
6 WPTQ, 2 BMBT, 8 ZLQW, 18 KTJDG, 1 XMNCP, 6 MZWV, 1 RJRHP => 6 FHTLT
15 XDBXC, 2 LTCX, 1 VRPVC => 6 ZLQW
13 WPTQ, 10 LTCX, 3 RJRHP, 14 XMNCP, 2 MZWV, 1 ZLQW => 1 ZDVW
5 BMBT => 4 WPTQ
189 ORE => 9 KTJDG
1 MZWV, 17 XDBXC, 3 XCVML => 2 XMNCP
12 VRPVC, 27 CNZTR => 2 XDBXC
15 KTJDG, 12 BHXH => 5 XCVML
3 BHXH, 2 VRPVC => 7 MZWV
121 ORE => 7 VRPVC
7 XCVML => 6 RJRHP
5 BHXH, 4 VRPVC => 5 LTCX";
        string part1, part2;
        using (var reader = new StringReader(input))
        {
            (part1, part2) = Day14.GetAnswer(reader);
        }

        Assert.Equal("2210736", part1);
        Assert.Equal("460664", part2);
    }
}

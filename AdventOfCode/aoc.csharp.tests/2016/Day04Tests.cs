using aoc.csharp._2016;
using System.IO;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace aoc.csharp.tests._2016;

public class Day04Tests(ITestOutputHelper _output)
{
    [Fact]
    public async Task Answer()
    {
        using var input = await Input.GetAsync(2016, 04);
        var (part1, part2) = Day04.GetAnswer(input);

        _output.WriteLine($"Part 1: {part1}");
        _output.WriteLine($"Part 2: {part2}");
    }

    [Fact]
    public void Part1Sample()
    {
        string input = "aaaaa-bbb-z-y-x-123[abxyz]\n" +
                       "a-b-c-d-e-f-g-h-987[abcde]\n" +
                       "not-a-real-room-404[oarel]\n" +
                       "totally-real-room-200[decoy]";

        int sum = 0;
            string? line;
        using (var reader = new StringReader(input))
        {
            while ((line = reader.ReadLine()) != null) {
                var room = Day04.ParseRoom(line);
                if (Day04.IsRealRoom(room))
                {
                    sum += room.SectorId;
                }
            }
        }

        Assert.Equal(1514, sum);
    }

    [Fact]
    public void Part2Sample()
    {
        var room = new Day04.RoomInformation("qzmt-zixmtkozy-ivhz", 343, "");

        string result = Day04.ShiftCypher(room.Name, room.SectorId);

        Assert.Equal("very encrypted name", result);
    }
}

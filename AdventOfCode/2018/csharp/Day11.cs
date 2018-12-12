using System.Threading.Tasks;
using input;
using Xunit;
using Xunit.Abstractions;

namespace csharp
{
    public class Day11
    {
        private ITestOutputHelper _output;
        private static readonly string _input = GetInput.Day(11);

        public Day11(ITestOutputHelper output)
        {
            _output = output;
        }

        [Theory]
        [InlineData(3, 5, 8, 4)]
        [InlineData(122, 79, 57, -5)]
        [InlineData(217, 196, 39, 0)]
        [InlineData(101, 153, 71, 4)]
        public void TestPowerLevel(int x, int y, int serial, int expected)
        {
            var actual = GetPowerLevel(x, y, serial);
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(18, "33,45")]
        [InlineData(42, "21,61")]
        public void Part1Sample(int serial, string expected)
        {
            var result = FindHighest3x3PowerRegion(serial);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void Part1()
        {
            var serial = int.Parse(_input);
            var result = FindHighest3x3PowerRegion(serial);
            _output.WriteLine("{0}", result);
        }

        [Theory]
        [InlineData(18, "90,269,16")]
        [InlineData(42, "232,251,12")]
        public void Part2Sample(int serial, string expected)
        {
            var result = FindHighestPowerRegion(serial);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void Part2()
        {
            var serial = int.Parse(_input);
            var result = FindHighestPowerRegion(serial);
            _output.WriteLine("{0}", result);
        }

        private string FindHighest3x3PowerRegion(int serial)
        {
            var powerGrid = GeneratePowerGrid(serial);
            var summedSquares = GetSummedSquares(powerGrid, 3);
            var highest = FindHighestPowerRegion(summedSquares);
            return $"{highest.x+1},{highest.y+1}";
        }

        private string FindHighestPowerRegion(int serial)
        {
            var powerGrid = GeneratePowerGrid(serial);
            int maxPower = int.MinValue;
            int maxSize = -1;
            int maxX = -1;
            int maxY = -1;

            var results = new (int size, int power, int x, int y)[300];
            var tmp = GetSummedSquares(powerGrid, 16);
            var asdfsdf = FindHighestPowerRegion(tmp);

            Parallel.For(1, 301, size =>
                {
                    var summedSquares = GetSummedSquares(powerGrid, size);
                    var highest = FindHighestPowerRegion(summedSquares);
                    results[size - 1] = (size, highest.power, highest.x, highest.y);
                });


            foreach (var result in results)
            {
                if (result.power > maxPower)
                {
                    maxPower = result.power;
                    maxSize = result.size;
                    maxX = result.x;
                    maxY = result.y;
                }
            }

            return $"{maxX + 1},{maxY + 1},{maxSize}";
        }

        private int[,] GeneratePowerGrid(int serial)
        {
            var grid = new int[300, 300];

            for (var y = 0; y < 300; y++)
            {
                for (var x = 0; x < 300; x++)
                {
                    grid[x, y] = GetPowerLevel(x + 1, y + 1, serial);
                }
            }

            return grid;
        }

        private int GetPowerLevel(int x, int y, int serial)
        {
            int rackId = x + 10;
            int powerLevelStart = rackId * y;
            int withSerial = powerLevelStart + serial;
            int multiplied = withSerial * rackId;
            int hundreds = (multiplied / 100) % 10;
            int powerLevel = hundreds - 5;
            return powerLevel;
        }

        private int[,] GetSummedSquares(int[,] powerGrid, int size)
        {
            var width = powerGrid.GetLength(0) - size + 1;
            var height = powerGrid.GetLength(1) - size + 1;

            var squares = new int[width, height];

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    int sum = 0;
                    for (var dy = 0; dy < size; dy++)
                    {
                        for (var dx = 0; dx < size; dx++)
                        {
                            sum += powerGrid[x + dx, y + dy];
                        }
                    }
                    squares[x, y] = sum;
                }
            }

            return squares;
        }

        private (int x, int y, int power) FindHighestPowerRegion(int[,] summedSquares)
        {
            int maxValue = int.MinValue;
            int maxX = -1;
            int maxY = -1;
            int width = summedSquares.GetLength(0);
            int height = summedSquares.GetLength(1);

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    var value = summedSquares[x, y];
                    if (value > maxValue)
                    {
                        maxValue = value;
                        maxX = x;
                        maxY = y;
                    }
                }
            }

            return (maxX, maxY, maxValue);
        }
    }
}

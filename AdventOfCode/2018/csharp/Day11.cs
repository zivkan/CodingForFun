using System.Diagnostics;
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
        [InlineData(18, "33,45", 29)]
        [InlineData(42, "21,61", 30)]
        public void Part1Sample(int serial, string expected, int expectedPower)
        {
            var (result, power) = FindHighest3x3PowerRegion(serial);
            Assert.Equal(expectedPower, power);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void Part1()
        {
            var serial = int.Parse(_input);
            var (result, power) = FindHighest3x3PowerRegion(serial);
            Assert.Equal("20,54", result);
            Assert.Equal(30, power);
        }

        [Theory]
        [InlineData(18, "90,269,16", 113)]
        [InlineData(42, "232,251,12", 119)]
        public void Part2Sample(int serial, string expected, int expectedPower)
        {
            var (result, power) = FindHighestPowerRegion(serial);
            Assert.Equal(expectedPower, power);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void Part2()
        {
            var serial = int.Parse(_input);
            var (result, power) = FindHighestPowerRegion(serial);
            Assert.Equal("233,93,13", result);
            Assert.Equal(141, power);
        }

        private (string, int) FindHighest3x3PowerRegion(int serial)
        {
            var powerGrid = GeneratePowerGrid(serial);
            var summedSquares = (int[,])powerGrid.Clone();
            UpdateSummedSquares(powerGrid, 2, summedSquares);
            UpdateSummedSquares(powerGrid, 3, summedSquares);
            var highest = FindHighestPowerRegion(summedSquares, 3);
            return ($"{highest.x+1},{highest.y+1}", highest.power);
        }

        private (string, int) FindHighestPowerRegion(int serial)
        {
            var powerGrid = GeneratePowerGrid(serial);
            var summedSquares = (int[,])powerGrid.Clone();

            int maxSize = 1;
            var (maxX, maxY, maxPower) = FindHighestPowerRegion(summedSquares, 1);

            for (var size = 2; size <= 300; size++)
            {
                UpdateSummedSquares(powerGrid, size, summedSquares);
                var current = FindHighestPowerRegion(summedSquares, size);
                if (current.power > maxPower)
                {
                    maxPower = current.power;
                    maxX = current.x;
                    maxY = current.y;
                    maxSize = size;
                }
            }

            return ($"{maxX + 1},{maxY + 1},{maxSize}", maxPower);
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

        private void UpdateSummedSquares(int[,] powerGrid, int size, int[,] summedSquares)
        {
            var width = powerGrid.GetLength(0) - size + 1;
            var height = powerGrid.GetLength(1) - size + 1;

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    int sum = summedSquares[x,y];
                    for (var i = 0; i < size - 1; i++)
                    {
                        sum = sum +
                           powerGrid[x + size - 1, y + i] +
                           powerGrid[x + i, y + size - 1];
                    }
                    sum = sum + powerGrid[x + size - 1, y + size - 1];
                    summedSquares[x, y] = sum;
                }
            }

#if DEBUG
            Debug.Assert(width == height);
            for (int i = 0; i < width; i++)
            {
                summedSquares[i, width] = int.MaxValue;
                summedSquares[width, i] = int.MaxValue;
            }
            summedSquares[width, width] = int.MaxValue;
#endif
        }

        private (int x, int y, int power) FindHighestPowerRegion(int[,] summedSquares, int size)
        {
            int maxValue = int.MinValue;
            int maxX = -1;
            int maxY = -1;
            int width = summedSquares.GetLength(0) - size + 1;
            int height = summedSquares.GetLength(1) - size + 1;

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

using System.Diagnostics;
using System.IO;

namespace aoc.csharp._2018
{
    public class Day11 : ISolver
    {
        public (string Part1, string Part2) GetSolution(TextReader input)
        {
            return GetAnswer(input);
        }

        public static (string Part1, string Part2) GetAnswer(TextReader input)
        {
            var text = input.ReadToEnd();
            var serial = int.Parse(text);
            (var part1, _) = FindHighest3x3PowerRegion(serial);
            (var part2, _) = FindHighestPowerRegion(serial);
            return (part1, part2);
        }

        public static (string, int) FindHighest3x3PowerRegion(int serial)
        {
            var powerGrid = GeneratePowerGrid(serial);
            var summedSquares = (int[,])powerGrid.Clone();
            UpdateSummedSquares(powerGrid, 2, summedSquares);
            UpdateSummedSquares(powerGrid, 3, summedSquares);
            var (x, y, power) = FindHighestPowerRegion(summedSquares, 3);
            return ($"{x + 1},{y + 1}", power);
        }

        public static (string, int) FindHighestPowerRegion(int serial)
        {
            var powerGrid = GeneratePowerGrid(serial);
            var summedSquares = (int[,])powerGrid.Clone();

            int maxSize = 1;
            var (maxX, maxY, maxPower) = FindHighestPowerRegion(summedSquares, 1);

            for (var size = 2; size <= 300; size++)
            {
                UpdateSummedSquares(powerGrid, size, summedSquares);
                var (x, y, power) = FindHighestPowerRegion(summedSquares, size);
                if (power > maxPower)
                {
                    maxPower = power;
                    maxX = x;
                    maxY = y;
                    maxSize = size;
                }
            }

            return ($"{maxX + 1},{maxY + 1},{maxSize}", maxPower);
        }

        private static int[,] GeneratePowerGrid(int serial)
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

        public static int GetPowerLevel(int x, int y, int serial)
        {
            int rackId = x + 10;
            int powerLevelStart = rackId * y;
            int withSerial = powerLevelStart + serial;
            int multiplied = withSerial * rackId;
            int hundreds = (multiplied / 100) % 10;
            int powerLevel = hundreds - 5;
            return powerLevel;
        }

        private static void UpdateSummedSquares(int[,] powerGrid, int size, int[,] summedSquares)
        {
            var width = powerGrid.GetLength(0) - size + 1;
            var height = powerGrid.GetLength(1) - size + 1;

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    int sum = summedSquares[x, y];
                    for (var i = 0; i < size - 1; i++)
                    {
                        sum = sum +
                           powerGrid[x + size - 1, y + i] +
                           powerGrid[x + i, y + size - 1];
                    }
                    sum += powerGrid[x + size - 1, y + size - 1];
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

        private static (int x, int y, int power) FindHighestPowerRegion(int[,] summedSquares, int size)
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

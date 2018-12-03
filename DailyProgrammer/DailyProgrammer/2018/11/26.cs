using System;
using Xunit;

namespace DailyProgrammer._2018._11
{
    public class _26
    {
        [Theory]
        [InlineData("#FF6347", 255, 99, 71)]
        [InlineData("#B8860B", 184, 134, 11)]
        [InlineData("#BDB76B", 189, 183, 107)]
        [InlineData("#0000CD", 0, 0, 205)]
        public void Challenge(string expected, byte r, byte g, byte b)
        {
            var actual = $"#{r:X2}{g:X2}{b:X2}";
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("#3C444C", "#000000", "#778899")]
        [InlineData("#DCB1D9", "#E6E6FA", "#FF69B4", "#B0C4DE")]
        public void Bonus(string expected, params string[] input)
        {
            ulong count = 0;
            decimal r = 0, g = 0, b = 0;

            foreach (var i in input)
            {
                count++;
                r += Convert.ToByte(i.Substring(1, 2), 16);
                g += Convert.ToByte(i.Substring(3, 2), 16);
                b += Convert.ToByte(i.Substring(5, 2), 16);
            }

            byte fr = (byte)Math.Round(r / count);
            byte fg = (byte)Math.Round(g / count);
            byte fb = (byte)Math.Round(b / count);

            var actual = $"#{fr:X2}{fg:X2}{fb:X2}";
            Assert.Equal(expected, actual);
        }
    }
}

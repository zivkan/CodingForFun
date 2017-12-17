using System;
using input;
using Xunit;
using Xunit.Abstractions;

namespace csharp
{
    public class Day17
    {
        private ITestOutputHelper _output;
        private string _input;

        public Day17(ITestOutputHelper output)
        {
            _output = output;
            _input = GetInput.Day(17);
        }

        [Fact]
        public void Sample()
        {
            var buffer = SpinLock(2017, 3);
            var value = ValueAfter(buffer, 2017);
            Assert.Equal(638, value);
        }

        [Fact]
        public void Puzzle()
        {
            var buffer = SpinLock(2017, 371);
            var value = ValueAfter(buffer, 2017);
            _output.WriteLine("part 1 = {0}", value);

            int after0 = -1;
            int current = 0;
            for (int i = 1; i <= 50000000; i++)
            {
                current = (current + 371) % i + 1;
                if (current == 1)
                {
                    after0 = i;
                }
            }

            _output.WriteLine("part 2 = {0}", after0);
        }

        private int[] SpinLock(int iterations, int spins)
        {
            int[] buffer = new int[iterations + 1];
            buffer[0] = 0;

            int current = 0;

            for (int i = 1; i <= iterations; i++)
            {
                current = (current + spins) % i;

                int toCopy = i - current - 1;
                if (toCopy > 0)
                {
                    Array.Copy(buffer, current + 1, buffer, current + 2, i - current - 1);
                }

                current++;
                buffer[current] = i;
            }

            return buffer;
        }

        private int ValueAfter(int[] buffer, int value)
        {
            for (int i = 0; i < buffer.Length; i++)
            {
                if (buffer[i] == value)
                {
                    return buffer[(i + 1) % buffer.Length];
                }
            }

            throw new Exception();
        }
    }
}

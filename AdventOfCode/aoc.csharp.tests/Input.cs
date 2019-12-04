using System.IO;
using System.Reflection;

namespace aoc.csharp.tests
{
    internal static class Input
    {
        private static readonly Assembly Assembly = typeof(Input).Assembly;

        internal static StreamReader Get(int year, int day)
        {
            var resourceName = $"aoc.csharp.tests._{year}.day{day:D2}.txt";
            var stream = Assembly.GetManifestResourceStream(resourceName);
            if (stream == null)
            {
                throw new FileNotFoundException();
            }

            return new StreamReader(stream);
        }
    }
}

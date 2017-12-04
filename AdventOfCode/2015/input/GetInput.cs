using System;
using System.IO;

namespace input
{
    public static class GetInput
    {
        public static string Day(int day)
        {
            string filename = $"input.Day{day:d2}.txt";
            using (var stream = typeof(GetInput).Assembly.GetManifestResourceStream(filename))
            {
                if (stream == null)
                {
                    throw new ArgumentOutOfRangeException();
                }

                using (var reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }
        }
    }
}

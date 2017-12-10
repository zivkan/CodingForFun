using System.IO;
using System.Reflection;

namespace input
{
    public static class GetInput
    {
        private static Assembly assembly = typeof(GetInput).Assembly;

        public static string Day(int day)
        {
            string resourceName = $"input.Day{day:D2}.txt";
            using (var stream = assembly.GetManifestResourceStream(resourceName))
            using (var reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }
    }
}

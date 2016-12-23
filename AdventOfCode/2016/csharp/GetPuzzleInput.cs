using System.IO;

namespace csharp
{
    internal class GetPuzzleInput
    {
        internal static TextReader Day(int day)
        {
            return new StreamReader($@"..\..\..\input\Day{day}.txt");
        }

        internal static string DayText(int day)
        {
            return File.ReadAllText($@"..\..\..\input\Day{day}.txt");
        }
    }
}

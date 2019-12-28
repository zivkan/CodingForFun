using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace aoc.csharp._2018
{
    public class Day04 : ISolver
    {
        public (string Part1, string Part2) GetSolution(TextReader input)
        {
            return GetAnswer(input);
        }

        public static (string Part1, string Part2) GetAnswer(TextReader input)
        {
            var text = input.ReadToEnd();
            var part1 = Part1Answer(text);
            var part2 = Part2Answer(text);
            return (part1.ToString(), part2.ToString());
        }

        public static long Part1Answer(string input)
        {
            var lines = ParseLines(input);
            var naps = GetNaps(lines).ToList();
            int sleepiestGuard = FindSleepiestGuard(naps);
            var (sleepiestTime, _) = FindSleepiestTime(sleepiestGuard, naps);
            return (long)sleepiestGuard * (long)sleepiestTime;
        }

        public static long Part2Answer(string input)
        {
            var lines = ParseLines(input);
            var naps = GetNaps(lines).ToList();

            var result = naps.GroupBy(n => n.Guard)
                .Select(g =>
                {
                    var guard = g.Key;
                    var (minute, times) = FindSleepiestTime(guard, naps);
                    return new { Guard = guard, Minute = minute, Times = times };
                })
                .OrderByDescending(n => n.Times)
                .First();
            return (long)result.Guard * (long)result.Minute;
        }

        private static List<LineInfo> ParseLines(string input)
        {
            List<LineInfo> lines = new List<LineInfo>();
            using (var reader = new StringReader(input))
            {
                string? line;
                while ((line = reader.ReadLine()) != null)
                {
                    int start = line.IndexOf('[');
                    int end = line.IndexOf(']');
                    var dateTime = DateTime.ParseExact(line.Substring(start + 1, end - start - 1), "yyyy-MM-dd HH:mm", null);
                    char c = line[end + 2];
                    LineType lineType;
                    int guard = 0;
                    if (c == 'G')
                    {
                        lineType = LineType.StartShift;
                        start = line.IndexOf('#') + 1;
                        while (line[start] >= '0' && line[start] <= '9')
                        {
                            guard = guard * 10 + line[start] - '0';
                            start++;
                        }
                    }
                    else if (c == 'f')
                    {
                        lineType = LineType.FallAsleep;
                    }
                    else if (c == 'w')
                    {
                        lineType = LineType.WakeUp;
                    }
                    else
                    {
                        throw new Exception();
                    }

                    var lineInfo = new LineInfo
                    {
                        DateTime = dateTime,
                        LineType = lineType,
                        Guard = guard
                    };
                    lines.Add(lineInfo);
                }
            }

            return lines;
        }

        private static IEnumerable<Nap> GetNaps(List<LineInfo> lines)
        {
            lines.Sort(new Comparison<LineInfo>((a, b) => Comparer<DateTime>.Default.Compare(a.DateTime, b.DateTime)));
            int guard = 0;
            using var enumerator = lines.GetEnumerator();
            while (enumerator.MoveNext())
            {
                if (enumerator.Current.LineType == LineType.StartShift)
                {
                    guard = enumerator.Current.Guard;
                }
                else if (enumerator.Current.LineType == LineType.FallAsleep)
                {
                    DateTime start = enumerator.Current.DateTime;
                    if (enumerator.MoveNext())
                    {
                        if (enumerator.Current.LineType == LineType.WakeUp)
                        {
                            DateTime end = enumerator.Current.DateTime;
                            yield return new Nap
                            {
                                Guard = guard,
                                Start = start,
                                End = end
                            };
                        }
                        else
                        {
                            throw new Exception();
                        }
                    }
                    else
                    {
                        throw new Exception();
                    }
                }
                else
                {
                    throw new Exception();
                }
            }
        }

        private static int FindSleepiestGuard(List<Nap> naps)
        {
            var guards = new Dictionary<int, TimeSpan>();
            foreach (var nap in naps)
            {
                if (!guards.TryGetValue(nap.Guard, out TimeSpan soFar))
                {
                    soFar = TimeSpan.Zero;
                }

                soFar += nap.End - nap.Start;

                guards[nap.Guard] = soFar;
            }

            TimeSpan max = TimeSpan.Zero;
            int id = -1;
            foreach (var guard in guards)
            {
                if (guard.Value > max)
                {
                    max = guard.Value;
                    id = guard.Key;
                }
            }

            if (id < 0)
            {
                throw new Exception();
            }

            return id;
        }

        private static (int minute, int times) FindSleepiestTime(int sleepiestGuard, List<Nap> naps)
        {
            var minutes = new Dictionary<int, int>();
            for (int minute = 0; minute < 60; minute++)
            {
                minutes[minute] = 0;
            }

            foreach (var nap in naps.Where(n => n.Guard == sleepiestGuard))
            {
                for (var time = nap.Start; time < nap.End; time = time.AddMinutes(1))
                {
                    minutes[time.Minute] += 1;
                }
            }

            int max = 0;
            int min = -1;
            foreach (var minute in minutes)
            {
                if (minute.Value > max)
                {
                    max = minute.Value;
                    min = minute.Key;
                }
            }

            if (min == -1)
            {
                throw new Exception();
            }

            return (min, max);
        }

        [DebuggerDisplay("#{Guard} {LineType} ({DateTime})")]
        private class LineInfo
        {
            public DateTime DateTime { get; set; }
            public LineType LineType { get; set; }
            public int Guard { get; set; }
        }

        private enum LineType
        {
            StartShift,
            FallAsleep,
            WakeUp
        }

        [DebuggerDisplay("#{Guard} ({Start} - {End})")]
        private class Nap
        {
            public int Guard { get; set; }
            public DateTime Start { get; set; }
            public DateTime End { get; set; }
        }
    }
}

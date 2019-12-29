using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace aoc.csharp._2017
{
    public class Day12 : ISolver
    {
        public (string Part1, string Part2) GetSolution(TextReader input)
        {
            return GetAnswer(input);
        }

        public static (string Part1, string Part2) GetAnswer(TextReader input)
        {
            var text = input.ReadToEnd();
            var groups = GroupPrograms(text);
            var part1 = groups.Single(g => g.Contains(0)).Count;
            var part2 = groups.Count;
            return (part1.ToString(), part2.ToString());
        }

        public static List<HashSet<int>> GroupPrograms(string sample)
        {
            var programs = new List<List<int>>();

            using (var reader = new StringReader(sample))
            {
                string? line;
                while ((line = reader.ReadLine()) != null)
                {
                    int index = line.IndexOf(" <-> ");
                    int pid = int.Parse(line.Substring(0, index));
                    if (pid != programs.Count)
                    {
                        throw new Exception();
                    }

                    string connectionsString = line.Substring(index + " <-> ".Length);
                    var connections = connectionsString.Split(',').Select(s => int.Parse(s.Trim())).ToList();
                    programs.Add(connections);
                }
            }

            HashSet<int> ungroupedPids = new HashSet<int>();
            for (int pid = 0; pid < programs.Count; pid++)
            {
                ungroupedPids.Add(pid);
            }

            List<HashSet<int>> groups = new List<HashSet<int>>();
            while (ungroupedPids.Count > 0)
            {
                HashSet<int> containing = new HashSet<int>();
                int pid = ungroupedPids.First();
                containing.Add(pid);
                ungroupedPids.Remove(pid);

                Stack<int> check = new Stack<int>();
                foreach (var connection in programs[pid])
                {
                    check.Push(connection);
                }

                while (check.Count > 0)
                {
                    pid = check.Pop();
                    containing.Add(pid);
                    ungroupedPids.Remove(pid);
                    foreach (var connection in programs[pid])
                    {
                        if (!containing.Contains(connection))
                        {
                            check.Push(connection);
                        }
                    }
                }

                groups.Add(containing);
            }

            return groups;
        }
    }
}

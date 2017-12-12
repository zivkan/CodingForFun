using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using input;
using Xunit;
using Xunit.Abstractions;

namespace csharp
{
    public class Day12
    {
        private ITestOutputHelper _output;
        private string _input;

        private const string _sample = @"0 <-> 2
1 <-> 1
2 <-> 0, 3, 4
3 <-> 2, 4
4 <-> 2, 3, 6
5 <-> 6
6 <-> 4, 5";

        public Day12(ITestOutputHelper output)
        {
            _output = output;
            _input = GetInput.Day(12);
        }

        [Fact]
        public void Sample()
        {
            var groups = GroupPrograms(_sample);
            Assert.Equal(6, groups.Single(g=>g.Contains(0)).Count);
            Assert.Equal(2, groups.Count);
        }

        [Fact]
        public void Puzzle()
        {
            var groups = GroupPrograms(_input);
            _output.WriteLine("connections to pid 0 = {0}", groups.Single(g=>g.Contains(0)).Count);
            _output.WriteLine("groups = {0}", groups.Count);
        }

        private List<HashSet<int>> GroupPrograms(string sample)
        {
            var programs = new List<List<int>>();

            using (var reader = new StringReader(sample))
            {
                string line;
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
            for(int pid = 0; pid < programs.Count; pid++)
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

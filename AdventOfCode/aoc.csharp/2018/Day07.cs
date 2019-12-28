using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace aoc.csharp._2018
{
    public class Day07 : ISolver
    {
        public (string Part1, string Part2) GetSolution(TextReader input)
        {
            return GetAnswer(input);
        }

        public static (string Part1, string Part2) GetAnswer(TextReader input)
        {
            var text = input.ReadToEnd();
            (var part1, _) = GetOrder(text, 1, 0);
            (_, var part2)= GetOrder(text, 5, 60);
            return (part1, part2.ToString());
        }

        public static (string order, int elapsedTime) GetOrder(string input, int numWorkers, int timeOverhead)
        {
            var steps = ParseSteps(input);
            var scheduler = new Scheduler(steps);
            var workers = new List<Worker>(numWorkers);
            for (int worker = 0; worker < numWorkers; worker++)
            {
                workers.Add(new Worker());
            }

            var result = new StringBuilder();
            var elapsedTime = 0;
            var finishedThisTime = new List<char>();

            while (workers.Count > 0)
            {
                // phase 1 progress started work and complete anything finished
                foreach (var worker in workers)
                {
                    if (worker.Step != null)
                    {
                        worker.Remaining--;
                        if (worker.Remaining == 0)
                        {
                            finishedThisTime.Add(worker.Step.Value);
                            worker.Step = null;
                        }
                    }
                }

                if (finishedThisTime.Count > 0)
                {
                    finishedThisTime.Sort();
                    foreach (var finished in finishedThisTime)
                    {
                        result.Append(finished);
                        scheduler.Complete(finished);
                    }
                    finishedThisTime.Clear();
                }

                // phase 2 have idle workers start new ready tasks
                foreach (var worker in workers)
                {
                    if (worker.Step == null)
                    {
                        worker.Step = scheduler.GetNextReady();
                        if (worker.Step != null)
                        {
                            worker.Remaining = char.ToUpperInvariant(worker.Step.Value) - 'A' + 1 + timeOverhead;
                        }
                    }
                }

                // phase 3 idle wokers go home when all steps have been started
                if (scheduler.Remaining == 0)
                {
                    for (int worker = 0; worker < workers.Count; worker++)
                    {
                        if (workers[worker].Step == null)
                        {
                            workers.RemoveAt(worker);
                            worker--;
                        }
                    }
                }

                elapsedTime++;

                // phase 4 check for bugs
                if (workers.Count > 0 && workers.All(w => w.Step == null)) throw new Exception();
            }

            return (result.ToString(), elapsedTime - 1);
        }

        private static List<(char Id, HashSet<char> Dependencies)> ParseSteps(string input)
        {
            var result = new Dictionary<char, HashSet<char>>();
            using (var reader = new StringReader(input))
            {
                string? line;
                while ((line = reader.ReadLine()) != null)
                {
                    char dependency = line[5];
                    char stepId = line[36];

                    if (result.TryGetValue(stepId, out HashSet<char>? dependencies))
                    {
                        dependencies.Add(dependency);
                    }
                    else
                    {
                        dependencies = new HashSet<char>
                        {
                            dependency
                        };
                        result.Add(stepId, dependencies);
                    }

                    if (!result.ContainsKey(dependency))
                    {
                        result.Add(dependency, new HashSet<char>());
                    }
                }
            }

            return result.Select(kvp => (kvp.Key, kvp.Value)).OrderBy(r => r.Key).ToList();
        }

        private class Scheduler
        {
            private readonly List<(char Id, HashSet<char> Dependencies)> _state;

            public Scheduler(List<(char Id, HashSet<char> Dependencies)> initialState)
            {
                _state = initialState;
            }

            public char? GetNextReady()
            {
                for (int i = 0; i < _state.Count; i++)
                {
                    if (_state[i].Dependencies.Count == 0)
                    {
                        var selected = _state[i].Id;
                        _state.RemoveAt(i);
                        return selected;
                    }
                }

                return null;
            }

            public void Complete(char id)
            {
                for (int i = 0; i < _state.Count; i++)
                {
                    _state[i].Dependencies.Remove(id);
                }
            }

            public int Remaining => _state.Count;
        }

        private class Worker
        {
            public char? Step { get; set; }
            public int Remaining { get; set; }
        }
    }
}

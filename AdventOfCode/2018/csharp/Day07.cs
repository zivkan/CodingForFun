using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using input;
using Xunit;
using Xunit.Abstractions;

namespace csharp
{
    public class Day07
    {
        private ITestOutputHelper _output;
        private string _input;
        private static readonly string _sampleInput =
            "Step C must be finished before step A can begin.\n" +
            "Step C must be finished before step F can begin.\n" +
            "Step A must be finished before step B can begin.\n" +
            "Step A must be finished before step D can begin.\n" +
            "Step B must be finished before step E can begin.\n" +
            "Step D must be finished before step E can begin.\n" +
            "Step F must be finished before step E can begin.";

        public Day07(ITestOutputHelper output)
        {
            _output = output;
            _input = GetInput.Day(7);
        }

        [Fact]
        public void Part1Sample()
        {
            var result = GetOrder(_sampleInput, 1, 0);
            Assert.Equal("CABDFE", result.order);
        }

        [Fact]
        public void Part1()
        {
            var result = GetOrder(_input, 1, 0);
            _output.WriteLine("{0}", result.order);
        }

        [Fact]
        public void Part2Sample()
        {
            var result = GetOrder(_sampleInput, 2, 0);
            Assert.Equal("CABFDE", result.order);
            Assert.Equal(15, result.elapsedTime);
        }

        [Fact]
        public void Part2()
        {
            var result = GetOrder(_input, 5, 60);
            _output.WriteLine("{0}", result.elapsedTime);
        }


        private (string order, int elapsedTime) GetOrder(string input, int numWorkers, int timeOverhead)
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

        private List<(char Id, HashSet<char> Dependencies)> ParseSteps(string input)
        {
            var result = new Dictionary<char, HashSet<char>>();
            using (var reader = new StringReader(input))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    char dependency = line[5];
                    char stepId = line[36];

                    HashSet<char> dependencies;
                    if (result.TryGetValue(stepId, out dependencies))
                    {
                        dependencies.Add(dependency);
                    }
                    else
                    {
                        dependencies = new HashSet<char>();
                        dependencies.Add(dependency);
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
            private List<(char Id, HashSet<char> Dependencies)> _state;

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

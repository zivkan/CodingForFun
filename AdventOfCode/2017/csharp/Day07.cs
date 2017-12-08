using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Xunit;
using Xunit.Abstractions;

namespace csharp
{
    public class Day07
    {
        private ITestOutputHelper _output;
        private string _input;

        private static Regex _regex = new Regex("^(?<name>[a-z]*) \\((?<weight>[0-9]*)\\)( -> (?<children>[a-z, ]*))?");
        private static Regex _childRegex = new Regex("(?<child>[a-z]+)");

        private const string _sample = @"pbga (66)
xhth (57)
ebii (61)
havc (66)
ktlj (57)
fwft (72) -> ktlj, cntj, xhth
qoyq (66)
padx (45) -> pbga, havc, qoyq
tknk (41) -> ugml, padx, fwft
jptl (61)
ugml (68) -> gyxo, ebii, jptl
gyxo (61)
cntj (57)";

        public Day07(ITestOutputHelper output)
        {
            _output = output;
            _input = GetPuzzleInput.DayText(7);
        }

        [Fact]
        public void Part1Sample()
        {
            var tree = BuildTree(_sample);
            Assert.Equal("tknk", tree.Name);
        }

        [Fact]
        public void Part1()
        {
            var tree = BuildTree(_input);
            _output.WriteLine("Root node = {0}", tree.Name);
        }

        [Fact]
        public void Part2Sample()
        {
            var tree = BuildTree(_sample);
            var weightDiff = GetWeightDifference(tree);
            Assert.Equal(60, weightDiff);
        }

        [Fact]
        public void Part2()
        {
            var tree = BuildTree(_input);
            var weightDiff = GetWeightDifference(tree);
            _output.WriteLine("Correct weight = {0}", weightDiff);
        }

        private int GetWeightDifference(Node tree)
        {
            Node parentNode = null;
            Node node = tree;
            bool foundErrorNode = false;
            while (!foundErrorNode)
            {
                if (node.Children == null)
                {
                    throw new Exception();
                }

                Dictionary<int, int> weights = new Dictionary<int, int>();
                foreach (var child in node.Children)
                {
                    int count;
                    if (weights.TryGetValue(child.TotalWeight, out count))
                    {
                        count++;
                        weights[child.TotalWeight] = count;
                    }
                    else
                    {
                        weights[child.TotalWeight] = 1;
                    }
                }

                var wrongWeight = weights.SingleOrDefault(w => w.Value == 1);
                if (wrongWeight.Key == 0)
                {
                    foundErrorNode = true;
                }
                else
                {
                    parentNode = node;
                    node = node.Children.Single(n => n.TotalWeight == wrongWeight.Key);
                }
            }

            int correctWeight = parentNode.Children.First(n => n.TotalWeight != node.TotalWeight).TotalWeight;
            int diff = correctWeight - node.TotalWeight;

            return node.Weight + diff;
        }

        private Node BuildTree(string input)
        {
            Dictionary<string, Node> nodes = new Dictionary<string, Node>();

            using (var reader = new StringReader(input))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    var match = _regex.Match(line);
                    if (!match.Success)
                    {
                        throw new Exception();
                    }

                    var name = match.Groups["name"].Value;
                    var weightString = match.Groups["weight"].Value;
                    int weight = int.Parse(weightString);
                    string childrenList = match.Groups["children"]?.Value;
                    List<Node> children = null;
                    if (childrenList != null && !string.IsNullOrWhiteSpace(childrenList))
                    {
                        var childrenMatches = _childRegex.Matches(childrenList);
                        var childrenNames = new List<string>(childrenMatches.Count);
                        foreach (Match child in childrenMatches)
                        {
                            if (!string.IsNullOrWhiteSpace(child.Value))
                            {
                                childrenNames.Add(child.Value);
                            }
                        }

                        children = new List<Node>(childrenNames.Count);
                        foreach (var childName in childrenNames)
                        {
                            Node childNode;
                            if (nodes.TryGetValue(childName, out childNode))
                            {
                                childNode.IsChild = true;
                            }
                            else
                            {
                                childNode = new Node
                                {
                                    Name = childName,
                                    IsChild = true
                                };
                                nodes.Add(childName, childNode);
                            }
                            children.Add(childNode);
                        }
                    }

                    Node node;
                    if (nodes.TryGetValue(name, out node))
                    {
                        node.Weight = weight;
                        node.Children = children;
                    }
                    else
                    {
                        node = new Node
                        {
                            Name = name,
                            Weight = weight,
                            Children = children,
                            IsChild = false
                        };
                        nodes.Add(name, node);
                    }
                }
            }

            var root = nodes.Values.Single(n => !n.IsChild);

            CalculateTotalWeight(root);

            return root;
        }

        private void CalculateTotalWeight(Node node)
        {
            if (node.Children == null)
            {
                node.TotalWeight = node.Weight;
            }
            else
            {
                foreach (var child in node.Children)
                {
                    CalculateTotalWeight(child);
                }
                node.TotalWeight = node.Weight + node.Children.Sum(n => n.TotalWeight);
            }
        }

        private class Node
        {
            public string Name { get; set; }
            public int Weight { get; set; }
            public List<Node> Children { get; set; }
            public bool IsChild { get; set; }
            public int TotalWeight { get; set; }
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace aoc.csharp._2017
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
            var tree = BuildTree(text);
            var part1 = tree.Name;
            var part2 = GetWeightDifference(tree);
            return (part1, part2.ToString());
        }

        private static readonly Regex _regex = new Regex("^(?<name>[a-z]*) \\((?<weight>[0-9]*)\\)( -> (?<children>[a-z, ]*))?");
        private static readonly Regex _childRegex = new Regex("(?<child>[a-z]+)");

        public static int GetWeightDifference(Node tree)
        {
            Node? parentNode = null;
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
                    if (weights.TryGetValue(child.TotalWeight, out int count))
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

            if (parentNode == null)
            {
                throw new Exception();
            }

            int correctWeight = parentNode.Children.First(n => n.TotalWeight != node.TotalWeight).TotalWeight;
            int diff = correctWeight - node.TotalWeight;

            return node.Weight + diff;
        }

        public static Node BuildTree(string input)
        {
            Dictionary<string, Node> nodes = new Dictionary<string, Node>();

            using (var reader = new StringReader(input))
            {
                string? line;
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
                    string? childrenList = match.Groups["children"]?.Value;
                    List<Node> children = new List<Node>();
                    if (childrenList != null && !string.IsNullOrWhiteSpace(childrenList))
                    {
                        var childrenMatches = _childRegex.Matches(childrenList);
                        var childrenNames = new List<string>(childrenMatches.Count);
                        foreach (Match? child in childrenMatches)
                        {
                            if (child == null)
                            {
                                throw new Exception();
                            }

                            if (!string.IsNullOrWhiteSpace(child.Value))
                            {
                                childrenNames.Add(child.Value);
                            }
                        }

                        children = new List<Node>(childrenNames.Count);
                        foreach (var childName in childrenNames)
                        {
                            if (nodes.TryGetValue(childName, out Node? childNode))
                            {
                                childNode.IsChild = true;
                            }
                            else
                            {
                                childNode = new Node(childName)
                                {
                                    IsChild = true
                                };
                                nodes.Add(childName, childNode);
                            }
                            children.Add(childNode);
                        }
                    }

                    if (nodes.TryGetValue(name, out Node? node))
                    {
                        node.Weight = weight;
                        node.Children = children;
                    }
                    else
                    {
                        node = new Node(name)
                        {
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

        public static void CalculateTotalWeight(Node node)
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

        public class Node
        {
            public string Name { get; }
            public int Weight { get; set; }
            public List<Node> Children { get; set; }
            public bool IsChild { get; set; }
            public int TotalWeight { get; set; }

            public Node(string name)
            {
                Name = name;
                Children = new List<Node>();
            }
        }
    }
}

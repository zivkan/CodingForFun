using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace aoc.csharp._2019
{
    public class Day06 : ISolver
    {
        public (string Part1, string Part2) GetSolution(TextReader input)
        {
            return GetAnswer(input);
        }

        public static (string Part1, string Part2) GetAnswer(TextReader input)
        {
            var graph = ParseGraph(input);

            var totalDepth = graph.Select(kvp => kvp.Value.Depth).Sum();

            var youPath = GetPathToRoot("YOU", graph);
            var sanPath = GetPathToRoot("SAN", graph);
            var path = GetShortestPath(youPath, sanPath);

            // zero transfers is a path with SAN, YOU and one node in common.
            var transfers = path.Count - 3;

            return (totalDepth.ToString(), transfers.ToString());
        }

        private static List<string> GetShortestPath(List<string> path1, List<string> path2)
        {
            if (path1[path1.Count-1] != path2[path2.Count-1])
            {
                throw new ArgumentException("paths do not share common root");
            }

            var result = new List<string>();
            result.AddRange(path1);

            bool pathsDiverged = false;

            for (int i = path2.Count-2; i >= 0; i--)
            {
                if (pathsDiverged)
                {
                    result.Add(path2[i]);
                }
                else
                {
                    if (path2[i] == result[result.Count-2])
                    {
                        result.RemoveAt(result.Count - 1);
                    }
                    else
                    {
                        pathsDiverged = true;
                        result.Add(path2[i]);
                    }
                }
            }

            return result;
        }

        private static List<string> GetPathToRoot(string name, Dictionary<string, Node> graph)
        {
            Node? node = graph[name];
            var result = new List<string>();

            while (node != null)
            {
                result.Add(node.Name);
                node = node.Parent;
            }

            return result;
        }

        private static List<KeyValuePair<string,string>> ParseMapping(TextReader input)
        {
            var result = new List<KeyValuePair<string, string>>();
            string? line;
            while ((line = input.ReadLine()) != null)
            {
                var seperator = line.IndexOf(')');
                var key = new string(line.AsSpan(0, seperator));
                var value = new string(line.AsSpan(seperator + 1));
                var kvp = new KeyValuePair<string, string>(key, value);
                result.Add(kvp);
            }

            return result;
        }

        public static Dictionary<string, Node> ParseGraph(TextReader input)
        {
            var mapping = ParseMapping(input);

            var result = new Dictionary<string, Node>();
            Node node = new Node("COM")
            {
                Depth = 0
            };
            result.Add(node.Name, node);

            // get full list of nodes, and set parent/children
            foreach (var kvp in mapping)
            {
                var parent = GetOrCreate(kvp.Key, result);
                node = GetOrCreate(kvp.Value, result);

                parent.Children.Add(node);
                node.Parent = parent;
            }

            // calculate depth for all nodes
            var toProcess = new Stack<Node>();
            node = result["COM"];
            foreach (var child in node.Children)
            {
                toProcess.Push(child);
            }

            while (toProcess.Count > 0)
            {
                node = toProcess.Pop();
                node.Depth = node.Parent.Depth + 1;

                foreach(var child in node.Children)
                {
                    toProcess.Push(child);
                }
            }

            return result;
        }

        private static Node GetOrCreate(string name, Dictionary<string, Node> graph)
        {
            if (graph.TryGetValue(name, out var node))
            {
                return node;
            }

            node = new Node(name);
            graph.Add(name, node);
            return node;
        }

        public class Node
        {
            public Node(string name)
            {
                Name = name;
            }

            public string Name { get; }
            public int Depth { get; set; } = -1;
            public Node? Parent { get; set; }
            public List<Node> Children { get; set; } = new List<Node>();
        }
    }
}

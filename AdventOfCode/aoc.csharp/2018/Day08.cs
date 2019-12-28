using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace aoc.csharp._2018
{
    public class Day08 : ISolver
    {
        public (string Part1, string Part2) GetSolution(TextReader input)
        {
            return GetAnswer(input);
        }

        public static (string Part1, string Part2) GetAnswer(TextReader input)
        {
            var text = input.ReadToEnd();
            var part1 = GetSumOfMetadata(text);
            var part2 = GetRootNodeValue(text);
            return (part1.ToString(), part2.ToString());
        }

        private static readonly Regex _regex = new Regex(@"(\d+)");

        public static int GetSumOfMetadata(string input)
        {
            var tree = ParseTree(input);
            var stack = new Stack<Node>();
            stack.Push(tree);
            int sum = 0;

            while (stack.TryPop(out Node? node))
            {
                sum += node.Metadata.Sum();
                foreach (var child in node.Children)
                {
                    stack.Push(child);
                }
            }

            return sum;
        }

        public static int GetRootNodeValue(string input)
        {
            var tree = ParseTree(input);

            var result = GetNodeValue(tree);
            return result;
        }

        private static int GetNodeValue(Node node)
        {
            if (node.Children.Count == 0)
            {
                return node.Metadata.Sum();
            }
            else
            {
                var childrenValues = new int[node.Children.Count];
                for (int c = 0; c < node.Children.Count; c++)
                {
                    childrenValues[c] = GetNodeValue(node.Children[c]);
                }

                int sum = 0;
                foreach (var m in node.Metadata)
                {
                    if (m > 0 && m <= childrenValues.Length)
                    {
                        sum += childrenValues[m - 1];
                    }
                }

                return sum;
            }
        }

        private static Node ParseTree(string input)
        {
            using var enumerator = GetIntStream(input).GetEnumerator();
            var result = ParseTree(enumerator);
            if (enumerator.MoveNext()) throw new Exception();
            return result;
        }

        private static Node ParseTree(IEnumerator<int> enumerator)
        {
            if (!enumerator.MoveNext()) throw new Exception();
            int numChildren = enumerator.Current;
            if (!enumerator.MoveNext()) throw new Exception();
            int numMetadata = enumerator.Current;

            var node = new Node();

            for (int c = 0; c < numChildren; c++)
            {
                node.Children.Add(ParseTree(enumerator));
            }

            for (int m = 0; m < numMetadata; m++)
            {
                if (!enumerator.MoveNext()) throw new Exception();
                node.Metadata.Add(enumerator.Current);
            }

            return node;
        }

        private static IEnumerable<int> GetIntStream(string input)
        {
            var results = _regex.Matches(input);
            foreach (Match? result in results)
            {
                if (result == null) throw new Exception();
                var str = result.Captures[0].Value;
                int i = int.Parse(str);
                yield return i;
            }
        }

        private class Node
        {
            public List<Node> Children { get; }
            public List<int> Metadata { get; }

            public Node()
            {
                Children = new List<Node>();
                Metadata = new List<int>();
            }
        }
    }
}

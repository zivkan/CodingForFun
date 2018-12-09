using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using input;
using Xunit;
using Xunit.Abstractions;

namespace csharp
{
    public class Day08
    {
        private ITestOutputHelper _output;
        private string _input;
        private static readonly string _sampleInput =
            "2 3 0 3 10 11 12 1 1 0 1 99 2 1 1 2";
        private static readonly Regex _regex = new Regex(@"(\d+)");

        public Day08(ITestOutputHelper output)
        {
            _output = output;
            _input = GetInput.Day(8);
        }

        [Fact]
        public void Part1Sample()
        {
            var result = GetSumOfMetadata(_sampleInput);
            Assert.Equal(138, result);
        }

        [Fact]
        public void Part1()
        {
            var result = GetSumOfMetadata(_input);
            _output.WriteLine("{0}", result);
        }

        [Fact]
        public void Part2Sample()
        {
            var result = GetRootNodeValue(_sampleInput);
            Assert.Equal(66, result);
        }

        [Fact]
        public void Part2()
        {
            var result = GetRootNodeValue(_input);
            _output.WriteLine("{0}", result);
        }

        private int GetSumOfMetadata(string input)
        {
            var tree = ParseTree(input);
            var stack = new Stack<Node>();
            stack.Push(tree);
            int sum = 0;

            while (stack.TryPop(out Node node))
            {
                sum += node.Metadata.Sum();
                foreach (var child in node.Children)
                {
                    stack.Push(child);
                }
            }

            return sum;
        }

        private int GetRootNodeValue(string input)
        {
            var tree = ParseTree(input);

            var result = GetNodeValue(tree);
            return result;
        }

        private int GetNodeValue(Node node)
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
                foreach(var m in node.Metadata)
                {
                    if (m > 0 && m <= childrenValues.Length)
                    {
                        sum += childrenValues[m - 1];
                    }
                }

                return sum;
            }
        }

        private Node ParseTree(string input)
        {
            using (var enumerator = GetIntStream(input).GetEnumerator())
            {
                var result = ParseTree(enumerator);
                if (enumerator.MoveNext()) throw new Exception();
                return result;
            }
        }

        private Node ParseTree(IEnumerator<int> enumerator)
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

        private IEnumerable<int> GetIntStream(string input)
        {
            var results = _regex.Matches(input);
            foreach (Match result in results)
            {
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

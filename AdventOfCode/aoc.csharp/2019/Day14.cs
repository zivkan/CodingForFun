using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace aoc.csharp._2019
{
    public class Day14 : ISolver
    {
        public (string Part1, string Part2) GetSolution(TextReader input)
        {
            return GetAnswer(input);
        }

        public static (string Part1, string Part2) GetAnswer(TextReader input)
        {
            var reactions = ParseInput(input);

            var part1 = GetMinimumOreForFuel(reactions, 1);
            var part2 = GetMaxFuelQuantity(reactions, 1000000000000);

            return (part1.ToString(), part2.ToString());
        }

        private static long GetMaxFuelQuantity(List<Node> reactions, long oreQuantity)
        {
            long min = 0;
            long max = oreQuantity;

            while (max - min > 1)
            {
                long middle = (max - min) / 2 + min;
                var ore = GetMinimumOreForFuel(reactions, middle);
                var result = ore.CompareTo(oreQuantity);
                if (result == 0)
                {
                    return middle;
                }
                else if (result < 0)
                {
                    min = middle;
                }
                else
                {
                    max = middle;
                }
            }

            return min;
        }

        private static List<Node> Clone(List<Node> nodes)
        {
            var clone = new List<Node>();
            foreach (var node in nodes)
            {
                var newNode = new Node(node.Chemical, node.BatchSize, node.Ingredients);
                clone.Add(newNode);
            }

            return clone;
        }

        private static long GetMinimumOreForFuel(List<Node> nodes, long fuelQuantity)
        {
            var clone = Clone(nodes);

            var chemLab = new ChemLab();
            foreach (var node in clone)
            {
                node.SetChemLab(chemLab);
                chemLab.Add(node);
            }

            var fuel = clone.Single(n => n.Chemical == "FUEL");
            fuel.Get(fuelQuantity);

            var ore = clone.Single(n => n.Chemical == "ORE");

            return ore.Requested;
        }

        private static List<Node> ParseInput(TextReader input)
        {
            var result = new List<Node>();

            string? line;
            while ((line = input.ReadLine()) != null)
            {
                static int ReadInt(string line, ref int pos)
                {
                    int start = pos;
                    int len = 0;
                    while (pos < line.Length && line[pos] >= '0' && line[pos] <= '9')
                    {
                        pos++;
                        len++;
                    }

                    return int.Parse(line.AsSpan(start, len));
                }

                static string ReadName(string line, ref int pos)
                {
                    int start = pos;
                    int len = 0;
                    while (pos < line.Length && line[pos] >= 'A' && line[pos] <= 'Z')
                    {
                        pos++;
                        len++;
                    }

                    return line.Substring(start, len);
                }

                var materials = new List<(string, long)>();
                int pos = 0;
                int qty;
                string material;

                while (true)
                {
                    qty = ReadInt(line, ref pos);
                    if (line[pos] != ' ') throw new Exception();
                    pos++;
                    material = ReadName(line, ref pos);

                    materials.Add((material, qty));

                    if (MemoryExtensions.Equals(line.AsSpan(pos, 4), " => ", StringComparison.Ordinal))
                    {
                        pos += 4;
                        break;
                    }
                    else if (MemoryExtensions.Equals(line.AsSpan(pos, 2), ", ", StringComparison.Ordinal))
                    {
                        pos += 2;
                    }
                    else
                    {
                        throw new Exception();
                    }
                }

                qty = ReadInt(line, ref pos);
                if (line[pos] != ' ') throw new Exception();
                pos++;
                material = ReadName(line, ref pos);

                if (pos != line.Length) throw new Exception();

                result.Add(new Node(material, qty, materials));
            }

            result.Add(new Node("ORE", 1, new List<(string, long)>(0)));

            return result;
        }

        private interface INode
        {
            string Chemical { get; }
            long Requested { get; }
            long Created { get; }

            void Get(long quantity);
        }

        private interface IChemLab
        {
            void Get(string chemical, long quantity);
        }

        private class Node : INode
        {
            private IChemLab? chemLab;

            public Node(string name, long createQuantity, IReadOnlyList<(string chemical, long quantity)> ingredients)
            {
                Chemical = name;
                BatchSize = createQuantity;
                Ingredients = ingredients;

                this.chemLab = null;
                Requested = 0;
                Created = 0;
            }

            public string Chemical { get; }

            public long Requested { get; private set; }

            public long Created { get; private set; }

            public long BatchSize { get; }

            public IReadOnlyList<(string chemical, long quantity)> Ingredients { get; }

            public void Get(long quantity)
            {
                var spare = Created - Requested;
                if (spare < quantity)
                {
                    var need = quantity - spare;
                    var batches = (need + BatchSize - 1) / BatchSize;

                    foreach (var tuple in Ingredients)
                    {
                        if (chemLab == null)
                        {
                            throw new InvalidOperationException();
                        }

                        chemLab.Get(tuple.chemical, batches * tuple.quantity);
                    }

                    Created += batches * BatchSize;
                }

                Requested += quantity;
            }

            public void SetChemLab(IChemLab chemLab)
            {
                this.chemLab = chemLab;
            }
        }

        private class ChemLab : IChemLab
        {
            private readonly Dictionary<string, INode> nodes;

            public ChemLab()
            {
                nodes = new Dictionary<string, INode>();
            }

            public void Get(string chemical, long quantity)
            {
                nodes[chemical].Get(quantity);
            }

            public void Add(INode node)
            {
                nodes.Add(node.Chemical, node);
            }
        }
    }
}

using AdventOfCode.Common;
using Microsoft.Diagnostics.Runtime.Utilities;

namespace AdventOfCode.Puzzles._2022.Day12
{
    internal sealed partial class Solution : ISolution
    {
        private int _result;

        public void PartOne(string[] input)
        {
            var grid = new Grid(input);
            grid.Traverse(grid.End!, 0);
            _result = grid.Start!.DistanceFromEnd;
        }

        public void PartTwo(string[] input)
        {
            _result = int.MaxValue;
            for (var i = 0; i < input.Length; i++)
            {
                if (input[i].Contains('S'))
                {
                    input[i] = input[i].Replace('S', 'a');
                    break;
                }
            }

            for (var i = 0; i < input.Length; i++)
            {
                for (var j = 0; j < input[i].Length; j++)
                {
                    if (input[i][j] == 'a')
                    {
                        var newArray = new string[input.Length];
                        Array.Copy(input, newArray, input.Length);
                        var cArray = newArray[i].ToCharArray();
                        cArray[j] = 'S';
                        newArray[i] = new string(cArray);

                        var grid = new Grid(newArray);
                        grid.Traverse(grid.End!, 0);

                        if (grid.Start!.DistanceFromEnd < _result)
                        {
                            _result = grid.Start!.DistanceFromEnd;
                        }
                    }
                }
            }
        }

        private sealed class Grid
        {
            public Dictionary<(int, int), Node> Nodes { get; private set; }
            public Node? Start { get; set; }
            public Node? End { get; private set; }

            private int _height;
            private int _width;

            public Queue<Node> LowestElevation { get; private set; }

            public Grid(string[] input) 
            {
                Nodes = new Dictionary<(int, int), Node>();
                _height = input.Length;
                _width = input[0].Length;
                BuildGrid(input);
                LowestElevation = new Queue<Node>(Nodes.Select(kvp => kvp.Value).Where(x => x.CharElevation == 'a'));
            }

            public void BuildGrid(string[] input)
            {
                for (var y = 0; y < input.Length; y++)
                {
                    for (var x = 0; x < input[y].Length; x++)
                    {
                        var node = new Node(input[y][x]);
                        Nodes.Add((x, y), node);

                        if (input[y][x] == 'S')
                        {
                            Start = node;
                            node.IsStart = true;
                        }
                        else if (input[y][x] == 'E')
                        {
                            End = node;
                            node.IsEnd = true;
                        }
                    }
                }

                foreach (var kvp in Nodes)
                {
                    var x = kvp.Key.Item1;
                    var y = kvp.Key.Item2;
                    var node = kvp.Value;

                    if (x - 1 >= 0) node.AddNode(Nodes[(x - 1, y)]);
                    if (x + 1 < _width) node.AddNode(Nodes[(x + 1, y)]);
                    if (y - 1 >= 0) node.AddNode(Nodes[(x, y - 1)]);
                    if (y + 1 < _height) node.AddNode(Nodes[(x, y + 1)]);
                }
            }

            public void Reset()
            {
                foreach (var node in Nodes)
                {
                    node.Value.DistanceFromEnd = int.MaxValue;
                    node.Value.IsStart = false;
                }
            }

            public void Traverse(Node node, int curr)
            {
                node.DistanceFromEnd = curr;

                foreach (var parent in node.Parents)
                {
                    if (parent.DistanceFromEnd > curr + 1)
                        Traverse(parent, curr + 1);
                }
            }
        }

        private sealed class Node
        {
            
            public char CharElevation { get; private set; }
            public int Elevation { get; private set; }
            public List<Node> Children { get; private set; }
            public List<Node> Parents { get; private set; }

            public bool IsStart { get; set; }
            public bool IsEnd { get; set; }

            public int DistanceFromEnd = int.MaxValue;

            public Node(char elevation)
            {
                CharElevation = elevation;
                Elevation = elevation == 'S' ? 'a' : elevation == 'E' ? 'z' : elevation;
                Children = new List<Node>();
                Parents = new List<Node>();
            }

            public void AddNode (Node node)
            {
                if (!IsEnd)
                    AddChild(node);

                if (!IsStart && !node.IsEnd)
                    AddParent(node);
            }

            private void AddChild (Node node)
            {
                if (node.Elevation <= Elevation + 1)
                {
                    Children.Add(node);
                }
            }

            private void AddParent(Node node)
            {
                if (node.Elevation >= Elevation - 1)
                {
                    Parents.Add(node);
                }
            }
        }
    }
}

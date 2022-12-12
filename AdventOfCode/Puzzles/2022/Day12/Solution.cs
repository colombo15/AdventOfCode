using AdventOfCode.Common;
using Microsoft.Diagnostics.Runtime.Utilities;

namespace AdventOfCode.Puzzles._2022.Day12
{
    internal sealed partial class Solution : ISolution
    {
        private int _result;

        public void PartOne(string[] input)
        {
            var graph = new Graph(input);
            var end = graph.BreathSearchFirst('S');
            _result = end.CalculatePath();
        }

        public void PartTwo(string[] input)
        {
            var graph = new Graph(input);
            var end = graph.BreathSearchFirst('a');
            _result = end.CalculatePath();
        }

        private sealed class Graph
        {
            public Vertex? Root { get; set; }

            public Graph(string[] input)
            {
                var vertices = new Dictionary<(int, int), Vertex>();

                for (var y = 0; y < input.Length; y++)
                {
                    for (var x = 0; x < input[y].Length; x++)
                    {
                        var elevation = input[y][x];
                        var coord = (x, y);

                        if (elevation == 'E')
                        {
                            Root = new Vertex('z');
                            vertices.Add(coord, Root);
                            continue;
                        }

                        vertices.Add(coord, new Vertex(elevation));
                    }
                }

                foreach (var kvp in vertices)
                {
                    var x = kvp.Key.Item1;
                    var y = kvp.Key.Item2;
                    var vertex = kvp.Value;

                    if (x - 1 >= 0) vertex.AddEdge(vertices[(x - 1, y)]);
                    if (x + 1 < input[y].Length) vertex.AddEdge(vertices[(x + 1, y)]);
                    if (y - 1 >= 0) vertex.AddEdge(vertices[(x, y - 1)]);
                    if (y + 1 < input.Length) vertex.AddEdge(vertices[(x, y + 1)]);
                }
            }

            public Vertex BreathSearchFirst(char goal)
            {
                Vertex retval;
                var q = new Queue<Vertex>();
                var root = Root!;
                root.Explored = true;

                q.Enqueue(root);

                while (q.Count > 0)
                {
                    retval = q.Dequeue();

                    if (retval.Elevation == goal)
                    {
                        return retval;
                    }
                    
                    foreach (var edge in retval.Edges)
                    {
                        if (!edge.Explored)
                        {
                            edge.Explored = true;
                            edge.Parent = retval;
                            q.Enqueue(edge);
                        }
                    }
                }

                return root;
            }

            public sealed class Vertex
            {
                public int Elevation { get; private set; }
                public List<Vertex> Edges { get; private set; }
                public Vertex? Parent { get; set; }
                public bool Explored { get; set; }

                public Vertex(char elevation)
                {
                    Elevation = elevation;
                    Edges = new List<Vertex>();
                }

                public int CalculatePath()
                {
                    if (Parent == null)
                        return 0;
                    return Parent.CalculatePath() + 1;
                }

                public void AddEdge(Vertex v)
                {
                    if (v.Elevation >= Elevation - 1 || v.Elevation == 83)
                        Edges.Add(v);
                }
            }
        }
    }
}

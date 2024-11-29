using System.Collections.Generic;

namespace AdventOfCode._2023.Day17;

public class Solution : ISolution
{
    public void Puzzle1(string[] input)
    {
        //var sorted = new SortedSet<SuperNode>();
        var dist = new Dictionary<Node, int>();
        var prev = new Dictionary<Node, Node>();
        var Q = new HashSet<Node>();
        var sorted = new SortedSet<SuperNode>();

        for (var y = 0; y < input.Length; y++)
        {
            for (var x = 0; x < input[y].Length; x++)
            {
                if (x == 0 && y == 0)
                {
                    var node = new Node()
                    {
                        x = x, 
                        y = y, 
                        c = 'N', 
                        count = 0
                    };
                    dist.Add(node, 0);
                    Q.Add(node);
                    sorted.Add(new SuperNode(0, node));
                    //sorted.Add(new SuperNode(0, node));
                }
                else
                {
                    var node = new Node()
                    {
                        x = x,
                        y = y,
                        c = 'D',
                        count = 1
                    };

                    dist.Add(node with { count = 1 }, 2000000000);
                    dist.Add(node with { count = 2 }, 2000000000);
                    dist.Add(node with { count = 3 }, 2000000000);
                    sorted.Add(new SuperNode(2000000000, node with { count = 1 }));
                    sorted.Add(new SuperNode(2000000000, node with { count = 2 }));
                    sorted.Add(new SuperNode(2000000000, node with { count = 3 }));
                    Q.Add(node with { count = 1 });
                    Q.Add(node with { count = 2 });
                    Q.Add(node with { count = 3 });

                    node.c = 'U';
                    dist.Add(node with { count = 1 }, 2000000000);
                    dist.Add(node with { count = 2 }, 2000000000);
                    dist.Add(node with { count = 3 }, 2000000000);
                    sorted.Add(new SuperNode(2000000000, node with { count = 1 }));
                    sorted.Add(new SuperNode(2000000000, node with { count = 2 }));
                    sorted.Add(new SuperNode(2000000000, node with { count = 3 }));
                    Q.Add(node with { count = 1 });
                    Q.Add(node with { count = 2 });
                    Q.Add(node with { count = 3 });

                    node.c = 'L';
                    dist.Add(node with { count = 1 }, 2000000000);
                    dist.Add(node with { count = 2 }, 2000000000);
                    dist.Add(node with { count = 3 }, 2000000000);
                    sorted.Add(new SuperNode(2000000000, node with { count = 1 }));
                    sorted.Add(new SuperNode(2000000000, node with { count = 2 }));
                    sorted.Add(new SuperNode(2000000000, node with { count = 3 }));
                    Q.Add(node with { count = 1 });
                    Q.Add(node with { count = 2 });
                    Q.Add(node with { count = 3 });

                    node.c = 'R';
                    dist.Add(node with { count = 1 }, 2000000000);
                    dist.Add(node with { count = 2 }, 2000000000);
                    dist.Add(node with { count = 3 }, 2000000000);
                    sorted.Add(new SuperNode(2000000000, node with { count = 1 }));
                    sorted.Add(new SuperNode(2000000000, node with { count = 2 }));
                    sorted.Add(new SuperNode(2000000000, node with { count = 3 }));
                    Q.Add(node with { count = 1 });
                    Q.Add(node with { count = 2 });
                    Q.Add(node with { count = 3 });
                }
            }
        }

        while (Q.Count > 0)
        {
            var u = sorted.First().node;
            var neighbors = GetNeighbors(u, prev, input[0].Length, input.Length);
            Q.Remove(u);
            sorted.Remove(sorted.First());

            foreach (var v in neighbors)
            {
                if (dist.TryGetValue(v, out int value))
                {
                    var alt = dist[u] + (input[v.y][v.x] - 48);
                    if (alt < value)
                    {
                        //sorted.Add(new SuperNode(alt, v));
                        dist[v] = alt;
                        sorted.Add(new SuperNode(alt, v));
                        if (!prev.TryAdd(v, u))
                        {
                            prev[v] = u;
                        }
                    }
                }
            }
        }

        Console.WriteLine(dist.Where(x => x.Key.x == input[0].Length - 1 && x.Key.y == input.Length - 1).OrderBy(x => x.Value).First().Value);

        static List<Node> GetNeighbors(Node node, Dictionary<Node, Node> prev, int maxWidth, int maxHeight) 
        {
            if (node.c == 'N')
            {
                return new List<Node> 
                { 
                    new() { x = 1, y = 0, c = 'L', count = 1 },
                    new() { x = 0, y = 1, c = 'U', count = 1 }
                };
            }

            var retval = new List<Node>();
            var upNode = new Node() { x = node.x, y = node.y - 1, c = 'D', count = node.c == 'D' ? node.count + 1 : 1 };
            var downNode = new Node() { x = node.x, y = node.y + 1, c = 'U', count = node.c == 'U' ? node.count + 1 : 1 };
            var leftNode = new Node() { x = node.x - 1, y = node.y, c = 'R', count = node.c == 'R' ? node.count + 1 : 1 };
            var rightNode = new Node() { x = node.x + 1, y = node.y, c = 'L', count = node.c == 'L' ? node.count + 1 : 1 };

            if (!(prev.ContainsKey(node) && upNode.x == prev[node].x && upNode.y == prev[node].y))
            {
                retval.Add(upNode);
            }
            if (!(prev.ContainsKey(node) && downNode.x == prev[node].x && downNode.y == prev[node].y))
            {
                retval.Add(downNode);
            }
            if (!(prev.ContainsKey(node) && leftNode.x == prev[node].x && leftNode.y == prev[node].y))
            {
                retval.Add(leftNode);
            }
            if (!(prev.ContainsKey(node) && rightNode.x == prev[node].x && rightNode.y == prev[node].y))
            {
                retval.Add(rightNode);
            }

            return retval;
        }
    }

	public void Puzzle2(string[] input)
	{
		Console.WriteLine("2023 - Day 17 - Part 2");
	}

    public record class SuperNode : IComparable
    {
        public SuperNode(int val, Node n)
        {
            value = val;
            node = n;
        }

        public int value;
        public Node node;

        public int CompareTo(object? obj)
        {
            if (obj == null) return 1;

            var otherSuperNode = obj as SuperNode;

            if (otherSuperNode != null)
            {
                var intComp = value.CompareTo(otherSuperNode.value);
                if (intComp != 0) return intComp;

                intComp = node.x.CompareTo(otherSuperNode.node.x);
                if (intComp != 0) return intComp;

                intComp = node.y.CompareTo(otherSuperNode.node.y);
                if (intComp != 0) return intComp;

                intComp = node.c.CompareTo(otherSuperNode.node.c);
                if (intComp != 0) return intComp;

                intComp = node.count.CompareTo(otherSuperNode.node.count);
                return intComp;
            }
            else
                throw new ArgumentException("Object is not a SupeNode");
        }
    }

    public record class Node
    {
        public int x;
        public int y;
        public char c;
        public int count;
    }
}

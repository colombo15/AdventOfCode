namespace AdventOfCode._2023.Day17;

public class Solution : ISolution
{
    public void Puzzle1(string[] input)
    {
        Console.WriteLine(new Graph(input).FindSmallestHeatLoss());
    }

    public void Puzzle2(string[] input)
	{
        Console.WriteLine(new Graph(input).FindSmallestHeatLoss2());
    }

    public sealed class Graph
    {
        private readonly Vertex[,] _graph;
        private readonly Vertex _root;
        private readonly Vertex _goal;

        public Graph(string[] input)
        {
            _graph = new Vertex[input[0].Length, input.Length];

            for (var i = 0; i < input[0].Length; i++)
            {
                for(var j = 0; j < input.Length; j++)
                {
                    _graph[i, j] = new Vertex(input[j][i] - 48);
                }
            }

            for (int i = 0; i < _graph.GetLength(0); i++)
            {
                for (int j = 0; j < _graph.GetLength(1); j++)
                {
                    if (i + 1 < _graph.GetLength(0))
                    {
                        _graph[i, j].AddNeighbor(_graph[i + 1, j], Direction.Right);
                    }
                    if (j + 1 < _graph.GetLength(1))
                    {
                        _graph[i, j].AddNeighbor(_graph[i, j + 1], Direction.Down);
                    }
                    if (i - 1 >= 0)
                    {
                        _graph[i, j].AddNeighbor(_graph[i - 1, j], Direction.Left);
                    }
                    if (j - 1 >= 0)
                    {
                        _graph[i, j].AddNeighbor(_graph[i, j - 1], Direction.Up);
                    }
                }
            }

            _root = _graph[0, 0];
            _goal = _graph[_graph.GetLength(0) - 1, _graph.GetLength(1) - 1];
        }

        public int FindSmallestHeatLoss()
        {
            var cache = new HashSet<(Vertex vertex, Direction from, int count)>();
            var priorityQueue = new PriorityQueue<(Vertex vertex, Direction from, int count), int>();
            _root.Estimate = 0;
            priorityQueue.Enqueue((_root, Direction.Left, 0), _root.Estimate);
            while (priorityQueue.TryDequeue(out var tuple, out var estimate))
            {
                var (ptr, fromDirection, currCount) = tuple;
                foreach (var (neighbor, toDirection) in ptr.Neighbors)
                {
                    if (fromDirection == toDirection) continue;
                    var nextCount = Opposite(fromDirection) == toDirection ? currCount + 1 : 1;
                    if (nextCount > 3) continue;
                    var nextEstimate = estimate + neighbor.Weight;
                    var cacheCheck = (neighbor, Opposite(toDirection), nextCount);
                    if (cache.Contains(cacheCheck)) continue;
                    cache.Add(cacheCheck);
                    neighbor.SetEstimate(nextEstimate);
                    priorityQueue.Enqueue((neighbor, Opposite(toDirection), nextCount), nextEstimate);
                }
            }
            return _goal.Estimate;
        }

        public int FindSmallestHeatLoss2()
        {
            var cache = new HashSet<(Vertex vertex, Direction from, int count)>();
            var priorityQueue = new PriorityQueue<(Vertex vertex, Direction from, int count), int>();
            _root.Estimate = 0;
            priorityQueue.Enqueue((_root, Direction.Left, 0), _root.Estimate);
            while (priorityQueue.TryDequeue(out var tuple, out var estimate))
            {
                var (ptr, fromDirection, currCount) = tuple;
                foreach (var (n, toDirection) in ptr.Neighbors)
                {
                    if (fromDirection == toDirection) continue;
                    var nextCount = Opposite(fromDirection) == toDirection ? currCount + 1 : 1;
                    if (nextCount > 10) continue;
                    var neighbor = n;
                    var heat = 0;
                    var cancel = false;
                    if (nextCount == 1)
                    {
                        for (var i = 0; i < 3; i++)
                        {
                            heat += neighbor.Weight;
                            var tempN = neighbor.GetNeighbor(toDirection);
                            if (tempN!.Value.vertex is null)
                            {
                                cancel = true;
                                break;
                            }
                            neighbor = tempN.Value.vertex;
                        }
                        nextCount = 4;
                    }
                    if (cancel) continue;
                    var nextEstimate = estimate + neighbor.Weight + heat;
                    var cacheCheck = (neighbor, Opposite(toDirection), nextCount);
                    if (cache.Contains(cacheCheck)) continue;
                    cache.Add(cacheCheck);
                    neighbor.SetEstimate(nextEstimate);
                    priorityQueue.Enqueue((neighbor, Opposite(toDirection), nextCount), nextEstimate);
                }
            }
            return _goal.Estimate;
        }

        private sealed class Vertex(int weight)
        {
            public int Weight { get; private set; } = weight;
            public int Estimate { get; set; } = int.MaxValue;
            public bool Explored { get; set; } = false;
            public List<(Vertex vertex, Direction direction)> Neighbors { get; private set; } = [];
            public void AddNeighbor(Vertex v, Direction d) => Neighbors.Add((v, d));
            public void SetEstimate(int val) => Estimate = val < Estimate ? val : Estimate;
            public (Vertex vertex, Direction direction)? GetNeighbor(Direction direction) => Neighbors.FirstOrDefault(x => x.direction == direction);
        }

        private enum Direction
        {
            Left,
            Right,
            Up,
            Down
        }

        private static Direction Opposite(Direction d)
        {
            return d switch
            {
                Direction.Up => Direction.Down,
                Direction.Down => Direction.Up,
                Direction.Left => Direction.Right,
                Direction.Right => Direction.Left,
                _ => throw new NotImplementedException()
            };
        }
    }
}

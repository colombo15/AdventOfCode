namespace AdventOfCode._2023.Day17;

public class Solution : ISolution
{
    public void Puzzle1(string[] input)
    {
        Console.WriteLine(new Graph(input).FindSmallestHeatLoss());
    }

	public void Puzzle2(string[] input)
	{
		Console.WriteLine("2023 - Day 17 - Part 2");
	}

    public sealed class Graph
    {
        private readonly Vertex[,] _graph;
        private readonly Vertex _root;
        private readonly Vertex _goal;

        public Graph(string[] input)
        {
            _graph = new Vertex[input[0].Length, input.Length];

            for (var i = 0; i < input.Length; i++)
            {
                for(var j = 0; j < input[i].Length; j++)
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
            var priorityQueue = new PriorityQueue<(Vertex vertex, Direction from, int count), int>();
            _root.Estimate = 0;
            priorityQueue.Enqueue((_root, Direction.Down, 0), _root.Estimate);
            while(priorityQueue.TryDequeue(out var tuple, out var priority))
            {
                var (ptr, fromDirection, currCount) = tuple;
                if (ptr.Explored) 
                    continue;
                ptr.Explored = true;
                foreach (var (neighbor, toDirection) in ptr.Neighbors)
                {
                    var nextCount = Opposite(fromDirection) == toDirection ? currCount + 1 : 1;
                    if (neighbor.Explored || nextCount > 3) continue;
                    neighbor.SetEstimate(ptr.Estimate + neighbor.Weight);
                    priorityQueue.Enqueue((neighbor, Opposite(toDirection), nextCount), neighbor.Estimate);
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

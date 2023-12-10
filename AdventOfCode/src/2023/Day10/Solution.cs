using System.Linq;
using static AdventOfCode._2023.Day08.Solution;

namespace AdventOfCode._2023.Day10;

public class Solution : ISolution
{
	public void Puzzle1(string[] input)
	{
		var parsedInput = ParseInput(input);
		var nodes = parsedInput.Item1;
		var start = parsedInput.Item2;

        nodes[start].SetSteps(0);
        var currentNodes = new List<(Direction, Node)>();

		var coord = GetNextCoord(start, Direction.North);
        if (CanGo(Direction.North, coord, nodes))
			currentNodes.Add((Direction.North, nodes[coord]));

        coord = GetNextCoord(start, Direction.East);
        if (CanGo(Direction.East, coord, nodes))
            currentNodes.Add((Direction.East, nodes[coord]));

        coord = GetNextCoord(start, Direction.South);
        if (CanGo(Direction.South, coord, nodes))
            currentNodes.Add((Direction.South, nodes[coord]));

        coord = GetNextCoord(start, Direction.West);
        if (CanGo(Direction.West, coord, nodes))
            currentNodes.Add((Direction.West, nodes[coord]));

        var steps = 0;

		while (currentNodes.Any())
		{
			var tempArr = new List<(Direction, Node)>(currentNodes);
			currentNodes.RemoveAll(x => true);

			foreach (var node in tempArr)
			{
				node.Item2.SetSteps(steps);
                var nextDirection = GetNextDirection(node.Item2, node.Item1);

                var nextNode = nodes[GetNextCoord(node.Item2.Coord, nextDirection)];
				if (nextNode.Steps == -1)
				{
                    currentNodes.Add((nextDirection, nextNode));
                }
            }

			steps++;
		}

		Console.WriteLine(nodes.Values.Where(x => x.Steps != -1).Count() / 2);
    }

    public void Puzzle2(string[] input)
	{
        var parsedInput = ParseInput(input);
        var nodes = parsedInput.Item1;
        var start = parsedInput.Item2;
        nodes[start].SetSteps(0);
        var startDir = new List<Direction>();
        var currentNodes = new List<(Direction, Node)>();

        var coord = GetNextCoord(start, Direction.North);
        if (CanGo(Direction.North, coord, nodes))
        {
            currentNodes.Add((Direction.North, nodes[coord]));
            startDir.Add(Direction.North);
        }

        coord = GetNextCoord(start, Direction.East);
        if (CanGo(Direction.East, coord, nodes))
        {
            currentNodes.Add((Direction.East, nodes[coord]));
            startDir.Add(Direction.East);
        }

        coord = GetNextCoord(start, Direction.South);
        if (CanGo(Direction.South, coord, nodes))
        {
            currentNodes.Add((Direction.South, nodes[coord]));
            startDir.Add(Direction.South);
        }

        coord = GetNextCoord(start, Direction.West);
        if (CanGo(Direction.West, coord, nodes))
        {
            currentNodes.Add((Direction.West, nodes[coord]));
            startDir.Add(Direction.West);
        }
        nodes[start].Directions = startDir.ToArray();

        var steps = 0;

        while (currentNodes.Any())
        {
            var tempArr = new List<(Direction, Node)>(currentNodes);
            currentNodes.RemoveAll(x => true);

            foreach (var node in tempArr)
            {
                node.Item2.SetSteps(steps);
                var nextDirection = GetNextDirection(node.Item2, node.Item1);

                var nextNode = nodes[GetNextCoord(node.Item2.Coord, nextDirection)];
                if (nextNode.Steps == -1)
                {
                    currentNodes.Add((nextDirection, nextNode));
                }
            }

            steps++;
        }

        var blowup = BlowUpGrid(nodes);
        var grid = blowup.Item1;
        var emptyNodes = blowup.Item2;
        var total = 0;
        foreach (var node in emptyNodes)
        {
            if (IsInLoop(node, grid))
            {
                total++;
            }
        }

        Console.WriteLine(total);

        static bool IsInLoop((int, int) node, Dictionary<(int, int), char> grid)
        {
            var explored = new HashSet<(int, int)>();
            var queue = new Queue<(int, int)>();

            explored.Add(node);
            queue.Enqueue(node);

            while (queue.Count > 0)
            {
                var v = queue.Dequeue();

                var vNorth = GetNextCoord(v, Direction.North);
                var vEast = GetNextCoord(v, Direction.East);
                var vSouth = GetNextCoord(v, Direction.South);
                var vWest = GetNextCoord(v, Direction.West);
                if (!grid.ContainsKey(vNorth) || !grid.ContainsKey(vEast) || !grid.ContainsKey(vSouth) || !grid.ContainsKey(vWest))
                {
                    return false;
                }
                if (grid[vNorth] == ' ' && !explored.Contains(vNorth))
                {
                    explored.Add(vNorth);
                    queue.Enqueue(vNorth);
                }
                if (grid[vEast] == ' ' && !explored.Contains(vEast))
                {
                    explored.Add(vEast);
                    queue.Enqueue(vEast);
                }
                if (grid[vSouth] == ' ' && !explored.Contains(vSouth))
                {
                    explored.Add(vSouth);
                    queue.Enqueue(vSouth);
                }
                if (grid[vWest] == ' ' && !explored.Contains(vWest))
                {
                    explored.Add(vWest);
                    queue.Enqueue(vWest);
                }
            }
            return true;
        }
    }

    private static (Dictionary<(int, int), char>, List<(int, int)>) BlowUpGrid(Dictionary<(int, int), Node> nodes)
    {
        var retval = new Dictionary<(int, int), char>();
        var empty = new List<(int, int)>();

        foreach (var node in nodes.Values)
        {
            var x = 1 + (node.Coord.Item1 * 3);
            var y = 1 + (node.Coord.Item2 * 3);

            if (node.Symbol == '.' || node.Steps == -1)
            {
                retval.Add((x - 1, y - 1)   , ' ');
                retval.Add((x, y - 1)       , ' ');
                retval.Add((x + 1, y - 1)   , ' ');
                retval.Add((x - 1, y)       , ' ');
                retval.Add((x, y)           , ' ');
                retval.Add((x + 1, y)       , ' ');
                retval.Add((x - 1, y + 1)   , ' ');
                retval.Add((x, y + 1)       , ' ');
                retval.Add((x + 1, y + 1)   , ' ');

                empty.Add((x, y));
            }
            else
            {
                retval.Add((x - 1, y - 1), ' ');
                retval.Add((x, y - 1), node.Directions.Contains(Direction.North) ? '|' : ' ');
                retval.Add((x + 1, y - 1), ' ');
                retval.Add((x - 1, y), node.Directions.Contains(Direction.West) ? '-' : ' ');
                retval.Add((x, y), node.Symbol);
                retval.Add((x + 1, y), node.Directions.Contains(Direction.East) ? '-' : ' ');
                retval.Add((x - 1, y + 1), ' ');
                retval.Add((x, y + 1), node.Directions.Contains(Direction.South) ? '|' : ' ');
                retval.Add((x + 1, y + 1), ' ');
            }
        }

        return (retval, empty);
    }

    private static Direction GetNextDirection(Node node,  Direction fromDirection)
	{
		return node.Directions.Where(x => x != GetOppositeDirection(fromDirection)).FirstOrDefault();
	}

    private static bool CanGo(Direction fromDirection, (int, int) targetPos, Dictionary<(int, int), Node> nodes)
	{
		if (!nodes.ContainsKey(targetPos)) return false;
        return nodes[targetPos].Directions.Contains(GetOppositeDirection(fromDirection));
    }

	private static (int, int) GetNextCoord((int, int) coord, Direction direction)
	{
        return direction switch
        {
            Direction.North => (coord.Item1, coord.Item2 - 1),
            Direction.West => (coord.Item1 - 1, coord.Item2),
            Direction.East => (coord.Item1 + 1, coord.Item2),
            Direction.South => (coord.Item1, coord.Item2 + 1),
            _ => throw new NotImplementedException()
        };
    }

	private static Direction GetOppositeDirection(Direction direction)
	{
		return direction switch
        {
            Direction.North => Direction.South,
            Direction.West => Direction.East,
            Direction.East => Direction.West,
            Direction.South => Direction.North,
            _ => throw new NotImplementedException()
        };
    }

	private static (Dictionary<(int, int), Node>, (int, int)) ParseInput(string[] input)
	{
		var retval = new Dictionary<(int, int), Node>();
		var startX = -1;
		var startY = -1;

		for (var i = 0; i < input.Length; i++)
		{
            for (var j = 0; j < input[i].Length; j++)
            {
                retval.Add((j, i), new Node(input[i][j], (j, i)));
                if (input[i][j] == 'S')
				{
					startX = j;
					startY = i;
                }
			}
		}

		return (retval, (startX, startY));
	}

    private record class Node
	{
		public char Symbol { get; private set; }
		public (int, int) Coord { get; private set; }
		public Direction[] Directions { get; set; }
		public int Steps { get; private set; }
        public bool? IsInLoop { get; set; }

        public Node(char symbol, (int, int) coord) 
		{
			Symbol = symbol;
			Coord = coord;
			Directions = new Direction[2];
			Steps = -1;
            IsInLoop = null;

            switch (Symbol)
			{
				case '|':
					Directions[0] = Direction.North;
					Directions[1] = Direction.South;
					break;
                case '-':
                    Directions[0] = Direction.East;
                    Directions[1] = Direction.West;
                    break;
				case 'F':
                    Directions[0] = Direction.East;
                    Directions[1] = Direction.South;
                    break;
				case 'J':
                    Directions[0] = Direction.North;
                    Directions[1] = Direction.West;
                    break;
				case 'L':
                    Directions[0] = Direction.North;
                    Directions[1] = Direction.East;
                    break;
				case '7':
                    Directions[0] = Direction.West;
                    Directions[1] = Direction.South;
                    break;
				default:
					break;
            }
        }

		public void SetSteps(int steps) 
		{ 
			if (Steps == -1)
				Steps = steps;
		}
    }

	private enum Direction
	{
		East,
		West,
		North,
		South
	}
}

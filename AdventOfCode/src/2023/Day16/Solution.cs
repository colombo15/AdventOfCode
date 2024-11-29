namespace AdventOfCode._2023.Day16;

public class Solution : ISolution
{
    public static HashSet<(int x, int y, Direction dir)> _cache = new();
    public static HashSet<(int x, int y)> _energized = new();

    public void Puzzle1(string[] input)
	{
		var grid = new Grid(input);
        Console.WriteLine(Run(grid, (0, 0), Direction.West));
	}

	public void Puzzle2(string[] input)
	{
        var grid = new Grid(input);
        var highest = 0;

        // West
        for (var i = 0; i < input.Length; i++)
        {
            var val = Run(grid, (0, i), Direction.West);
            if (val > highest)
            { 
                highest = val;
            }
        }
        // North
        for (var i = 0; i < input[0].Length; i++)
        {
            var val = Run(grid, (i, 0), Direction.North);
            if (val > highest)
            {
                highest = val;
            }
        }
        // East
        for (var i = 0; i < input.Length; i++)
        {
            var val = Run(grid, (input[0].Length - 1, i), Direction.East);
            if (val > highest)
            {
                highest = val;
            }
        }
        // South
        for (var i = 0; i < input[0].Length; i++)
        {
            var val = Run(grid, (i, input.Length - 1), Direction.South);
            if (val > highest)
            {
                highest = val;
            }
        }

        Console.WriteLine(highest);
    }

    public int Run(Grid grid, (int x, int y) startingCoord, Direction startingDirection)
    {
        _energized.Clear();
        _cache.Clear();
        var beam = grid[startingCoord].Beam(startingDirection).ToList();
        while (beam.Count > 0)
        {
            var result = beam[0].Item1?.Beam(beam[0].Item2);
            if (result != null)
            {
                foreach (var item in result)
                {
                    beam.Add(item);
                }
            }

            beam.RemoveAt(0);
        }
        return _energized.Count();
    }

	public class Grid
	{
		public Dictionary<(int x, int y), Node> Nodes = new();

        public Node this[(int x, int y) coord]
        {
            get { return Nodes[coord]; }
        }

        public Grid(string[] input)
		{
			for (var y = 0; y < input.Length; y++)
			{
				for (var x = 0; x < input[y].Length; x++)
				{
					Nodes.Add((x, y), new Node(x, y, input[y][x], this));
				}
			}
		}
	}

	public class Node
	{
		public (int x, int y) Coord;
		public char Symbol;

        public Node? North;
        public Node? South;
        public Node? East;
        public Node? West;

        public Node(int x, int y, char symbol, Grid grid)
		{
			Coord = (x, y);
			Symbol = symbol;

			if (grid.Nodes.ContainsKey((x, y - 1)))
			{
				North = grid.Nodes[(x, y - 1)];
				grid.Nodes[(x, y - 1)].South = this;
            }
            if (grid.Nodes.ContainsKey((x, y + 1)))
            {
                South = grid.Nodes[(x, y + 1)];
                grid.Nodes[(x, y + 1)].North = this;
            }
            if (grid.Nodes.ContainsKey((x - 1, y)))
            {
                West = grid.Nodes[(x - 1, y)];
                grid.Nodes[(x - 1, y)].East = this;
            }
            if (grid.Nodes.ContainsKey((x + 1, y)))
            {
                East = grid.Nodes[(x + 1, y)];
                grid.Nodes[(x + 1, y)].West = this;
            }
        }

		public (Node, Direction)[] Beam(Direction from)
		{
            if (_cache.Contains((Coord.x, Coord.y, from)))
            {
                return Array.Empty<(Node, Direction)>();
            }
            else
            {
                _cache.Add((Coord.x, Coord.y, from));
            }

            _energized.Add(Coord);

            if (Symbol == '.')
            {
                var to = GetOpposite(from);
                var nextNode = GetNextNode(to);
                var newFrom = GetOpposite(to);
                return new[] { (nextNode!, newFrom) }; 
            }
            else if (Symbol == '/')
            {
                if (from == Direction.North || from == Direction.South)
                {
                    var to = GetRight(from);
                    var nextNode = GetNextNode(to);
                    var newFrom = GetOpposite(to);
                    return new[] { (nextNode!, newFrom) };
                }
                else
                {
                    var to = GetLeft(from);
                    var nextNode = GetNextNode(to);
                    var newFrom = GetOpposite(to);
                    return new[] { (nextNode!, newFrom) };
                }
            }
            else if (Symbol == '\\')
            {
                if (from == Direction.North || from == Direction.South)
                {
                    var to = GetLeft(from);
                    var nextNode = GetNextNode(to);
                    var newFrom = GetOpposite(to);
                    return new[] { (nextNode!, newFrom) };
                }
                else
                {
                    var to = GetRight(from);
                    var nextNode = GetNextNode(to);
                    var newFrom = GetOpposite(to);
                    return new[] { (nextNode!, newFrom) };
                }
            }
            else if (Symbol == '-')
            {
                if (from == Direction.North || from == Direction.South)
                {
                    var arr = new (Node, Direction)[2];

                    var to = GetLeft(from);
                    var nextNode = GetNextNode(to);
                    var newFrom = GetOpposite(to);
                    arr[0] = (nextNode!, newFrom);

                    to = GetRight(from);
                    nextNode = GetNextNode(to);
                    newFrom = GetOpposite(to);
                    arr[1] = (nextNode!, newFrom);
                    return arr;
                }
                else
                {
                    var to = GetOpposite(from);
                    var nextNode = GetNextNode(to);
                    var newFrom = GetOpposite(to);
                    return new[] { (nextNode!, newFrom) };
                }
            }
            else if (Symbol == '|')
            {
                if (from == Direction.East || from == Direction.West)
                {
                    var arr = new (Node, Direction)[2];

                    var to = GetLeft(from);
                    var nextNode = GetNextNode(to);
                    var newFrom = GetOpposite(to);
                    arr[0] = (nextNode!, newFrom);

                    to = GetRight(from);
                    nextNode = GetNextNode(to);
                    newFrom = GetOpposite(to);
                    arr[1] = (nextNode!, newFrom);
                    return arr;
                }
                else
                {
                    var to = GetOpposite(from);
                    var nextNode = GetNextNode(to);
                    var newFrom = GetOpposite(to);
                    return new[] { (nextNode!, newFrom) };
                }
            }
            return Array.Empty<(Node, Direction)>();
        }

        public Node? GetNextNode(Direction to)
        {
            return to switch
            {
                Direction.North => North,
                Direction.South => South,
                Direction.West => West,
                Direction.East => East,
                _ => throw new NotImplementedException()
            };
        }
    }
	
	public static Direction GetOpposite(Direction dir)
	{
        return dir switch
        {
            Direction.North => Direction.South,
			Direction.South => Direction.North,
			Direction.West => Direction.East,
			Direction.East => Direction.West,
			_ => throw new NotImplementedException()
        };
    }

    public static Direction GetLeft(Direction dir)
    {
        return dir switch
        {
            Direction.North => Direction.East,
            Direction.South => Direction.West,
            Direction.West => Direction.North,
            Direction.East => Direction.South,
            _ => throw new NotImplementedException()
        };
    }

    public static Direction GetRight(Direction dir)
    {
        return dir switch
        {
            Direction.North => Direction.West,
            Direction.South => Direction.East,
            Direction.West => Direction.South,
            Direction.East => Direction.North,
            _ => throw new NotImplementedException()
        };
    }

    public enum Direction
	{
		North,
		South,
		East,
		West
	}
}

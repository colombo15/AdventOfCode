using static System.Net.Mime.MediaTypeNames;

namespace AdventOfCode._2024.Day06;

public class Solution : ISolution
{
    private Grid _grid;

	public void Puzzle1(string[] input)
	{
        _grid = new Grid(input);
		Console.WriteLine(_grid.MoveGuard() + " : 4967");
	}

	public void Puzzle2(string[] input)
	{
        Console.WriteLine(_grid.MoveGuard2());
    }

	private class Grid
	{
		private Dictionary<(int x, int y), char> _grid = [];
		private (int x, int y) _guard;
        private (int x, int y) _guardOriginal;
        private HashSet<(int x, int y)> _visited = [];

        public Grid(string[] input)
		{
			for (var i = input.Length - 1; i >= 0; i--)
			{
				for (var j = 0; j < input[i].Length; j++)
				{
					if (input[i][j] != '^')
					{
                        _grid.Add((j, input.Length - 1 - i), input[i][j]);
                    }
					else
					{
						_guard = (j, input.Length - 1 - i);
                        _guardOriginal = _guard;
                        _grid.Add(_guard, ',');
                    }
				}
			}
		}

		public int MoveGuard()
		{
			var direction = Direction.North;
			var next = Next(direction);

            while (_grid.ContainsKey(next))
			{
				if (_grid[next] == '#')
				{
					direction = ChangeDirection(direction);
				}
				else
				{
                    _guard = next;
                    _visited.Add(_guard);
				}
				next = Next(direction);
            }

			return _visited.Count;

            (int x, int y) Next(Direction direction) => direction switch
            {
                Direction.North => (_guard.x, _guard.y + 1),
                Direction.East => (_guard.x + 1, _guard.y),
                Direction.South => (_guard.x, _guard.y - 1),
                Direction.West => (_guard.x - 1, _guard.y),
                _ => throw new NotImplementedException(),
            };

            Direction ChangeDirection(Direction direction) => direction switch
            {
                Direction.North => Direction.East,
                Direction.East => Direction.South,
                Direction.South => Direction.West,
                Direction.West => Direction.North,
                _ => throw new NotImplementedException(),
            };
        }

        public int MoveGuard2()
        {
			var total = 0;
			
			foreach (var v in _visited)
			{
                _guard = _guardOriginal;
                _grid[v] = '#';
                var direction = Direction.North;
                var next = Next(direction);
                var stops = new HashSet<(int x, int y, Direction dir)>();
                while (_grid.ContainsKey(next))
                {
                    if (stops.Contains((next.x, next.y, direction)))
                    {
                        total++;
                        break;
                    }
                    if (_grid[next] == '#')
                    {
                        stops.Add((next.x, next.y, direction));
                        direction = ChangeDirection(direction);
                    }
                    else
                    {
                        _guard = next;
                    }
                    next = Next(direction);
                }

                _grid[v] = '.';
            }
			return total;

            (int x, int y) Next(Direction direction) => direction switch
            {
                Direction.North => (_guard.x, _guard.y + 1),
                Direction.East => (_guard.x + 1, _guard.y),
                Direction.South => (_guard.x, _guard.y - 1),
                Direction.West => (_guard.x - 1, _guard.y),
                _ => throw new NotImplementedException(),
            };

            Direction ChangeDirection(Direction direction) => direction switch
            {
                Direction.North => Direction.East,
                Direction.East => Direction.South,
                Direction.South => Direction.West,
                Direction.West => Direction.North,
                _ => throw new NotImplementedException(),
            };
        }

        private enum Direction
		{
			North,
			East,
			South,
			West
		}
	}
}

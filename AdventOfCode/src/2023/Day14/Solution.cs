using System.Collections.ObjectModel;

namespace AdventOfCode._2023.Day14;

public class Solution : ISolution
{
	private int _width = 0;
    private int _height = 0;
	private Dictionary<(int x, int y), char> _mirror = new();
	private HashSet<(int x, int y)> _rocks = new();
    private List<HashSet<(int x, int y)>> _cache = new();
    private List<(int, int)> _cacheScores = new();
    public void Puzzle1(string[] input)
	{
        _mirror = new();
        _rocks = new();
        _width = input[0].Length;
        _height = input.Length;

		for (int y = 0; y < input.Length; y++)
		{
            for (int x = 0; x < input[y].Length; x++)
            {
                _mirror.Add((x, y), input[y][x]);
				if (input[y][x] == 'O')
				{
                    _rocks.Add((x, y));
				}
            }
        }

        TiltNorth();

        var total = 0;
		foreach (var (x, y) in _rocks)
		{
			total += _height - y;
		}

		Console.WriteLine(total);
    }

	public void Puzzle2(string[] input)
	{
        var total = 0;
        _mirror = new();
        _rocks = new();
        _width = input[0].Length;
        _height = input.Length;

        for (int y = 0; y < input.Length; y++)
        {
            for (int x = 0; x < input[y].Length; x++)
            {
                _mirror.Add((x, y), input[y][x]);
                if (input[y][x] == 'O')
                {
                    _rocks.Add((x, y));
                }
            }
        }

        var cacheHitCount = 0;
        for (var i = 1; i <= 1000000000; i++)
        {
            TiltNorth();
            TiltWest();
            TiltSouth();
            TiltEast();

            var cacheHit = false;
            for (var c = 0; c < _cache.Count; c++)
            {
                if (_rocks.Count == _cache[c].Count && _rocks.All(_cache[c].Contains))
                {
                    cacheHit = true;
                    cacheHitCount++;
                    var score = 0;
                    foreach (var (x, y) in _rocks)
                    {
                        score += _height - y;
                    }
                    _cacheScores.Add((score, i));
                }
            }

            if (!cacheHit)
            {
                _cache.Add(_rocks.ToHashSet());
            }
            else if (_cacheScores.Count > 5 && _cacheScores.Count % 2 == 0)
            {

                var join = string.Join(",", _cacheScores.Select(x => x.Item1.ToString()));
                var str1 = join[..(join.Length / 2)];
                var str2 = join[((join.Length / 2) + 1)..];
                if (str1 == str2)
                {
                    var div = _cacheScores.Count / 2;
                    var division = (((1000000000 - _cacheScores[0].Item2) / div) * div) + _cacheScores[0].Item2;
                    var diff = 1000000000 - division;
                    var target = diff + +_cacheScores[0].Item2;
                    total = _cacheScores.Where(x => x.Item2 == target).Select(x => x.Item1).First();
                    break;
                }
            }
        }

        Console.WriteLine(total);
    }

	private void Print()
	{
		for (var y = 0; y < _height; y++)
		{
			for (var x = 0; x < _width; x++)
			{
				Console.Write(_mirror[(x, y)]);
			}
			Console.WriteLine();
		}
	}

	private void TiltNorth()
	{
		var newRocks = new HashSet<(int x, int y)>();
		
		for (var y = 0; y < _height; y++)
		{
			var rocks = _rocks.Where(rock => rock.y == y).ToArray();
			foreach (var rock in rocks)
			{
				var moved = false;
				var lastCoord = rock;
				var newCoord = (rock.x, y : y - 1);

				while (_mirror.ContainsKey(newCoord) && _mirror[newCoord] == '.')
				{
					_mirror[newCoord] = 'O';
					_mirror[lastCoord] = '.';
                    lastCoord = newCoord;
                    newCoord = (newCoord.x, newCoord.y - 1);
					moved = true;
                }

				if (moved)
				{
                    newRocks.Add(lastCoord);
                }
                else
				{
					newRocks.Add(rock);
                }
            }
		}
		_rocks = newRocks;
    }

    private void TiltSouth()
    {
        var newRocks = new HashSet<(int x, int y)>();

        for (var y = _height - 1; y >= 0; y--)
        {
            var rocks = _rocks.Where(rock => rock.y == y).ToArray();
            foreach (var rock in rocks)
            {
                var moved = false;
                var lastCoord = rock;
                var newCoord = (rock.x, y: y + 1);

                while (_mirror.ContainsKey(newCoord) && _mirror[newCoord] == '.')
                {
                    _mirror[newCoord] = 'O';
                    _mirror[lastCoord] = '.';
                    lastCoord = newCoord;
                    newCoord = (newCoord.x, newCoord.y + 1);
                    moved = true;
                }

                if (moved)
                {
                    newRocks.Add(lastCoord);
                }
                else
                {
                    newRocks.Add(rock);
                }
            }
        }
        _rocks = newRocks;
    }

    private void TiltEast()
    {
        var newRocks = new HashSet<(int x, int y)>();

        for (var x = _width - 1; x >= 0; x--)
        {
            var rocks = _rocks.Where(rock => rock.x == x).ToArray();
            foreach (var rock in rocks)
            {
                var moved = false;
                var lastCoord = rock;
                var newCoord = (x: x + 1, rock.y);

                while (_mirror.ContainsKey(newCoord) && _mirror[newCoord] == '.')
                {
                    _mirror[newCoord] = 'O';
                    _mirror[lastCoord] = '.';
                    lastCoord = newCoord;
                    newCoord = (newCoord.x + 1, newCoord.y);
                    moved = true;
                }

                if (moved)
                {
                    newRocks.Add(lastCoord);
                }
                else
                {
                    newRocks.Add(rock);
                }
            }
        }
        _rocks = newRocks;
    }

    private void TiltWest()
    {
        var newRocks = new HashSet<(int x, int y)>();

        for (var x = 0; x < _width; x++)
        {
            var rocks = _rocks.Where(rock => rock.x == x).ToArray();
            foreach (var rock in rocks)
            {
                var moved = false;
                var lastCoord = rock;
                var newCoord = (x: x - 1, rock.y);

                while (_mirror.ContainsKey(newCoord) && _mirror[newCoord] == '.')
                {
                    _mirror[newCoord] = 'O';
                    _mirror[lastCoord] = '.';
                    lastCoord = newCoord;
                    newCoord = (newCoord.x - 1, newCoord.y);
                    moved = true;
                }

                if (moved)
                {
                    newRocks.Add(lastCoord);
                }
                else
                {
                    newRocks.Add(rock);
                }
            }
        }
        _rocks = newRocks;
    }

    private static bool IsReverseLookup(Direction direction)
	{
		return direction == Direction.East || direction == Direction.South;
	}

	private (int, int, int, int) GetStartingIndexes(Direction direction)
	{
		return direction switch
        {
            Direction.North => (0, _width, 0, 0),
            Direction.South => (_width - 1, 0, 0, 0),
            Direction.West => (0, 0, 0, _width),
            Direction.East => (0, 0, _width - 1, 0),
            _ => throw new NotImplementedException(),
        };
	}

	private enum Direction
	{
		North,
		South,
		West,
		East
	}
}

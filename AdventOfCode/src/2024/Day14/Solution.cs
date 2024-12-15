using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace AdventOfCode._2024.Day14;

public partial class Solution : ISolution
{
	private (int width, int height) _tiles = (width: 101, height: 103);

	public void Puzzle1(string[] input)
	{
		var grid = new Dictionary<(int x, int y), List<Robot>>();
        FillGrid(grid);
        var seconds = 100;

		foreach(var robotStats in input)
		{
			var robot = new Robot(robotStats, grid, _tiles);
			robot.Move(seconds);
            grid[robot.Pos].Add(robot);
		}

		var quads = new int[4];
		var midX = (int)Math.Ceiling(_tiles.width / 2.0);
		var midY = (int)Math.Ceiling(_tiles.height / 2.0);
		for (var y = 0; y < _tiles.height; y++)
        {
			if (y == midY - 1) continue;
            for (var x = 0; x < _tiles.width; x++)
            {
                if (x == midX - 1) continue;
                var quad =  (x >= midX ? 1 : 0) + (y >= midY ? 2 : 0);
				quads[quad] += grid[(x, y)].Count;
            }
        }

		Console.WriteLine(quads.Aggregate(1, (x, y) => x * y));
	}

	public void Puzzle2(string[] input)
	{
        var grid = new Dictionary<(int x, int y), List<Robot>>();
        FillGrid(grid);
        var robots = new List<Robot>();

        foreach (var robotStats in input)
        {
            var robot = new Robot(robotStats, grid, _tiles);
			robots.Add(robot);
            grid[robot.Pos].Add(robot);
        }

        foreach (var robot in robots)
        {
            grid[robot.Pos].Remove(robot);
            robot.Move(6464 + 52);
            grid[robot.Pos].Add(robot);
        }
        Console.Clear();
        Print(grid);
        Console.WriteLine(6464 + 52);
	}

    [GeneratedRegex("-?\\d+")]
    private static partial Regex ParseInts();

	private void FillGrid(Dictionary<(int x, int y), List<Robot>> grid)
	{
        grid.Clear();
		for (var y = 0; y < _tiles.height; y++)
		{
            for (var x = 0; x < _tiles.width; x++)
            {
                grid.Add((x, y), []);
            }
        }
    }

	private void Print(Dictionary<(int x, int y), List<Robot>> grid)
	{
        for (var y = 0; y < _tiles.height; y++)
        {
            for (var x = 0; x < _tiles.width; x++)
            {
				Console.Write(grid[(x, y)].Count > 0 ? '*' : ' ');
            }
			Console.WriteLine();
        }
    }

	private class Robot
	{
		public (int x, int y) Pos { get; private set; }
        private (int x, int y) _vel;
		private readonly Dictionary<(int x, int y), List<Robot>> _grid;
		private readonly (int width, int height) _tiles;

        public Robot(string stats, Dictionary<(int x, int y), List<Robot>> grid, (int width, int height) tiles)
		{
            var matches = ParseInts().Matches(stats).Select(x => int.Parse(x.Value)).ToArray();
            Pos = (matches[0], matches[1]);
            _vel = (matches[2], matches[3]);
			_grid = grid;
			_tiles = tiles;
        }

		public void Move(int seconds)
		{
			for (var s = 0; s < seconds; s++)
			{
				var next = (x: Pos.x + _vel.x, y: Pos.y + _vel.y);
				if (next.x < 0)
				{
                    next.x = _tiles.width + next.x;
                }
				else if (next.x >= _tiles.width)
				{
					next.x -= _tiles.width;
                }
				if (next.y < 0)
				{
					next.y = _tiles.height + next.y;
				}
				else if (next.y >= _tiles.height)
				{
                    next.y -= _tiles.height;
                }
                Pos = next;
			}
		}
	}
}

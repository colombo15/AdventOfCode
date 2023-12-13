using System.Runtime.InteropServices;

namespace AdventOfCode._2023.Day13;

public class Solution : ISolution
{
	public void Puzzle1(string[] input)
	{
		var total = 0;
		var mazes = ParseInput(input);
		foreach (var maze in mazes)
		{
			var vert = FindVertical(maze);
			var hori = FindHorizontal(maze);
            total += vert + hori;
        }
		Console.WriteLine(total);
	}

	public void Puzzle2(string[] input)
	{
        var total = 0;
        var mazes = ParseInput(input);
        foreach (var maze in mazes)
        {
            var vert = FindVertical(maze);
            var hori = FindHorizontal(maze);

			var skipVert = vert;
			var skipHori = hori / 100;

            var width = maze.Where(x => x.Key.Item2 == 0).Count();
            var height = maze.Where(x => x.Key.Item1 == 0).Count();

			for (var i = 0; i < height; i++)
			{
				var b = false;
				for (var j = 0; j < width; j++)
				{
					maze[(j, i)] = maze[(j, i)] == '.' ? '#' : '.';
                    var vert2 = FindVertical(maze, skipVert);
                    var hori2 = FindHorizontal(maze, skipHori);

					if (vert2 != vert && vert2 > 0)
					{
						total += vert2;
                        b = true;
                        break;
                    }
                    if (hori2 != hori && hori2 > 0)
                    {
                        total += hori2;
						b = true;
						break;
                    }
					maze[(j, i)] = maze[(j, i)] == '.' ? '#' : '.';
                }

				if (b)
				{
					break;
				}
			}
        }
        Console.WriteLine(total);
    }

	private static int FindHorizontal(Dictionary<(int, int), char> maze, int skip = -1)
	{
        var retval = 0;
        var width = maze.Where(x => x.Key.Item2 == 0).Count();
        var height = maze.Where(x => x.Key.Item1 == 0).Count();

		for (var i = 1; i < height; i++)
		{
			if (i == skip) continue;

            var row1 = new string(maze.Where(x => x.Key.Item2 == i - 1).Select(x => x.Value).ToArray());
            var row2 = new string(maze.Where(x => x.Key.Item2 == i).Select(x => x.Value).ToArray());
			if (row1 == row2)
			{
                var size = i >= (int)Math.Ceiling(height / 2.0) ? height - i : i;
                var mirror = true;
                for (var j = 0; j < width; j++)
                {
                    var left = new string(maze.Where(x => Enumerable.Range(i - size, size).Contains(x.Key.Item2) && x.Key.Item1 == j).Select(x => x.Value).Reverse().ToArray());
                    var right = new string(maze.Where(x => Enumerable.Range(i, size).Contains(x.Key.Item2) && x.Key.Item1 == j).Select(x => x.Value).ToArray());

                    if (left != right)
                    {
                        mirror = false;
                        break;
                    }
                }
                if (mirror)
                {
                    return i * 100;
                }
            }
        }

        return retval;
    }

	private static int FindVertical(Dictionary<(int, int), char> maze, int skip = -1)
	{
		var retval = 0;
        var width = maze.Where(x => x.Key.Item2 == 0).Count();
        var height = maze.Where(x => x.Key.Item1 == 0).Count();

		for (var i = 1; i < width; i++)
		{
            if (i == skip) continue;

            var col1 = new string(maze.Where(x => x.Key.Item1 == i - 1).Select(x => x.Value).ToArray());
            var col2 = new string(maze.Where(x => x.Key.Item1 == i).Select(x => x.Value).ToArray());
			if (col1 == col2)
            {
                var size = i >= (int)Math.Ceiling(width / 2.0) ? width - i : i;
                var mirror = true;
                for (var j = 0; j < height; j++)
				{
                    var left = new string(maze.Where(x => Enumerable.Range(i - size, size).Contains(x.Key.Item1) && x.Key.Item2 == j).Select(x => x.Value).Reverse().ToArray());
                    var right = new string(maze.Where(x => Enumerable.Range(i, size).Contains(x.Key.Item1) && x.Key.Item2 == j).Select(x => x.Value).ToArray());

					if (left != right)
					{
						mirror = false;
						break;
					}
                }
				if (mirror)
				{
                    return i;
				}
			}
        }
		return retval;
	}

	private static List<Dictionary<(int, int), char>> ParseInput(string[] input)
	{
		var retval = new List<Dictionary<(int, int), char>>() { new() };
		var y = 0;
		foreach (var line in input)
		{
			if (line == "")
			{
				retval.Add(new Dictionary<(int, int), char>());
				y = 0;
				continue;
			}
			for (var i = 0; i < line.Length; i++)
			{
				retval.Last().Add((i, y), line[i]);
			}
			y++;
		}
		return retval;
	}
}

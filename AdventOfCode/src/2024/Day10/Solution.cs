namespace AdventOfCode._2024.Day10;

public class Solution : ISolution
{
	public void Puzzle1(string[] input)
	{
        var grid = new Grid(input);
		Console.WriteLine(grid.GetTotalScore());
	}

	public void Puzzle2(string[] input)
	{
        var grid = new Grid(input);
        Console.WriteLine(grid.GetTotalScore2());
    }

    private class Grid
    {
        private readonly Dictionary<(int x, int y), int> _grid = [];
        private readonly List<(int x, int y)> _trailheads = [];

        public Grid(string[] input)
        {
            for (var i = input.Length - 1; i >= 0; i--)
            {
                for (var j = 0; j < input[i].Length; j++)
                {
                    if (input[i][j] != '.')
                    {
                        var coord = (j, input.Length - 1 - i);
                        _grid.Add(coord, input[i][j] - 48);
                        if (input[i][j] - 48 == 0)
                        {
                            _trailheads.Add(coord);
                        }
                    }
                }
            }
        }

        public int GetTotalScore()
        {
            var trails = new Dictionary<(int x, int y), HashSet<(int x, int y)>>();
            foreach (var trailhead in _trailheads)
            {
                trails.Add(trailhead, []);
                GetScore(trailhead, trailhead);
            }
            return trails.Select(x => x.Value.Count).Sum();

            void GetScore((int x, int y) coord, (int x, int y) trailhead)
            {
                if (_grid[coord] == 9) trails[trailhead].Add(coord);

                var curr = _grid[coord];
                var north = (coord.x, coord.y + 1);
                var east = (coord.x + 1, coord.y);
                var south = (coord.x, coord.y - 1);
                var west = (coord.x - 1, coord.y);

                if (_grid.TryGetValue(north, out int val) && val == curr + 1)
                {
                    GetScore(north, trailhead);
                }
                if (_grid.TryGetValue(east, out val) && val == curr + 1)
                {
                    GetScore(east, trailhead);
                }
                if (_grid.TryGetValue(south, out val) && val == curr + 1)
                {
                    GetScore(south, trailhead);
                }
                if (_grid.TryGetValue(west, out val) && val == curr + 1)
                {
                    GetScore(west, trailhead);
                }
            }
        }

        public int GetTotalScore2()
        {
            var trails = new Dictionary<(int x, int y), int>();
            foreach (var trailhead in _trailheads)
            {
                trails.Add(trailhead, 0);
                GetScore(trailhead, trailhead);
            }
            return trails.Select(x => x.Value).Sum();

            void GetScore((int x, int y) coord, (int x, int y) trailhead)
            {
                if (_grid[coord] == 9) trails[trailhead]++;

                var curr = _grid[coord];
                var north = (coord.x, coord.y + 1);
                var east = (coord.x + 1, coord.y);
                var south = (coord.x, coord.y - 1);
                var west = (coord.x - 1, coord.y);

                if (_grid.TryGetValue(north, out int val) && val == curr + 1)
                {
                    GetScore(north, trailhead);
                }
                if (_grid.TryGetValue(east, out val) && val == curr + 1)
                {
                    GetScore(east, trailhead);
                }
                if (_grid.TryGetValue(south, out val) && val == curr + 1)
                {
                    GetScore(south, trailhead);
                }
                if (_grid.TryGetValue(west, out val) && val == curr + 1)
                {
                    GetScore(west, trailhead);
                }
            }
        }
    }
}

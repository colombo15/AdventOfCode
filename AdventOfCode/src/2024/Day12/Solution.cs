using System.Drawing;

namespace AdventOfCode._2024.Day12;

public class Solution : ISolution
{
	public void Puzzle1(string[] input)
	{
        var grid = new Grid(input);
		Console.WriteLine(grid.GetFenceCost());
	}

	public void Puzzle2(string[] input)
	{
        var grid = new Grid(input);
        Console.WriteLine(grid.GetFenceCost(true));
    }

    private class Grid
    {
        private readonly Dictionary<(int x, int y), char> _grid = [];
        private readonly HashSet<(int x, int y)> _unchecked = [];

        public Grid(string[] input)
        {
            for (var i = input.Length - 1; i >= 0; i--)
            {
                for (var j = 0; j < input[i].Length; j++)
                {
                    var coord = (j, input.Length - 1 - i);
                    _grid.Add(coord, input[i][j]);
                    _unchecked.Add(coord);
                }
            }
        }

        public long GetFenceCost(bool part2 = false)
        {
            var _fieldStats = new List<(int area, int prerimeter)>();
            foreach (var coord in _grid.Keys)
            {
                if (_unchecked.Contains(coord))
                {
                    var result = FloodFill(coord);

                    if (!part2)
                    {
                        _fieldStats.Add(result.stat);
                    }
                    else
                    {
                        _fieldStats.Add((result.stat.area, GetSides(_grid[coord], result.areas, result.borders)));
                    }
                }
            }

            return _fieldStats.Select(x => (long)(x.area * x.prerimeter)).Sum();

            ((int area, int prerimeter) stat, HashSet<(int x, int y)> areas, HashSet<(int x, int y)> borders) FloodFill((int x, int y) coord)
            {
                var areas = new HashSet<(int x, int y)>{ coord };
                var borders = new HashSet<(int x, int y)>();
                var area = 1;
                var prerimeter = 0;
                _unchecked.Remove(coord);

                var north = (coord.x, coord.y + 1);
                if (_unchecked.Contains(north) && _grid[coord] == _grid[north])
                {
                    var result = FloodFill(north);
                    borders.UnionWith(result.borders);
                    areas.UnionWith(result.areas);
                    area += result.stat.area;
                    prerimeter += result.stat.prerimeter;
                }
                else if (!_grid.ContainsKey(north) || (_grid.TryGetValue(north, out char value) && _grid[coord] != value))
                {
                    prerimeter++;
                    borders.Add(north);
                }

                var east = (coord.x + 1, coord.y);
                if (_unchecked.Contains(east) && _grid[coord] == _grid[east])
                {
                    var result = FloodFill(east);
                    borders.UnionWith(result.borders);
                    areas.UnionWith(result.areas);
                    area += result.stat.area;
                    prerimeter += result.stat.prerimeter;
                }
                else if (!_grid.ContainsKey(east) || (_grid.TryGetValue(east, out char value) && _grid[coord] != value))
                {
                    prerimeter++;
                    borders.Add(east);
                }

                var south = (coord.x, coord.y - 1);
                if (_unchecked.Contains(south) && _grid[coord] == _grid[south])
                {
                    var result = FloodFill(south);
                    borders.UnionWith(result.borders);
                    areas.UnionWith(result.areas);
                    area += result.stat.area;
                    prerimeter += result.stat.prerimeter;
                }
                else if (!_grid.ContainsKey(south) || (_grid.TryGetValue(south, out char value) && _grid[coord] != value))
                {
                    prerimeter++;
                    borders.Add(south);
                }

                var west = (coord.x - 1, coord.y);
                if (_unchecked.Contains(west) && _grid[coord] == _grid[west])
                {
                    var result = FloodFill(west);
                    borders.UnionWith(result.borders);
                    areas.UnionWith(result.areas);
                    area += result.stat.area;
                    prerimeter += result.stat.prerimeter;
                }
                else if (!_grid.ContainsKey(west) || (_grid.TryGetValue(west, out char value) && _grid[coord] != value))
                {
                    prerimeter++;
                    borders.Add(west);
                }

                return ((area, prerimeter), areas, borders);
            }

            int GetSides(char crop, HashSet<(int x, int y)> areas, HashSet<(int x, int y)> borders)
            {
                var total = 0;
                var touch = new HashSet<((int x, int y) coord, Direction dir)>();
                while (borders.Count > 0)
                {
                    var curr = borders.First();
                    RemoveBorder(crop, curr);
                    while (touch.Count > 0)
                    {
                        RemoveTouch(touch.First());
                        total++;
                    }
                }
                return total;

                void RemoveTouch(((int x, int y) coord, Direction dir) t)
                {
                    var coord = t.coord;
                    touch.Remove(t);
                    var north = ((coord.x, coord.y + 1), t.dir);
                    var east = ((coord.x + 1, coord.y), t.dir);
                    var south = ((coord.x, coord.y - 1), t.dir);
                    var west = ((coord.x - 1, coord.y), t.dir);

                    if (touch.Contains(north))
                    {
                        RemoveTouch(north);
                    }
                    if (touch.Contains(east))
                    {
                        RemoveTouch(east);
                    }
                    if (touch.Contains(south))
                    {
                        RemoveTouch(south);
                    }
                    if (touch.Contains(west))
                    {
                        RemoveTouch(west);
                    }
                }

                void RemoveBorder(char crop, (int x, int y) coord)
                {
                    borders.Remove(coord);
                    var north = (coord.x, coord.y + 1);
                    var east = (coord.x + 1, coord.y);
                    var south = (coord.x, coord.y - 1);
                    var west = (coord.x - 1, coord.y);

                    if (areas.Contains(north))
                    {
                        touch.Add((north, Direction.North));
                    }
                    if (areas.Contains(east))
                    {
                        touch.Add((east, Direction.East));
                    }
                    if (areas.Contains(south))
                    {
                        touch.Add((south, Direction.South));
                    }
                    if (areas.Contains(west))
                    {
                        touch.Add((west, Direction.West));
                    }

                    if (borders.Contains(north))
                    {
                        RemoveBorder(crop, north);
                    }
                    if (borders.Contains(east))
                    {
                        RemoveBorder(crop, east);
                    }
                    if (borders.Contains(south))
                    {
                        RemoveBorder(crop, south);
                    }
                    if (borders.Contains(west))
                    {
                        RemoveBorder(crop, west);
                    }
                }
            }

            

            (int x, int y) BackCoord(Direction direction, (int x, int y) coord) => direction switch
            {
                Direction.North => (coord.x, coord.y - 1),
                Direction.East => (coord.x - 1, coord.y),
                Direction.South => (coord.x, coord.y + 1),
                Direction.West => (coord.x + 1, coord.y),
                _ => throw new NotImplementedException(),
            };

            Direction BackDirection(Direction direction) => direction switch
            {
                Direction.North => Direction.South,
                Direction.East => Direction.West,
                Direction.South => Direction.North,
                Direction.West => Direction.East,
                _ => throw new NotImplementedException(),
            };

            (int x, int y) OppTurnCoord(Direction direction, (int x, int y) coord) => direction switch
            {
                Direction.North => (coord.x - 1, coord.y),
                Direction.East => (coord.x, coord.y + 1),
                Direction.South => (coord.x + 1, coord.y),
                Direction.West => (coord.x, coord.y - 1),
                _ => throw new NotImplementedException(),
            };

            Direction OppTurnDirection(Direction direction) => direction switch
            {
                Direction.North => Direction.West,
                Direction.East => Direction.North,
                Direction.South => Direction.East,
                Direction.West => Direction.South,
                _ => throw new NotImplementedException(),
            };

            (int x, int y) NextCoord(Direction direction, (int x, int y) coord) => direction switch
            {
                Direction.North => (coord.x, coord.y + 1),
                Direction.East => (coord.x + 1, coord.y),
                Direction.South => (coord.x, coord.y - 1),
                Direction.West => (coord.x - 1, coord.y),
                _ => throw new NotImplementedException(),
            };

            (int x, int y) NextTurnCoord(Direction direction, (int x, int y) coord) => direction switch
            {
                Direction.North => (coord.x + 1, coord.y),
                Direction.East => (coord.x, coord.y - 1),
                Direction.South => (coord.x - 1, coord.y),
                Direction.West => (coord.x, coord.y + 1),
                _ => throw new NotImplementedException(),
            };

            Direction NextTurnDirection(Direction direction) => direction switch
            {
                Direction.North => Direction.East,
                Direction.East => Direction.South,
                Direction.South => Direction.West,
                Direction.West => Direction.North,
                _ => throw new NotImplementedException(),
            };
        }
    }

    

    private enum Direction
    {
        North,
        East,
        South,
        West
    }
}

using System.Linq;

namespace AdventOfCode._2024.Day16;

public class Solution : ISolution
{
	public void Puzzle1(string[] input)
	{
        var grid = new Grid(input);
		Console.WriteLine(grid.BFS());
	}

	public void Puzzle2(string[] input)
	{
        var grid = new Grid(input);
        var max = grid.BFS();
        var grid2 = new Grid2(input);
        Console.WriteLine(grid2.BFS(max));
    }

    private class Grid
    {
        private Dictionary<(int x, int y), char> _grid = [];
        private (int x, int y) _start;
        private (int x, int y) _end;
        private int _height;
        private int _width;

        public Grid(string[] input)
        {
            _height = input.Length;
            _width = input[0].Length;
            for (var i = input.Length - 1; i >= 0; i--)
            {
                for (var j = 0; j < input[i].Length; j++)
                {
                    if (input[i][j] == '.' || input[i][j] == '#')
                    {
                        _grid.Add((j, input.Length - 1 - i), input[i][j]);
                    }
                    else if (input[i][j] == 'S')
                    {
                        _start = (j, input.Length - 1 - i);
                        _grid.Add(_start, '.');
                    }
                    else if (input[i][j] == 'E')
                    {
                        _end = (j, input.Length - 1 - i);
                        _grid.Add(_end, '.');
                    }
                }
            }
        }

        void Print(int count)
        {
            for (var y = _height - 1; y >= 0; y--)
            {
                for (var x = 0; x < _width; x++)
                {
                    Console.Write(_grid[(x, y)]);
                }
                Console.WriteLine();
            }

            Console.WriteLine($" Count {count}");
        }

        public int BFS()
        {
            var result = 0;
            var curr = (coord: _start, dir: Direction.East);
            var nextMoves = new PriorityQueue<((int x, int y) coord, Direction dir), int>();
            GenerateNextMoves(curr, result);

            while (nextMoves.TryDequeue(out var next, out int count))
            {
                if (next.coord == _end) return count;
                _grid[next.coord] = 'X';
                GenerateNextMoves(next, count);
            }

            return result;

            void GenerateNextMoves(((int x, int y) coord, Direction dir) node, int count)
            {
                var next = Next(node);
                if (_grid.TryGetValue(next.coord, out var value) && value == '.')
                {
                    nextMoves.Enqueue(next, count + 1);
                }
                var clockwise = Next((node.coord, Clockwise(node.dir)));
                if (_grid.TryGetValue(clockwise.coord, out value) && value == '.')
                {
                    nextMoves.Enqueue(clockwise, count + 1001);
                }
                var counterClock = Next((node.coord, CounterClockwise(node.dir)));
                if (_grid.TryGetValue(counterClock.coord, out value) && value == '.')
                {
                    nextMoves.Enqueue(counterClock, count + 1001);
                }
            }

            ((int x, int y) coord, Direction dir) Next(((int x, int y) coord, Direction dir) node) => node.dir switch
            {
                Direction.North => ((node.coord.x, node.coord.y + 1), node.dir),
                Direction.East => ((node.coord.x + 1, node.coord.y), node.dir),
                Direction.South => ((node.coord.x, node.coord.y - 1), node.dir),
                Direction.West => ((node.coord.x - 1, node.coord.y), node.dir),
                _ => throw new NotImplementedException(),
            };

            Direction Clockwise(Direction direction) => direction switch
            {
                Direction.North => Direction.East,
                Direction.East => Direction.South,
                Direction.South => Direction.West,
                Direction.West => Direction.North,
                _ => throw new NotImplementedException(),
            };

            Direction CounterClockwise(Direction direction) => direction switch
            {
                Direction.North => Direction.West,
                Direction.East => Direction.North,
                Direction.South => Direction.East,
                Direction.West => Direction.South,
                _ => throw new NotImplementedException(),
            };
        }
    }

    private class Grid2
    {
        private Dictionary<(int x, int y), (char tile, List<List<(int x, int y)>> paths)> _grid = [];
        private (int x, int y) _start;
        private (int x, int y) _end;
        private int _height;
        private int _width;

        public Grid2(string[] input)
        {
            _height = input.Length;
            _width = input[0].Length;
            for (var i = input.Length - 1; i >= 0; i--)
            {
                for (var j = 0; j < input[i].Length; j++)
                {
                    if (input[i][j] == '.' || input[i][j] == '#')
                    {
                        _grid.Add((j, input.Length - 1 - i), (input[i][j], new List<List<(int x, int y)>>()));
                    }
                    else if (input[i][j] == 'S')
                    {
                        _start = (j, input.Length - 1 - i);
                        _grid.Add(_start, ('.', new List<List<(int x, int y)>>()));
                    }
                    else if (input[i][j] == 'E')
                    {
                        _end = (j, input.Length - 1 - i);
                        _grid.Add(_end, ('.', new List<List<(int x, int y)>>()));
                    }
                }
            }
        }

        void Print(int count)
        {
            for (var y = _height - 1; y >= 0; y--)
            {
                for (var x = 0; x < _width; x++)
                {
                    Console.Write(_grid[(x, y)]);
                }
                Console.WriteLine();
            }

            Console.WriteLine($" Count {count}");
        }

        public int BFS(int max)
        {
            var cache = new Dictionary<((int x, int y) coord, Direction dir), int>();
            var hs = new HashSet<(int x, int y)> { _start, _end };
            var curr = (coord: _start, dir: Direction.East);
            var nextMoves = new PriorityQueue<(((int x, int y) coord, Direction dir) node, (int x, int y)[] paths), int>();
            GenerateNextMoves((curr, []), 0);

            while (nextMoves.TryDequeue(out var next, out int count))
            {
                if (count > max) continue;
                if (count == max && next.node.coord == _end)
                {
                    foreach (var p in next.paths)
                    {
                        hs.Add(p);
                    }
                }
                GenerateNextMoves(next, count);
            }

            return hs.Count;

            void GenerateNextMoves((((int x, int y) coord, Direction dir) node, (int x, int y)[] paths) node, int count)
            {
                if (cache.TryGetValue(node.node, out var c)) 
                {
                    if (c < count) return;
                    cache[node.node] = count;
                }
                else
                {
                    cache.Add(node.node, count);
                }

                var next = Next(node.node);
                var nextPath = new (int x, int y)[node.paths.Length + 1];
                Array.Copy(node.paths, nextPath, node.paths.Length);
                nextPath[^1] = next.coord;

                if (_grid.TryGetValue(next.coord, out var value) && value.tile == '.')
                {
                    nextMoves.Enqueue((next, nextPath), count + 1);
                }

                var clockwise = Next((node.node.coord, Clockwise(node.node.dir)));
                var clockwisePath = new (int x, int y)[node.paths.Length + 1];
                Array.Copy(node.paths, clockwisePath, node.paths.Length);
                clockwisePath[^1] = clockwise.coord;

                if (_grid.TryGetValue(clockwise.coord, out value) && value.tile == '.')
                {
                    nextMoves.Enqueue((clockwise, clockwisePath), count + 1001);
                }

                var counterClock = Next((node.node.coord, CounterClockwise(node.node.dir)));
                var counterClockwisePath = new (int x, int y)[node.paths.Length + 1];
                Array.Copy(node.paths, counterClockwisePath, node.paths.Length);
                counterClockwisePath[^1] = counterClock.coord;

                if (_grid.TryGetValue(counterClock.coord, out value) && value.tile == '.')
                {
                    nextMoves.Enqueue((counterClock, counterClockwisePath), count + 1001);
                }
            }

            ((int x, int y) coord, Direction dir) Next(((int x, int y) coord, Direction dir) node) => node.dir switch
            {
                Direction.North => ((node.coord.x, node.coord.y + 1), node.dir),
                Direction.East => ((node.coord.x + 1, node.coord.y), node.dir),
                Direction.South => ((node.coord.x, node.coord.y - 1), node.dir),
                Direction.West => ((node.coord.x - 1, node.coord.y), node.dir),
                _ => throw new NotImplementedException(),
            };

            Direction Clockwise(Direction direction) => direction switch
            {
                Direction.North => Direction.East,
                Direction.East => Direction.South,
                Direction.South => Direction.West,
                Direction.West => Direction.North,
                _ => throw new NotImplementedException(),
            };

            Direction CounterClockwise(Direction direction) => direction switch
            {
                Direction.North => Direction.West,
                Direction.East => Direction.North,
                Direction.South => Direction.East,
                Direction.West => Direction.South,
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

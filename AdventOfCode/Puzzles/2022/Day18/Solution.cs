using AdventOfCode.Common;

namespace AdventOfCode.Puzzles._2022.Day18
{
    internal sealed partial class Solution : ISolution
    {
        private int _result;
        private HashSet<Coord> _coords = new();
        private int _xWall = 0;
        private int _yWall = 0;
        private int _zWall = 0;

        public void PartOne(string[] input)
        {
            ParseInput(input);

            foreach (var c in _coords)
            {
                if (!_coords.Contains(c with { X = c.X + 1 })) _result++;
                if (!_coords.Contains(c with { X = c.X - 1 })) _result++;
                if (!_coords.Contains(c with { Y = c.Y + 1 })) _result++;
                if (!_coords.Contains(c with { Y = c.Y - 1 })) _result++;
                if (!_coords.Contains(c with { Z = c.Z + 1 })) _result++;
                if (!_coords.Contains(c with { Z = c.Z - 1 })) _result++;
            }
        }

        public void PartTwo(string[] input)
        {
            ParseInput(input);

            _xWall = _coords.Select(c => c.X).Max() + 5;
            _yWall = _coords.Select(c => c.Y).Max() + 5;
            _zWall = _coords.Select(c => c.Z).Max() + 5;

            foreach (var c in _coords)
            {
                if (IsExposed(c with { X = c.X + 1 })) _result++;
                if (IsExposed(c with { X = c.X - 1 })) _result++;
                if (IsExposed(c with { Y = c.Y + 1 })) _result++;
                if (IsExposed(c with { Y = c.Y - 1 })) _result++;
                if (IsExposed(c with { Z = c.Z + 1 })) _result++;
                if (IsExposed(c with { Z = c.Z - 1 })) _result++;
            }
        }

        private bool IsExposed(Coord c)
        {
            if (_coords.Contains(c)) return false;

            var visited = new HashSet<Coord>();
            var check = new Queue<Coord>();

            check.Enqueue(c with { X = c.X + 1 });
            check.Enqueue(c with { X = c.X - 1 });
            check.Enqueue(c with { Y = c.Y + 1 });
            check.Enqueue(c with { Y = c.Y - 1 });
            check.Enqueue(c with { Z = c.Z + 1 });
            check.Enqueue(c with { Z = c.Z - 1 });

            while(check.Any())
            {
                var temp = check.Dequeue();
                visited.Add(temp);

                if (_coords.Contains(temp)) continue;

                var next = temp with { X = temp.X + 1 };
                if (!visited.Contains(next) && !_coords.Contains(next) && !check.Contains(next))
                {
                    if (next.X >= _xWall) return true;
                    check.Enqueue(next);
                }

                next = temp with { X = temp.X - 1 };
                if (!visited.Contains(next) && !_coords.Contains(next) && !check.Contains(next))
                {
                    if (next.X <= 0) return true;
                    check.Enqueue(next);
                }

                next = temp with { Y = temp.Y + 1 };
                if (!visited.Contains(next) && !_coords.Contains(next) && !check.Contains(next))
                {
                    if (next.Y >= _yWall) return true;
                    check.Enqueue(next);
                }

                next = temp with { Y = temp.Y - 1 };
                if (!visited.Contains(next) && !_coords.Contains(next) && !check.Contains(next))
                {
                    if (next.Y <= 0) return true;
                    check.Enqueue(next);
                }

                next = temp with { Z = temp.Z + 1 };
                if (!visited.Contains(next) && !_coords.Contains(next) && !check.Contains(next))
                {
                    if (next.Z >= _zWall) return true;
                    check.Enqueue(next);
                }

                next = temp with { Z = temp.Z - 1 };
                if (!visited.Contains(next) && !_coords.Contains(next) && !check.Contains(next))
                {
                    if (next.Z <= 0) return true;
                    check.Enqueue(next);
                }
            }

            return false;
        }

        private void ParseInput(string[] input)
        {
            foreach (var item in input)
            {
                _coords.Add(new Coord(item));
            }
        }

        private record class Coord
        {
            public int X; 
            public int Y;
            public int Z;

            public Coord(string input)
            {
                var split = input.Split(',');
                this.X = int.Parse(split[0]);
                this.Y = int.Parse(split[1]);
                this.Z = int.Parse(split[2]);
            }
        }
    }
}

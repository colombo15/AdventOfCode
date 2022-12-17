using AdventOfCode.Common;
using System.Diagnostics;

namespace AdventOfCode.Puzzles._2022.Day17
{
    internal sealed partial class Solution : ISolution
    {
        private int _result;
        private readonly int _lowX = 0;
        private readonly int _highX = 8;
        private readonly HashSet<Coord> _stoppedRocks = new();
        private readonly Dictionary<Coord, int> _rocksAdded = new();
        private int _towerHeight = 0;
        private int _curr = 0;
        private string? _wind;
        private long _iterations;

        public void PartOne(string[] input)
        {
            _wind = input[0];
            _iterations = 2022;

            for (long i = 0; i < _iterations; i++)
            {
                var rock = RockFactory(i, 3, _towerHeight + 3);
                do { WindGust(rock); } while (MakeRockFall(rock));
            }

            _result = _towerHeight;
        }

        public void PartTwo(string[] input)
        {
            _iterations = 1000000000000;
            _wind = input[0];

            for (long i = 0; i < _iterations; i++)
            {
                var rock = RockFactory(i, 3, _towerHeight + 3);
                do { WindGust(rock); } while (MakeRockFall(rock));
                CheckRepetition();
            }

            _result = _towerHeight;
        }

        private bool CheckRepetition()
        {
            var list = _stoppedRocks.GroupBy(x => x.y).Select(x => x.ToHashSet()).ToList();

            if (list == null || list.Count % 2 != 0) return false;

            
            var count = list.Count / 2;
            var l1 = list.Take(count).ToList();
            var l2 = list.Take(count..^0).ToList();
            var diff = _towerHeight / 2;

            for (var i = 0; i < l1.Count; i++)
            {
                if (l1[i].Count != l2[i].Count) return false;
            }

            for (var i = 0; i < l1.Count; i++)
            {
                foreach (var c in l2[i])
                {
                    var temp = new Coord(c.x, c.y - diff);
                    if (!l1[i].Contains(temp)) return false;
                }
            }

            return true;
        }

        private void WindGust(IRock rock)
        {
            if (_wind![_curr % _wind.Length] == '>')
                MoveRockRight(rock);
            else
                MoveRockLeft(rock);
            _curr++;
        }

        private bool MakeRockFall(IRock rock)
        {
            if (rock.CanFall(_stoppedRocks))
            {
                rock.Anchor.y--;
                return true;
            }

            var rockPoints = rock.GetRockPoints();
            foreach (var rockPoint in rockPoints)
            {
                _stoppedRocks.Add(rockPoint);
                _rocksAdded.Add(rockPoint, _curr);
            }

            var highest = rockPoints.Select(x => x.y).Max() + 1;
            if (highest > _towerHeight)
            {
                _towerHeight = highest;
            }

            return false;
        }

        private void MoveRockLeft(IRock rock)
        {
            if (rock.CanMoveLeft(_stoppedRocks, _lowX))
                rock.Anchor.x--;
        }

        private void MoveRockRight(IRock rock)
        {
            if (rock.CanMoveRight(_stoppedRocks, _highX))
                rock.Anchor.x++;
        }

        private static IRock RockFactory(long count, int x, int y)
        {
            return (count % 5) switch
            {
                0 => new HoriLine(x, y),
                1 => new Plus(x, y),
                2 => new BackwardsL(x, y),
                3 => new VertLine(x, y),
                4 => new Square(x, y),
                _ => throw new UnreachableException(),
            };
        }

        private interface IRock
        {
            bool CanFall(HashSet<Coord> stoppedRocks);
            bool CanMoveLeft(HashSet<Coord> stoppedRocks, int lowX);
            bool CanMoveRight(HashSet<Coord> stoppedRocks, int highX);
            List<Coord> GetRockPoints();
            Coord Anchor { get; set; }

        }

        private sealed class HoriLine : IRock
        {
            public Coord Anchor { get; set; }

            public HoriLine(int anchorX, int anchorY) 
            { 
                Anchor = new Coord(anchorX, anchorY);
            }

            public List<Coord> GetRockPoints()
            {
                var list = new List<Coord>
                {
                    new Coord(Anchor.x, Anchor.y),
                    new Coord(Anchor.x + 1, Anchor.y),
                    new Coord(Anchor.x + 2, Anchor.y),
                    new Coord(Anchor.x + 3, Anchor.y)
                };

                return list;
            }

            public bool CanFall(HashSet<Coord> stoppedRocks)
            {
                return Anchor.y - 1 >= 0
                    && !stoppedRocks.Contains(new Coord(Anchor.x, Anchor.y - 1))
                    && !stoppedRocks.Contains(new Coord(Anchor.x + 1, Anchor.y - 1))
                    && !stoppedRocks.Contains(new Coord(Anchor.x + 2, Anchor.y - 1))
                    && !stoppedRocks.Contains(new Coord(Anchor.x + 3, Anchor.y - 1));
            }

            public bool CanMoveLeft(HashSet<Coord> stoppedRocks, int lowX)
            {
                return 
                    Anchor.x - 1 > lowX
                    && !stoppedRocks.Contains(new Coord(Anchor.x - 1, Anchor.y));
            }

            public bool CanMoveRight(HashSet<Coord> stoppedRocks, int highX)
            {
                return
                    Anchor.x + 4 < highX
                    && !stoppedRocks.Contains(new Coord(Anchor.x + 4, Anchor.y));
            }
        }

        private sealed class Plus : IRock
        {
            public Coord Anchor { get; set; }

            public Plus(int anchorX, int anchorY) 
            {
                Anchor = new Coord(anchorX, anchorY);
            }

            public List<Coord> GetRockPoints()
            {
                var list = new List<Coord>
                {
                    new Coord(Anchor.x + 1, Anchor.y),
                    new Coord(Anchor.x, Anchor.y + 1),
                    new Coord(Anchor.x + 1, Anchor.y + 1),
                    new Coord(Anchor.x + 2, Anchor.y + 1),
                    new Coord(Anchor.x + 1, Anchor.y + 2)
                };

                return list;
            }

            public bool CanFall(HashSet<Coord> stoppedRocks)
            {
                return Anchor.y - 1 >= 0
                    && !stoppedRocks.Contains(new Coord(Anchor.x, Anchor.y))
                    && !stoppedRocks.Contains(new Coord(Anchor.x + 1, Anchor.y - 1))
                    && !stoppedRocks.Contains(new Coord(Anchor.x + 2, Anchor.y));
            }

            public bool CanMoveLeft(HashSet<Coord> stoppedRocks, int lowX)
            {
                return
                    Anchor.x - 1 > lowX
                    && !stoppedRocks.Contains(new Coord(Anchor.x, Anchor.y))
                    && !stoppedRocks.Contains(new Coord(Anchor.x - 1, Anchor.y + 1))
                    && !stoppedRocks.Contains(new Coord(Anchor.x, Anchor.y + 2));
            }

            public bool CanMoveRight(HashSet<Coord> stoppedRocks, int highX)
            {
                return
                    Anchor.x + 3 < highX
                    && !stoppedRocks.Contains(new Coord(Anchor.x + 2, Anchor.y))
                    && !stoppedRocks.Contains(new Coord(Anchor.x + 3, Anchor.y + 1))
                    && !stoppedRocks.Contains(new Coord(Anchor.x + 2, Anchor.y + 2));
            }
        }

        private sealed class BackwardsL : IRock
        {
            public Coord Anchor { get; set; }

            public BackwardsL(int anchorX, int anchorY) 
            {
                Anchor = new Coord(anchorX, anchorY);
            }

            public List<Coord> GetRockPoints()
            {
                var list = new List<Coord>
                {
                    new Coord(Anchor.x, Anchor.y),
                    new Coord(Anchor.x + 1, Anchor.y),
                    new Coord(Anchor.x + 2, Anchor.y),
                    new Coord(Anchor.x + 2, Anchor.y + 1),
                    new Coord(Anchor.x + 2, Anchor.y + 2)
                };

                return list;
            }

            public bool CanFall(HashSet<Coord> stoppedRocks)
            {
                return Anchor.y - 1 >= 0
                    && !stoppedRocks.Contains(new Coord(Anchor.x, Anchor.y - 1))
                    && !stoppedRocks.Contains(new Coord(Anchor.x + 1, Anchor.y - 1))
                    && !stoppedRocks.Contains(new Coord(Anchor.x + 2, Anchor.y - 1));
            }

            public bool CanMoveLeft(HashSet<Coord> stoppedRocks, int lowX)
            {
                return
                    Anchor.x - 1 > lowX
                    && !stoppedRocks.Contains(new Coord(Anchor.x - 1, Anchor.y))
                    && !stoppedRocks.Contains(new Coord(Anchor.x + 2, Anchor.y + 1))
                    && !stoppedRocks.Contains(new Coord(Anchor.x + 2, Anchor.y + 2));
            }

            public bool CanMoveRight(HashSet<Coord> stoppedRocks, int highX)
            {
                return
                    Anchor.x + 3 < highX
                    && !stoppedRocks.Contains(new Coord(Anchor.x + 3, Anchor.y))
                    && !stoppedRocks.Contains(new Coord(Anchor.x + 3, Anchor.y + 1))
                    && !stoppedRocks.Contains(new Coord(Anchor.x + 3, Anchor.y + 2));
            }
        }

        private sealed class VertLine : IRock
        {
            public Coord Anchor { get; set; }

            public VertLine(int anchorX, int anchorY) 
            {
                Anchor = new Coord(anchorX, anchorY);
            }

            public List<Coord> GetRockPoints()
            {
                var list = new List<Coord>
                {
                    new Coord(Anchor.x, Anchor.y),
                    new Coord(Anchor.x, Anchor.y + 1),
                    new Coord(Anchor.x, Anchor.y + 2),
                    new Coord(Anchor.x, Anchor.y + 3)
                };

                return list;
            }

            public bool CanFall(HashSet<Coord> stoppedRocks)
            {
                return Anchor.y - 1 >= 0
                    && !stoppedRocks.Contains(new Coord(Anchor.x, Anchor.y - 1));
            }

            public bool CanMoveLeft(HashSet<Coord> stoppedRocks, int lowX)
            {
                return
                    Anchor.x - 1 > lowX
                    && !stoppedRocks.Contains(new Coord(Anchor.x - 1, Anchor.y))
                    && !stoppedRocks.Contains(new Coord(Anchor.x - 1, Anchor.y + 1))
                    && !stoppedRocks.Contains(new Coord(Anchor.x - 1, Anchor.y + 2))
                    && !stoppedRocks.Contains(new Coord(Anchor.x - 1, Anchor.y + 3));
            }

            public bool CanMoveRight(HashSet<Coord> stoppedRocks, int highX)
            {
                return
                    Anchor.x + 1 < highX
                    && !stoppedRocks.Contains(new Coord(Anchor.x + 1, Anchor.y))
                    && !stoppedRocks.Contains(new Coord(Anchor.x + 1, Anchor.y + 1))
                    && !stoppedRocks.Contains(new Coord(Anchor.x + 1, Anchor.y + 2))
                    && !stoppedRocks.Contains(new Coord(Anchor.x + 1, Anchor.y + 3));
            }
        }

        private sealed class Square : IRock
        {
            public Coord Anchor { get; set; }

            public Square(int anchorX, int anchorY) 
            {
                Anchor = new Coord(anchorX, anchorY);
            }

            public List<Coord> GetRockPoints()
            {
                var list = new List<Coord>
                {
                    new Coord(Anchor.x, Anchor.y),
                    new Coord(Anchor.x + 1, Anchor.y),
                    new Coord(Anchor.x, Anchor.y + 1),
                    new Coord(Anchor.x + 1, Anchor.y + 1)
                };

                return list;
            }

            public bool CanFall(HashSet<Coord> stoppedRocks)
            {
                return
                    Anchor.y - 1 >= 0
                    && !stoppedRocks.Contains(new Coord(Anchor.x, Anchor.y - 1))
                    && !stoppedRocks.Contains(new Coord(Anchor.x + 1, Anchor.y - 1));
            }

            public bool CanMoveLeft(HashSet<Coord> stoppedRocks, int lowX)
            {
                return
                    Anchor.x - 1 > lowX
                    && !stoppedRocks.Contains(new Coord(Anchor.x - 1, Anchor.y))
                    && !stoppedRocks.Contains(new Coord(Anchor.x - 1, Anchor.y + 1));
            }

            public bool CanMoveRight(HashSet<Coord> stoppedRocks, int highX)
            {
                return
                    Anchor.x + 2 < highX
                    && !stoppedRocks.Contains(new Coord(Anchor.x + 2, Anchor.y))
                    && !stoppedRocks.Contains(new Coord(Anchor.x + 2, Anchor.y + 1));
            }
        }

        private sealed record class Coord
        {
            public int x;
            public int y;

            public Coord(int x, int y)
            {
                this.x = x;
                this.y = y;
            }
        }
    }
}

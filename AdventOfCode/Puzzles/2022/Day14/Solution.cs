using AdventOfCode.Common;
using System.Drawing;

namespace AdventOfCode.Puzzles._2022.Day14
{
    internal sealed partial class Solution : ISolution
    {
        private int _result;

        private readonly Coord SandStart = new(500, 0);
        private int _lowestPoint;

        public void PartOne(string[] input)
        {
            var coordinates = ParseInput(input);
            _lowestPoint = coordinates.Select(x => x.y).Max();
            _result = PourSand(coordinates);
        }

        public void PartTwo(string[] input)
        {
            var coordinates = ParseInput(input);
            _lowestPoint = coordinates.Select(x => x.y).Max() + 2;
            _result = PourSand(coordinates, true);
        }

        private HashSet<Coord> ParseInput(string[] input)
        {
            var coordinates = new HashSet<Coord>();

            foreach (var item in input)
            {
                var structure = item.Split(" -> ");

                var start = structure[0].Split(',');
                var x1 = int.Parse(start[0]);
                var y1 = int.Parse(start[1]);

                coordinates.Add(new Coord(x1, y1));

                for (var i = 1; i < structure.Length; i++)
                {
                    var goal = structure[i].Split(',');
                    var x2 = int.Parse(goal[0]);
                    var y2 = int.Parse(goal[1]);

                    if (x2 > x1)
                    {
                        for (var j = 1; x1 + j <= x2; j++)
                        {
                            coordinates.Add(new Coord(x1 + j, y1));
                        }
                    }
                    else if (x1 > x2)
                    {
                        for (var j = -1; x1 + j >= x2; j--)
                        {
                            coordinates.Add(new Coord(x1 + j, y1));
                        }
                    }
                    else if (y2 > y1)
                    {
                        for (var j = 1; y1 + j <= y2; j++)
                        {
                            coordinates.Add(new Coord(x1, y1 + j));
                        }
                    }
                    else if (y1 > y2)
                    {
                        for (var j = -1; y1 + j >= y2; j--)
                        {
                            coordinates.Add(new Coord(x1, y1 + j));
                        }
                    }

                    x1 = x2;
                    y1 = y2;
                }
            }

            return coordinates;
        }

        private int PourSand(HashSet<Coord> coordinates, bool infiniteFloor = false)
        {
            var sandAtRest = 0;
            var currenSand = SandStart with { };

            while (true)
            {
                var bellow = new Coord(currenSand.x , currenSand.y + 1);
                if (coordinates.Contains(bellow) || (infiniteFloor && currenSand.y + 1 == _lowestPoint))
                {
                    var bellowleft = new Coord(currenSand.x - 1, currenSand.y + 1);
                    if (coordinates.Contains(bellowleft) || (infiniteFloor && currenSand.y + 1 == _lowestPoint))
                    {
                        var bellowright = new Coord(currenSand.x + 1, currenSand.y + 1);
                        if (coordinates.Contains(bellowright) || (infiniteFloor && currenSand.y + 1 == _lowestPoint))
                        {
                            coordinates.Add(currenSand);
                            sandAtRest++;
                            currenSand = SandStart with { };
                        }
                        else
                        {
                            currenSand.x++;
                            currenSand.y++;
                        }
                    }
                    else
                    {
                        currenSand.x--;
                        currenSand.y++;
                    }
                }
                else
                {
                    currenSand.y++;
                }

                if (currenSand.y > _lowestPoint || (infiniteFloor && coordinates.Contains(SandStart)))
                    break;
            }

            return sandAtRest;
        }

        private record class Coord
        {
            public int x;
            public int y;

            public Coord(int x, int y)
            {
                this.x = x;
                this.y = y;
            }
            
            public Coord (string coord)
            {
                var split = coord.Split(',');
                x = int.Parse(split[0]);
                y = int.Parse(split[1]);
            }
        }
    }
}

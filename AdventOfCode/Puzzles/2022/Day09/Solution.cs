using AdventOfCode.Common;
using System.Diagnostics;

namespace AdventOfCode.Puzzles._2022.Day09
{
    internal sealed partial class Solution : ISolution
    {
        private int _result;

        public void PartOne(string[] input)
        {
            var rope = new Rope();
            
            foreach (var item in input)
            {
                var (direction, steps) = 
                    item.Split(' ') switch { var split => (split[0][0], int.Parse(split[1])) };

                rope.Move(direction, steps);
            }

            _result = rope.GetTailVisitedCount();
        }

        public void PartTwo(string[] input)
        {
            var rope = new Rope(10);

            foreach (var item in input)
            {
                var (direction, steps) = 
                    item.Split(' ') switch { var split => (split[0][0], int.Parse(split[1])) };

                rope.Move(direction, steps);
            }

            _result = rope.GetTailVisitedCount();
        }

        private class Rope
        {
            private readonly Coord[] Knots;
            private readonly HashSet<string> TailTraveledCoords;

            public Rope (int knotCount = 2)
            {
                if (knotCount < 2)
                    knotCount = 2;

                Knots = new Coord[knotCount];
                TailTraveledCoords = new HashSet<string>();

                for (var i = 0; i < knotCount; i++)
                {
                    Knots[i] = new Coord();
                }
            }

            public void Move(char direction, int steps)
            {
                for (var i = 0; i < steps; i++)
                {
                    MoveHead(direction);
                    for (var j = 1; j < Knots.Length; j++)
                    {
                        MoveKnot(j);

                        if (j == Knots.Length - 1)
                        {
                            AddTailCoordToHashSet();
                        }
                    }
                }
            }

            public int GetTailVisitedCount()
            {
                return TailTraveledCoords.Count;
            }

            private void MoveHead(char direction)
            {
                var head = Knots[0];

                switch (direction)
                {
                    case 'U': head.y++; break;
                    case 'D': head.y--; break;
                    case 'L': head.x--; break;
                    case 'R': head.x++; break;
                    default: throw new UnreachableException();
                }
            }

            private void MoveKnot(int knotIndex)
            {
                var prev = Knots[knotIndex - 1];
                var knot = Knots[knotIndex];

                var xDiff = prev.x - knot.x;
                var yDiff = prev.y - knot.y;

                var xAbsDiff = Math.Abs(xDiff);
                var yAbsDiff = Math.Abs(yDiff);

                if (xAbsDiff <= 1 && yAbsDiff <= 1) return;

                if ((xAbsDiff == 2 && yAbsDiff == 0) || (xAbsDiff >= 2 && yAbsDiff >= 1) || (xAbsDiff >= 1 && yAbsDiff >= 2))
                {
                    knot.MoveX(xDiff > 0);
                }

                if ((xAbsDiff >= 2 && yAbsDiff >= 1) || (xAbsDiff >= 1 && yAbsDiff >= 2) || (xAbsDiff == 0 && yAbsDiff == 2))
                {
                    knot.MoveY(yDiff > 0);
                }
            }

            private void AddTailCoordToHashSet()
            {
                TailTraveledCoords.Add(Knots.Last().x.ToString() + ',' + Knots.Last().y.ToString());
            }

            private class Coord
            {
                public Coord() 
                {
                    x = 0;
                    y = 0;
                }

                public int x;
                public int y;

                public void MoveX(bool up)
                {
                    if (up) x++;
                    else x--;
                }

                public void MoveY(bool up)
                {
                    if (up) y++;
                    else y--;
                }
            }
        }
    }
}

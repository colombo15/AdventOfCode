using AdventOfCode.Common;
using System.Diagnostics;

namespace AdventOfCode.Puzzles._2022.Day09
{
    internal sealed class Solution : ISolution
    {
        private int _result;

        public void PartOne(string[] input)
        {
            var rope = new Rope(2);
            
            foreach (var item in input)
            {
                var split = item.Split(' ');
                rope.Move(split[0][0], int.Parse(split[1]));
            }

            _result = rope.TailVisitedCount();
        }

        public void PartTwo(string[] input)
        {
            var rope = new Rope(10);

            foreach (var item in input)
            {
                var split = item.Split(' ');
                rope.Move(split[0][0], int.Parse(split[1]));
            }

            _result = rope.TailVisitedCount();
        }

        private class Rope
        {
            public Rope (int knotCount)
            {
                Knots = new List<Coord>();
                hs = new HashSet<string>();

                for (var i = 0; i < knotCount; i++)
                {
                    Knots.Add(new Coord());
                }
            }

            private readonly List<Coord> Knots;

            private readonly HashSet<string> hs;

            public void Move(char direction, int steps)
            {
                AddTailCoordToHashSet();

                for (var i = 0; i < steps; i++)
                {
                    MoveHead(direction);
                    for (var j = 1; j < Knots.Count; j++)
                    {
                        MoveKnot(j);
                    }
                    AddTailCoordToHashSet();
                }
            }

            public int TailVisitedCount()
            {
                return hs.Count;
            }

            private void MoveHead(char direction)
            {
                var head = Knots[0];
                switch (direction)
                {
                    case 'U':
                        head.y++;
                        break;
                    case 'D':
                        head.y--;
                        break;
                    case 'L':
                        head.x--;
                        break;
                    case 'R':
                        head.x++;
                        break;
                    default:
                        throw new UnreachableException();
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

                if ((xAbsDiff >= 2 && yAbsDiff >= 1) || (xAbsDiff >= 1 && yAbsDiff >= 2))
                {
                    knot.MoveX(xDiff);
                    knot.MoveY(yDiff);
                }
                else if (xAbsDiff == 0 && yAbsDiff == 2)
                {
                    knot.MoveY(yDiff);
                }
                else if (xAbsDiff == 2 && yAbsDiff == 0)
                {
                    knot.MoveX(xDiff);
                }
                else
                {
                    throw new UnreachableException();
                }
            }

            private void AddTailCoordToHashSet()
            {
                hs.Add(Knots.Last().x.ToString() + ',' + Knots.Last().y.ToString());
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

                public void MoveX(int xDiff)
                {
                    if (xDiff > 0)
                        x++;
                    else
                        x--;
                }

                public void MoveY(int yDiff)
                {
                    if (yDiff > 0)
                        y++;
                    else
                        y--;
                }
            }
        }

        public void Print()
        {
            Console.WriteLine(_result);
        }

        public bool IsPartOneCorrect()
        {
            return _result == 6376;
        }

        public bool IsPartTwoCorrect()
        {
            return _result == 2607;
        }
    }
}

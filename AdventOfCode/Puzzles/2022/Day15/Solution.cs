using AdventOfCode.Common;
using System.Text.RegularExpressions;

namespace AdventOfCode.Puzzles._2022.Day15
{
    internal sealed partial class Solution : ISolution
    {
        private long _result;
        private int _max;

        public void PartOne(string[] input)
        {
            var sensors = new List<Coord>();
            var target = 2000000;

            foreach (var item in input)
            {
                var match = MyRegex().Matches(item);

                var x1 = int.Parse(match[0].Groups[1].ToString());
                var y1 = int.Parse(match[0].Groups[2].ToString());
                var x2 = int.Parse(match[0].Groups[3].ToString());
                var y2 = int.Parse(match[0].Groups[4].ToString());

                sensors.Add(new Coord(x1, y1, new Coord(x2, y2)));
            }

            var targetCoords = new HashSet<(int, int)>();
            foreach (var sensor in sensors)
            {
                var val = sensor.manhattanDistance - Math.Abs(target - sensor.y);
                for (var i = -1 * val; i <= val; i++)
                {
                    targetCoords.Add((sensor.x + i, target));
                }
                targetCoords.Remove((sensor.closestBeacon!.x, sensor.closestBeacon!.y));
            }

            _result = targetCoords.Count;
        }

        public void PartTwo(string[] input)
        {
            _max = 4000000;
            var arr = new List<Range>[_max + 1];
            var sensors = new List<Coord2>();

            foreach (var item in input)
            {
                var match = MyRegex().Matches(item);

                var x1 = int.Parse(match[0].Groups[1].ToString());
                var y1 = int.Parse(match[0].Groups[2].ToString());
                var x2 = int.Parse(match[0].Groups[3].ToString());
                var y2 = int.Parse(match[0].Groups[4].ToString());

                sensors.Add(new Coord2(x1, y1, new Coord(x2, y2)));
            }
            
            foreach (var sensor in sensors)
            {
                foreach (var r in sensor.ranges)
                {
                    AddRange(r.Item2, r.Item1);
                }
            }

            for (var i = 0; i < arr.Length; i++)
            {
                if (arr[i] == null) continue;
                var a = arr[i];
                a.Sort((x1, x2) => x1.Start.Value > x2.Start.Value ? 1 : -1);
                while (a.Count > 1)
                {
                    var merge = Merge(a[0], a[1]);

                    if (merge.HasValue)
                    {
                        a[0] = merge.Value;
                        a.RemoveAt(1);
                    }
                    else
                    {
                        _result = ((a[0].End.Value + 1) * (long)4000000) + i;
                        return;
                    } 
                }
            }

            void AddRange(Range range, int index)
            {
                if (arr[index] == null)
                    arr[index] = new List<Range>();
                arr[index].Add(range);
            }

            Range? Merge(Range r1, Range r2)
            {
                if (r1.Start.Value > r2.Start.Value)
                {
                    (r1, r2) = (r2, r1);
                }

                if (r2.Start.Value >= r1.Start.Value && r2.Start.Value <= r1.End.Value + 1)
                {
                    return new Range(r1.Start, r2.End.Value > r1.End.Value ? r2.End.Value : r1.End.Value);
                }

                return null;
            }
        }

        private sealed record class Coord
        {
            public int x;
            public int y;
            public Coord? closestBeacon;
            public int manhattanDistance;

            public Coord (int x, int y)
            {
                this.x = x;
                this.y = y;
            }

            public Coord(int x, int y, Coord closestBeacon)
            {
                this.x = x;
                this.y = y;
                this.closestBeacon = closestBeacon;
                manhattanDistance = Math.Abs(x - closestBeacon.x) + Math.Abs(y - closestBeacon.y);
            }
        }

        private sealed record class Coord2
        {
            public int x;
            public int y;
            public Coord? closestBeacon;
            public int manhattanDistance;
            public List<(int, Range)> ranges;

            public Coord2(int x, int y)
            {
                this.x = x;
                this.y = y;
            }

            public Coord2(int x, int y, Coord closestBeacon)
            {
                this.x = x;
                this.y = y;
                this.closestBeacon = closestBeacon;
                manhattanDistance = Math.Abs(x - closestBeacon.x) + Math.Abs(y - closestBeacon.y);

                ranges = new List<(int, Range)>();

                for (var i = -1 * manhattanDistance; i <= manhattanDistance; i++)
                {
                    var target = y + i;
                    if (target < 0) continue;
                    if (target > 4000000) continue;

                    var val = manhattanDistance - Math.Abs(target - y);

                    var start = x - val;
                    if (start < 0)
                        start = 0;

                    var end = x + val;
                    if (end > 4000000)
                        end = 4000000;

                    ranges.Add((target, new Range(start, end)));
                }
            }
        }

        [GeneratedRegex("Sensor at x=(-?\\d+), y=(-?\\d+): closest beacon is at x=(-?\\d+), y=(-?\\d+)")]
        private static partial Regex MyRegex();
    }
}

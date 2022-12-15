using AdventOfCode.Common;
using System.Text.RegularExpressions;

namespace AdventOfCode.Puzzles._2022.Day15
{
    internal sealed partial class Solution : ISolution
    {
        private int _result;

        public void PartOne(string[] input)
        {
            var sensors = new List<Coord>();
            var target = 10;

            foreach (var item in input)
            {
                var match = MyRegex().Matches(item);

                var x1 = int.Parse(match[0].Groups[1].ToString());
                var y1 = int.Parse(match[0].Groups[2].ToString());
                var x2 = int.Parse(match[0].Groups[3].ToString());
                var y2 = int.Parse(match[0].Groups[4].ToString());

                sensors.Add(new Coord(x1, y1, new Coord(x2, y2)));
            }


            var targetCoords = new List<Coord>();
            foreach (var sensor in sensors)
            {
                if (sensor.y < target && sensor.y + sensor.manhattanDistance >= target)
                {
                    var val = sensor.y + sensor.manhattanDistance;
                    val = val - target + 1;
                    _result += ((val - 1) * 2) + 1;
                }
                else if (sensor.y > target && sensor.y - sensor.manhattanDistance < target)
                {
                    var val = sensor.y + (-1 * sensor.manhattanDistance);
                    val += sensor.manhattanDistance - target;
                    _result += ((val - 1) * 2) + 1;
                }
                else if (sensor.y == target)
                {

                }
            }

            throw new NotImplementedException();
        }

        public void PartTwo(string[] input)
        {
            throw new NotImplementedException();
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

        [GeneratedRegex("Sensor at x=(-?\\d+), y=(-?\\d+): closest beacon is at x=(-?\\d+), y=(-?\\d+)")]
        private static partial Regex MyRegex();
    }
}

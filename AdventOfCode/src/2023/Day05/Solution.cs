using System.Text.RegularExpressions;
using System.Linq;

namespace AdventOfCode._2023.Day05;

public partial class Solution : ISolution
{
	public void Puzzle1(string[] input)
	{
		var seeds = Numbers().Matches(input[0]).Select(m => long.Parse(m.Value)).ToArray();
		var map = new List<((long, long)[], (long, long)[])>();
        var locations = new List<long>();

        for (var i = 0; i < input.Length; i++)
		{
			if (input[i] == "")
			{
                var sourceMap = new List<(long, long)>();
                var destMap = new List<(long, long)>();
                i += 2;
				while (i < input.Length && input[i] != "")
				{
					var nums = Numbers().Matches(input[i]).Select(m => long.Parse(m.Value)).ToArray();
					destMap.Add((nums[0], nums[0] + nums[2]));
					sourceMap.Add((nums[1], nums[1] + nums[2]));
					i++;
                }
				map.Add((destMap.ToArray(), sourceMap.ToArray()));
                i--;
            }
		}

		foreach (var seed in seeds)
		{
            long val = seed;
            foreach (var m in map)
            {
                val = GetValue(val, m.Item1, m.Item2);
            }
            locations.Add(val);
        }

        Console.WriteLine(locations.OrderBy(x => x).First());
    }

	public void Puzzle2(string[] input)
	{
        var seedRanges = Numbers().Matches(input[0]).Select(m => long.Parse(m.Value)).ToArray();
        var seedMap = new List<(long, long)>();
        for (var i = 0; i < seedRanges.Length; i += 2)
        {
            seedMap.Add((seedRanges[i], seedRanges[i + 1]));
        }

        var map = new List<((long, long)[], (long, long)[])>();
        var locations = -1;

        for (var i = 0; i < input.Length; i++)
        {
            if (input[i] == "")
            {
                var sourceMap = new List<(long, long)>();
                var destMap = new List<(long, long)>();
                i += 2;
                while (i < input.Length && input[i] != "")
                {
                    var nums = Numbers().Matches(input[i]).Select(m => long.Parse(m.Value)).ToArray();
                    destMap.Add((nums[0], nums[0] + nums[2]));
                    sourceMap.Add((nums[1], nums[1] + nums[2]));
                    i++;
                }
                map.Add((destMap.ToArray(), sourceMap.ToArray()));
                i--;
            }
        }

        long lowest = 0;
        var exit = false;
        while (true)
        {
            long val = lowest;
            for (var j = map.Count - 1; j >= 0; j--)
            {
                val = GetValueReverse(val, map[j].Item1, map[j].Item2);
            }
            foreach (var seed in seedMap)
            {
                if (seed.Item1 <= val && val <= seed.Item1 + seed.Item2)
                {
                    exit = true;
                    break;
                }
            }
            if (exit) break;
            lowest++;
        }

        Console.WriteLine(lowest);
    }

    [GeneratedRegex("[0-9]+")]
    private static partial Regex Numbers();

	private static long GetValue(long val, (long, long)[] dest, (long, long)[] source)
    {
        var target = -1;

        for (var i = 0; i < source.Length; i++)
        {
            if (source[i].Item1 <= val && val <= source[i].Item2)
            {
                target = i;
                break;
            }
        }

        if (target == -1)
        {
            return val;
        }
        else
        {
            var diff = val - source[target].Item1;
            return dest[target].Item1 + diff;
        }
    }

    private static long GetValueReverse(long val, (long, long)[] dest, (long, long)[] source)
    {
        var target = -1;

        for (var i = 0; i < dest.Length; i++)
        {
            if (dest[i].Item1 <= val && val <= dest[i].Item2)
            {
                target = i;
                break;
            }
        }

        if (target == -1)
        {
            return val;
        }
        else
        {
            var diff = val - dest[target].Item1;
            return source[target].Item1 + diff;
        }
    }
}

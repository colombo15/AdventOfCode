using System.Text.RegularExpressions;

namespace AdventOfCode._2023.Day06;

public partial class Solution : ISolution
{
	public void Puzzle1(string[] input)
	{
		var times = Numbers().Matches(input[0]).Select(x => long.Parse(x.Value)).ToArray();
		var distances = Numbers().Matches(input[1]).Select(x => long.Parse(x.Value)).ToArray();
		var results = new long[times.Length];

		for (var i = 0; i < times.Length; i++)
		{
			var pressTime = 0;
			while (Distance(times[i], pressTime) <= distances[i])
			{
				pressTime++;
			}
			results[i] = times[i] - pressTime * 2 + 1;
		}
		Console.WriteLine(results.Aggregate(1, (x, y) => (int)(x * y)));
	}

	public void Puzzle2(string[] input)
	{
        var times = new long[] { long.Parse(Numbers().Matches(input[0]).Aggregate("", (x, y) => x + y)) };
        var distances = new long[] { long.Parse(Numbers().Matches(input[1]).Aggregate("", (x, y) => x + y)) };
        var results = new long[times.Length];

        for (var i = 0; i < times.Length; i++)
        {
            var pressTime = 0;
            while (Distance(times[i], pressTime) <= distances[i])
            {
                pressTime++;
            }
            results[i] = times[i] - pressTime * 2 + 1;
        }
        Console.WriteLine(results.Aggregate(1, (x, y) => (int)(x * y)));
    }

	public static long Distance(long time, long speed)
	{
		return speed * (time - speed);
    }

    [GeneratedRegex("[0-9]+")]
    private static partial Regex Numbers();
}

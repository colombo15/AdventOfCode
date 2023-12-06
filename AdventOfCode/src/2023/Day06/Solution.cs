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
            var quad = Quad(times[i], distances[i]);
            results[i] = quad.Item2 - quad.Item1;
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
            var quad = Quad(times[i], distances[i]);
            results[i] = quad.Item2 - quad.Item1;
        }
        Console.WriteLine(results.Aggregate(1, (x, y) => (int)(x * y)));
    }

    [GeneratedRegex("[0-9]+")]
    private static partial Regex Numbers();

	public static (long, long) Quad(long time, long distance)
	{
        distance++;
        var plus = (long)Math.Ceiling(((-1 * time) + Math.Sqrt((time * time) - (4 * distance))) / -2);
        var minus = (long)Math.Ceiling(((-1 * time) - Math.Sqrt((time * time) - (4 * distance))) / -2);

        return (plus, minus);
	}
}

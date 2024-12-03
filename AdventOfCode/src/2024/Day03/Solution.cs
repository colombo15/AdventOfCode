using System.Text.RegularExpressions;

namespace AdventOfCode._2024.Day03;

public partial class Solution : ISolution
{
	public void Puzzle1(string[] input)
	{
		var total = 0;
		foreach (var inst in input)
		{
            total += ParseInst()
                .Matches(inst)
                .Select(x => {
				    var split = x.Groups[1].Value.Split(',').Select(int.Parse);
				    return split.First() * split.Last();
			    })
                .Sum();
        }
		Console.WriteLine(total);
	}

	public void Puzzle2(string[] input)
	{
        var total = 0;
        var enabled = true;

        foreach (var inst in input)
        {
            total += ParseInst2()
                .Matches(inst)
                .Select(x => {
                    if (x.Value.StartsWith("mul"))
                    {
                        if (enabled)
                        {
                            var split = x.Groups[1].Value.Split(',').Select(int.Parse);
                            return split.First() * split.Last();
                        }
                    }
                    else if (x.Value.StartsWith("do("))
                    {
                        enabled = true;
                    }
                    else
                    {
                        enabled = false;
                    }
                    return 0;
                })
                .Sum();
        }
        Console.WriteLine(total);
    }

    [GeneratedRegex("mul\\(([0-9]+,[0-9]+)\\)")]
    private static partial Regex ParseInst();

    [GeneratedRegex("mul\\(([0-9]+,[0-9]+)\\)|do\\(\\)|don't\\(\\)")]
    private static partial Regex ParseInst2();
}

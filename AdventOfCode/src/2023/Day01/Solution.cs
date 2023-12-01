using System.Text.RegularExpressions;

namespace AdventOfCode._2023.Day01;

public partial class Solution : ISolution
{
	public void Puzzle1(string[] input)
	{
		var total = input
			.Select(i => Part1Regex().Matches(i))
			.Select(matches => int.Parse(matches.First().Value + matches.Last().Value))
			.Sum();

		Console.WriteLine(total);
	}

	public void Puzzle2(string[] input)
	{
		var total = input
			.Select(i =>
			{
				var first = Part2Regex_FromFront().Matches(i).First().Value;
				var last = Part2Regex_FromBack().Matches(i).First().Value;
				return int.Parse(Convert(first) + Convert(last));
			})
			.Sum();
		
		Console.WriteLine(total);
	}

    [GeneratedRegex("[0-9]")]
    private static partial Regex Part1Regex();

    [GeneratedRegex("[0-9]|one|two|three|four|five|six|seven|eight|nine")]
    private static partial Regex Part2Regex_FromFront();

    [GeneratedRegex("[0-9]|one|two|three|four|five|six|seven|eight|nine", RegexOptions.RightToLeft)]
    private static partial Regex Part2Regex_FromBack();

    private static string Convert(string s)
    {
        return s switch
        {
            "one" => "1",
            "two" => "2",
            "three" => "3",
            "four" => "4",
            "five" => "5",
            "six" => "6",
            "seven" => "7",
            "eight" => "8",
            "nine" => "9",
            _ => s
        };
    }
}
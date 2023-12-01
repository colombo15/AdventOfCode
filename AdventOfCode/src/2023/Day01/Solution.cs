using System.Text.RegularExpressions;

namespace AdventOfCode._2023.Day01;

public partial class Solution : ISolution
{
    public void Puzzle1(string[] input)
    {
        var total = 0;

        foreach (var item in input)
        {
            for (var i = 0; i < item.Length; i++)
            {
                if (item[i] <= 57 && item[i] >= 49)
                {
                    total += (item[i] - 48) * 10;
                    break;
                }
            }

            for (var i = item.Length - 1; i >= 0; i--)
            {
                if (item[i] <= 57 && item[i] >= 49)
                {
                    total += (item[i] - 48);
                    break;
                }
            }
        }

        Console.WriteLine(total);
    }

    public void Puzzle2(string[] input)
    {
        var total = 0;

        foreach (var i in input)
        {
            var first = Part2Regex_FromFront().Match(i).Value;
            var last = Part2Regex_FromBack().Match(i).Value;
            total += (Convert(first) * 10) + Convert(last);
        }
        
        Console.WriteLine(total);
    }

    [GeneratedRegex("[0-9]|one|two|three|four|five|six|seven|eight|nine")]
    private static partial Regex Part2Regex_FromFront();

    [GeneratedRegex("[0-9]|one|two|three|four|five|six|seven|eight|nine", RegexOptions.RightToLeft)]
    private static partial Regex Part2Regex_FromBack();

    private static int Convert(string s)
    {
        return s switch
        {
            "one" => 1,
            "two" => 2,
            "three" => 3,
            "four" => 4,
            "five" => 5,
            "six" => 6,
            "seven" => 7,
            "eight" => 8,
            "nine" => 9,
            _ => s[0] - 48
        };
    }
}
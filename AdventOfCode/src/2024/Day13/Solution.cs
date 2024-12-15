using System.Text.RegularExpressions;

namespace AdventOfCode._2024.Day13;

public partial class Solution : ISolution
{
    public void Puzzle1(string[] input)
	{
        var machines = new List<((decimal x, decimal y) buttonA, (decimal x, decimal y) buttonB, (decimal x, decimal y) prize)>();
        for (var i = 0; i < input.Length; i += 4)
		{
            machines.Add((ParseInput(input[i]), ParseInput(input[i + 1]), ParseInput(input[i + 2])));
        }

        long total = 0;
        foreach (var machine in machines)
        {
            total += GetMinButtonPresses(machine);
        }

        Console.WriteLine(total);
	}

	public void Puzzle2(string[] input)
	{
        var machines = new List<((decimal x, decimal y) buttonA, (decimal x, decimal y) buttonB, (decimal x, decimal y) prize)>();
        for (var i = 0; i < input.Length; i += 4)
        {
            var prize = ParseInput(input[i + 2]);
            machines.Add((ParseInput(input[i]), ParseInput(input[i + 1]), (prize.x + 10000000000000, prize.y + 10000000000000)));
        }

        long total = 0;
        foreach (var machine in machines)
        {
            total += GetMinButtonPresses(machine, true);
        }

        Console.WriteLine(total);
    }

	private static (decimal x, decimal y) ParseInput(string input)
	{
		var matches = ParseInts().Matches(input).Select(x => decimal.Parse(x.Value));
		return (matches.First(), matches.Last());
    }

    [GeneratedRegex("\\d+")]
    private static partial Regex ParseInts();

    private long GetMinButtonPresses(((decimal x, decimal y) buttonA, (decimal x, decimal y) buttonB, (decimal x, decimal y) prize) machine, bool part2 = false)
    {
        var (btnA, btnB, prize) = machine;
        var bPress = (prize.y - ((btnA.y * prize.x) / btnA.x)) / (btnB.y - ((btnA.y * btnB.x) / btnA.x));
        var aPress = (prize.x - (btnB.x * bPress)) / btnA.x;
        var roundA = (long)Math.Round(aPress);
        var roundB = (long)Math.Round(bPress);

        if ((roundA * btnA.x) + (roundB * btnB.x) != prize.x)
        {
            return 0;
        }
        else if ((roundA * btnA.y) + (roundB * btnB.y) != prize.y)
        {
            return 0;
        }
        
        if (!part2 && (aPress > 100 || bPress > 100))
        {
            return 0;
        }

        return (roundA * 3) + roundB;
    }
}

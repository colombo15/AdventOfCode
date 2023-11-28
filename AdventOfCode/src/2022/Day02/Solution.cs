namespace AdventOfCode._2022.Day02;

public class Solution : ISolution
{
	public void Puzzle1(string[] input)
	{
		var total = 0;

		foreach (var item in input)
		{
			var split = item.Split(' ');
            total += CalculateScore(split[0][0], split[1][0]);
        }

        Console.WriteLine(total);
	}

	public void Puzzle2(string[] input)
	{
        var total = 0;

        foreach (var item in input)
        {
            var split = item.Split(' ');
            total += CalculateScore2(split[0][0], split[1][0]);
        }

        Console.WriteLine(total);
    }

	private static int CalculateScore(char opp, char you)
	{
        return opp switch
        {
            'A' => you switch
            {
                'X' => 4,
                'Y' => 8,
                'Z' => 3,
                _ => throw new NotImplementedException(),
            },
            'B' => you switch
            {
                'X' => 1,
                'Y' => 5,
                'Z' => 9,
                _ => throw new NotImplementedException(),
            },
            'C' => you switch
            {
                'X' => 7,
                'Y' => 2,
                'Z' => 6,
                _ => 0,
            },
            _ => throw new NotImplementedException(),
        };
    }

    private static int CalculateScore2(char opp, char you)
    {
        return opp switch
        {
            'A' => you switch
            {
                'X' => 3,
                'Y' => 4,
                'Z' => 8,
                _ => throw new NotImplementedException(),
            },
            'B' => you switch
            {
                'X' => 1,
                'Y' => 5,
                'Z' => 9,
                _ => throw new NotImplementedException(),
            },
            'C' => you switch
            {
                'X' => 2,
                'Y' => 6,
                'Z' => 7,
                _ => 0,
            },
            _ => throw new NotImplementedException(),
        };
    }
}

namespace AdventOfCode._2024.Day04;

public class Solution : ISolution
{
	public void Puzzle1(string[] input)
	{
		var total = 0;
		for(var i = 0; i < input.Length; i++)
		{
			for (var j = 0; j < input[i].Length; j++)
			{
				total += SearchNorth(input, j, i, "XMAS");
				total += SearchNorthEast(input, j, i, "XMAS");
				total += SearchEast(input, j, i, "XMAS");
				total += SearchSouthEast(input, j, i, "XMAS");
				total += SearchSouth(input, j, i, "XMAS");
				total += SearchSouthWest(input, j, i, "XMAS");
				total += SearchWest(input, j, i, "XMAS");
				total += SearchNorthWest(input, j, i, "XMAS");
            }
        }
		Console.WriteLine(total);
	}

	public void Puzzle2(string[] input)
	{
        var total = 0;
        for (var i = 0; i < input.Length; i++)
        {
            for (var j = 0; j < input[i].Length; j++)
            {
                if (input[i][j] == 'A')
                {
                    if (SearchNorthWest(input, j, i, "AM") != 1 && SearchSouthEast(input, j, i, "AM") != 1) continue;
                    if (SearchNorthWest(input, j, i, "AS") != 1 && SearchSouthEast(input, j, i, "AS") != 1) continue;
                    if (SearchNorthEast(input, j, i, "AM") != 1 && SearchSouthWest(input, j, i, "AM") != 1) continue;
                    if (SearchNorthEast(input, j, i, "AS") != 1 && SearchSouthWest(input, j, i, "AS") != 1) continue;
                    total++;
                }
            }
        }
        Console.WriteLine(total);
    }

	public static int SearchNorth(string[] input, int x, int y, string word)
	{
        if (y < word.Length - 1) return 0;
        for (var i = 0; i < word.Length; i++)
        {
            if (input[y - i][x] != word[i]) return 0;
        }
		return 1;
	}

    public static int SearchNorthEast(string[] input, int x, int y, string word)
    {
        if (y < word.Length - 1 || x > input[0].Length - word.Length) return 0;
        for (var i = 0; i < word.Length; i++)
        {
            if (input[y - i][x + i] != word[i]) return 0;
        }
        return 1;
    }

    public static int SearchEast(string[] input, int x, int y, string word)
    {
        if (x > input[0].Length - word.Length) return 0;
        for (var i = 0; i < word.Length; i++)
        {
            if (input[y][x + i] != word[i]) return 0;
        }
        return 1;
    }

    public static int SearchSouthEast(string[] input, int x, int y, string word)
    {
        if (y > input.Length - word.Length || x > input[0].Length - word.Length) return 0;
        for (var i = 0; i < word.Length; i++)
        {
            if (input[y + i][x + i] != word[i]) return 0;
        }
        return 1;
    }

    public static int SearchSouth(string[] input, int x, int y, string word)
    {
        if (y > input.Length - word.Length) return 0;
        for (var i = 0; i < word.Length; i++)
        {
            if (input[y + i][x] != word[i]) return 0;
        }
        return 1;
    }

    public static int SearchSouthWest(string[] input, int x, int y, string word)
    {
        if (y > input.Length - word.Length || x < word.Length - 1) return 0;
        for (var i = 0; i < word.Length; i++)
        {
            if (input[y + i][x - i] != word[i]) return 0;
        }
        return 1;
    }

    public static int SearchWest(string[] input, int x, int y, string word)
    {
        if (x < word.Length - 1) return 0;
        for (var i = 0; i < word.Length; i++)
        {
            if (input[y][x - i] != word[i]) return 0;
        }
        return 1;
    }

    public static int SearchNorthWest(string[] input, int x, int y, string word)
    {
        if (y < word.Length - 1 || x < word.Length - 1) return 0;
        for (var i = 0; i < word.Length; i++)
        {
            if (input[y - i][x - i] != word[i]) return 0;
        }
        return 1;
    }
}

namespace AdventOfCode._2023.Day03;

public class Solution : ISolution
{
	public void Puzzle1(string[] input)
	{
        var total = 0;

		for (var i = 0; i < input.Length; i++)
		{
			for (var j = 0; j < input[i].Length; j++)
			{
				if (IsNotDigit(input[i][j])) continue;

				var isPart = false;
				var number = "";

                while (j < input[i].Length && IsDigit(input[i][j]))
                {
                    number += input[i][j];

                    // Check Top Line
                    if (!isPart && i > 0)
                    {
                        if (!isPart)
                        {
                            isPart = j - 1 > 0 && !IsDigitOrDot(input[i - 1][j - 1]);
                        }

                        if (!isPart)
                        {
                            isPart = !IsDigitOrDot(input[i - 1][j]);
                        }

                        if (!isPart)
                        {
                            isPart = j + 1 < input[i].Length && !IsDigitOrDot(input[i - 1][j + 1]);
                        }
                    }

                    // Check Same Line
                    if (!isPart)
                    {
                        isPart = j - 1 > 0 && !IsDigitOrDot(input[i][j - 1]);
                    }

                    if (!isPart)
                    {
                        isPart = j + 1 < input[i].Length && !IsDigitOrDot(input[i][j + 1]);
                    }

                    // Check Bottom Line
                    if (!isPart && i < input[i].Length - 1)
                    {
                        if (!isPart)
                        {
                            isPart = j - 1 > 0 && !IsDigitOrDot(input[i + 1][j - 1]);
                        }

                        if (!isPart)
                        {
                            isPart = !IsDigitOrDot(input[i + 1][j]);
                        }

                        if (!isPart)
                        {
                            isPart = j + 1 < input[i].Length && !IsDigitOrDot(input[i + 1][j + 1]);
                        }
                    }
                    j++;
                }

                if (isPart)
                {
                    total += int.Parse(number);
                }
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
                if (input[i][j] == '*')
                {
                    var gears = new List<string>();

                    // top right
                    if (i - 1 >= 0 && j - 1 >= 0 && IsDigit(input[i - 1][j - 1]))
                    {
                        gears.Add("");

                        var startIndex = j - 1;
                        while (startIndex - 1 >= 0 && IsDigit(input[i - 1][startIndex - 1]))
                        {
                            startIndex--;
                        }

                        while (startIndex < input[i].Length && IsDigit(input[i - 1][startIndex]))
                        {
                            gears[^1] += input[i - 1][startIndex];
                            startIndex++;
                        }
                    }
                    // top middle
                    else if (i - 1 >= 0 && IsDigit(input[i - 1][j]))
                    {
                        gears.Add("");

                        var startIndex = j;

                        while (startIndex < input[i].Length && IsDigit(input[i - 1][startIndex]))
                        {
                            gears[^1] += input[i - 1][startIndex];
                            startIndex++;
                        }
                    }
                    // top left
                    if (i - 1 >= 0 && j + 1 < input[i].Length && IsDigit(input[i - 1][j + 1]) && !IsDigit(input[i - 1][j]))
                    {
                        gears.Add("");

                        var startIndex = j + 1;

                        while (startIndex < input[i].Length && IsDigit(input[i - 1][startIndex]))
                        {
                            gears[^1] += input[i - 1][startIndex];
                            startIndex++;
                        }
                    }

                    // right
                    if (j - 1 >= 0 && IsDigit(input[i][j - 1]))
                    {
                        gears.Add("");

                        var startIndex = j - 1;
                        while (startIndex - 1 >= 0 && IsDigit(input[i][startIndex - 1]))
                        {
                            startIndex--;
                        }

                        while (startIndex < input[i].Length && IsDigit(input[i][startIndex]))
                        {
                            gears[^1] += input[i][startIndex];
                            startIndex++;
                        }
                    }

                    // left
                    if (j + 1 < input[i].Length && IsDigit(input[i][j + 1]))
                    {
                        gears.Add("");

                        var startIndex = j + 1;

                        while (startIndex < input[i].Length && IsDigit(input[i][startIndex]))
                        {
                            gears[^1] += input[i][startIndex];
                            startIndex++;
                        }
                    }

                    // bottom right
                    if (i + 1 < input.Length && j - 1 >= 0 && IsDigit(input[i + 1][j - 1]))
                    {
                        gears.Add("");

                        var startIndex = j - 1;
                        while (startIndex - 1 >= 0 && IsDigit(input[i + 1][startIndex - 1]))
                        {
                            startIndex--;
                        }

                        while (startIndex < input[i].Length && IsDigit(input[i + 1][startIndex]))
                        {
                            gears[^1] += input[i + 1][startIndex];
                            startIndex++;
                        }
                    }
                    // bottom middle
                    else if (i + 1 < input.Length && IsDigit(input[i + 1][j]))
                    {
                        gears.Add("");

                        var startIndex = j;

                        while (startIndex < input[i].Length && IsDigit(input[i + 1][startIndex]))
                        {
                            gears[^1] += input[i + 1][startIndex];
                            startIndex++;
                        }
                    }
                    // bottom left
                    if (i + 1 < input.Length && j + 1 < input[i].Length && IsDigit(input[i + 1][j + 1]) && !IsDigit(input[i + 1][j]))
                    {
                        gears.Add("");

                        var startIndex = j + 1;

                        while (startIndex < input[i].Length && IsDigit(input[i + 1][startIndex]))
                        {
                            gears[^1] += input[i + 1][startIndex];
                            startIndex++;
                        }
                    }

                    if (gears.Count == 2)
                    {
                        total += int.Parse(gears[0]) * int.Parse(gears[1]);
                    }
                }
            }
        }

        Console.WriteLine(total);
    }

    public static bool IsDigit(char c)
    {
        return c >= '0' && c <= '9';
    }

    public static bool IsNotDigit(char c)
	{
		return !(c >= '0' && c <= '9');
	}

    public static bool IsDigitOrDot(char c)
    {
        return (c >= '0' && c <= '9') || c == '.';
    }
}

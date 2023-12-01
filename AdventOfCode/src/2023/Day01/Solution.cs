namespace AdventOfCode._2023.Day01;

public class Solution : ISolution
{
	public void Puzzle1(string[] input)
	{
		var total = 0;

		foreach (var item in input)
		{
			var number = "";
			for (var i = 0; i < item.Length; i++)
			{
				if (int.TryParse(item[i].ToString(), out var num))
				{
					number += num;
					break;
				}
			}

            for (var i = item.Length - 1; i >= 0; i--)
            {
                if (int.TryParse(item[i].ToString(), out var num))
                {
                    number += num;
                    break;
                }
            }

			total += int.Parse(number);
        }


		Console.WriteLine(total);
	}

	public void Puzzle2(string[] input)
	{
        var total = 0;

        foreach (var item in input)
        {
            var number = "";
            for (var i = 0; i < item.Length; i++)
            {
                var num = GetNum(item, i);
                if (num != -1)
                {
                    number += num;
                    break;
                }
            }

            for (var i = item.Length - 1; i >= 0; i--)
            {
                var num = GetNum(item, i);
                if (num != -1)
                {
                    number += num;
                    break;
                }
            }

            total += int.Parse(number);
        }


        Console.WriteLine(total);

        int GetNum(string item, int i)
        {
            if (int.TryParse(item[i].ToString(), out var num))
            {
                return num;
            }

            if (i <= item.Length - 3 && item.Substring(i, 3) == "one")
            {
                return 1;
            }

            if (i <= item.Length - 3 && item.Substring(i, 3) == "two")
            {
                return 2;
            }

            if (i <= item.Length - 5 && item.Substring(i, 5) == "three")
            {
                return 3;
            }

            if (i <= item.Length - 4 && item.Substring(i, 4) == "four")
            {
                return 4;
            }

            if (i <= item.Length - 4 && item.Substring(i, 4) == "five")
            {
                return 5;
            }

            if (i <= item.Length - 3 && item.Substring(i, 3) == "six")
            {
                return 6;
            }

            if (i <= item.Length - 5 && item.Substring(i, 5) == "seven")
            {
                return 7;
            }

            if (i <= item.Length - 5 && item.Substring(i, 5) == "eight")
            {
                return 8;
            }

            if (i <= item.Length - 4 && item.Substring(i, 4) == "nine")
            {
                return 9;
            }

            return -1;
        }
    }
}

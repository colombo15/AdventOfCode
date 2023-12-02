namespace AdventOfCode._2023.Day02;

public class Solution : ISolution
{
	public void Puzzle1(string[] input)
	{
		var total = 0;

		for(var i = 0; i < input.Length; i++)
		{
			var game = ParseInput(input[i]);
            var good = true;

			foreach (var round in game)
			{
				if (round.red > 12 || round.green > 13 || round.blue > 14)
				{
					good = false;
					break;
				}
			}

			if (good)
				total += i + 1;
		}

		Console.WriteLine(total);
	}

	public void Puzzle2(string[] input)
	{
        var total = 0;

        foreach (var item in input)
        {
			var game = ParseInput(item);

            var maxRed = 0;
			var maxGreen = 0;
			var maxBlue = 0;

            foreach (var round in game)
            {
                if (round.red > maxRed)
					maxRed = round.red;
				if (round.green > maxGreen)
					maxGreen = round.green;
				if (round.blue > maxBlue)
					maxBlue = round.blue;
            }

            total += maxRed * maxGreen * maxBlue;
        }

        Console.WriteLine(total);
    }

	public static List<Round> ParseInput(string input)
	{
		var retval = new List<Round>();

		var rounds = input.Split(": ")[1].Split("; ");
		foreach (var round in rounds)
		{
			var split = round.Split(", ");

            var blue = 0;
            var red = 0;
			var green = 0;

			foreach (var color in split)
			{
				var c = color.Split(' ');
				switch (c[1])
				{
					case "blue":
                        blue = int.Parse(c[0]); 
						break;
                    case "red":
                        red = int.Parse(c[0]);
                        break;
                    case "green":
                        green = int.Parse(c[0]);
                        break;
					default:
						break;
                }
			}

			retval.Add(new Round { blue = blue, red = red, green = green });
        }

        return retval;
	}

    public struct Round
	{
		public int blue;
		public int green; 
		public int red;
	}
}

namespace AdventOfCode._2022.Day01;

public class Solution : ISolution
{
	public void Puzzle1(string[] input)
	{
		var mostCals = 0;
		var curr = 0;
		
		foreach (var i in input)
		{
			if (i == "")
			{
				if (curr > mostCals)
				{
					mostCals = curr;
				}
				curr = 0;
				continue;
			}
			curr += int.Parse(i);
		}

		if (curr > mostCals)
		{
			mostCals = curr;
		}
		
		Console.WriteLine(mostCals);
	}

	public void Puzzle2(string[] input)
	{
		var elves = new List<int>() { 0 };

		foreach (var i in input)
		{
			if (i == "")
			{
				elves.Add(0);
				continue;
			}
			elves[^1] += int.Parse(i);
		}
		
		var total = elves
			.OrderDescending()
			.Take(3)
			.Sum();
		
		Console.WriteLine(total);
	}
}

namespace AdventOfCode._2023.Day11;

public class Solution : ISolution
{
	public void Puzzle1(string[] input)
	{
		var universe = ExpandUniverse(input);
		var galaxies = GetGalaxyCoords(universe);
        long total = 0;

		for (var i = 0; i < galaxies.Count; i++)
		{
			for (var j = i; j < galaxies.Count; j++)
			{
				total += Math.Abs(galaxies[i].Item1 - galaxies[j].Item1) + Math.Abs(galaxies[i].Item2 - galaxies[j].Item2);
			}
		}

		Console.WriteLine(total);
    }

	public void Puzzle2(string[] input)
	{
        var universe = ExpandUniverse(input);
        var galaxies = GetGalaxyCoords(universe, 1000000);
        long total = 0;

        for (var i = 0; i < galaxies.Count; i++)
        {
            for (var j = i; j < galaxies.Count; j++)
            {
                total += Math.Abs(galaxies[i].Item1 - galaxies[j].Item1) + Math.Abs(galaxies[i].Item2 - galaxies[j].Item2);
            }
        }

        Console.WriteLine(total);
    }

	private static List<List<char>> ExpandUniverse(string[] input)
	{
		var retval = new List<List<char>>();

		foreach (var item in input)
		{
			retval.Add(new List<char>(item));
		}

		for (var i = 0; i < retval[0].Count; i++)
		{
			var expandCol = !retval.Select(x => x[i]).Where(x => x == '#').Any();
			if (expandCol)
			{
				for (var j = 0; j < retval.Count; j++)
				{
					retval[j][i] = 'x';
				}
			}
		}

        for (var i = 0; i < retval.Count; i++)
        {
			var expandRow = !retval[i].Where(x => x == '#').Any();
            if (expandRow)
            {
                for (var j = 0; j < retval[i].Count; j++)
                {
					retval[i][j] = 'x';
                }
            }
        }

        return retval;
	}

	private static List<(long, long)> GetGalaxyCoords(List<List<char>> universe, int count = 2)
	{
		var retval = new List<(long, long)>();
		long y = 0;
		for (var i = 0; i < universe.Count; i++)
		{
            long x = 0;

            if (universe[i][0] == 'x')
            {
                y += count;
            }
            else
            {
                for (var j = 0; j < universe[i].Count; j++)
                {
                    if (universe[i][j] == 'x')
                    {
                        x += count;
                        continue;
                    }
                    if (universe[i][j] == '#')
                    {
                        retval.Add((y, x));
                    }
                    x++;
                }
                y++;
            }
            
        }

        return retval;
	}
}

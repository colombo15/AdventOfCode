namespace AdventOfCode._2024.Day11;

public class Solution : ISolution
{
	public void Puzzle1(string[] input)
	{
		Console.WriteLine(input[0].Split(' ').Select(x => Explode(long.Parse(x), 25)).Sum());
	}

	public void Puzzle2(string[] input)
	{
        Console.WriteLine(input[0].Split(' ').Select(x => Explode(long.Parse(x), 75)).Sum());
    }

	private static bool IsEvenDigits(long number) => number.ToString().Length % 2 == 0;
	private readonly Dictionary<(long, long), long> _cache = [];
	private long Explode(long rock, long count)
	{
        if (count == 0)
        {
            return 1;
        }
		if (_cache.TryGetValue((rock, count), out long value))
		{
            return value;
		}
        if (rock == 0)
        {
            var retval = Explode(1, count - 1);
            _cache.TryAdd((rock, count), retval);
            return retval;
        }
        else if (IsEvenDigits(rock))
        {
            var str = rock.ToString();
			var retval = Explode(long.Parse(str[..(str.Length / 2)]), count - 1) + Explode(long.Parse(str[(str.Length / 2)..]), count - 1);
			_cache.TryAdd((rock, count), retval);
            return retval;
        }
        else
        {
			var retval = Explode(rock * 2024, count - 1);
            _cache.TryAdd((rock, count), retval);
            return retval;
        }
    }
}

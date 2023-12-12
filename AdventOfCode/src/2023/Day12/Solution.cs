namespace AdventOfCode._2023.Day12;

public partial class Solution : ISolution
{
	private string _springs = "";
	private int[] _damaged = Array.Empty<int>();
    private Dictionary<(int, int, int), long> _cache = new Dictionary<(int, int, int), long>();

	public void Puzzle1(string[] input)
	{
		long total = 0;
        foreach (var item in input)
		{
			var split = item.Split(' ');
			_springs = split[0];
			_damaged = split[1].Split(',').Select(int.Parse).ToArray();
			_cache.Clear();
			total += GetCount(0, 0, 0);
        }
		Console.WriteLine(total);
	}

	public void Puzzle2(string[] input)
	{
        long total = 0;
        foreach (var item in input)
        {
            var split = item.Split(' ');
            _springs = split[0] + '?' + split[0] + '?' + split[0] + '?' + split[0] + '?' + split[0];

            var arr = split[1].Split(',').Select(int.Parse);
            var myList = new List<int>();
            myList.AddRange(arr);
            myList.AddRange(arr);
            myList.AddRange(arr);
            myList.AddRange(arr);
            myList.AddRange(arr);
            _damaged = myList.ToArray();

            _cache.Clear();
            total += GetCount(0, 0, 0);
        }
        Console.WriteLine(total);
    }

	private long GetCount(int pos, int damagedPos, int damagedCount)
    {
        long retval = 0;

		if (_cache.ContainsKey((pos, damagedPos, damagedCount)))
		{
			return _cache[(pos, damagedPos, damagedCount)];
		}

		if (damagedPos >= _damaged.Length)
		{
			if (pos < _springs.Length && _springs[pos..].Contains('#'))
			{
                return 0;
			}
            return 1;
        }

		if (_damaged[damagedPos] == damagedCount && (pos >= _springs.Length || _springs[pos] == '?' || _springs[pos] == '.'))
		{
            retval += GetCount(pos + 1, damagedPos + 1, 0);
        }
		else if (pos >= _springs.Length)
		{
            return retval;
		}
		else if (_springs[pos] == '#')
		{
            retval += GetCount(pos + 1, damagedPos, damagedCount + 1);
        }
        else if (_springs[pos] == '.')
        {
			if (damagedCount > 0)
			{
                return retval;
			}
            retval += GetCount(pos + 1, damagedPos, damagedCount);
        }
        else if (_springs[pos] == '?')
        {
            retval += GetCount(pos + 1, damagedPos, damagedCount + 1);

            if (damagedCount > 0)
            {
                return retval;
            }
            retval += GetCount(pos + 1, damagedPos, damagedCount);
        }

		_cache.TryAdd((pos, damagedPos, damagedCount), retval);
        return retval;
    }
}

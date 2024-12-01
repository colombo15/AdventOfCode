namespace AdventOfCode._2024.Day01;

public class Solution : ISolution
{
	public void Puzzle1(string[] input)
	{
		var total = 0;
		var l1 = new int[input.Length];
		var l2 = new int[input.Length];

		for(var i = 0; i < input.Length; i++)
		{
			var split = input[i].Split("   ").Select(int.Parse).ToArray();
			l1[i] = split[0];
			l2[i] = split[1];
		}

		Array.Sort(l1);
		Array.Sort(l2);

		for (var i = 0; i < l1.Length; i++)
		{
			total += Math.Abs(l1[i] - l2[i]);
		}

		Console.WriteLine(total);
	}

	public void Puzzle2(string[] input)
	{
        var total = 0;
        var l1 = new int[input.Length];
        var l2 = new Dictionary<int, int>();

        for (var i = 0; i < input.Length; i++)
        {
            var split = input[i].Split("   ").Select(int.Parse).ToArray();
            l1[i] = split[0];
			if (!l2.TryAdd(split[1], 1))
			{
				l2[split[1]]++;
            }
        }

        for (var i = 0; i < l1.Length; i++)
        {
			if (l2.TryGetValue(l1[i], out var val))
			{
                total += l1[i] * val;
            }
        }

        Console.WriteLine(total);
    }
}

namespace AdventOfCode._2024.Day05;

public class Solution : ISolution
{
	public void Puzzle1(string[] input)
	{
        var (rules, updates) = ParseInput(input);
        var total = 0;
		foreach (var update in updates)
        {
            if (IsCorrectOrder(update, rules))
            {
                total += update[(int)Math.Floor(update.Length / 2.0)];
            }
        }
		Console.WriteLine(total);
	}

	public void Puzzle2(string[] input)
	{
        var (rules, updates) = ParseInput(input);
        var total = 0;
        foreach (var update in updates)
        {
            if (!IsCorrectOrder(update, rules))
            {
                total += FixOrder(update, rules)[(int)Math.Floor(update.Length / 2.0)];
            }
        }
        Console.WriteLine(total);
    }

    private static bool IsCorrectOrder(int[] update, Dictionary<int, HashSet<int>> rules)
    {
        for (var i = 1; i < update.Length; i++)
        {
            for(var j = 0; j < i; j++)
            {
                if (rules.TryGetValue(update[i], out HashSet<int>? value) && value.Contains(update[j]))
                {
                    return false;
                }
            }
        }
        return true;
    }

    private static int[] FixOrder(int[] update, Dictionary<int, HashSet<int>> rules)
    {
        var newArray = new int[update.Length];
        Array.Copy(update, newArray, update.Length);

        for (var i = 1; i < newArray.Length; i++)
        {
            for (var j = 0; j < i; j++)
            {
                if (rules.TryGetValue(newArray[i], out HashSet<int>? value) && value.Contains(newArray[j]))
                {
                    var temp = newArray[i];
                    newArray[i] = newArray[j];
                    newArray[j] = temp;
                }
            }
        }
        return newArray;
    }

    private static (Dictionary<int, HashSet<int>>, List<int[]>) ParseInput(string[] input)
	{
        var rules = new Dictionary<int, HashSet<int>>();
        var updates = new List<int[]>();
        var flip = false;
        foreach (var i in input)
        {
            if (!flip)
            {
                if (i == "")
                {
                    flip = true;
                    continue;
                }
                var split = i.Split('|').Select(int.Parse).ToArray();
                if (rules.TryGetValue(split[0], out HashSet<int>? value))
                {
                    value.Add(split[1]);
                }
                else
                {
                    rules.Add(split[0], [split[1]]);
                }
            }
            else
            {
                updates.Add(i.Split(',').Select(int.Parse).ToArray());
            }
        }
        return (rules, updates);
    }
}

namespace AdventOfCode._2024.Day02;

public class Solution : ISolution
{
	public void Puzzle1(string[] input)
	{
		var totalGoodReports = 0;
		foreach (var report in input)
		{
			var split = report.Split(' ').Select(int.Parse).ToArray();
			if (split[0] > split[^1])
			{
				var good = true;
				for (var i = 0; i < split.Length - 1; i++)
				{
					if (split[i] <= split[i + 1] || split[i] - split[i + 1] > 3)
					{
						good = false;
						break;
					}
				}
				if (good)
				{
					totalGoodReports++;
				}
			}
			else if (split[0] < split[^1])
			{
                var good = true;
                for (var i = 0; i < split.Length - 1; i++)
                {
                    if (split[i] >= split[i + 1] || split[i + 1] - split[i] > 3)
                    {
                        good = false;
                        break;
                    }
                }
                if (good)
                {
                    totalGoodReports++;
                }
            }
        }
        Console.WriteLine(totalGoodReports);
    }

	public void Puzzle2(string[] input)
	{
        var totalGoodReports = 0;
        foreach (var report in input)
        {
            if (IsGood(report))
            {
                totalGoodReports++;
            }
        }
        Console.WriteLine(totalGoodReports);
    }

	public bool IsGood(string report)
	{
        var split = report.Split(' ').Select(int.Parse).ToList();
        if (split[0] > split[^1])
        {
            for (var i = 0; i < split.Count - 1; i++)
            {
                if (split[i] <= split[i + 1] || split[i] - split[i + 1] > 3)
                {
                    var l1 = new List<int>(split);
                    var l2 = new List<int>(split);
                    l1.RemoveAt(i);
                    l2.RemoveAt(i + 1);
                    return IsGood(l1) || IsGood(l2);
                }
            }
            return true;
        }
        else if (split[0] < split[^1])
        {
            for (var i = 0; i < split.Count - 1; i++)
            {
                if (split[i] >= split[i + 1] || split[i + 1] - split[i] > 3)
                {
                    var l1 = new List<int>(split);
                    var l2 = new List<int>(split);
                    l1.RemoveAt(i);
                    l2.RemoveAt(i + 1);
                    return IsGood(l1) || IsGood(l2);
                }
            }
            return true;
        }
        return false;
    }

    public bool IsGood(List<int> report)
    {
        var split = report;
        var good = true;
        if (split[0] > split[^1])
        {
            for (var i = 0; i < split.Count - 1; i++)
            {
                if (split[i] <= split[i + 1] || split[i] - split[i + 1] > 3)
                {
                    good = false;
                    break;
                }
            }
            return good;
        }
        else if (split[0] < split[^1])
        {
            for (var i = 0; i < split.Count - 1; i++)
            {
                if (split[i] >= split[i + 1] || split[i + 1] - split[i] > 3)
                {
                    good = false;
                    break;
                }
            }
            return good;
        }
        return false;
    }
}

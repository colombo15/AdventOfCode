namespace AdventOfCode._2023.Day09;

public class Solution : ISolution
{
	public void Puzzle1(string[] input)
	{
		var total = 0;
		foreach (var item in input)
		{
			var nums = item.Split(' ').Select(int.Parse).ToArray();
			total += GetNextValue(nums);
		}
		Console.WriteLine(total);
	}

	public void Puzzle2(string[] input)
	{
        var total = 0;
        foreach (var item in input)
        {
            var nums = item.Split(' ').Select(int.Parse).ToArray();
            total += GetNextValueBackwards(nums);
        }
        Console.WriteLine(total);
    }

	private static int GetNextValue(int[] nums)
	{
		foreach (var item in nums)
		{
			if (item != 0)
			{
				var newNums = new int[nums.Length - 1];
				for (var i = 1; i < nums.Length; i++)
				{
					newNums[i - 1] = nums[i] - nums[i - 1];
				}
				return nums.Last() + GetNextValue(newNums);
			}
		}
		return 0;
	}

    private static int GetNextValueBackwards(int[] nums)
    {
        foreach (var item in nums)
        {
            if (item != 0)
            {
                var newNums = new int[nums.Length - 1];
                for (var i = 1; i < nums.Length; i++)
                {
                    newNums[i - 1] = nums[i] - nums[i - 1];
                }
                return nums.First() - GetNextValueBackwards(newNums);
            }
        }
        return 0;
    }
}

namespace AdventOfCode._2022.Day03;

public class Solution : ISolution
{
	public void Puzzle1(string[] input)
	{
		var total = 0;
		
		// ReSharper disable once LoopCanBeConvertedToQuery
		foreach (var t in input)
		{
			var count = t.Length / 2;
			var container1 = t[..count].ToHashSet();

			// ReSharper disable once ForeachCanBePartlyConvertedToQueryUsingAnotherGetEnumerator
			foreach (var c in t[count..])
			{
				// ReSharper disable once InvertIf
				if (container1.Contains(c))
				{
					var iChar = (int)c;
					total += iChar >= 97 ? iChar - 96 : iChar - 38;
					break;
				}
			}
		}
		
		Console.WriteLine(total);
	}

	public void Puzzle2(string[] input)
	{
		var total = 0;
		
		for (var i = 0; i < input.Length; i += 3)
		{
			var hs1 = input[i].ToHashSet();
			var hs2 = new HashSet<char>();
			
			// ReSharper disable once ForeachCanBePartlyConvertedToQueryUsingAnotherGetEnumerator
			foreach (var in2 in input[i + 1])
			{
				if (hs1.Contains(in2))
				{
					hs2.Add(in2);
				}
			}
			
			// ReSharper disable once ForeachCanBePartlyConvertedToQueryUsingAnotherGetEnumerator
			foreach (var in3 in input[i + 2])
			{
				if (hs2.Contains(in3))
				{
					var iChar = (int)in3;
					total += iChar >= 97 ? iChar - 96 : iChar - 38;
					break;
				}
			}
		}

		Console.WriteLine(total);
	}
}

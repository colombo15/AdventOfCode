using System.Text.RegularExpressions;

namespace AdventOfCode._2023.Day04;

public partial class Solution : ISolution
{
	public void Puzzle1(string[] input)
	{
		var total = 0;

		foreach (var item in input)
		{
			var winners = new HashSet<string>();
			var matches = Nums().Matches(item);
			var checkWin = true;
			var score = 0;

            foreach (var match in matches.Skip(1))
			{
				if (checkWin && match.Value != "|")
				{
                    winners.Add(match.Value);
                }
				else if (checkWin && match.Value == "|")
				{
					checkWin = false;
				}
				else
				{
					if (winners.Contains(match.Value))
					{
						score = score == 0 ? 1 : score * 2;
					}
				}
			}
			
			total += score;
		}

		Console.WriteLine(total);
	}

	public void Puzzle2(string[] input)
    {
        var scores = new int[input.Length];
        var cards = Enumerable.Repeat(1, input.Length).ToArray();

        for (var i = 0; i < input.Length; i++)
        {
            var winners = new HashSet<string>();
            var matches = Nums().Matches(input[i]);
            var checkWin = true;
            var score = 0;

            foreach (var match in matches.Cast<Match>().Skip(1))
            {
                if (checkWin && match.Value != "|")
                {
                    winners.Add(match.Value);
                }
                else if (checkWin && match.Value == "|")
                {
                    checkWin = false;
                }
                else
                {
                    if (winners.Contains(match.Value))
                    {
                        score++;
                    }
                }
            }

            scores[i] = score;
        }

        for (var i = 0; i < cards.Length; i++)
        {
            for (var k = 1; k <= scores[i]; k++)
            {
                cards[i + k] += cards[i];
            }
        }

        Console.WriteLine(cards.Sum());
    }

    [GeneratedRegex("[0-9]+|[|]")]
    private static partial Regex Nums();
}

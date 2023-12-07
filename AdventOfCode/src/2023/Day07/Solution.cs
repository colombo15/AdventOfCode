namespace AdventOfCode._2023.Day07;

public class Solution : ISolution
{
	public void Puzzle1(string[] input)
	{
		var total = 0;
		var hands = new List<Hand>();
		foreach (var i in input.Select(x => x.Split(' ')))
		{
			hands.Add(new Hand(i[0], i[1]));
		}
		hands.Sort(new HandComparer());
		for (var i = 0; i < hands.Count; i++)
		{
			total += (i + 1) * hands[i].Bid;
		}
		Console.WriteLine(total);
	}

	public void Puzzle2(string[] input)
	{
        var total = 0;
        var hands = new List<Hand>();
        foreach (var i in input.Select(x => x.Split(' ')))
        {
            hands.Add(new Hand(i[0], i[1], true));
        }
        hands.Sort(new HandComparer());
        for (var i = 0; i < hands.Count; i++)
        {
            total += (i + 1) * hands[i].Bid;
        }
        Console.WriteLine(total);
    }

    public class HandComparer : Comparer<Hand>
    {
        public override int Compare(Hand? x, Hand? y)
        {
			if (x == null || y == null) throw new NullReferenceException();
			if (x.Type == y.Type)
			{
                for (var i = 0; i < x.Cards.Length; i++)
                {
                    if (x.Cards[i] != y.Cards[i])
                    {
                        return CardRank(x.Cards[i]).CompareTo(CardRank(y.Cards[i]));
                    }
                }
            }
            return x.Type.CompareTo(y.Type);
        }

        private static int CardRank(char c)
		{
            return c switch
            {
                'A' => 13,
                'K' => 12,
                'Q' => 11,
                'J' => 10,
                'T' => 9,
                '9' => 8,
                '8' => 7,
                '7' => 6,
                '6' => 5,
                '5' => 4,
                '4' => 3,
                '3' => 2,
                '2' => 1,
                _ => 0
            };
        }
    }

    public record class Hand
	{
		public string Cards { get; private set; }
		public int Bid { get; private set; }
		public int Type { get; private set; }

		public Hand(string cards, string bid, bool part2 = false)
		{
			Cards = cards;
			Bid = int.Parse(bid);

			var cardDict = new Dictionary<char, int>();
            var jokers = 0;
			foreach (var card in cards)
			{
                if (part2 && card == 'J')
                {
                    jokers++;
                }
                else
                {
                    cardDict.TryAdd(card, 0);
                    cardDict[card]++;
                }
			}
			var vals = cardDict.Values.OrderByDescending(x => x).ToArray();

            if (part2)
            {
                if (vals.Length == 0)
                {
					vals = new int[1];
                }
                vals[0] += jokers;
                Cards = Cards.Replace('J', 'X');
            }

			if (vals[0] == 5)
			{
				Type = 7;
			}
			else if (vals[0] == 4)
			{
				Type = 6;
			}
			else if (vals[0] == 3 && vals[1] == 2)
			{
				Type = 5;
			}
			else if (vals[0] == 3)
			{
				Type = 4;
			}
			else if (vals[0] == 2 && vals[1] == 2)
			{
                Type = 3;
            }
            else if (vals[0] == 2)
            {
                Type = 2;
            }
            else
            {
                Type = 1;
            }
        }
	}
}

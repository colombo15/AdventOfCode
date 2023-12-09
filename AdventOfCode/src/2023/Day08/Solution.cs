using System.Text.RegularExpressions;

namespace AdventOfCode._2023.Day08;

public partial class Solution : ISolution
{
	public void Puzzle1(string[] input)
	{
		var nodes = new Dictionary<string, Node>();
		var pos = "AAA";
		var rl = input[0];
		var index = 0;
		var steps = 0;

		for (var i = 2 ; i < input.Length; i++)
		{
            var matches = MyRegex().Matches(input[i]).Select(x => x.Value).ToArray();
			nodes.Add(matches[0], new Node(matches[0], matches[1], matches[2]));
		}

		while (pos != "ZZZ")
		{
			steps++;

			if (rl[index] == 'L')
			{
				pos = nodes[pos].Left;
            }
            else
			{
                pos = nodes[pos].Right;
            }

            index++;
			if (index > rl.Length - 1)
			{
				index = 0;
			}
        }

		Console.WriteLine(steps);
	}

	public void Puzzle2(string[] input)
	{
        var nodes = new Dictionary<string, Node>();
        var rl = input[0];
        var index = 0;
        var steps = 0;

		var startNodes = new List<string>();
		var endNodes = new HashSet<string>();

        for (var i = 2; i < input.Length; i++)
        {
            var matches = MyRegex().Matches(input[i]).Select(x => x.Value).ToArray();
			var node = new Node(matches[0], matches[1], matches[2]);

            nodes.Add(matches[0], node);
			if (node.Element[2] == 'A')
			{
				startNodes.Add(node.Element);
			}
			else if (node.Element[2] == 'Z')
			{
				endNodes.Add(node.Element);
			}
        }

		var recording = new(string, int, int, int)[startNodes.Count];

        while (!IsComplete(recording))
        {
            steps++;

            for (var i = 0; i < startNodes.Count; i++)
            {
                if (rl[index] == 'L')
                {
                    startNodes[i] = nodes[startNodes[i]].Left;
                }
                else
                {
                    startNodes[i] = nodes[startNodes[i]].Right;
                }
				if (startNodes[i][2] == 'Z')
				{
                    RecordValues(recording, i, startNodes[i], steps, index);
                }
            }

            index++;
            if (index > rl.Length - 1)
            {
                index = 0;
            }
        }

        ulong total = 1;
		foreach (var node in recording)
		{
			total *= (ulong)node.Item2;
        }

		Console.WriteLine(lcm(recording[0].Item2, lcm(recording[1].Item2, lcm(recording[2].Item2, lcm(recording[3].Item2, lcm(recording[4].Item2, recording[5].Item2))))));

        static bool IsComplete((string, int, int, int)[] recording)
		{
			foreach (var node in recording)
			{
				if (node.Item3 == 0)
				{
					return false;
				}
			}
			return true;
		}

		static void RecordValues((string, int, int, int)[] recording, int index, string node, int steps, int rlIndex)
		{
			if (recording[index].Item1 is null)
			{
				recording[index].Item1 = node;
                recording[index].Item2 = steps;
				recording[index].Item4 = rlIndex;
            }
			else if (recording[index].Item3 == 0)
			{
				recording[index].Item3 = steps - recording[index].Item2;
			}
        }

        static long gcf(long a, long b)
        {
            while (b != 0)
            {
                long temp = b;
                b = a % b;
                a = temp;
            }
            return a;
        }

        static long lcm(long a, long b)
        {
            return (a / gcf(a, b)) * b;
        }
    }

	public record class Node
	{
		public string Element { get; private set; }
		public string Left { get; private set; }
		public string Right { get; private set; }

		public Node(string element, string left, string right)
		{
			Element = element;
			Left = left;
			Right = right;
		}
	}

    [GeneratedRegex("[A-Z]{3}")]
    private static partial Regex MyRegex();
}

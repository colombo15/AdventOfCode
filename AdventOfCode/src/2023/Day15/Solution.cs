using System.Reflection.Emit;
using static AdventOfCode._2023.Day15.Solution;

namespace AdventOfCode._2023.Day15;

public class Solution : ISolution
{
	public void Puzzle1(string[] input)
	{
		var total = input[0].Split(',').Select(ToAsciiString).Sum();
		Console.WriteLine(total);
	}

	public void Puzzle2(string[] input)
	{
		var instructions = input[0].Split(',').Select(ToCommand).ToArray();
		var boxes = InitBoxArray();
		var mirrors = new Dictionary<string, Mirror>();

		foreach (var opp in instructions)
		{
			if (opp.opperation == '-')
			{
				boxes[opp.box].Remove(opp.label);
			}
			else if (opp.opperation == '=')
			{
				var newMirror = new Mirror() { label = opp.label, type = opp.focalLength };

                if (boxes[opp.box].Exists(opp.label))
				{
                    boxes[opp.box].Swap(newMirror);
                }
				else
				{
                    boxes[opp.box].Add(newMirror);
                }
            }
		}

		long total = 0;
		foreach (var box in boxes)
		{
			total += box.CalcFocusingPower();
		}

        Console.WriteLine(total);
	}

	private static int ToAsciiString(string input)
	{
		var retval = 0;
		foreach (var c in input)
		{
			retval += c;
			retval *= 17;
			retval %= 256;
		}
		return retval;
	}

	public class Box
	{
		public Box (int boxNumber)
		{
            _boxNumber = boxNumber;
        }

		private int _boxNumber;
		public List<Mirror> mirrors = new();
		private readonly Dictionary<string, int> mirrorsHS = new();
		private readonly HashSet<string> labels = new();

		public void Remove(string label)
		{
			if (Exists(label))
			{
                mirrors.RemoveAt(mirrorsHS[label]);
                labels.Remove(label);
                ReKey();
            }
        }

		public void Add(Mirror m)
		{
			mirrors.Add(m);
			labels.Add(m.label);
			mirrorsHS.Add(m.label, mirrors.Count - 1);
		}

		public bool Exists(string label)
		{
			return labels.Contains(label);
		}

		private void ReKey()
		{
			mirrorsHS.Clear();
			for (var i = 0; i < mirrors.Count; i++)
			{
				mirrorsHS.Add(mirrors[i].label, i);
			}
		}

		public void Swap(Mirror m)
		{
            mirrors[mirrorsHS[m.label]] = m;
        }

		public long CalcFocusingPower()
		{
			long retval = 0;
			for (int i = 0; i < mirrors.Count; i++)
			{
				retval += _boxNumber * (i + 1) * mirrors[i].type;
            }
			return retval;
		}
	}

	public record class Mirror
	{
		public required string label;
		public int type;
	}

	public struct Command
	{
		public string label;
		public int box;
		public char opperation;
		public int focalLength;
	}

	public static Command ToCommand(string input)
	{
		var split = input.Split("=");
		if (split.Length == 2)
		{
			return new Command
			{
				label = split[0],
				box = ToAsciiString(split[0]),
				opperation = '=',
				focalLength = int.Parse(split[1])
			};
		}
		else
		{
            return new Command
            {
				label = split[0][..^1],
                box = ToAsciiString(split[0][..^1]),
                opperation = '-',
                focalLength = 0
            };
        }
	}

    public static Box[] InitBoxArray()
    {
		var retval = new Box[256];
        for (var i = 0; i < retval.Length; i++)
        {
            retval[i] = new Box(i + 1);
        }
		return retval;
    }
}

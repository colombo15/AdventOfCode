using System.Collections.Generic;

namespace AdventOfCode._2024.Day08;

public class Solution : ISolution
{
	public void Puzzle1(string[] input)
	{
        var grid = new Grid(input);
		Console.WriteLine(grid.FindAllAntinodes());
	}

	public void Puzzle2(string[] input)
	{
        var grid = new Grid(input);
        Console.WriteLine(grid.FindAllAntinodes2());
    }

    private class Grid
    {
        private readonly Dictionary<(int x, int y), char> _grid = [];
        private readonly Dictionary<char, List<(int x, int y)>> _antennas = [];

        public Grid(string[] input)
        {
            for (var i = input.Length - 1; i >= 0; i--)
            {
                for (var j = 0; j < input[i].Length; j++)
                {
                    _grid.Add((j, input.Length - 1 - i), input[i][j]);
                    if (input[i][j] != '.' && !_antennas.TryAdd(input[i][j], [(j, input.Length - 1 - i)]))
                    {
                        _antennas[input[i][j]].Add((j, input.Length - 1 - i));
                    }
                }
            }
        }

        public int FindAllAntinodes()
        {
            var antinodes = new HashSet<(int x, int y)>();

            foreach (var antenna in _antennas.Keys)
            {
                for(var i = 0; i < _antennas[antenna].Count; i++)
                {
                    for (var j = i + 1; j < _antennas[antenna].Count; j++)
                    {
                        foreach (var anti in GetAntinodes(_antennas[antenna][i], _antennas[antenna][j]))
                        {
                            if (_grid.ContainsKey(anti))
                            {
                                antinodes.Add(anti);
                            }
                        }
                    }
                }
            }

            return antinodes.Count;
        }

        public int FindAllAntinodes2()
        {
            var antinodes = new HashSet<(int x, int y)>();

            foreach (var antenna in _antennas.Keys)
            {
                for (var i = 0; i < _antennas[antenna].Count; i++)
                {
                    for (var j = i + 1; j < _antennas[antenna].Count; j++)
                    {
                        foreach (var anti in GetAntinodes2(_antennas[antenna][i], _antennas[antenna][j]))
                        {
                            if (_grid.ContainsKey(anti))
                            {
                                antinodes.Add(anti);
                            }
                        }
                    }
                }
            }

            return antinodes.Count;
        }

        private static (int x, int y)[] GetAntinodes((int x, int y) first, (int x, int y) second)
        {
            (int x, int y) antinode1 = (-1, -1);
            (int x, int y) antinode2 = (-1, -1);

            var diffX = Math.Abs(first.x - second.x);
            var diffY = Math.Abs(first.y - second.y);

            antinode1.x = first.x > second.x ? first.x + diffX : first.x - diffX;
            antinode1.y = first.y > second.y ? first.y + diffY : first.y - diffY;

            antinode2.x = first.x > second.x ? second.x - diffX : second.x + diffX;
            antinode2.y = first.y > second.y ? second.y - diffY : second.y + diffY;

            return [antinode1, antinode2];
        }

        private List<(int x, int y)> GetAntinodes2((int x, int y) first, (int x, int y) second)
        {
            var antinodes = new List<(int x, int y)>();

            var diffX = Math.Abs(first.x - second.x);
            var diffY = Math.Abs(first.y - second.y);

            var antinode1 = first;
            var antinode2 = second;

            while(_grid.ContainsKey(antinode1))
            {
                antinodes.Add(antinode1);
                antinode1.x = first.x > second.x ? antinode1.x + diffX : antinode1.x - diffX;
                antinode1.y = first.y > second.y ? antinode1.y + diffY : antinode1.y - diffY;
            }

            while (_grid.ContainsKey(antinode2))
            {
                antinodes.Add(antinode2);
                antinode2.x = first.x > second.x ? antinode2.x - diffX : antinode2.x + diffX;
                antinode2.y = first.y > second.y ? antinode2.y - diffY : antinode2.y + diffY;
            }

            return antinodes;
        }
    }
}

using AdventOfCode.Common;
using System;

namespace AdventOfCode.Puzzles._2022.Day08
{
    internal sealed class Solution : ISolution
    {
        private int _result;

        public void PartOne(string[] input)
        {
            var length = input.Length;
            var grid = new int[length, length];

            for (var i = 0; i < length; i++)
            {
                for (var j = 0; j < input[i].Length; j++)
                {
                    if (i == 0 || i == length - 1 || j == 0 || j == length - 1)
                    {
                        grid[i, j] = -1 * (input[i][j] - 48);
                    }
                    else
                    {
                        grid[i, j] = (input[i][j] - 48);
                    }
                }
            }

            for (var i = 1; i < length - 1; i++)
            {
                var last = grid[i, 0] * -1;
                for (var j = 1; j < length - 1; j++)
                {
                    var val = grid[i, j] > 0 ? grid[i, j] : grid[i, j] * -1;
                    if (val > last)
                    {
                        last = val;

                        if (grid[i, j] > 0)
                            grid[i, j] = grid[i, j] * -1;
                    }
                }
            }

            for (var i = length - 1; i > 0; i--)
            {
                var last = grid[i, length - 1] * -1;
                for (var j = length - 1; j > 0; j--)
                {
                    var val = grid[i, j] > 0 ? grid[i, j] : grid[i, j] * -1;
                    if (val > last)
                    {
                        last = val;

                        if (grid[i, j] > 0)
                            grid[i, j] = grid[i, j] * -1;
                    }
                }
            }

            for (var j = 1; j < length - 1; j++)
            {
                var last = grid[0, j] * -1;
                for (var i = 1; i < length - 1; i++)
                {
                    var val = grid[i, j] > 0 ? grid[i, j] : grid[i, j] * -1;
                    if (val > last)
                    {
                        last = val;

                        if (grid[i, j] > 0)
                            grid[i, j] = grid[i, j] * -1;
                    }
                }
            }

            for (var j = length - 1; j > 0; j--)
            {
                var last = grid[length - 1, j] * -1;
                for (var i = length - 1; i > 0; i--)
                {
                    var val = grid[i, j] > 0 ? grid[i, j] : grid[i, j] * -1;
                    if (val > last)
                    {
                        last = val;

                        if (grid[i, j] > 0)
                            grid[i, j] = grid[i, j] * -1;
                    }
                }
            }

            for (var i = 0; i < length; i++)
            {
                if (grid[i, 0] == 0)
                    grid[i, 0] = -1;

                if (grid[0, i] == 0)
                    grid[0, i] = -1;

                if (grid[i, length - 1] == 0)
                    grid[i, length - 1] = -1;

                if (grid[length - 1, i] == 0)
                    grid[length - 1, i] = -1;
            }

            foreach (var num in grid)
            {
                if (num < 0)
                    _result++;
            }
        }

        public void PartTwo(string[] input)
        {
            var length = input.Length;
            var grid = new int[length, length];
            var highest = 0;

            for (var i = 0; i < length; i++)
            {
                for (var j = 0; j < input[i].Length; j++)
                {
                    grid[i, j] = input[i][j] - 48;
                }
            }

            for (var i = 0; i < length; i++)
            {
                for (var j = 0; j < length; j++)
                {
                    var up = 0;
                    var down = 0;
                    var left = 0;
                    var right = 0;

                    var prev = grid[i, j];
                    for (var index = i - 1; index >= 0; index--)
                    {
                        up++;
                        if (grid[index, j] >= prev) break;
                    }

                    prev = grid[i, j];
                    for (var index = i + 1; index < length; index++)
                    {
                        down++;
                        if (grid[index, j] >= prev) break;
                    }

                    prev = grid[i, j];
                    for (var index = j - 1; index >= 0; index--)
                    {
                        left++;
                        if (grid[i, index] >= prev) break;
                    }

                    prev = grid[i, j];
                    for (var index = j + 1; index < length; index++)
                    {
                        right++;
                        if (grid[i, index] >= prev) break;
                    }

                    var result = up * down * left * right;
                    if (result > highest)
                        highest = result;
                }
            }

            _result = highest;
        }

        public void Print()
        {
            Console.WriteLine(_result);
        }

        public bool IsPartOneCorrect()
        {
            return false;
        }

        public bool IsPartTwoCorrect()
        {
            return false;
        }
    }
}

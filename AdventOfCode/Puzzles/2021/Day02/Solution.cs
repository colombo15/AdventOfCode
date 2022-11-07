using AdventOfCode.Common;
using Iced.Intel;

namespace AdventOfCode.Puzzles._2021.Day02
{
    internal sealed class Solution : ISolution
    {
        private int depth = 0;
        private int pos = 0;
        private int aim = 0;

        public void Reset()
        {
            depth = 0;
            pos = 0;
            aim = 0;
        }

        public void PartOne(string[] input)
        {
            GeneralizedSolution(input, (Cmd x) => PartOneCommands(x));
        }

        public void PartTwo(string[] input)
        {
            GeneralizedSolution(input, (Cmd x) => PartTwoCommands(x));
        }

        private void GeneralizedSolution(string[] input, Action<Cmd> ExecCommand)
        {
            var commands = input.Select(x =>
            {
                var split = x.Split(' ');
                return new Cmd { Name = split[0], Value = int.Parse(split[1]) };
            });

            foreach (var command in commands)
            {
                ExecCommand(command);
            }
        }

        private void PartOneCommands(Cmd command)
        {
            switch (command.Name)
            {
                case "forward":
                    pos += command.Value;
                    break;
                case "up":
                    depth -= command.Value;
                    break;
                case "down":
                    depth += command.Value;
                    break;
            }
        }

        private void PartTwoCommands(Cmd command)
        {
            switch (command.Name)
            {
                case "forward":
                    pos += command.Value;
                    depth += aim * command.Value;
                    break;
                case "up":
                    aim -= command.Value;
                    break;
                case "down":
                    aim += command.Value;
                    break;
            }
        }

        public void Print()
        {
            Console.WriteLine(pos * depth);
        }

        public bool IsPartOneCorrect()
        {
            return pos * depth == 1815044;
        }

        public bool IsPartTwoCorrect()
        {
            return pos * depth == 1739283308;
        }

        private struct Cmd
        {
            public string Name;
            public int Value;
        }
    }
}

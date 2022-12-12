using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Disassemblers;
using BenchmarkDotNet.Running;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Common
{
    [MemoryDiagnoser]
    public class Benchmark
    {
        private List<string[]>? inputs;
        private List<ISolution>? solutions;

        [GlobalSetup]
        public async Task GlobalSetup()
        {
            // Load all inputs
            inputs = new List<string[]>
            {
                await PuzzleInputService.ReadPuzzleInput(2022, 1),
                await PuzzleInputService.ReadPuzzleInput(2022, 2),
                await PuzzleInputService.ReadPuzzleInput(2022, 3),
                await PuzzleInputService.ReadPuzzleInput(2022, 4),
                await PuzzleInputService.ReadPuzzleInput(2022, 5),
                await PuzzleInputService.ReadPuzzleInput(2022, 6),
                await PuzzleInputService.ReadPuzzleInput(2022, 7),
                await PuzzleInputService.ReadPuzzleInput(2022, 8),
            };
        }

        [IterationSetup]
        public void IterationSetup()
        {
            // Create 2 solutions for each day (part 1 and part 2)
            // These don't have constructors so no computation is happening here
            solutions = new List<ISolution>
            {
                new Puzzles._2022.Day01.Solution(),
                new Puzzles._2022.Day01.Solution(),
                new Puzzles._2022.Day02.Solution(),
                new Puzzles._2022.Day02.Solution(),
                new Puzzles._2022.Day03.Solution(),
                new Puzzles._2022.Day03.Solution(),
                new Puzzles._2022.Day04.Solution(),
                new Puzzles._2022.Day04.Solution(),
                new Puzzles._2022.Day05.Solution(),
                new Puzzles._2022.Day05.Solution(),
                new Puzzles._2022.Day06.Solution(),
                new Puzzles._2022.Day06.Solution(),
                new Puzzles._2022.Day07.Solution(),
                new Puzzles._2022.Day07.Solution(),
                new Puzzles._2022.Day08.Solution(),
                new Puzzles._2022.Day08.Solution(),
            };
        }

        [Benchmark]
        public void RunAll()
        {
            // Run all the solutions!
            for (var i = 0; i < solutions.Count; i += 2)
            {
                var index = (int)Math.Floor(i / 2.0);

                solutions[i].PartOne(inputs[index]);
                if (!solutions[i].IsPartOneCorrect())
                {
                    Console.WriteLine("FAIL");
                    break;
                }

                solutions[i + 1].PartTwo(inputs[index]);
                if (!solutions[i + 1].IsPartTwoCorrect())
                {
                    Console.WriteLine("FAIL");
                    break;
                }
            }
        }
    }
}

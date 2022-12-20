using AdventOfCode.Common;
using BenchmarkDotNet.Running;
using System;
using System.Configuration;
using System.Diagnostics;

namespace AdventOfCode
{
    internal static class RunnerService
    {
        public static async Task RunCurrentDayAsync()
        {
            var year = ConfigurationManager.AppSettings.Get("Year");
            var day = ConfigurationManager.AppSettings.Get("Day");
            if (year == null || day == null) return;
            day = int.Parse(day) >= 10 ? $"{day}" : $"0{day}";

            var solutionExecutor = new SolutionExecutor(year, day);
            var input = await PuzzleInputService.ReadPuzzleInput();
            var currentDirectory = Directory.GetParent(Environment.CurrentDirectory)!.Parent!.Parent!.FullName;
            var testFiles = Directory.GetFiles(currentDirectory + $"/Puzzles/{year}/Day{day}/Tests");

            Console.WriteLine($"{year} - Day {day}\n-------------");
            Console.WriteLine("PART ONE");
            try
            {
                for (var i = 0; i < testFiles.Length; i++)
                {
                    var testInput = PuzzleInputService.ReadPuzzleInput(testFiles[i]);
                    solutionExecutor.PartOne(testInput);
                }
                solutionExecutor.PartOne(input);
                Console.WriteLine(solutionExecutor.IsPartOneCorrect() ? "CORRECT" : "WRONG");
            }
            catch (NotImplementedException) { }
            catch (Exception) { throw; }


            Console.WriteLine("-------------");
            Console.WriteLine("PART TWO");
            try
            {
                for (var i = 0; i < testFiles.Length; i++)
                {
                    var testInput = PuzzleInputService.ReadPuzzleInput(testFiles[i]);
                    solutionExecutor.PartTwo(testInput);
                }
                solutionExecutor.PartTwo(input);
                Console.WriteLine(solutionExecutor.IsPartTwoCorrect() ? "CORRECT" : "WRONG");
            }
            catch (NotImplementedException) { }
            catch (Exception) { throw; }

            Console.WriteLine("-------------");
            Console.Read();
        }

        public async static Task RunAllDays()
        {
            // Load all inputs
            var inputs = new List<string[]>
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

            // Create 2 solutions for each day (part 1 and part 2)
            // These don't have constructors so no computation is happening here
            var solutions = new List<ISolution> 
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

            var sw = new Stopwatch();
            sw.Start();

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

            sw.Stop();

            Console.WriteLine(sw.Elapsed.ToString());
            Console.Read();
        }
    }
}

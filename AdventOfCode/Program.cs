using AdventOfCode.Common;
using BenchmarkDotNet.Running;
using System.Configuration;

//BenchmarkRunner.Run<Benchmark>();

var yearSetting = ConfigurationManager.AppSettings.Get("Year");
if (yearSetting == null) throw new ConfigurationErrorsException("Year not provided in app.config");

var daySetting = ConfigurationManager.AppSettings.Get("Day");
if (daySetting == null) throw new ConfigurationErrorsException("Day not provided in app.config");

var year = ConfigurationManager.AppSettings.Get("Year");
var day = ConfigurationManager.AppSettings.Get("Day");
if (year == null || day == null) return;
day = int.Parse(day) > 10 ? $"{day}" : $"0{day}";

var solutionExecutor = new SolutionExecutor(year, day);
var input = await PuzzleInputService.DownloadPuzzleInput();
var currentDirectory = Directory.GetParent(Environment.CurrentDirectory)!.Parent!.Parent!.FullName;
var testFiles = Directory.GetFiles(currentDirectory + $"\\Puzzles\\{year}\\Day{day}\\Tests");

Console.WriteLine($"{year} - Day {day}\n-------------");
Console.WriteLine("PART ONE");
try
{
    for(var i = 0; i < testFiles.Length; i++)
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
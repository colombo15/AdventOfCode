using AdventOfCode.Common;
using BenchmarkDotNet.Running;
using System.Configuration;

//BenchmarkRunner.Run<Benchmark>();

var yearSetting = ConfigurationManager.AppSettings.Get("Year");
if (yearSetting == null) throw new ConfigurationErrorsException("Year not provided in app.config");

var daySetting = ConfigurationManager.AppSettings.Get("Day");
if (daySetting == null) throw new ConfigurationErrorsException("Day not provided in app.config");

var year = int.Parse(yearSetting);
var day = int.Parse(daySetting);
var dayStr = day > 10 ? $"{day}" : $"0{day}";

var assemblyName = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
if (assemblyName == null) throw new NullReferenceException("Assembly Name not found");

var solutionObjHandle = Activator.CreateInstance(assemblyName, $"AdventOfCode.Puzzles._{year}.Day{dayStr}.Solution");
if (solutionObjHandle == null) throw new NullReferenceException($"Solution class not found for {year} - Day{dayStr}");

var solutionObj = solutionObjHandle.Unwrap();
if (solutionObj == null) throw new NullReferenceException("Solution not Unwrapped properly");

var solution = (ISolution)solutionObj;
var input = await PuzzleInputService.DownloadPuzzleInput(year, day);

var solutionExecutor = new SolutionExecutor(solution, input);
var currentDirectory = Directory.GetParent(Environment.CurrentDirectory)!.Parent!.Parent!.FullName;
var testFiles = Directory.GetFiles(currentDirectory + $"\\Puzzles\\{year}\\Day{dayStr}\\Data");

Console.WriteLine($"{year} - Day {dayStr}\n");
Console.WriteLine("PART ONE\n");

try
{
    if (testFiles.Any()) Console.WriteLine("TESTS\n----------");
    for(var i = 0; i < testFiles.Length; i++)
    {
        Console.WriteLine($"Test {i + 1}");
        var testInput = PuzzleInputService.ReadPuzzleInput(testFiles[i]);
        var testExecutor = new SolutionExecutor(solution, testInput);
        testExecutor.PartOne();
        Console.WriteLine();
    }

    Console.WriteLine("RESULT\n----------");
    solutionExecutor.PartOne();
    Console.WriteLine(solutionExecutor.IsPartOneCorrect() ? "CORRECT" : "WRONG");
}
catch (NotImplementedException) { }
catch (Exception) { throw; }


Console.WriteLine("\n\n\nPART TWO\n");

try
{
    if (testFiles.Any()) Console.WriteLine("TESTS\n----------");
    for (var i = 0; i < testFiles.Length; i++)
    {
        Console.WriteLine($"Test {i + 1}");
        var testInput = PuzzleInputService.ReadPuzzleInput(testFiles[i]);
        var testExecutor = new SolutionExecutor(solution, testInput);
        testExecutor.PartTwo();
        Console.WriteLine();
    }

    Console.WriteLine("RESULT\n----------");
    solutionExecutor.PartTwo();
    Console.WriteLine(solutionExecutor.IsPartTwoCorrect() ? "CORRECT" : "WRONG");
}
catch (NotImplementedException) { }
catch (Exception) { throw; }

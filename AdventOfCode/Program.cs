using AdventOfCode;
Console.WriteLine("\n\n");

/*
 * This project is meant to work by simply executing it. It will create all the files it needs to
 * run properly. If an error occurs during init, follow the instructions to resolve.
 *
 * Executing this will create the input file from AoC, it will also generate the Solution cs file.
 */

// Change the execution mode to handle different scenarios
const Mode mode = Mode.Specific;

// When mode is set to Specific, these values will be used
// ReSharper disable once RedundantAssignment
var year = 2022;
// ReSharper disable once RedundantAssignment
var day = 1;

if (mode == Mode.Current)
{
    var curr = DateTime.Now;
    if (curr.Month != 12)
    {
        year = curr.Month != 12 ? curr.Year - 1 : curr.Year;
        day = 25;
    }
    else
    {
        year = curr.Year;
        day = curr.Day > 25 ? 25 : curr.Day;
    }
}

if (!Util.InitProject()) return;
if (!await Util.InitPuzzle(year, day)) return;
var puzzleInput = Util.GetPuzzleInput(year, day);

var assemblyName = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
if (assemblyName == null)
{
    throw new NullReferenceException("Assembly Name not found");
}

var dayStr = day < 10 ? "0" + day : day.ToString();
var solutionObjHandle = Activator.CreateInstance(assemblyName, $"AdventOfCode._{year}.Day{dayStr}.Solution");
if (solutionObjHandle == null)
{
    throw new NullReferenceException($"Solution class not found for {year} - Day{day}");
}

var solutionObj = solutionObjHandle.Unwrap();
if (solutionObj == null)
{
    throw new NullReferenceException("Solution not Unwrapped properly");
}

var solution = (ISolution)solutionObj;
solution.Puzzle1(puzzleInput);
solution.Puzzle2(puzzleInput);
    
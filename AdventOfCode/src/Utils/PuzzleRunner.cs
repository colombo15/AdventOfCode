using System.Diagnostics;

namespace AdventOfCode;

public static class PuzzleRunner
{
    public static async Task Run(Mode mode, int year, int day)
    {
        // When mode = mode.Current, set year/day to the most recent AoC day
        if (mode == Mode.Current)
        {
            var curr = DateTime.Now;
            if (curr.Month == 12)
            {
                year = curr.Year;
                day = curr.Day > 25 ? 25 : curr.Day;
            }
            else
            {
                year = curr.Year - 1;
                day = 25;
            }
        }

        Console.WriteLine($"\n\nPuzzle: {year} Day {day}\n\n");

        if (!Util.InitProject()) return;
        if (!await Util.InitPuzzle(year, day)) return;

        var puzzleInput = Util.GetPuzzleInput(year, day);
        var solution = Util.GetSolution(year, day);

        var timer = new Stopwatch();
        timer.Start();

        solution.Puzzle1(puzzleInput);
        timer.Stop();

        var puzzle1Time = timer.Elapsed;

        timer.Reset();

        timer.Start();
        solution.Puzzle2(puzzleInput);
        timer.Stop();

        var puzzle2Time = timer.Elapsed;

        Console.WriteLine($"\n\nPart 1 time: {puzzle1Time}\nPart 2 time: {puzzle2Time}\n");
    }
}

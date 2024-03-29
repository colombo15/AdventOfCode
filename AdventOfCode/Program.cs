﻿using AdventOfCode;
using System.Diagnostics;

// ReSharper disable HeuristicUnreachableCode
#pragma warning disable CS0162 // Unreachable code detected

Console.WriteLine("\n\n");

/*
 * This project is meant to work by simply executing it. It will create all the files it needs to
 * run properly. If an error occurs during init, follow the instructions to resolve.
 *
 * Executing this will create the input file from AoC, it will also generate the Solution cs file.
 */

// Change the execution mode to handle different scenarios
//const Mode mode = Mode.Current;
const Mode mode = Mode.Current;

// When mode is set to Specific, these values will be used
// ReSharper disable once RedundantAssignment
var year = 2023;
// ReSharper disable once RedundantAssignment
var day = 3;

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
#pragma warning restore CS0162 // Unreachable code detected

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
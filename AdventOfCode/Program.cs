using AdventOfCode;

/*
 * This project is meant to work by simply executing it. It will create all the files it needs to
 * run properly. If an error occurs during init, follow the instructions to resolve.
 *
 * Executing this will create the input file from AoC, it will also generate the Solution cs file.
 */

// Change the execution mode to handle different scenarios
const Mode mode = Mode.Current;

// When mode is set to Mode.Specific, these values will be used
var year = 2023;
var day = 17;

await PuzzleRunner.Run(mode, year, day);
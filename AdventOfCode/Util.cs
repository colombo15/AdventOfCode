using System.Configuration;
using System.Diagnostics.CodeAnalysis;
using System.Net;

namespace AdventOfCode;

[SuppressMessage("ReSharper", "InvertIf")]
public static class Util
{
    private static readonly string? ProjDir = Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.FullName;
    
    /// <summary>
    /// Initializes the project, will not do anything if it has already ran
    /// </summary>
    public static bool InitProject()
    {
        var appConfigPath = $"{ProjDir}/app.config";
        
        // Create app.config if it does not exist
        if (!File.Exists(appConfigPath))
        {
            using var sw = File.AppendText(appConfigPath);
            sw.WriteLine("<?xml version=\"1.0\" encoding=\"utf-8\" ?>");
            sw.WriteLine("<configuration>");
            sw.WriteLine("\t<appSettings>");
            sw.WriteLine("\t\t<add key=\"auth-token\" value=\"\" />");
            sw.WriteLine("\t\t<add key=\"email\" value=\"\" />");
            sw.WriteLine("\t</appSettings>");
            sw.WriteLine("</configuration>");
            Console.WriteLine($"\napp.config file created, please fill it in to continue\n");
            return false;
        }
        
        return true;
    }
    
    /// <summary>
    /// Downloads the puzzle input for the giver year and day
    /// </summary>
    /// <param name="year"></param>
    /// <param name="day"></param>
    public static async Task<bool> InitPuzzle(int year, int day)
    {
        var inputsDir = $"{ProjDir}/inputs";
        // Create /inputs folder if it does not exist
        _ = Directory.CreateDirectory(inputsDir);
        if (!await DownloadPuzzleInput(year, day)) return false;
        
        var srcDir = $"{ProjDir}/src";
        _ = Directory.CreateDirectory(srcDir);
        
        var yearDir = $"{srcDir}/{year}";
        _ = Directory.CreateDirectory(yearDir);
        
        var dayStr = day < 10 ? "0" + day : day.ToString();
        var dayDir = $"{yearDir}/Day{dayStr}";
        _ = Directory.CreateDirectory(dayDir);
        
        var solutionFile = $"{dayDir}/Solution.cs";
        if (!File.Exists(solutionFile))
        {
            await using var sw = File.AppendText(solutionFile);
            await sw.WriteLineAsync($"namespace AdventOfCode._{year}.Day{dayStr};");
            await sw.WriteLineAsync("");
            await sw.WriteLineAsync("public class Solution : ISolution");
            await sw.WriteLineAsync("{");
            await sw.WriteLineAsync("\tpublic void Puzzle1(string[] input)");
            await sw.WriteLineAsync("\t{");
            await sw.WriteLineAsync($"\t\tConsole.WriteLine(\"{year} - Day {dayStr} - Part 1\");");
            await sw.WriteLineAsync("\t}");
            await sw.WriteLineAsync("");
            await sw.WriteLineAsync("\tpublic void Puzzle2(string[] input)");
            await sw.WriteLineAsync("\t{");
            await sw.WriteLineAsync($"\t\tConsole.WriteLine(\"{year} - Day {dayStr} - Part 2\");");
            await sw.WriteLineAsync("\t}");
            await sw.WriteLineAsync("}");
            
            Console.WriteLine($"Solution.cs file created for {year} Day{dayStr}");
            return false;
        }
        
        return true;
    }

    /// <summary>
    /// Creates a string array made of all the lines in the puzzle input file
    /// </summary>
    /// <param name="year"></param>
    /// <param name="day"></param>
    /// <returns></returns>
    public static string[] GetPuzzleInput(int year, int day)
    {
        var dayStr = day < 10 ? "0" + day : day.ToString();
        var inputFile = $"{ProjDir}/inputs/{year}{dayStr}.txt";
        return File.ReadAllText(inputFile).Split('\n');
    }

    public static ISolution GetSolution(int year, int day)
    {
        var assemblyName = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
        if (assemblyName == null)
        {
            throw new NullReferenceException("Assembly Name not found");
        }

        // ReSharper disable once ConditionIsAlwaysTrueOrFalse
        var dayStr = day < 10 ? $"0{day}" : day.ToString();
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

        return (ISolution)solutionObj;
    }
    
    private static async Task<bool> DownloadPuzzleInput(int year, int day)
    {
        if (day is > 25 or < 1) return false;

        var dayStr = day < 10 ? $"0{day}" : day.ToString();
        var inputFile = $"{ProjDir}/inputs/{year}{dayStr}.txt";
        if (File.Exists(inputFile))
        {
            return true;
        }
        
        var authToken = ConfigurationManager.AppSettings["auth-token"];
        if (string.IsNullOrEmpty(authToken))
        {
            Console.WriteLine($"\nERROR: auth-token is null or empty, please fill it in in app.config\n");
            return false;
        }
        
        var email = ConfigurationManager.AppSettings["email"];
        // ReSharper disable once InvertIf
        if (string.IsNullOrEmpty(email))
        {
            Console.WriteLine($"\nERROR: email is null or empty, please fill it in in app.config\n");
            return false;
        }

        var baseAddress = new Uri("https://adventofcode.com/");
        var cookieContainer = new CookieContainer();

        using var handler = new HttpClientHandler();
        handler.CookieContainer = cookieContainer;
        
        using var client = new HttpClient(handler);
        client.BaseAddress = baseAddress;

        cookieContainer.Add(baseAddress, new Cookie("session", authToken));

        client.DefaultRequestHeaders.UserAgent.ParseAdd(
            $"Mozilla/5.0 (+via https://github.com/colombo15/AdventOfCode/ by {email})");

        var response = await client.GetAsync($"{year}/day/{day}/input");
        response.EnsureSuccessStatusCode();

        var input = await response.Content.ReadAsStringAsync();
        input = input.TrimEnd('\n');
        
        File.Create(inputFile).Close();
        await File.WriteAllTextAsync(inputFile, input);

        return true;
    }
}

public enum Mode
{
    Current,
    Specific
}
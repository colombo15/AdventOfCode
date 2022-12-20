using System.Configuration;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http;

namespace AdventOfCode.Common
{
    internal static class PuzzleInputService
    {
        private static readonly string BASE_ADDRESS = "https://adventofcode.com/";

        public static async Task<string[]> ReadPuzzleInput()
        {
            var year = int.Parse(ConfigurationManager.AppSettings.Get("Year")!);
            var day = int.Parse(ConfigurationManager.AppSettings.Get("Day")!);

            var basePath = AppDomain.CurrentDomain.BaseDirectory.Remove(AppDomain.CurrentDomain.BaseDirectory.IndexOf("bin"));
            var dayStr = day < 10 ? "0" + day : day.ToString();
            var filePath = $"{basePath}/Puzzles/{year}/day{dayStr}/input.txt";
            if (!File.Exists(filePath))
            {
                var input = await DownloadPuzzleInput(year, day);
                File.Create(filePath).Close();
                File.WriteAllText(filePath, input);
            }

            return File.ReadAllText(filePath).Split('\n');
        }

        public static async Task<string[]> ReadPuzzleInput(int year, int day)
        {
            var basePath = AppDomain.CurrentDomain.BaseDirectory.Remove(AppDomain.CurrentDomain.BaseDirectory.IndexOf("bin"));
            var dayStr = day < 10 ? "0" + day : day.ToString();
            var filePath = $"{basePath}\\Puzzles\\{year}\\Day{dayStr}\\input.txt";
            if (!File.Exists(filePath))
            {
                var input = await DownloadPuzzleInput(year, day);
                File.Create(filePath).Close();
                File.WriteAllText(filePath, input);
            }

            return File.ReadAllText(filePath).Split('\n');
        }

        public static async Task<string> DownloadPuzzleInput(int year, int day)
        {
            var authToken = ConfigurationManager.AppSettings.Get("AuthToken")!;
            var email = ConfigurationManager.AppSettings.Get("Email")!;

            var baseAddress = new Uri(BASE_ADDRESS);
            var cookieContainer = new CookieContainer();

            using var handler = new HttpClientHandler() { CookieContainer = cookieContainer };
            using var client = new HttpClient(handler) { BaseAddress = baseAddress };

            cookieContainer.Add(baseAddress, new Cookie("session", authToken));

            client.DefaultRequestHeaders.UserAgent.ParseAdd($"Mozilla/5.0 (+via https://github.com/colombo15/AdventOfCode/ by {email})");

            var response = await client.GetAsync($"{year}/day/{day}/input");
            response.EnsureSuccessStatusCode();

            var input = await response.Content.ReadAsStringAsync();

            return input.TrimEnd('\n');
        }

        public static string[] ReadPuzzleInput(string fileName)
        {
            return File.ReadLines(fileName).ToArray();
        }
    }
}

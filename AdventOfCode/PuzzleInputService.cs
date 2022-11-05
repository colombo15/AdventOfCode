using System.Configuration;
using System.Net;

namespace AdventOfCode
{
    internal static class PuzzleInputService
    {
        private static readonly string BASE_ADDRESS = "https://adventofcode.com/";

        public static async Task<string[]> GetPuzzleInput()
        {
            var year = int.Parse(ConfigurationManager.AppSettings.Get("Year")!);
            var day = int.Parse(ConfigurationManager.AppSettings.Get("Day")!);

            return await GetPuzzleInput(year, day);
        }

        private static async Task<string[]> GetPuzzleInput(int year, int day)
        {
            var authToken = ConfigurationManager.AppSettings.Get("AuthToken")!;

            var baseAddress = new Uri(BASE_ADDRESS);
            var cookieContainer = new CookieContainer();

            using var handler = new HttpClientHandler() { CookieContainer = cookieContainer };
            using var client = new HttpClient(handler) { BaseAddress = baseAddress };

            cookieContainer.Add(baseAddress, new Cookie("session", authToken));

            var response = await client.GetAsync($"{year}/day/{day}/input");
            response.EnsureSuccessStatusCode();

            var input = await response.Content.ReadAsStringAsync();
            var retval = input.Split('\n', StringSplitOptions.RemoveEmptyEntries);

            return retval;
        }
    }
}

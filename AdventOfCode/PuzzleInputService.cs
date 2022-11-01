using System.Net;

namespace AdventOfCode
{
    internal static class PuzzleInputService
    {
        private static readonly string _baseAddress = "https://adventofcode.com/";

        public static async Task<IEnumerable<string>> GetPuzzleInput(int year, int day)
        {
            var baseAddress = new Uri(_baseAddress);
            var cookieContainer = new CookieContainer();
            var oauthToken = OAuthSessionToken.AOC_OAUTH_TOKEN;

            using var handler = new HttpClientHandler() { CookieContainer = cookieContainer };
            using var client = new HttpClient(handler) { BaseAddress = baseAddress };

            cookieContainer.Add(baseAddress, new Cookie("session", oauthToken));

            var response = await client.GetAsync($"{year}/day/{day}/input");
            response.EnsureSuccessStatusCode();

            var input = await response.Content.ReadAsStringAsync();
            var retval = input.TrimEnd('\n').Split('\n');

            return retval;
        }
    }
}

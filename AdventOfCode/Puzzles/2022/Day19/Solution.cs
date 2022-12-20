using AdventOfCode.Common;
using System.Text.RegularExpressions;

namespace AdventOfCode.Puzzles._2022.Day19
{
    internal sealed partial class Solution : ISolution
    {
        private int _result;
        public int _time;

        public void PartOne(string[] input)
        {
            var blueprints = new List<Blueprint>();
            var results = new List<(int, int)>();
            _time = 24;

            foreach (var item in input)
            {
                var g = MyRegex().Match(item).Groups;
                blueprints.Add(new Blueprint(g[1].Value, g[2].Value, g[3].Value, g[4].Value, g[5].Value, g[6].Value, g[7].Value));
            }
            
            foreach (var item in blueprints)
            {
                ExecuteBluePrint(item, new Resources());
            }

            void ExecuteBluePrint(Blueprint b, Resources r, int time = 1)
            {
                if (time == _time)
                {
                    results.Add((b.BlueprintId, r.Geode));
                    return;
                }

                var newR = r with
                {
                    Ore = r.Ore + r.OreRobots,
                    Clay = r.Clay + r.ClayRobots,
                    Obsidian = r.Obsidian + r.ObsidianRobots,
                    Clay = r.Clay + r.ClayRobots,
                };

            }
        }

        public void PartTwo(string[] input)
        {
            throw new NotImplementedException();
        }

        private sealed class Blueprint
        {
            public int BlueprintId { get; }
            public int OreRobotCost { get; }
            public int ClayRobotCost { get; }
            public int ObsidianRobotOreCost { get; }
            public int ObsidianRobotClayCost { get; }
            public int GeodeRobotOreCost { get; }
            public int GeodeRobotObsidianCost { get; }

            public Blueprint(
                string blueprintId, 
                string oreRobotCost,
                string clayRobotCost,
                string obsidianRobotOreCost,
                string obsidianRobotClayCost,
                string geodeRobotOreCost,
                string geodeRobotObsidianCost
            ) { 
                BlueprintId = int.Parse(blueprintId);
                OreRobotCost = int.Parse(oreRobotCost);
                ClayRobotCost = int.Parse(clayRobotCost);
                ObsidianRobotOreCost = int.Parse(obsidianRobotOreCost);
                ObsidianRobotClayCost = int.Parse(obsidianRobotClayCost);
                GeodeRobotOreCost = int.Parse(geodeRobotOreCost);
                GeodeRobotObsidianCost = int.Parse(geodeRobotObsidianCost);
            }
        }

        [GeneratedRegex("Blueprint (\\d+): Each ore robot costs (\\d+) ore\\. Each clay robot costs (\\d+) ore\\. Each obsidian robot costs (\\d+) ore and (\\d+) clay\\. Each geode robot costs (\\d+) ore and (\\d+) obsidian\\.")]
        private static partial Regex MyRegex();

        private sealed record class Resources
        {
            public int Ore;
            public int Clay;
            public int Obsidian;
            public int Geode;

            public int OreRobots = 1;
            public int ClayRobots;
            public int ObsidianRobots;
            public int GeodeRobots;

            public int BakingOreRobots;
            public int BakingClayRobots;
            public int BakingObsidianRobots;
            public int BakingGeodeRobots;

            public Resources() { }
        }
    }
}

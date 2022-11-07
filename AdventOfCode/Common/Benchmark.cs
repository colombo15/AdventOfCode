using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Disassemblers;
using BenchmarkDotNet.Running;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Common
{
    [MemoryDiagnoser]
    public class Benchmark
    {
        private string[] input = Array.Empty<string>();

        [GlobalSetup]
        public async Task Setup()
        {
            input = await PuzzleInputService.DownloadPuzzleInput();
        }

        [Benchmark]
        public void For()
        {
            var count = 0;
            var gap = 3;
            var intput_int = input.Select(x => int.Parse(x)).ToArray();

            for (var i = gap; i < intput_int.Length; i++)
            {
                count += intput_int[i - gap] < intput_int[i] ? 1 : 0;
            }
        }

        [Benchmark]
        public void ForEach()
        {
            var count = 0;
            var gap = 3;
            var intput_int = input.Select(x => int.Parse(x)).ToArray();

            for (var i = gap; i < intput_int.Length; i++)
            {
                count += intput_int[i - gap] < intput_int[i] ? 1 : 0;
            }
        }
    }
}

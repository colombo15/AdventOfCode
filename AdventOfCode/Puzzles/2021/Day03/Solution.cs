using AdventOfCode.Common;
using Microsoft.Diagnostics.Runtime.Utilities;
using System.Text;

namespace AdventOfCode.Puzzles._2021.Day03
{
    internal sealed class Solution : ISolution
    {
        private uint _result;

        public void PartOne(string[] input)
        {
            var size = input[0].Length;
            var nums = input.Select(x => x.ConvertBinaryToUInt());
            var mid = nums.Count() % 2 == 0 ? (nums.Count() / 2) + 1 : ((nums.Count() - 1) / 2) + 1;
            var pow2 = new uint[size];
            var common = new int[size];
            uint gamma = 0;
            uint epsilon;

            for (var i = size; i > 0; i--)
            {
                pow2[size - i] = (uint)Math.Pow(2, i);
            }
            
            foreach(var num in nums)
            {
                for (var i = 0; i < pow2.Length; i++)
                {
                    if ((num & pow2[i]) == pow2[i]) common[i]++;
                }
            }

            for(var i = 0; i < common.Length; i++)
            {
                if (common[i] >= mid) gamma += pow2[i];
            }

            epsilon = ~gamma ^ (uint.MaxValue - (uint)Math.Pow(2, size) + 1);
            _result = gamma * epsilon;
        }

        public void PartTwo(string[] input)
        {
            var size = input[0].Length - 1;
            var nums = input.Select(x => x.ConvertBinaryToUInt()).ToList();
            if (nums == null) return;

            var mid = nums.Count % 2 == 0 ? (int)(nums.Count / 2) + 1 : (int)((nums.Count - 1) / 2) + 1;
            var pow2 = new uint[size + 1];

            for (var i = size; i >= 0; i--)
            {
                pow2[size - i] = (uint)Math.Pow(2, i);
            }

            var oxy = new List<uint>(nums);
            var curr = 0;
            while (oxy.Count > 1)
            {
                var common = 0;

                foreach (var num in oxy)
                {
                    if ((num & pow2[curr]) == pow2[curr])
                        common++;
                    else
                        common--;
                }

                if (common >= 0)
                    oxy = oxy.Where(x => (x & pow2[curr]) == pow2[curr]).ToList();
                else
                    oxy = oxy.Where(x => (x & pow2[curr]) != pow2[curr]).ToList();

                curr++;
            }

            var co2 = new List<uint>(nums);
            curr = 0;
            while (co2.Count > 1)
            {
                var common = 0;

                foreach (var num in co2)
                {
                    if ((num & pow2[curr]) == pow2[curr])
                        common++;
                    else
                        common--;
                }

                if (common < 0)
                    co2 = co2.Where(x => (x & pow2[curr]) == pow2[curr]).ToList();
                else
                    co2 = co2.Where(x => (x & pow2[curr]) != pow2[curr]).ToList();

                curr++;
            }

            _result = oxy[0] * co2[0];
        }

        public void Print()
        {
            Console.WriteLine(_result);
        }

        public bool IsPartOneCorrect()
        {
            return _result == 1458194;
        }

        public bool IsPartTwoCorrect()
        {
            return _result == 2829354;
        }
    }
}

using AdventOfCode.Common;
using System.Diagnostics;

namespace AdventOfCode.Puzzles._2022.Day11
{
    internal sealed partial class Solution : ISolution
    {
        private static Monkey[]? _monkeys;
        private long _result;
        private static long _modProduct;

        public void PartOne(string[] input)
        {
            CreateMonkeys(input, true);
            ExecuteRounds(20);
            var highestTwo = _monkeys!.OrderByDescending(x => x._inspectCount).Take(2).ToArray();
            _result = highestTwo[0]._inspectCount * highestTwo[1]._inspectCount;
        }

        public void PartTwo(string[] input)
        {
            CreateMonkeys(input, false);
            ExecuteRounds(10000);
            var highestTwo = _monkeys!.OrderByDescending(x => x._inspectCount).Take(2).ToArray();
            _result = highestTwo[0]._inspectCount * highestTwo[1]._inspectCount;
        }

        private static void CreateMonkeys(string[] input, bool worry)
        {
            var monkeyCount = (input.Length + 2) / 7;
            _monkeys = new Monkey[monkeyCount];

            for (var i = 0; i < monkeyCount; i++)
            {
                var temp = new string[5];
                Array.Copy(input, (i * 7) + 1, temp, 0, 5);
                _monkeys[i] = new Monkey(temp, worry);
            }

            var oppValues = _monkeys.Select(x => x._testDivisible);
            _modProduct = 1;
            foreach (var oppValue in oppValues)
            {
                _modProduct *= (long)oppValue;
            }
            
        }

        private static void ExecuteRounds(int count)
        {
            for (var i = 0; i < count; i++) 
            {
                foreach (var monkey in _monkeys!)
                {
                    monkey.InspectItems();
                }
            }
        }

        private class Monkey
        {
            readonly public Queue<long> _items;
            readonly private char _opperation;
            readonly private int _opperationValue = 1;
            readonly bool _opperationOld;
            readonly public int _testDivisible;
            readonly private int _trueMonkey;
            readonly private int _falseMonkey;
            public long _inspectCount;

            readonly private bool _worry;

            public Monkey(string[] input, bool worry) 
            { 
                var split = input[0][18..].Split(", ");
                _items = new Queue<long>(split.Select(x => long.Parse(x)));
                split = input[1][23..].Split(' ');
                _opperation = split[0][0];
                _opperationOld = split[1] == "old";
                if (!_opperationOld)
                    _opperationValue = int.Parse(split[1]);
                _testDivisible = int.Parse(input[2][21..]);
                _trueMonkey = int.Parse(input[3][29..]);
                _falseMonkey = int.Parse(input[4][30..]);
                _worry = worry;
            }

            public void InspectItems()
            {
                while (_items.Count > 0)
                {
                    _inspectCount++;
                    var curr = _items.Dequeue();
                    curr = Opperation(curr);

                    if (_worry)
                        curr = (long)Math.Floor((decimal)(curr / 3));
                    else
                        curr %= _modProduct;

                    if (curr % _testDivisible == 0)
                        _monkeys![_trueMonkey]._items.Enqueue(curr);
                    else
                        _monkeys![_falseMonkey]._items.Enqueue(curr);
                }
            }

            private long Opperation(long item)
            {
                if (_opperationOld)
                    return item * item;
                else if (_opperation == '+')
                    return item + _opperationValue;
                else if (_opperation == '*')
                    return item * _opperationValue;

                throw new UnreachableException();
            }
        }
    }
}

using AdventOfCode.Common;
using System.Collections.Specialized;
using System.Runtime.InteropServices;

namespace AdventOfCode.Puzzles._2021.Day04
{
    internal sealed class Solution : ISolution
    {
        private int _result;

        public void PartOne(string[] input)
        {
            var draws = (ReadOnlySpan<string>)input[0].Split(',').ToArray();
            var bingoCards = ParseInput(input);
            OrderedDictionary? winner = null;
            var last = 0;

            foreach(var draw in draws)
            {
                foreach(var card in CollectionsMarshal.AsSpan(bingoCards))
                {
                    if (card.Contains(draw)) card[draw] = true;

                    if (IsWinner(card))
                    {
                        winner = card;
                        break;
                    }
                }

                if (winner != null)
                {
                    last = int.Parse(draw);
                    break;
                }
            }

            _result = GetCardValue(winner!, last);
        }

        public void PartTwo(string[] input)
        {
            var draws = (ReadOnlySpan<string>)input[0].Split(',').ToArray();
            var bingoCards = ParseInput(input);
            var winner = new List<OrderedDictionary>();
            var last = 0;

            foreach (var draw in draws)
            {
                foreach (var card in CollectionsMarshal.AsSpan(bingoCards))
                {
                    if (card.Contains(draw)) card[draw] = true;

                    if (IsWinner(card))
                    {
                        winner.Add(card);
                    }
                }

                if (winner.Any())
                {
                    last = int.Parse(draw);
                    winner.ForEach(x => bingoCards.Remove(x));
                    if (!bingoCards.Any()) break;
                    winner.Clear();
                }
            }

            _result = GetCardValue(winner.First(), last);
        }

        public void Print()
        {
            Console.WriteLine(_result);
        }

        public bool IsPartOneCorrect()
        {
            return _result == 38594;
        }

        public bool IsPartTwoCorrect()
        {
            return _result == 21184;
        }

        private static List<OrderedDictionary> ParseInput(string[] input)
        {
            var bingoCards = new List<OrderedDictionary>();

            for (var i = 1; i < input.Length; i++)
            {
                if ((i - 1) % 6 == 0)
                {
                    bingoCards.Add(new OrderedDictionary());
                }

                if (string.IsNullOrEmpty(input[i])) continue;

                var numbers = (ReadOnlySpan<string>)input[i].Split(' ', StringSplitOptions.RemoveEmptyEntries).ToArray();
                foreach (var num in numbers)
                {
                    bingoCards.Last().Add(num, false);
                }
            }

            return bingoCards;
        }

        private static bool IsWinner(OrderedDictionary c)
        {
            return
                (IsStamped(c,  0) && IsStamped(c,  1) && IsStamped(c,  2) && IsStamped(c,  3) && IsStamped(c,  4)) ||
                (IsStamped(c,  5) && IsStamped(c,  6) && IsStamped(c,  7) && IsStamped(c,  8) && IsStamped(c,  9)) ||
                (IsStamped(c, 10) && IsStamped(c, 11) && IsStamped(c, 12) && IsStamped(c, 13) && IsStamped(c, 14)) ||
                (IsStamped(c, 15) && IsStamped(c, 16) && IsStamped(c, 17) && IsStamped(c, 18) && IsStamped(c, 19)) ||
                (IsStamped(c, 20) && IsStamped(c, 21) && IsStamped(c, 22) && IsStamped(c, 23) && IsStamped(c, 24)) ||
                (IsStamped(c,  0) && IsStamped(c,  5) && IsStamped(c, 10) && IsStamped(c, 15) && IsStamped(c, 20)) ||
                (IsStamped(c,  1) && IsStamped(c,  6) && IsStamped(c, 11) && IsStamped(c, 16) && IsStamped(c, 21)) ||
                (IsStamped(c,  2) && IsStamped(c,  7) && IsStamped(c, 12) && IsStamped(c, 17) && IsStamped(c, 22)) ||
                (IsStamped(c,  3) && IsStamped(c,  8) && IsStamped(c, 13) && IsStamped(c, 18) && IsStamped(c, 23)) ||
                (IsStamped(c,  4) && IsStamped(c,  9) && IsStamped(c, 14) && IsStamped(c, 19) && IsStamped(c, 24));
        }

        private static bool IsStamped(OrderedDictionary card, int index)
        {
            return (bool)card[index]!;
        }

        private static int GetCardValue(OrderedDictionary card, int last)
        {
            var retval = 0;
            var keys = new string[card.Count];
            card.Keys.CopyTo(keys, 0);

            for(var i = 0; i < card.Count; i++)
            {
                retval += !IsStamped(card, i) ? int.Parse(keys[i]) : 0;
            }

            return retval * last;
        }
    }
}

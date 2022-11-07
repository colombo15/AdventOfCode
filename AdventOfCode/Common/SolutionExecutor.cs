namespace AdventOfCode.Common
{
    internal sealed class SolutionExecutor
    {
        private readonly ISolution _solution;
        private readonly string[] _input;

        public SolutionExecutor(ISolution solution, string[] input)
        {
            _solution = solution;
            _input = input;
        }

        public void PartOne()
        {
            _solution.Reset();
            _solution.PartOne(_input);
            _solution.Print();
        }

        public void PartTwo()
        {
            _solution.Reset();
            _solution.PartTwo(_input);
            _solution.Print();
        }

        public bool IsPartOneCorrect()
        {
            return _solution.IsPartOneCorrect();
        }

        public bool IsPartTwoCorrect()
        {
            return _solution.IsPartTwoCorrect();
        }
    }
}

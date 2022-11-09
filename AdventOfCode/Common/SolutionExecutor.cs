namespace AdventOfCode.Common
{
    internal sealed class SolutionExecutor
    {
        private ISolution? _solution;
        private readonly string _year;
        private readonly string _day;

        public SolutionExecutor(string year, string day)
        {
            _year = year;
            _day = day;
        }

        public void PartOne(string[] input)
        {
            _solution = CreateSolution();
            _solution.PartOne(input);
            _solution.Print();
        }

        public void PartTwo(string[] input)
        {
            _solution = CreateSolution();
            _solution.PartTwo(input);
            _solution.Print();
        }

        public bool IsPartOneCorrect()
        {
            return _solution != null && _solution.IsPartOneCorrect();
        }

        public bool IsPartTwoCorrect()
        {
            return _solution != null && _solution.IsPartTwoCorrect();
        }

        private ISolution CreateSolution()
        {
            var assemblyName = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
            if (assemblyName == null) throw new NullReferenceException("Assembly Name not found");

            var solutionObjHandle = Activator.CreateInstance(assemblyName, $"AdventOfCode.Puzzles._{_year}.Day{_day}.Solution");
            if (solutionObjHandle == null) throw new NullReferenceException($"Solution class not found for {_year} - Day{_day}");

            var solutionObj = solutionObjHandle.Unwrap();
            if (solutionObj == null) throw new NullReferenceException("Solution not Unwrapped properly");

            return (ISolution)solutionObj;
        }
    }
}

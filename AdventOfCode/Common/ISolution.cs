using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Common
{
    internal interface ISolution
    {
        void PartOne(string[] input);
        void PartTwo(string[] input);
        void Print();
        bool IsPartOneCorrect();
        bool IsPartTwoCorrect();
    }
}

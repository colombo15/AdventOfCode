using AdventOfCode.Common;
using System.Diagnostics;

namespace AdventOfCode.Puzzles._2022.Day10
{
    internal sealed partial class Solution : ISolution
    {
        public long _result;

        public void PartOne(string[] input)
        {
            var cpu = new CatNodeRayTub(1);
            cpu.LoadProgram(input);
            cpu.RunProgram();
            _result = cpu.GetSignalStrength();
        }

        public void PartTwo(string[] input)
        {
            var cpu = new CatNodeRayTub(1, true);
            cpu.LoadProgram(input);
            cpu.RunProgram();
            Console.WriteLine();
        }

        private class CatNodeRayTub
        {
            private long _cycles = 1;
            private long[] _registers;
            private Inst[] _program;

            private long _nexSignalCheck = 20;
            private long _signalStr;

            private long[] _sprite = new long[3];

            private bool _enableDraw;

            public CatNodeRayTub(int registerCount, bool enableDraw = false)
            {
                _registers = Enumerable.Repeat((long)1, registerCount).ToArray();
                _sprite[1] = 1;
                _sprite[2] = 2;
                _enableDraw = enableDraw; 
            }

            public void LoadProgram(string[] input)
            {
                _program = input.Select(
                    x => { 
                        var split = x.Split(' '); 
                        return new Inst { cmd = split[0], val = split.Length > 1 ? int.Parse(split[1]) : 0 }; }
                    ).ToArray();
            }

            public void RunProgram()
            {
                foreach ( var inst in _program )
                {
                    ExecInst(inst);
                }
            }

            public long GetSignalStrength()
            {
                return _signalStr;
            }

            private void ExecInst (Inst ins)
            {
                switch (ins.cmd)
                {
                    case "noop":
                        ExecNoop();
                        break;
                    case "addx":
                        ExecAddX(ins.val, 0);
                        break;
                    default:
                        throw new UnreachableException();
                }
            }

            private void DoCycle()
            {
                Draw();
                _cycles++;
                if (_nexSignalCheck + 1 == _cycles)
                {
                    _signalStr += _nexSignalCheck * _registers[0];
                    _nexSignalCheck += 40;
                }
            }

            private void ExecAddX(int val, int regIndex)
            {
                DoCycle();
                DoCycle();
                _registers[regIndex] += val;
                MoveSprite(val);
            }

            private void ExecNoop()
            {
                DoCycle();
            }

            private void MoveSprite(long ammount)
            {
                for (var i = 0; i < _sprite.Length; i++)
                {
                    _sprite[i] += ammount;
                }
            }

            private void Draw()
            {
                if (!_enableDraw) return;

                if (_cycles % 40 == 1 && _cycles != 1)
                {
                    Console.WriteLine();
                    MoveSprite(40);
                }
                    
                Console.Write(_sprite.Contains(_cycles - 1) ? '#' : ' ');
            }

            private struct Inst
            {
                public string cmd;
                public int val;
            }
        }
    }
}

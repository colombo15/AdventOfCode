using AdventOfCode.Common;

namespace AdventOfCode.Puzzles._2022.Day07
{
    internal sealed class Solution : ISolution
    {
        private long _result;

        public void PartOne(string[] input)
        {
            var root = new TreeNode();
            var curr = root;
            var nodeReferences = new List<TreeNode>() { root };

            BuildTree(input, ref root, ref nodeReferences);

            _result = nodeReferences.Select((x) => x.GetTotalFileSize()).Where((x) => x <= 100000).Sum();
        }

        public void PartTwo(string[] input)
        {
            var root = new TreeNode();
            var curr = root;
            var nodeReferences = new List<TreeNode>() { root };

            BuildTree(input, ref root, ref nodeReferences);

            var fileSizes = nodeReferences.Select((x) => x.GetTotalFileSize()).OrderBy((x) => x);

            var unusedSpace = 70000000 - fileSizes.Last();
            foreach(var size in fileSizes)
            {
                if (unusedSpace + size >= 30000000)
                {
                    _result = size;
                    return;
                }
            }
        }

        private class TreeNode
        {
            public List<TreeNode> Children = new List<TreeNode>();
            public long FileSize = 0;
            public TreeNode Parent;
            public long GetTotalFileSize()
            {
                var result = FileSize;

                foreach (var c in Children)
                {
                    result += c.GetTotalFileSize();
                }

                return result;
            }
        }

        private void BuildTree(string[] input, ref TreeNode root, ref List<TreeNode> nodeReferences)
        {
            var curr = root;

            foreach (var item in input)
            {
                var split = item.Split(' ');
                if (split[1] == "cd")
                {
                    if (split[2] == "..")
                    {
                        curr = curr.Parent;
                    }
                    else
                    {
                        var newNode = new TreeNode();
                        nodeReferences.Add(newNode);
                        newNode.Parent = curr;
                        curr.Children.Add(newNode);
                        curr = newNode;
                    }
                }
                else if (split[1] != "ls" && split[0] != "dir")
                {
                    curr.FileSize += long.Parse(split[0]);
                }
            }
        }

        public void Print()
        {
            Console.WriteLine(_result);
        }

        public bool IsPartOneCorrect()
        {
            return _result == 1449447;
        }

        public bool IsPartTwoCorrect()
        {
            return _result == 8679207;
        }
    }
}

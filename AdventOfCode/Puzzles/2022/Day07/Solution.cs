using AdventOfCode.Common;

namespace AdventOfCode.Puzzles._2022.Day07
{
    internal sealed class Solution : ISolution
    {
        private long _result;

        public void PartOne(string[] input)
        {
            var root = new TreeNode();
            var nodeReferences = new List<TreeNode>() { root };
            var curr = root;

            foreach (var item in input)
            {
                if (item == "$ cd /") continue;

                if (item.StartsWith("$ cd"))
                {
                    if (item.EndsWith(".."))
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
                else if (!item.StartsWith("$ ls") && !item.StartsWith("dir"))
                {
                    curr.FileSize += long.Parse(item.Split(' ').First());
                }
            }

            _result = nodeReferences.Select((x) => x.GetTotalFileSize()).Where((x) => x <= 100000).Sum();
        }

        public void PartTwo(string[] input)
        {
            var root = new TreeNode();
            var nodeReferences = new List<TreeNode>() { root };
            var curr = root;

            foreach (var item in input)
            {
                if (item == "$ cd /") continue;

                if (item.StartsWith("$ cd"))
                {
                    if (item.EndsWith(".."))
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
                else if (!item.StartsWith("$ ls") && !item.StartsWith("dir"))
                {
                    curr.FileSize += long.Parse(item.Split(' ').First());
                }
            }

            var fileSizes = nodeReferences.Select((x) => x.GetTotalFileSize()).OrderBy((x) => x).ToArray();

            var unusedSpace = 70000000 - fileSizes.Last();
            for (var i = 0; i < fileSizes.Length; i++)
            {
                if (unusedSpace + fileSizes[i] >= 30000000)
                {
                    _result = fileSizes[i];
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

        public void Print()
        {
            Console.WriteLine(_result);
        }

        public bool IsPartOneCorrect()
        {
            return false;
        }

        public bool IsPartTwoCorrect()
        {
            return false;
        }
    }
}

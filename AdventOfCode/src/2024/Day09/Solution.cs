namespace AdventOfCode._2024.Day09;

public class Solution : ISolution
{
	public void Puzzle1(string[] input)
	{
		var disk = input[0].Select(x => int.Parse(x.ToString())).ToArray();
        var expandedDisk = ExpandDisk(disk);
        CompactDisk(expandedDisk);
        Console.WriteLine(GetCheckSum(expandedDisk));
	}

	public void Puzzle2(string[] input)
	{
        var disk = input[0].Select(x => int.Parse(x.ToString())).ToArray();
        var diskCombo = ExpandDisk2(disk);
        CompactDisk2(diskCombo);
        Console.WriteLine(GetCheckSum(diskCombo.expandedDisk));
	}

    private long GetCheckSum(List<int> compactedDisk)
    {
        long result = 0;
        for (var i = 0; i < compactedDisk.Count; i++)
        {
            if (compactedDisk[i] != -1)
            {
                result += compactedDisk[i] * i;
            }
        }
        return result;
    }

    private void CompactDisk(List<int> disk)
    {
        var nextFreeIndex = 0;

        for (var i = disk.Count - 1; i >= 0; i--)
        {
            if (disk[i] != -1)
            {
                nextFreeIndex = GetNextFreeIndex(nextFreeIndex);
                if (nextFreeIndex > i) return;
                disk[nextFreeIndex] = disk[i];
                disk[i] = -1;
            }
        }

        int GetNextFreeIndex(int prevIndex)
        {
            for (var i = prevIndex + 1; i < disk.Count; i++)
            {
                if (disk[i] == -1)
                {
                    return i;
                }
            }
            return -1;
        }
    }

    private void CompactDisk2((List<int> expandedDisk, Stack<(int num, List<int> size)> files, List<Queue<int>> freeSpace) diskCombo)
    {
        var (disk, files, freeSpace) = diskCombo;
        while (files.Count > 1)
        {
            var (num, size) = files.Pop();
            foreach (var free in freeSpace)
            {
                if (free.Count >= size.Count && free.Peek() > size[0]) break;
                if (free.Count >= size.Count)
                {
                    for (var i = 0; i < size.Count; i++)
                    {
                        disk[free.Dequeue()] = num;
                        disk[size[i]] = -1;
                    }
                    break;
                }
            }
        }
    }

    private List<int> ExpandDisk(int[] disk)
	{
        var newDisk = new List<int>();
        var currID = 0;
        var idMode = true;

        foreach (var num in disk)
        {
            if (idMode)
            {
                for (var i = 0; i < num; i++)
                {
                    newDisk.Add(currID);
                }
                currID++;
            }
            else
            {
                for (var i = 0; i < num; i++)
                {
                    newDisk.Add(-1);
                }
            }
            idMode = !idMode;
        }

        return newDisk;
    }

    private (List<int> expandedDisk, Stack<(int num, List<int> size)> files, List<Queue<int>> freeSpace) ExpandDisk2(int[] disk)
    {
        var newDisk = new List<int>();
        var freeSpace = new List<Queue<int>>();
        var files = new Stack<(int num, List<int> size)>();
        var currID = 0;
        var idMode = true;

        foreach (var num in disk)
        {
            if (idMode)
            {
                var size = new List<int>();
                for (var i = 0; i < num; i++)
                {
                    size.Add(newDisk.Count);
                    newDisk.Add(currID);
                }
                files.Push((currID, size));
                currID++;
            }
            else
            {
                if (num > 0)
                {
                    freeSpace.Add(new Queue<int>());
                    for (var i = 0; i < num; i++)
                    {
                        freeSpace[^1].Enqueue(newDisk.Count);
                        newDisk.Add(-1);
                    }
                }
            }
            idMode = !idMode;
        }

        return (newDisk, files, freeSpace);
    }
}

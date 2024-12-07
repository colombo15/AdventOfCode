namespace AdventOfCode._2024.Day07;

public class Solution : ISolution
{
	public void Puzzle1(string[] input)
	{
		Console.WriteLine(GetCalibrationResult(ParseInput(input)));
	}

	public void Puzzle2(string[] input)
	{
        Console.WriteLine(GetCalibrationResult2(ParseInput(input)));
    }

	private static long GetCalibrationResult(List<(long test, long[] opps)> equations)
	{
		long result = 0;
		foreach (var eq in equations)
		{
            if (Calc(eq, 1, eq.opps[0]))
			{
                result += eq.test;
            }
        }
		return result;
	}

    private static long GetCalibrationResult2(List<(long test, long[] opps)> equations)
    {
        long result = 0;
        foreach (var eq in equations)
        {
            if (Calc2(eq, 1, eq.opps[0]))
            {
                result += eq.test;
            }
        }
        return result;
    }

    private static bool Calc((long test, long[] opps) equation, int index, long accu)
    {
        if (index >= equation.opps.Length)
        {
            if (accu == equation.test)
            {
                return true;
            }
        }
        else
        {
            if (Calc(equation, index + 1, accu + equation.opps[index]))
            {
                return true;
            }
            if (Calc(equation, index + 1, accu * equation.opps[index]))
            {
                return true;
            }
        }
        return false;
    }

    private static bool Calc2((long test, long[] opps) equation, int index, long accu)
    {
        if (index >= equation.opps.Length)
        {
            if (accu == equation.test)
            {
                return true;
            }
        }
        else
        {
            if (Calc2(equation, index + 1, accu + equation.opps[index]))
            {
                return true;
            }
            if (Calc2(equation, index + 1, accu * equation.opps[index]))
            {
                return true;
            }

            var newEq = new long[equation.opps.Length - index];
            Array.Copy(equation.opps, index, newEq, 0, newEq.Length);
            newEq[0] = Combine(accu, equation.opps[index]);
            if (Calc2(equation, index + 1, newEq[0]))
            {
                return true;
            }
        }
        return false;
    }

    private static long Combine(long first, long second)
    {
        var digits = second.ToString().Length;
        for (var i = 0; i < digits; i++)
        {
            first *= 10;
        }
        return first + second;
    }

    private static List<(long test, long[] opps)> ParseInput(string[] input)
	{
		var result = new List<(long test, long[] opps)>();
        foreach (var i in input)
        {
            var split = i.Split(": ");
            result.Add((long.Parse(split[0]), split[1].Split(' ').Select(long.Parse).ToArray()));
        }
		return result;
    }

	private enum Inst
	{
		Add,
		Mult,
		Combine
	}
}

namespace AdventOfCode2024.Days.Day1;

internal class Part1 : DayPart
{
    //public override bool HasPrecedence => true;
    //public override string InputFile => "Example.txt";
    //public override bool ShouldRejectWhiteSpaceLines => false;

    public override void Run(List<string> input)
    {
        List<int> left = [];
        List<int> right = [];

        foreach (var line in input)
        {
            string[] split = line.Split(" ", StringSplitOptions.RemoveEmptyEntries);
            int l = int.Parse(split[0]);
            int r = int.Parse(split[1]);

            left.Add(l);
            right.Add(r);
        }

        int sum = 0;

        for (int i = 0; i < input.Count; i++)
        {
            int l = TakeMin(left);
            int r = TakeMin(right);

            sum += GetDistance(l, r);
        }

        Console.WriteLine(sum);
    }

    private static int TakeMin(List<int> list)
    {
        if (list.Count == 0)
        {
            throw new Exception();
        }

        int min = list.Min();
        list.Remove(min);

        return min;
    }

    private static int GetDistance(int left, int right)
    {
        return Math.Abs(right - left);
    }
}
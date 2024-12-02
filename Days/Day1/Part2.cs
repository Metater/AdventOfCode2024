namespace AdventOfCode2024.Days.Day1;

internal class Part2 : DayPart
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

        Dictionary<int, int> rightMultiplicity = [];

        foreach (var r in right)
        {
            if (rightMultiplicity.ContainsKey(r))
            {
                rightMultiplicity[r]++;
            }
            else
            {
                rightMultiplicity[r] = 1;
            }
        }

        int simularityScore = 0;

        for (int i = 0; i < input.Count; i++)
        {
            int l = left[i];
            simularityScore += l * rightMultiplicity.GetValueOrDefault(l, 0);
        }

        Console.WriteLine(simularityScore);
    }
}
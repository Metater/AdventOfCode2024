namespace AdventOfCode2024.Days.Day5;

internal class Part2 : DayPart
{
    public override bool HasPrecedence => true;
    //public override string InputFile => "Example.txt";
    public override bool ShouldRejectWhiteSpaceLines => false;

    public override void Run(List<string> input)
    {
        bool foundEmptyLine = false;

        List<(int X, int Y)> rules = [];
        List<List<int>> requests = [];

        foreach (var line in input)
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                foundEmptyLine = true;
                continue;
            }

            if (!foundEmptyLine)
            {
                var split = line.Split('|');
                rules.Add((int.Parse(split[0]), int.Parse(split[1])));
            }
            else
            {
                var split = line.Split(',', StringSplitOptions.TrimEntries);
                var request = split.Select(int.Parse).ToList();
                requests.Add(request);
            }
        }

        int sum = 0;
        foreach (var request in requests)
        {
            if (IsOrdered(request, rules))
            {
                //sum += GetMiddleNumber(request);
            }
            else
            {
                SortRequest(request, rules);
                sum += GetMiddleNumber(request);
            }
        }

        Console.WriteLine(sum);
    }

    private static int GetMiddleNumber(List<int> request)
    {
        return request[request.Count / 2];
    }

    private static bool IsOrdered(List<int> request, List<(int X, int Y)> rules)
    {
        var applicableRules = rules.Where(r => IsRuleApplicable(request, r)).ToList();

        return applicableRules.All(r => IsRuleBeingFollowed(request, r));
    }

    private static bool IsRuleApplicable(List<int> request, (int X, int Y) rule)
    {
        return request.Contains(rule.X) && request.Contains(rule.Y);
    }

    private static bool IsRuleBeingFollowed(List<int> request, (int X, int Y) rule)
    {
        return request.IndexOf(rule.X) < request.IndexOf(rule.Y);
    }

    private static void SortRequest(List<int> request, List<(int X, int Y)> rules)
    {
        request.Sort(new RequestComparer(rules));
    }

    private class RequestComparer(List<(int X, int Y)> rules) : IComparer<int>
    {
        private readonly List<(int X, int Y)> rules = rules;

        public int Compare(int x, int y)
        {
            if (rules.Any(r => (r.X == x && r.Y == y)))
            {
                return -1;
            }
            else
            {
                return 1;
            }
        }
    }
}
﻿namespace AdventOfCode2024.Days.Day2;

internal class Part2 : DayPart
{
    public override bool HasPrecedence => true;
    //public override string InputFile => "Example.txt";
    //public override bool ShouldRejectWhiteSpaceLines => false;

    public override void Run(List<string> input)
    {
        int safeReports = 0;
        foreach (var line in input)
        {
            List<int> report = line.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();
            List<int> reportCopy = [];

            bool isAllIncreasing = IsAllIncreasing(report);
            bool isAllDecreasing = IsAllDecreasing(report);
            bool isSlopeValid = IsSlopeValid(report);

            if ((isAllDecreasing || isAllIncreasing) && isSlopeValid)
            {
                safeReports++;
            }
            else
            {
                for (int i = 0; i < report.Count; i++)
                {
                    reportCopy.Clear();
                    reportCopy.AddRange(report);
                    reportCopy.RemoveAt(i);

                    if ((IsAllIncreasing(reportCopy) || IsAllDecreasing(reportCopy)) && IsSlopeValid(reportCopy))
                    {
                        safeReports++;
                        break;
                    }
                }
            }
        }

        Console.WriteLine(safeReports);
    }

    private static bool IsSlopeValid(List<int> report)
    {
        for (int i = 0; i < report.Count - 1; i++)
        {
            int a = report[i];
            int b = report[i + 1];

            int diff = Math.Abs(b - a);

            bool atLeastOne = diff >= 1;
            bool atMostThree = diff <= 3;
            if (!atLeastOne || !atMostThree)
            {
                return false;
            }
        }

        return true;
    }

    private static bool IsAllIncreasing(List<int> report)
    {
        int last = report[0];
        for (int i = 1; i < report.Count; i++)
        {
            if (report[i] <= last)
            {
                return false;
            }

            last = report[i];
        }

        return true;
    }

    private static bool IsAllDecreasing(List<int> report)
    {
        int last = report[0];
        for (int i = 1; i < report.Count; i++)
        {
            if (report[i] >= last)
            {
                return false;
            }

            last = report[i];
        }

        return true;
    }
}
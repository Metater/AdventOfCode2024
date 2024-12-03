using System.Text;

namespace AdventOfCode2024.Days.Day3;

internal class Part1 : DayPart
{
    public override bool HasPrecedence => true;
    //public override string InputFile => "Example.txt";
    //public override bool ShouldRejectWhiteSpaceLines => false;

    public override void Run(List<string> input)
    {
        // min: mul(X,Y) = 8 chars
        // max: mul(XXX,YYY) = 12 chars

        Queue<char> queue = [];
        StringBuilder sb = new();

        long sum = 0;

        foreach (var line in input)
        {
            queue.Clear();
            foreach (var c in line)
            {
                queue.Enqueue(c);
            }

            while (queue.Count > 0)
            {
                sb.Clear();

                if (queue.Dequeue() != 'm')
                {
                    continue;
                }

                if (queue.Dequeue() != 'u')
                {
                    continue;
                }


                if (queue.Dequeue() != 'l')
                {
                    continue;
                }

                if (queue.Dequeue() != '(')
                {
                    continue;
                }

                bool shouldContinue = false;

                while (queue.TryDequeue(out var c))
                {
                    if (char.IsDigit(c))
                    {
                        sb.Append(c);
                    }
                    else if (c == ',')
                    {
                        break;
                    }
                    else
                    {
                        shouldContinue = true;
                        break;
                    }
                }

                if (shouldContinue)
                {
                    continue;
                }

                string xString = sb.ToString();
                sb.Clear();

                while (queue.TryDequeue(out var c))
                {
                    if (char.IsDigit(c))
                    {
                        sb.Append(c);
                    }
                    else if (c == ')')
                    {
                        break;
                    }
                    else
                    {
                        shouldContinue = true;
                        break;
                    }
                }

                if (shouldContinue)
                {
                    continue;
                }

                string yString = sb.ToString();

                int x = int.Parse(xString);
                int y = int.Parse(yString);

                sum += x * y;
            }
        }

        Console.WriteLine(sum);
    }
}
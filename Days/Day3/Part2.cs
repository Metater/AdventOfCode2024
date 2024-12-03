using System.Text;

namespace AdventOfCode2024.Days.Day3;

internal class Part2 : DayPart
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

        foreach (var line in input)
        {
            sb.Append(line);
        }

        input.Clear();
        input.Add(sb.ToString());
        sb.Clear();

        long sum = 0;

        foreach (var line in input)
        {
            queue.Clear();
            foreach (var c in line)
            {
                queue.Enqueue(c);
            }

            int GetIndex() => line!.Length - queue.Count;
            bool IsEnabled()
            {
                int index = GetIndex();
                int lastDo = line!.LastIndexOf("do()", index);
                int lastDont = line.LastIndexOf("don't()", index);
                if (lastDo == -1 && lastDont == -1)
                {
                    return true;
                }

                return lastDo > lastDont;
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

                if (IsEnabled())
                {
                    sum += x * y;
                }
            }
        }

        Console.WriteLine(sum);
    }
}
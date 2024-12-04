namespace AdventOfCode2024.Days.Day4;

internal class Part2 : DayPart
{
    //public override bool HasPrecedence => true;
    //public override string InputFile => "Example.txt";
    //public override bool ShouldRejectWhiteSpaceLines => false;

    public override void Run(List<string> input)
    {
        int sum = 0;

        var grid = new CharacterGrid(input);
        for (int y = 0; y < grid.Height; y++)
        {
            for (int x = 0; x < grid.Width; x++)
            {
                if (grid.TryGet(x, y, out var value) && value == 'A')
                {
                    if (CheckXmas(grid, x, y))
                    {
                        sum++;
                    }
                }
            }
        }

        Console.WriteLine(sum);
    }

    private static bool CheckXmas(CharacterGrid grid, int x, int y)
    {
        if (!grid.TryGet(x + 1, y - 1, out var ne) || ne == 'A' || ne == 'X')
        {
            return false;
        }

        if (!grid.TryGet(x + 1, y + 1, out var se) || se == 'A' || se == 'X')
        {
            return false;
        }

        if (!grid.TryGet(x - 1, y + 1, out var sw) || sw == 'A' || sw == 'X')
        {
            return false;
        }

        if (!grid.TryGet(x - 1, y - 1, out var nw) || nw == 'A' || nw == 'X')
        {
            return false;
        }

        return ne != sw && nw != se;
    }

    private class CharacterGrid(List<string> grid)
    {
        // y, x
        public readonly char[,] grid = CreateGrid(grid);
        public int Height => grid.GetLength(0);
        public int Width => grid.GetLength(1);

        private static char[,] CreateGrid(List<string> grid)
        {
            var result = new char[grid.Count, grid[0].Length];
            for (int y = 0; y < grid.Count; y++)
            {
                for (int x = 0; x < grid[y].Length; x++)
                {
                    result[y, x] = grid[y][x];
                }
            }

            return result;
        }

        public bool TryGet(int x, int y, out char value)
        {
            if (x < 0 || x >= Width || y < 0 || y >= Height)
            {
                value = default;
                return false;
            }

            value = grid[y, x];
            return true;
        }
    }
}
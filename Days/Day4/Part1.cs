using System.Runtime.InteropServices;

namespace AdventOfCode2024.Days.Day4;

internal class Part1 : DayPart
{
    //public override bool HasPrecedence => true;
    //public override string InputFile => "Example.txt";
    //public override bool ShouldRejectWhiteSpaceLines => false;

    public override void Run(List<string> input)
    {
        List<Cell[]> groups = [];

        var grid = new CharacterGrid(input);
        for (int y = 0; y < grid.Height; y++)
        {
            for (int x = 0; x < grid.Width; x++)
            {
                CheckXmas(grid, groups, x, y, 1, 0); // east
                CheckXmas(grid, groups, x, y, -1, 0); // west
                CheckXmas(grid, groups, x, y, 0, 1); // south
                CheckXmas(grid, groups, x, y, 0, -1); // north

                CheckXmas(grid, groups, x, y, 1, -1); // ne
                CheckXmas(grid, groups, x, y, 1, 1); // se
                CheckXmas(grid, groups, x, y, -1, 1); // sw
                CheckXmas(grid, groups, x, y, -1, -1); // nw
            }
        }

        Console.WriteLine(groups.Count);
    }

    private static void CheckXmas(CharacterGrid grid, List<Cell[]> groups, int x, int y, int dx, int dy)
    {
        Cell[] group = new Cell[4];
        for (int i = 0; i < 4; i++)
        {
            int X = x + (dx * i);
            int Y = y + (dy * i);
            if (!grid.TryGet(X, Y, out var value))
            {
                return;
            }

            if (i == 0 && value != 'X')
            {
                return;
            }

            if (i == 1 && value != 'M')
            {
                return;
            }

            if (i == 2 && value != 'A')
            {
                return;
            }

            if (i == 3 && value != 'S')
            {
                return;
            }

            group[i] = new Cell { x = X, y = Y };
        }

        groups.Add(group);
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

    //private static bool AlreadyContainsGroup(Cell[] group, List<Cell[]> groups)
    //{
    //    foreach (var g in groups)
    //    {
    //        for (int i = 0; i < 4; i++)
    //        {
    //            if (g[i].index != group[i].index)
    //            {
    //                continue;
    //            }
    //        }

    //        return true;
    //    }

    //    return false;
    //}

    [StructLayout(LayoutKind.Explicit)]
    private struct Cell
    {
        [FieldOffset(0)]
        public long index;

        [FieldOffset(0)]
        public int x;
        [FieldOffset(4)]
        public int y;
    }
}
﻿using System.Runtime.InteropServices;

namespace AdventOfCode2024.Days.Day6;

internal class Part2 : DayPart
{
    //public override bool HasPrecedence => true;
    //public override string InputFile => "Example.txt";
    //public override bool ShouldRejectWhiteSpaceLines => false;

    public override void Run(List<string> input)
    {
        var grid = new CharacterGrid(input);

        (int x, int y) = grid.FindGuard();
        grid.TrySet(x, y, '.');

        var obstructableCells = grid.FindObstructableCells(x, y);

        int loopPermutations = 0;

        foreach ((int obstructionX, int obstructionY) in obstructableCells)
        {
            if (DoesCauseLoop(grid, x, y, obstructionX, obstructionY))
            {
                loopPermutations++;
            }
        }

        Console.WriteLine(loopPermutations);
    }

    private static bool DoesCauseLoop(CharacterGrid grid, int initialX, int initialY, int obstructionX, int obstructionY)
    {
        int x = initialX;
        int y = initialY;

        HashSet<ulong> distinctPositions = [];

        Direction direction = Direction.Up;

        int i = 0;

        while (grid.TryGet(x, y, out _))
        {
            (int nextX, int nextY) = Move(direction, x, y);
            if (!grid.TryGet(nextX, nextY, out var nextCell))
            {
                return false;
            }

            if (nextCell == '.' && !(nextX == obstructionX && nextY == obstructionY))
            {
                x = nextX;
                y = nextY;
            }
            else
            {
                direction = RotateClockwise(direction);
            }

            i++;

            if (i > 10_000)
            {
                return true;
            }
        }

        return false;
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

        public bool TrySet(int x, int y, char value)
        {
            if (x < 0 || x >= Width || y < 0 || y >= Height)
            {
                return false;
            }

            grid[y, x] = value;
            return true;
        }

        public (int x, int y) FindGuard()
        {
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    if (TryGet(x, y, out var value) && value == '^')
                    {
                        return (x, y);
                    }
                }
            }

            throw new Exception();
        }

        public List<(int x, int y)> FindObstructableCells(int guardX, int guardY)
        {
            List<(int x, int y)> cells = [];

            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    if (TryGet(x, y, out var value) && value == '.' && !(x == guardX && y == guardY))
                    {
                        cells.Add((x, y));
                    }
                }
            }

            return cells;
        }
    }

    private enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }

    private static Direction RotateClockwise(Direction direction)
    {
        return direction switch
        {
            Direction.Up => Direction.Right,
            Direction.Right => Direction.Down,
            Direction.Down => Direction.Left,
            Direction.Left => Direction.Up,
            _ => throw new Exception()
        };
    }

    private static (int x, int y) Move(Direction direction, int x, int y)
    {
        return direction switch
        {
            Direction.Up => (x, y - 1),
            Direction.Down => (x, y + 1),
            Direction.Left => (x - 1, y),
            Direction.Right => (x + 1, y),
            _ => throw new Exception()
        };
    }

    private class Guard
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Direction Direction { get; private set; }

        public void Turn()
        {
            Direction = RotateClockwise(Direction);
        }
    }

    [StructLayout(LayoutKind.Explicit)]
    private struct Position
    {
        [FieldOffset(0)]
        public int x;
        [FieldOffset(4)]
        public int y;

        [FieldOffset(0)]
        public ulong index;
    }
}
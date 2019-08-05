using System;

namespace MineField.Tests
{
    [Flags]
    public enum Direction
    {
        Unknown = 0,
        North = 1,
        East = 2, 
        West = 4,
        South = 8
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Xunit;

namespace MineField.Tests
{
    public class MineFieldTests
    {
        [Fact]
        public void FieldIs1x1_ThereIsASingleMine()
        {
            var field = new MineField(1, 1);
            var result = field.PlaceMines(new MineFieldInput("*"));

            Assert.Equal("*", result);
        }

        [Fact]
        public void FieldIs1x1_ThereIsNoMine()
        {
            var field = new MineField(1, 1);
            var result = field.PlaceMines(new MineFieldInput("."));

            Assert.Equal("0", result);
        }

        [Fact]
        public void FieldIs2x1_ThereIsASingleMineAt1x1()
        {
            var field = new MineField(2, 1);
            var result = field.PlaceMines(new MineFieldInput("*."));

            Assert.Equal("*1", result);
        }

        [Fact]
        public void FieldIs2x1_ThereIsASingleMineAt2x1()
        {
            var field = new MineField(2, 1);
            var result = field.PlaceMines(new MineFieldInput(".*"));

            Assert.Equal("1*", result);
        }

        [Fact]
        public void FieldIs3x1_ThereIsASingleMineAt1x1()
        {
            var field = new MineField(3, 1);
            var result = field.PlaceMines(new MineFieldInput("*.."));

            Assert.Equal("*10", result);
        }

        [Fact]
        public void FieldIs3x1_ThereIsASingleMineAt2x1()
        {
            var field = new MineField(3, 1);
            var result = field.PlaceMines(new MineFieldInput(".*."));

            Assert.Equal("1*1", result);
        }

        [Fact]
        public void FieldIs3x1_ThereIsASingleMineAt3x1()
        {
            var field = new MineField(3, 1);
            var result = field.PlaceMines(new MineFieldInput("..*"));

            Assert.Equal("01*", result);
        }

        [Fact]
        public void FieldIs3x1_ThereAreMinesAt1x1And3x1()
        {
            var field = new MineField(3, 1);
            var result = field.PlaceMines(new MineFieldInput("*.*"));

            Assert.Equal("*2*", result);
        }

        [Fact]
        public void FieldIs2x2_ThereIsASingleMineAt1x1()
        {
            var field = new MineField(2, 2);
            var result = field.PlaceMines(
                new MineFieldInput(fieldInput:
                    "*.\n" +
                    ".."));

            var expectedResult = 
                "*1\n" +
                "11";
            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public void FieldIs3x3_ThereIsAMineAtCenter()
        {
            var field = new MineField(3, 3);
            var result = field.PlaceMines(
                new MineFieldInput(fieldInput:
                    "...\n" +
                    ".*.\n" +
                    "..."));

            var expectedResult = 
                "111\n" +
                "1*1\n" +
                "111";
            Assert.Equal(expectedResult, result);
        }
    }

    public class MineField
    {
        public MineField(int m, int n)
        {
        }

        public string PlaceMines(MineFieldInput fieldInput)
        {
            var directions = new List<Direction>
            {
                Direction.North,
                Direction.North | Direction.West,
                Direction.East,
                Direction.North | Direction.East,
                Direction.West,
                Direction.South | Direction.West,
                Direction.South,
                Direction.South | Direction.East
            };

            var resultCharList = new List<char>();
            for (var y = 0; y < fieldInput.RowCount; y++)
            {
                if (y > 0)
                {
                    resultCharList.Add('\n');
                }

                for (var x = 0; x < fieldInput.ColumnLength; x++)
                {
                    if (fieldInput.HasMine(x, y))
                    {
                        resultCharList.Add('*');
                    }
                    else
                    {
                        var numberOfAdjacentMines = 0;
                        foreach (var direction in directions)
                        {
                            if (!fieldInput.IsAtEdge(x, y, direction) && fieldInput.HasMineInAdjacentCell(x, y, direction))
                            {
                                numberOfAdjacentMines++;
                            }
                        }

                        resultCharList.Add(numberOfAdjacentMines.ToString()[0]);
                    }
                }
            }

            return new string(resultCharList.ToArray());
        }
    }

    public class MineFieldInput
    {
        private readonly char[][] _fieldInput;
        
        public MineFieldInput(string fieldInput)
        {
            var fieldInputRows = fieldInput.Split('\n');
            _fieldInput = fieldInputRows.Select(x => x.ToCharArray()).ToArray();
        }

        public int ColumnLength => _fieldInput[0].Length;
        public int RowCount => _fieldInput.Length;

        public bool IsAtEdge(int x, int y, Direction edge)
        {
            var conditions = new List<Func<bool>>();

            if (edge.HasFlag(Direction.North))
            {
                conditions.Add(() => y == 0);
            }

            if (edge.HasFlag(Direction.East))
            {
                conditions.Add(() => x == ColumnLength - 1);
            }

            if (edge.HasFlag(Direction.West))
            {
                conditions.Add(() => x == 0);
            }

            if (edge.HasFlag(Direction.South))
            {
                conditions.Add(() => y == RowCount - 1);
            }

            foreach (var condition in conditions)
            {
                if (condition())
                {
                    return true;
                }
            }

            return false;
        }

        public bool HasMine(int x, int y)
        {
            return _fieldInput[y][x] == '*';
        }


        public bool HasMineInAdjacentCell(int x, int y, Direction direction)
        {
            int adjacentCellX = x;
            int adjacentCellY = y;

            if (direction.HasFlag(Direction.North))
            {
                adjacentCellY = y - 1;
            }

            if (direction.HasFlag(Direction.East))
            {
                adjacentCellX = x + 1;
            }

            if (direction.HasFlag(Direction.West))
            {
                adjacentCellX = x - 1;
            }

            if (direction.HasFlag(Direction.South))
            {
                adjacentCellY = y + 1;
            }


            return HasMine(adjacentCellX, adjacentCellY);
        }
    }

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

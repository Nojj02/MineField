using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace MineField.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void FieldIs1x1_ThereIsASingleMine()
        {
            var field = new MineField(1, 1);
            var result = field.PlaceMines(new MineFieldInput("*"));

            Assert.Equal("*", result);
        }

        [Fact]
        public void FieldIs1x1_ThereIsANoMine()
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
        public void FieldIs1x2_ThereIsASingleMineAt1x2()
        {
            var field = new MineField(2, 1);
            var result = field.PlaceMines(new MineFieldInput(".*"));

            Assert.Equal("1*", result);
        }
    }

    public class MineField
    {
        public MineField(int m, int n)
        {
        }

        public string PlaceMines(MineFieldInput fieldInput)
        {
            var resultCharList = new List<char>();
            for(var x = 0; x < fieldInput.Length; x++)
            {
                if (fieldInput.HasMine(x))
                {
                    resultCharList.Add('*');
                }
                else
                {
                    if (!fieldInput.IsAtEdge(x, Edge.West) && fieldInput.HasMineInAdjacentCell(x, Direction.West)
                        || !fieldInput.IsAtEdge(x, Edge.East) && fieldInput.HasMineInAdjacentCell(x, Direction.East)
                        )
                    {
                        resultCharList.Add('1');
                    }
                    else
                    {
                        resultCharList.Add('0');
                    }
                }
            }

            return new string(resultCharList.ToArray());
        }
    }

    public class MineFieldInput
    {
        private readonly char[] _fieldInput;

        public MineFieldInput(char[] fieldInput)
        {
            _fieldInput = fieldInput;
        }

        public MineFieldInput(string fieldInput)
            : this(fieldInput.ToCharArray())
        {

        }

        public int Length => _fieldInput.Length;

        public bool IsAtEdge(int x, Edge edge)
        {
            if (edge == Edge.West)
            {
                return x == 0;
            }

            if (edge == Edge.East)
            {
                return x == _fieldInput.Length - 1;
            }

            throw new InvalidOperationException("Unknown edge");
        }

        public bool HasMine(int x)
        {
            return _fieldInput[x] == '*';
        }


        public bool HasMineInAdjacentCell(int x, Direction direction)
        {
            int adjacentCellX;
            if (direction == Direction.West)
            {
                adjacentCellX = x - 1;
            }
            else if (direction == Direction.East)
            {
                adjacentCellX = x + 1;
            }
            else
            {
                throw new InvalidOperationException("Unsupported direction");
            }

            return HasMine(adjacentCellX);
        }
    }

    public enum Direction
    {
        Unknown = 0,
        East = 1, 
        West = 2
    }

    public enum Edge
    {
        East,
        West
    }
}

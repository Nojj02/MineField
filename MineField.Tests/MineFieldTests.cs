using System.Collections.Generic;
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
            var fieldOutput = new MineFieldOutput();
            
            for (var y = 0; y < fieldInput.RowCount; y++)
            {
                if (y > 0)
                {
                    fieldOutput.AddNewRow();
                }

                for (var x = 0; x < fieldInput.ColumnLength; x++)
                {
                    if (fieldInput.HasMine(x, y))
                    {
                        fieldOutput.SetMine();
                    }
                    else
                    {
                        var numberOfAdjacentMines = fieldInput.GetNumberOfAdjacentMines(x, y);

                        fieldOutput.SetNumberOfAdjacentMines(numberOfAdjacentMines);
                    }
                }
            }

            return fieldOutput.ToString();
        }
    }
}

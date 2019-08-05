using System.Collections.Generic;
using System.Linq;

namespace MineField.Tests
{
    public class MineFieldInput
    {
        private List<Direction> _directions = new List<Direction>
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

        private readonly char[][] _fieldInput;
        
        public MineFieldInput(string fieldInput)
        {
            var fieldInputRows = fieldInput.Split('\n');
            _fieldInput = fieldInputRows.Select(x => x.ToCharArray()).ToArray();
        }

        public int ColumnLength => _fieldInput[0].Length;
        public int RowCount => _fieldInput.Length;

        public bool HasMine(int x, int y)
        {
            return _fieldInput[y][x] == '*';
        }


        private bool IsAtEdge(int x, int y, Direction edge)
        {
            bool isAtEdge = false;

            if (edge.HasFlag(Direction.North))
            {
                isAtEdge = isAtEdge || y == 0;
            }

            if (edge.HasFlag(Direction.East))
            {
                isAtEdge = isAtEdge || x == ColumnLength - 1;
            }

            if (edge.HasFlag(Direction.West))
            {
                isAtEdge = isAtEdge || x == 0;
            }

            if (edge.HasFlag(Direction.South))
            {
                isAtEdge = isAtEdge || y == RowCount - 1;
            }

            return isAtEdge;
        }

        public int GetNumberOfAdjacentMines(int x, int y)
        {
            var numberOfAdjacentMines = 0;
            foreach (var direction in _directions)
            {
                if (!IsAtEdge(x, y, direction) && HasMineInAdjacentCell(x, y, direction))
                {
                    numberOfAdjacentMines++;
                }
            }

            return numberOfAdjacentMines;
        }

        private bool HasMineInAdjacentCell(int x, int y, Direction direction)
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
}
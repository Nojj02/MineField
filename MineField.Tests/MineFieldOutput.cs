using System.Collections.Generic;

namespace MineField.Tests
{
    public class MineFieldOutput
    {
        private readonly List<char> _output = new List<char>();

        public void AddNewRow()
        {
            _output.Add('\n');
        }

        public void SetMine()
        {
            _output.Add('*');
        }

        public void SetNumberOfAdjacentMines(int numberOfAdjacentMines)
        {
            _output.Add(numberOfAdjacentMines.ToString()[0]);
        }

        public override string ToString()
        {
            return new string(_output.ToArray());
        }
    }
}
using System.Collections.Generic;
using System.Linq;

namespace KataMinesweeper
{
    public class MinefieldBuilder
    {
        private readonly int _rows;
        private readonly List<string> _cells = new List<string>();

        public MinefieldBuilder(int rows)
        {
            _rows = rows;
        }

        public bool IsReady
        {
            get { return _cells.Count == _rows; }
        }

        public Minefield Build()
        {
            var cells = new Cell[_rows,Columns];
            for (var row = 0; row < _rows; ++row)
                AddRowCells(row, cells);
            return new Minefield(cells, new AdjacentMineCounter(cells));
        }

        public void AddRow(string line)
        {
            _cells.Add(line);
        }

        private int Columns
        {
            get { return _cells.First().Length; }
        }

        private void AddRowCells(int columns, Cell[,] cells)
        {
            for (var c = 0; c < Columns; ++c)
                cells[columns, c] = new Cell(_cells[columns][c]);
        }
    }
}
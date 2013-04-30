using System;
using System.Collections.Generic;
using System.Text;

namespace KataMinesweeper
{
    public class Minefield
    {
        private readonly Cell[,] _cells;
        private readonly AdjacentMineCounter _adjacentMineCounter;

        public Minefield(Cell[,] cells, AdjacentMineCounter adjacentMineCounter)
        {
            _cells = cells;
            _adjacentMineCounter = adjacentMineCounter;
        }

        public IEnumerable<string> Result(int fieldNumber)
        {
            yield return String.Format("Field #{0}:", fieldNumber);
            for (var row = 0; row < _cells.GetLength(0); row++)
                yield return ValuesForCellsInRow(row);
        }

        private string ValuesForCellsInRow(int row)
        {
            var sb = new StringBuilder();
            for (var column = 0; column < _cells.GetLength(1); column++)
                sb.Append(CellValue(row, column));
            return sb.ToString();
        }

        private object CellValue(int row, int column)
        {
            if (_cells[row, column].IsMine)
                return "*";
            return _adjacentMineCounter.AdjacentMineCount(row, column);
        }
    }
}
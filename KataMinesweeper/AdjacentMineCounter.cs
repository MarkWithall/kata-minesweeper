using System.Linq;

namespace KataMinesweeper
{
    public class AdjacentMineCounter
    {
        private readonly Cell[,] _cells;

        public AdjacentMineCounter(Cell[,] cells)
        {
            _cells = cells;
        }

        public int AdjacentMineCount(int row, int column)
        {
            var count = 0;
            foreach (var r in Enumerable.Range(row - 1, 3))
                count += AddColumnMines(r, column);
            return count;
        }

        private int AddColumnMines(int row, int column)
        {
            var count = 0;
            foreach (var c in Enumerable.Range(column - 1, 3))
                count += AdjacentMines(row, c);
            return count;
        }

        private int AdjacentMines(int row, int column)
        {
            if (CellInRange(row, column) && _cells[row, column].IsMine)
                return 1;
            return 0;
        }

        private bool CellInRange(int row, int column)
        {
            return row >= 0 && row < _cells.GetLength(0) && column >= 0 && column < _cells.GetLength(1);
        }
    }
}
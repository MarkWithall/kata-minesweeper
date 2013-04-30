using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KataMinesweeper
{
    public static class Minesweeper
    {
        public static IEnumerable<string> Sweep(IEnumerable<string> input)
        {
            var reader = new MinefieldReader();
            var minefields = reader.Read(input);
            return minefields.Results;
        }

        public static void Main(string[] args)
        {
        }
    }

    public class MinefieldReader
    {
        private readonly MinefieldCollectionBuilder _minefieldCollectionBuilder;
        private Action<string> _processLine;

        public MinefieldReader()
        {
            _minefieldCollectionBuilder = new MinefieldCollectionBuilder();
            _processLine = StartNewMinefield;
        }

        public MinefieldCollection Read(IEnumerable<string> lines)
        {
            foreach (var line in lines)
                _processLine(line);
            return _minefieldCollectionBuilder.Build();
        }

        private void StartNewMinefield(string line)
        {
            if (line == "0 0")
                return;
            var dimentions = line.Split(' ');
            _minefieldCollectionBuilder.StartNewMinefield(int.Parse(dimentions[0]), int.Parse(dimentions[1]));
            _processLine = AddRowToCurrentMinefield;
        }

        private void AddRowToCurrentMinefield(string line)
        {
            _minefieldCollectionBuilder.AddRowToCurrentMinefield(line);
            if (_minefieldCollectionBuilder.CurrentMinefieldIsReady)
                _processLine = StartNewMinefield;
        }
    }

    public class MinefieldCollectionBuilder
    {
        private readonly List<MinefieldBuilder> _mineFieldBuilders = new List<MinefieldBuilder>();

        public MinefieldCollection Build()
        {
            return new MinefieldCollection(_mineFieldBuilders.Select(mfb => mfb.Build()).ToList());
        }

        public bool CurrentMinefieldIsReady
        {
            get { return CurrentBuilder.IsReady; }
        }

        public void StartNewMinefield(int rows, int columns)
        {
            _mineFieldBuilders.Add(new MinefieldBuilder(rows, columns));
        }

        public void AddRowToCurrentMinefield(string line)
        {
            CurrentBuilder.AddRow(line);
        }

        private MinefieldBuilder CurrentBuilder
        {
            get { return _mineFieldBuilders.Last(); }
        }
    }

    public class MinefieldBuilder
    {
        private readonly int _rows;
        private readonly int _columns;
        private readonly List<string> _cells = new List<string>();

        public MinefieldBuilder(int rows, int columns)
        {
            _rows = rows;
            _columns = columns;
        }

        public bool IsReady
        {
            get { return _cells.Count == _rows; }
        }

        public Minefield Build()
        {
            var cells = new Cell[_rows,_columns];
            for (var row = 0; row < _rows; ++row)
                AddRowCells(row, cells);
            return new Minefield(cells, new AdjacentMineCounter(cells));
        }

        private void AddRowCells(int columns, Cell[,] cells)
        {
            for (var c = 0; c < _columns; ++c)
                cells[columns, c] = new Cell(_cells[columns][c]);
        }

        public void AddRow(string line)
        {
            _cells.Add(line);
        }
    }

    public class MinefieldCollection
    {
        private readonly ICollection<Minefield> _minefields;
        private readonly List<string> _result = new List<string>();

        public MinefieldCollection(ICollection<Minefield> minefields)
        {
            _minefields = minefields;
        }

        public IEnumerable<string> Results
        {
            get
            {
                var fieldNumber = 1;
                foreach (var minefield in _minefields)
                {
                    _result.AddRange(minefield.Result(fieldNumber++));
                    AddNewlineIfNeeded(fieldNumber, _minefields.Count());
                }
                return _result;
            }
        }

        private void AddNewlineIfNeeded(int fieldNumber, int totalMinefields)
        {
            if (fieldNumber > 1 && fieldNumber <= totalMinefields)
                _result.Add(String.Empty);
        }
    }

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

    public class Cell
    {
        private readonly char _value;

        public Cell(char value)
        {
            _value = value;
        }

        public bool IsMine
        {
            get { return _value == '*'; }
        }
    }
}

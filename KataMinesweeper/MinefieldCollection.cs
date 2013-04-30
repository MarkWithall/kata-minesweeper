using System;
using System.Collections.Generic;
using System.Linq;

namespace KataMinesweeper
{
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
}
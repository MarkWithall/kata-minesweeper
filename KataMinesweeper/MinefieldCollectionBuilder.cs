using System.Collections.Generic;
using System.Linq;

namespace KataMinesweeper
{
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

        public void StartNewMinefield(int rows)
        {
            _mineFieldBuilders.Add(new MinefieldBuilder(rows));
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
}
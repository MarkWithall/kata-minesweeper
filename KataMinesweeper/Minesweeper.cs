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
            var reader = new MinefieldReader(new MinefieldCollectionBuilder());
            var minefields = reader.Read(input);
            return minefields.Results;
        }

        public static void Main(string[] args)
        {
        }
    }
}

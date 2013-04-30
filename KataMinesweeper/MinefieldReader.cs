using System;
using System.Collections.Generic;

namespace KataMinesweeper
{
    public class MinefieldReader
    {
        private readonly MinefieldCollectionBuilder _minefieldCollectionBuilder;
        private Action<string> _processLine;

        public MinefieldReader(MinefieldCollectionBuilder minefieldCollectionBuilder)
        {
            _minefieldCollectionBuilder = minefieldCollectionBuilder;
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
            _minefieldCollectionBuilder.StartNewMinefield(int.Parse(dimentions[0]));
            _processLine = AddRowToCurrentMinefield;
        }

        private void AddRowToCurrentMinefield(string line)
        {
            _minefieldCollectionBuilder.AddRowToCurrentMinefield(line);
            if (_minefieldCollectionBuilder.CurrentMinefieldIsReady)
                _processLine = StartNewMinefield;
        }
    }
}
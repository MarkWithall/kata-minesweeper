namespace KataMinesweeper
{
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
namespace QueenChess
{
    public class PathFinderResult
    {
        public int StatesChecked { get; set; }
        public IDictionary<int, Tile[][]> WinStates { get; set; }
        public IDictionary<int, Tile[][]> States { get; set; }
        public IDictionary<TimeSpan, Tile[][]> TimeResults { get; internal set; }
    }
}
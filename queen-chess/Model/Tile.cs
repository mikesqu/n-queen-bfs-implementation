namespace QueenChess
{
    public class Tile : IComparable<Tile>
    {
        public int Id { get; set; }
        public bool IsPlaced { get; set; }
        public bool Skip { get; set; }
        public bool Explored { get; set; }

        public int CompareTo(Tile? other)
        {
            if (IsPlaced == other.IsPlaced && Skip == other.Skip)
            {
                return 0;
            }
            return -1;

        }
    }
}
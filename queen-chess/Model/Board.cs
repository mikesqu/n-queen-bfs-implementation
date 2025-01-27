namespace QueenChess
{
    public class Board
    {

        public Tile[][] Tiles { get; init; }
        public Board(int size)
        {
            int idCounter = 0;
            Tiles = new Tile[size][];

            for (int i = 0; i < Tiles.Length; i++)
            {
                Tiles[i] = new Tile[size];
                for (int j = 0; j < Tiles[i].Length; j++)
                {
                    Tiles[i][j] = new Tile()
                    {
                        Id = idCounter++
                    };
                }
            }

        }


    }
}
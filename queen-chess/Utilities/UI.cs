namespace QueenChess
{
    internal class UI
    {
        public void DrawBoard(Board board)
        {
            foreach (Tile[] row in board.Tiles)
            {
                Console.Write("|");
                foreach (Tile tile in row)
                {
                    string e = null!;
                    if (tile.IsPlaced)
                        e = "q";
                    else if (tile.Skip)
                        e = "\\";
                    else
                        e = "_";

                    Console.Write($"{e}|");
                }
                Console.Write("\n");
            }
        }

        public void DrawTiles(Tile[][] tiles, bool removeSkips = false)
        {
            foreach (Tile[] row in tiles)
            {
                Console.Write("|");
                foreach (Tile tile in row)
                {
                    string e = null!;
                    if (removeSkips)
                    {
                        if (tile.IsPlaced)
                            e = "q";
                        else
                            e = "_";
                    }
                    else
                    {
                        if (tile.IsPlaced)
                            e = "q";
                        else if (tile.Skip)
                            e = "\\";
                        else
                            e = "_";
                    }

                    Console.Write($"{e}|");
                }
                Console.Write("\n");
            }
        }

        internal void DrawResult(PathFinderResult result)
        {
            Console.WriteLine($"States checked: {result.StatesChecked}");
        }
    }
}
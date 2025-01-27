namespace QueenChess
{
    public class CollisionChecker
    {
        public bool HasCollisions(Tile[][] tiles)
        {
            if (CheckEachTile(tiles))
                return true;

            return false;
        }

        private bool CheckEachTile(Tile[][] tiles)
        {
            for (int i = 0; i < tiles.Length; i++)
            {
                for (int j = 0; j < tiles[i].Length; j++)
                {
                    if (tiles[i][j].IsPlaced)
                    {
                        if (Check4DiagonalDirections(tiles, i, j) || CheckRowAndColumn(tiles, i, j))
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        private bool CheckRowAndColumn(Tile[][] tiles, int i, int j)
        {
            int stepCounter = 1;

            for (int loop = 0; loop < tiles.Length; loop++)
            {
                //can go down
                if (stepCounter + i < tiles.Length)
                {
                    if (tiles[i + stepCounter][j].IsPlaced)
                        return true;
                }

                //can go right
                if (stepCounter + j < tiles[i].Length)
                {
                    if (tiles[i][j + stepCounter].IsPlaced)
                        return true;
                }

                stepCounter++;

            }

            return false;
        }


        private bool Check4DiagonalDirections(Tile[][] tiles, int i, int j)
        {
            int stepCounter = 1;

            for (int loop = 0; loop < tiles.Length; loop++)
            {
                //can go up
                if (i - stepCounter > 0)
                {
                    //can go left
                    if (j - stepCounter >= 0)
                    {
                        // up to the left
                        if (tiles[i - stepCounter][j - stepCounter].IsPlaced)
                            return true;
                    }

                    //can go right
                    if (tiles[i].Length > stepCounter + j)
                    {
                        //up to the right
                        if (tiles[i - stepCounter][j + stepCounter].IsPlaced)
                            return true;
                    }
                }

                //can go down
                if (tiles.Length > stepCounter + i)
                {
                    //can go left
                    if (j - stepCounter >= 0)
                    {
                        // down to the left
                        if (tiles[i + stepCounter][j - stepCounter].IsPlaced)
                            return true;
                    }
                    //can go right
                    if (tiles[i].Length > stepCounter + j)
                    {
                        //down to the right
                        if (tiles[i + stepCounter][j + stepCounter].IsPlaced)
                            return true;
                    }
                }

                stepCounter++;
            }

            return false;

        }
    }
}

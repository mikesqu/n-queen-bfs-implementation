namespace QueenChess
{
    public class PathFinder
    {
        private Board _board;
        private CollisionChecker _collisionChecker;
        private int _winStateN = -1;

        public PathFinder(Board board, CollisionChecker stateChecker, int winStateN = 3)
        {
            _board = board;
            _collisionChecker = stateChecker;
            _winStateN = winStateN;
        }

        public PathFinderResult GetResultBFS()
        {
            Tile[][] tiles = _board.Tiles;

            //key int - state at which win state occured
            IDictionary<int, Tile[][]> winStates = new Dictionary<int, Tile[][]>();

            int statesExplored = 0;

            IDictionary<int, Tile[][]> states = new Dictionary<int, Tile[][]>();

            DateTime startTime = DateTime.Now;

            IDictionary<DateTime, Tile[][]> timeSpans = new Dictionary<DateTime, Tile[][]>();
            IDictionary<TimeSpan, Tile[][]> timeResults = new Dictionary<TimeSpan, Tile[][]>();

            SearchBFS(tiles, 0, states, winStates, ref statesExplored, timeSpans);

            foreach (var item in timeSpans)
            {
                timeResults.Add(item.Key - startTime, item.Value);
            }

            return new PathFinderResult()
            {
                StatesChecked = statesExplored,
                WinStates = winStates,
                States = states,
                TimeResults = timeResults
            };

        }

        private void SearchBFS(Tile[][] tiles, int row, IDictionary<int, Tile[][]> states, IDictionary<int, Tile[][]> winStates, ref int statesExplored, IDictionary<DateTime, Tile[][]> timeSpans)
        {
            if (winStates.Count >= _winStateN)
            {
                return;
            }

            Tile[] searchRow = tiles[row];

            // foreach non skipped tile, select it and check for win state.
            GetWinStatesInRow(tiles, ref row, states, winStates, ref statesExplored, searchRow, timeSpans);

            // foreach non skipped tile, select it and go lower(deeper) 
            FindNewStates(tiles, ref row, states, winStates, ref statesExplored, searchRow, timeSpans);

            UnskipTheRow(tiles, row);
            UnplaceQueen(tiles, row);
        }

        private void UnplaceQueen(Tile[][] tiles, int row)
        {
            for (int i = 0; i < tiles.Length; i++)
            {
                tiles[row][i].IsPlaced = false;
            }
        }

        private void FindNewStates(Tile[][] tiles, ref int row, IDictionary<int, Tile[][]> states, IDictionary<int, Tile[][]> winStates, ref int statesExplored, Tile[] searchRow, IDictionary<DateTime, Tile[][]> timeSpans)
        {
            for (int i = 0; i < searchRow.Length; i++)
            {
                if (tiles[row][i].Skip == false && tiles[row][i].IsPlaced == false)
                {
                    tiles[row][i].IsPlaced = true;

                    //check if can go lower, if yes mark skip certain tiles
                    if (row + 1 < tiles.Length)
                    {
                        MarkNextRowithSkips(tiles, row, i);

                        if (i == 3 && row == 1)
                        {
                            Console.WriteLine("Before going bfs again...");
                        }
                        Tile[][] stateCopy = DeepCopy(tiles);
                        statesExplored++;
                        states.Add(statesExplored, stateCopy);

                        SearchBFS(tiles, row + 1, states, winStates, ref statesExplored, timeSpans);

                    }

                    //mark as explored
                    tiles[row][i].Explored = true;
                    //unplace it 
                    tiles[row][i].IsPlaced = false;
                }
            }
        }

        private void GetWinStatesInRow(Tile[][] tiles, ref int row, IDictionary<int, Tile[][]> states, IDictionary<int, Tile[][]> winStates, ref int statesExplored, Tile[] searchRow, IDictionary<DateTime, Tile[][]> timeSpans)
        {
            for (int i = 0; i < searchRow.Length; i++)
            {
                if (searchRow[i].IsPlaced)
                    throw new InvalidOperationException("This should not have happened, cant search row with already placed queens in it");

                if (searchRow[i].Skip == false)
                {
                    tiles[row][i].IsPlaced = true;

                    if (i == 3 && row == 1)
                    {
                        Console.WriteLine("Before going bfs again... in a GetWinState");
                    }
                    Tile[][] stateCopy = DeepCopy(tiles);

                    statesExplored++;

                    if (CheckWin(tiles))
                    {
                        winStates.Add(statesExplored, stateCopy);
                        timeSpans.Add(DateTime.Now, stateCopy);
                    }

                    states.Add(statesExplored, stateCopy);

                    tiles[row][i].IsPlaced = false;
                }
            }
        }

        private void UnskipTheRow(Tile[][] tiles, int row)
        {
            for (int i = 0; i < tiles.Length; i++)
            {
                tiles[row][i].Skip = false;
            }
        }

        private void MarkNextRowithSkips(Tile[][] tiles, int currentRow, int collumn)
        {

            //imidiate diagonal collisions
            if (collumn + 1 < tiles.Length && currentRow + 1 < tiles.Length)
                tiles[currentRow + 1][collumn + 1].Skip = true;
            if (currentRow + 1 < tiles.Length)
                tiles[currentRow + 1][collumn].Skip = true;
            if (collumn - 1 >= 0 && currentRow + 1 < tiles.Length)
                tiles[currentRow + 1][collumn - 1].Skip = true;


            // colisions raining down in collumns from from above
            List<int> deathCollumns = new List<int>();
            for (int i = 0; i < tiles.Length; i++)
            {
                for (int j = 0; j < tiles[i].Length; j++)
                {
                    if (tiles[i][j].IsPlaced)
                    {
                        deathCollumns.Add(j);
                    }
                }
            }
            foreach (int dC in deathCollumns)
            {
                tiles[currentRow + 1][dC].Skip = true;
            }

            // colisions raining down from above and sideways
            List<int> deathDiagonals = new List<int>();
            for (int i = 0; i < tiles.Length; i++)
            {
                for (int j = 0; j < tiles[i].Length; j++)
                {
                    if (tiles[i][j].IsPlaced)
                    {
                        deathDiagonals.AddRange(DiagonalIndexesCalculateCurrentRowSkipForDiagonal(tiles, i, j, currentRow));
                    }
                }
            }

            foreach (int dD in deathDiagonals)
            {
                tiles[currentRow + 1][dD].Skip = true;
            }
        }

        private IEnumerable<int> DiagonalIndexesCalculateCurrentRowSkipForDiagonal(Tile[][] tiles, int i, int j, int currentRow)
        {
            List<int> deathDiagonals = new List<int>();

            int row = i;
            int clm = j;

            //try to add lower right
            while (row < currentRow)
            {
                row++;
                clm++;
            }

            if (clm + 1 < tiles.Length)
                tiles[row + 1][clm + 1].Skip = true;


            row = i;
            clm = j;

            //try to add lower left
            while (row < currentRow)
            {
                row++;
                clm--;
            }

            if (clm - 1 > 0)
                tiles[row + 1][clm - 1].Skip = true;


            return deathDiagonals;
        }

        public bool CheckWin(Tile[][] tiles)
        {
            if (_collisionChecker.HasCollisions(tiles))
                return false;

            int winNumber = 0;

            if (tiles.Length == 2)
            {
                winNumber = 1;
            }
            else if (tiles.Length == 3)
            {
                winNumber = 2;
            }
            else
            {
                winNumber = tiles.Length;
            }

            if (CountQueens(tiles) == winNumber)
            {
                return true;
            }

            return false;
        }

        private int CountQueens(Tile[][] tiles)
        {
            int count = 0;
            foreach (Tile[] row in tiles)
            {
                foreach (Tile tile in row)
                {
                    if (tile.IsPlaced)
                        count++;
                }
            }
            return count;
        }

        private Tile[][] DeepCopy(Tile[][] tiles)
        {

            Tile[][] copy = new Tile[tiles.Length][];


            for (int i = 0; i < tiles.Length; i++)
            {
                copy[i] = new Tile[tiles.Length];
                for (int j = 0; j < tiles[i].Length; j++)
                {
                    copy[i][j] = new Tile()
                    {
                        Id = tiles[i][j].Id,
                        IsPlaced = tiles[i][j].IsPlaced,
                        Skip = tiles[i][j].Skip
                    };
                }
            }
            return copy;

        }




    }
}
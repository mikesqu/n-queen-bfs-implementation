using QueenChess;

namespace QueenChessTests;

public class PathFinderTests
{
    [Fact]
    public void CheckWin_PassedWith2WinState_True()
    {

        /*
            x|_|_
            _|_|x
            _|_|_
        */
        int idCounter = 0;
        int boardSize = 3;
        Tile[][] winState = new Tile[boardSize][];
        for (int i = 0; i < winState.Length; i++)
        {
            winState[i] = new Tile[boardSize];
            for (int j = 0; j < winState[i].Length; j++)
            {
                winState[i][j] = new Tile()
                {
                    Id = idCounter++
                };
            }
        }

        winState[2][0].IsPlaced = true;
        winState[1][2].IsPlaced = true;

        CollisionChecker cC = new CollisionChecker();
        PathFinder sut = new PathFinder(new Board(boardSize) { Tiles = winState }, cC);

        Assert.True(sut.CheckWin(winState));

    }

    [Fact]
    public void CheckWin_PassedWithRowCollisionState_True()
    {

        /*
            x|_|x
            _|_|_
            _|_|_
        */
        int idCounter = 0;
        int boardSize = 3;
        Tile[][] winState = new Tile[boardSize][];
        for (int i = 0; i < winState.Length; i++)
        {
            winState[i] = new Tile[boardSize];
            for (int j = 0; j < winState[i].Length; j++)
            {
                winState[i][j] = new Tile()
                {
                    Id = idCounter++
                };
            }
        }

        winState[0][0].IsPlaced = true;
        winState[0][2].IsPlaced = true;

        CollisionChecker cC = new CollisionChecker();
        PathFinder sut = new PathFinder(new Board(boardSize) { Tiles = winState }, cC);

        Assert.False(sut.CheckWin(winState));

    }

    [Fact]
    public void CheckWin_PassedWith4WinState_False()
    {

        /*
            _|_|x|_
            x|_|_|_
            _|_|_|x
            -|x|-|-
        */
        int idCounter = 0;
        int boardSize = 4;
        Tile[][] winState = new Tile[boardSize][];
        for (int i = 0; i < winState.Length; i++)
        {
            winState[i] = new Tile[boardSize];
            for (int j = 0; j < winState[i].Length; j++)
            {
                winState[i][j] = new Tile()
                {
                    Id = idCounter++
                };
            }
        }

        winState[0][2].IsPlaced = true;
        winState[1][0].IsPlaced = true;
        winState[2][3].IsPlaced = true;
        winState[3][1].IsPlaced = true;

        CollisionChecker cC = new CollisionChecker();
        PathFinder sut = new PathFinder(new Board(boardSize) { Tiles = winState }, cC);

        Assert.True(sut.CheckWin(winState));

    }

    [Fact]
    public void CheckWin_PassedWith4CollisionState_True()
    {

        /*
            _|_|x|_
            x|_|_|_
            _|_|_|x
            _|_|x|_
        */
        int idCounter = 0;
        int boardSize = 4;
        Tile[][] collisionState = new Tile[boardSize][];
        for (int i = 0; i < collisionState.Length; i++)
        {
            collisionState[i] = new Tile[boardSize];
            for (int j = 0; j < collisionState[i].Length; j++)
            {
                collisionState[i][j] = new Tile()
                {
                    Id = idCounter++
                };
            }
        }

        collisionState[0][2].IsPlaced = true;
        collisionState[1][0].IsPlaced = true;
        collisionState[2][3].IsPlaced = true;
        collisionState[3][2].IsPlaced = true;

        CollisionChecker cC = new CollisionChecker();
        PathFinder sut = new PathFinder(new Board(boardSize) { Tiles = collisionState }, cC);

        Assert.False(sut.CheckWin(collisionState));

    }

    [Fact]
    public void SearchBFS_PassedBoardSize3_ExpectedPathFinderResult()
    {

        /*
            Expected PathFinderResult:
                checked states: 8
                win states: 2
                win states tiles:

                x|_|_
                -|-|x
                _|_|_

                _|_|x
                x|-|-
                _|_|_
        */
        Dictionary<int, Tile[][]> winStates = new Dictionary<int, Tile[][]>();

        PathFinderResult expectedResult = new PathFinderResult()
        {
            StatesChecked = 8,
            WinStates = winStates
        };

        int boardSize = 3;

        Board board = new Board(boardSize);

        CollisionChecker cC = new CollisionChecker();

        PathFinder sut = new PathFinder(board, cC);

        PathFinderResult result = sut.GetResultBFS();

        Assert.Equal(2, result.WinStates.Count);
    }


    [Fact]
    public void SearchBFS_PassedBoardSize8_ExpectedPathFinderResult()
    {

        /*
            Expected PathFinderResult:
                checked states: 8
                win states: 2
                
        */
        Dictionary<int, Tile[][]> winStates = new Dictionary<int, Tile[][]>();

        PathFinderResult expectedResult = new PathFinderResult()
        {
            StatesChecked = 8,
            WinStates = winStates
        };

        int boardSize = 8;

        Board board = new Board(boardSize);

        CollisionChecker cC = new CollisionChecker();

        PathFinder sut = new PathFinder(board, cC, 100);

        PathFinderResult result = sut.GetResultBFS();

        Assert.Equal(92, result.WinStates.Count);
    }

    [Fact]
    public void SearchBFS_PassedWith5SizeBoard_DoesntContainIncorrectState()
    {
        /*
            Incorrent state with diagonal non skipping:

            |q|_|_|_|_|
            |\|\|_|_|q|
            |\|q|_|\|\|
            |_|_|_|_|_|
            |_|_|_|_|_|
        */

        Dictionary<int, Tile[][]> winStates = new Dictionary<int, Tile[][]>();

        int boardSize = 5;

        Board board = new Board(boardSize);

        CollisionChecker cC = new CollisionChecker();

        PathFinder sut = new PathFinder(board, cC, 100);

        PathFinderResult result = sut.GetResultBFS();

        Tile[][] invalidState = new Tile[boardSize][];

        for (int i = 0; i < boardSize; i++)
        {
            invalidState[i] = new Tile[boardSize];
            for (int j = 0; j < boardSize; j++)
            {
                invalidState[i][j] = new Tile();
            }
        }

        invalidState[0][0].IsPlaced = true;
        invalidState[1][0].Skip = true;
        invalidState[1][1].Skip = true;
        invalidState[1][4].IsPlaced = true;
        invalidState[2][0].Skip = true;
        invalidState[2][1].IsPlaced = true;
        invalidState[2][3].Skip = true;
        invalidState[2][4].Skip = true;


        Assert.False(ContainsState(result.States, invalidState));

    }


    [Fact]
    public void SearchBFS_PassedWith5SizeBoard_ContainCorrectState()
    {
        /*
            x|_|_
            -|-|x
            _|_|_
        */

        Dictionary<int, Tile[][]> winStates = new Dictionary<int, Tile[][]>();

        int boardSize = 3;

        Board board = new Board(boardSize);

        CollisionChecker cC = new CollisionChecker();

        PathFinder sut = new PathFinder(board, cC, 2);

        PathFinderResult result = sut.GetResultBFS();

        Tile[][] validState = new Tile[boardSize][];

        for (int i = 0; i < boardSize; i++)
        {
            validState[i] = new Tile[boardSize];
            for (int j = 0; j < boardSize; j++)
            {
                validState[i][j] = new Tile();
            }
        }

        validState[0][0].IsPlaced = true;
        validState[1][2].IsPlaced = true;
        validState[1][0].Skip = true;
        validState[1][1].Skip = true;

        Assert.True(ContainsState(result.WinStates, validState));

    }

    private bool ContainsState(IDictionary<int, Tile[][]> states, Tile[][] invalidState)
    {
        int stateSize = invalidState.Length * invalidState[0].Length;
        int cnt = 0;
        foreach (var state in states)
        {
            for (int i = 0; i < state.Value.Length; i++)
            {
                for (int j = 0; j < state.Value[i].Length; j++)
                {
                    if (stateSize <= cnt)
                    {
                        return true;
                    }

                    if (state.Value[i][j].CompareTo(invalidState[i][j]) == 0)
                        cnt++;
                    else
                    {
                        cnt = 0;
                        goto nexState;
                    }

                }
            }
        nexState:;
        }
        return false;
    }
}
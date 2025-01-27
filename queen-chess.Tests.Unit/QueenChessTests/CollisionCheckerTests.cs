using QueenChess;

namespace QueenChessTests;

public class CollisionCheckerTests
{
    [Fact]
    public void HasCollisions_PassedWith2WinState_False()
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


        CollisionChecker sut = new CollisionChecker();

        Assert.False(sut.HasCollisions(winState));

    }

    [Fact]
    public void HasCollisions_PassedWithDiagonalCollisionState_True()
    {

        /*
            _|_|x
            _|_|_
            x|_|_
        */
        int idCounter = 0;
        int boardSize = 3;
        Tile[][] colisionState = new Tile[boardSize][];
        for (int i = 0; i < colisionState.Length; i++)
        {
            colisionState[i] = new Tile[boardSize];
            for (int j = 0; j < colisionState[i].Length; j++)
            {
                colisionState[i][j] = new Tile()
                {
                    Id = idCounter++
                };
            }
        }

        colisionState[2][0].IsPlaced = true;
        colisionState[0][2].IsPlaced = true;

        CollisionChecker sut = new CollisionChecker();

        Assert.True(sut.HasCollisions(colisionState));

    }

    [Fact]
    public void HasCollisions_PassedWithRowCollisionState_True()
    {

        /*
            x|_|x
            _|_|_
            _|_|_
        */
        int idCounter = 0;
        int boardSize = 3;
        Tile[][] colisionState = new Tile[boardSize][];
        for (int i = 0; i < colisionState.Length; i++)
        {
            colisionState[i] = new Tile[boardSize];
            for (int j = 0; j < colisionState[i].Length; j++)
            {
                colisionState[i][j] = new Tile()
                {
                    Id = idCounter++
                };
            }
        }

        colisionState[0][0].IsPlaced = true;
        colisionState[0][2].IsPlaced = true;

        CollisionChecker sut = new CollisionChecker();

        Assert.True(sut.HasCollisions(colisionState));

    }


    [Fact]
    public void HasCollisions_PassedWith4WinState_False()
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

        CollisionChecker sut = new CollisionChecker();

        Assert.False(sut.HasCollisions(winState));

    }

    [Fact]
    public void HasCollisions_PassedWith4CollisionState_True()
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

        CollisionChecker sut = new CollisionChecker();

        Assert.True(sut.HasCollisions(collisionState));

    }

}
using System.Collections.Generic;
using NUnit.Framework;

namespace TicTacToeKata
{
    [TestFixture]
    public class GameTests
    {
        [Test]
        public void GivenAnEmptyBoard_StartedStateIdFalse()
        {
            var board = new Game();

            Assert.That(board.Started, Is.False);
        }

        [Test]
        public void GivenAnEmptyBoard_BoardShouldHave9EmptySpaces()
        {
            var board = new Game();

            Assert.That(board.Board.Count, Is.EqualTo(9));
        }


        [Test]
        public void GivenBoardWithOneOrMoreMove_StartedStatedIsTrue()
        {
            var board = new Game();

            board.MakeMove(new Point(0, 0));

            Assert.That(board.Started, Is.True);
        }

        [Test]
        public void GivenAMoveAt0_0_BoardShouldMakeMoveNextToIt()
        {
            var board = new Game();

            board.MakeMove(new Point(0,0));

            Assert.That(board.GetStateAt(new Point(0, 1)), Is.EqualTo("o"));
        }

        [Test]
        public void GivenAnEmpty_BoardAt0_1_ShouldBeEmpty()
        {
            var board = new Game();

            Assert.That(board.GetStateAt(new Point(0, 1)), Is.EqualTo(""));
        }

        [Test]
        public void GivenMove0_1_ShouldHaveStateOfX()
        {
            var board = new Game();

            board.MakeMove(new Point(0, 1));

            Assert.That(board.GetStateAt(new Point(0, 1)), Is.EqualTo("x"));
        }


        [Test, Ignore]
        public void GivenAMoveAt1_0_BoardShouldMakeMoveNextToIt()
        {
            var board = new Game();

            board.MakeMove(new Point(1, 0));

            Assert.That(board.GetStateAt(new Point(0, 1)), Is.EqualTo(""));
            Assert.That(board.GetStateAt(new Point(1, 1)), Is.EqualTo("o"));

        }


    }

    public class Game
    {
        public bool Started { get; set; }
        public Dictionary<Point, string> Board { get; set; }

        public Game()
        {
            Started = false;
            
            Board = new Dictionary<Point, string>();

            InitBoard();
        }

        private void InitBoard()
        {
            for (int x = 0; x < 3; x++)
                for (int y = 0; y < 3; y++)
                    Board.Add(new Point(x, y), "");
        }

        public void MakeMove(Point point)
        {
            Started = true;
            Board.Add(point, "x");
            
        }

        public string GetStateAt(Point point)
        {
            return Board[point];
        }
    }

    public class Point  
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}

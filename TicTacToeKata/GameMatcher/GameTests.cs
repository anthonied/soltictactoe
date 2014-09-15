using NUnit.Framework;
using System;
using SolTicTacToe.Domain;
using TicTacToeQgame;

namespace GameMatcher
{
    [TestFixture]
    public class GameMatcherTests
    {
        [Test]
        public void PlayersMakeMoves()
        {
            var player1 = new TicTacToeQ { Level = 1 };
            var player2 = new TicTacToeQ { Level = 2 };

            var board = new Board();

            playerMove(player1, board, "X");
            playerMove(player2, board, "O");
            playerMove(player1, board, "X");
            playerMove(player2, board, "O");
            playerMove(player1, board, "X");
            playerMove(player2, board, "O");
            playerMove(player1, board, "X");
            playerMove(player2, board, "O");
            playerMove(player1, board, "X");
        }

        [Test]
        public void GivenEmptyBoard_BoardStateIsNotStarted()
        {
            var game = new Game();

            var board = new Board();

            var currentState = game.CheckBoardState(board);

            Assert.That(currentState, Is.EqualTo(GameState.NotStarted));

        }

        [Test]
        public void GivenOneValidMove_BoardStateIsValid()
        {
            var game = new Game();

            var board = new Board();
            board.Moves["1,0"] = "X";
            var currentState = game.CheckBoardState(board);

            Assert.That(currentState, Is.EqualTo(GameState.InProgress));
        }

        [Test]
        public void GivenThreeXTopRow_BoardStateIsWonX()
        {
            var game = new Game();

            var board = new Board();
            board.Moves["1,0"] = "X";
            board.Moves["1,1"] = "X";
            board.Moves["1,2"] = "X";
            board.Moves["2,2"] = "O";
            board.Moves["2,1"] = "O";
            var currentState = game.CheckBoardState(board);
            game.CheckBoardState(board);
            Assert.That(currentState, Is.EqualTo(GameState.WonX));
        }

        [Test]
        public void GivenThreeOTopRow_BoardStateIsWonO()
        {
            var game = new Game();

            var board = new Board();
            board.Moves["1,0"] = "O";
            board.Moves["1,1"] = "O";
            board.Moves["1,2"] = "O";
            board.Moves["2,2"] = "X";
            board.Moves["2,1"] = "X";
            var currentState = game.CheckBoardState(board);
            game.CheckBoardState(board);
            Assert.That(currentState, Is.EqualTo(GameState.WonO));
        }
        
        [Test]
        public void GivenXCoordinateSmallerThanZero_MoveIsNotLegit()
        {
            var game = new Game();

            var board = new Board();
            var newMove = new Coordinate(-1, 0);
            
            var isLegit = game.CheckMoveIsLegit(board, newMove);

            Assert.False(isLegit);

        }

        [Test]
        public void GivenXNotLegitMove_GameStateShouldBeInvalidX()
        {
            var game = new Game();

            var board = new Board();
            var newMove = new Coordinate(-1, 0);

            game.ApplyMove(newMove, board, "X");

            var currentState = game.CheckBoardState(board);

            Assert.That(currentState, Is.EqualTo(GameState.InvalidX));

        }

        [Test]
        public void GivenYNotLegitMove_GameStateShouldBeInvalidY()
        {
            var game = new Game();

            var board = new Board();
            var newMove = new Coordinate(-1, 0);

            game.ApplyMove(newMove, board, "O");

            var currentState = game.CheckBoardState(board);

            Assert.That(currentState, Is.EqualTo(GameState.InvalidO));

        }

        [Test]
        public void GivenXOnOccupiedCell_BoardStateShouldBeInvalidX()
        {
            var game = new Game();

            var board = new Board();
            board.Moves["1,0"] = "X";

            var newMove = new Coordinate(1, 0);

            game.ApplyMove(newMove, board, "X");

            var currentState = game.CheckBoardState(board);

            Assert.That(currentState, Is.EqualTo(GameState.InvalidX));
        }

        [Test]
        public void GivenYOnOccupiedCell_BoardStateShouldBeInvalidY()
        {
            var game = new Game();

            var board = new Board();
            board.Moves["2,2"] = "O";

            var newMove = new Coordinate(2, 2);

            game.ApplyMove(newMove, board, "O");

            var currentState = game.CheckBoardState(board);

            Assert.That(currentState, Is.EqualTo(GameState.InvalidO));
        }

        [Test]
        public void GivenAFullBoardWithNoWinner_BoardStateShouldBeDraw()
        {
            var game = new Game();

            var board = new Board();
            board.Moves["0,0"] = "X";
            board.Moves["1,0"] = "O";
            board.Moves["2,0"] = "X";
            board.Moves["0,1"] = "O";
            board.Moves["1,1"] = "X";
            board.Moves["2,1"] = "O";
            board.Moves["0,2"] = "O";
            board.Moves["1,2"] = "X";
            board.Moves["2,2"] = "O";
            
            var currentState = game.CheckBoardState(board);

            Assert.That(currentState, Is.EqualTo(GameState.Draw));
        }

        [Test]
        public void GivenAFullBoardWithXWinner_BoardStateShouldBeWonX()
        {
            var game = new Game();

            var board = new Board();
            board.Moves["0,0"] = "X";
            board.Moves["1,0"] = "O";
            board.Moves["2,0"] = "X";
            board.Moves["0,1"] = "O";
            board.Moves["1,1"] = "X";
            board.Moves["2,1"] = "O";
            board.Moves["0,2"] = "X";
            board.Moves["1,2"] = "O";
            board.Moves["2,2"] = "X";
            
            var currentState = game.CheckBoardState(board);

            Assert.That(currentState, Is.EqualTo(GameState.WonX));
        }
        

        private void playerMove(ITicTacToe player, Board board, string markToMake)
        {
            var playerMove = player.MakeMove(board, markToMake);
            board.Moves[playerMove.Key] = markToMake;

            writeToConsole(board);
        }

        private void writeToConsole(Board board)
        {
            for (int x = 0; x < 3; x++)
            {
                for (int y = 0; y < 3; y++)
                {
                    var coordinate = new Coordinate(x, y);
                    Console.Write(" {0} ", board.Moves.ContainsKey(coordinate.Key) ? board.Moves[coordinate.Key] : ".");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }


}

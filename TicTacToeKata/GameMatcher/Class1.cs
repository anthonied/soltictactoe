using System.Runtime;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        private void playerMove(ITicTacToe player, Board board, string markToMake)
        {
            var playerMove = player.MakeMove(board, markToMake);
            board.Moves[playerMove.Key] = markToMake;

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

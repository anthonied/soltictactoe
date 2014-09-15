using System.Web.Mvc;
using ElsTicTacToeTests;
using jhhwilliams;
using Microsoft.Ajax.Utilities;
using SolTicTacToe.Domain;

namespace TicTacToe.Web.Controllers
{
    public class GameController : Controller
    {

        public ActionResult Index()
        {
            var game = new Game();
            var board = new Board();
            var p1 = new TttGame();
            var p2 = new TicTacToeGame();

            while (game.State == GameState.InProgress || game.State == GameState.NotStarted)
            {
                var nextMoveX = p1.MakeMove(board, "X");
                game.ApplyMove(nextMoveX, board, "X");
                game.State = game.CheckBoardState(board);
                if (game.State != GameState.InProgress)
                    break;
                var nextMoveY = p1.MakeMove(board, "O");
                game.ApplyMove(nextMoveY, board, "O");
                game.State = game.CheckBoardState(board);
            }



            var model = new GameModel {Board = new string[9]};
            model.Board[0] = board.Moves.ContainsKey("0,0") ? board.Moves["0,0"] : "";
            model.Board[1] = board.Moves.ContainsKey("1,0") ? board.Moves["1,0"] : "";
            model.Board[2] = board.Moves.ContainsKey("2,0") ? board.Moves["2,0"] : "";
            model.Board[3] = board.Moves.ContainsKey("0,1") ? board.Moves["0,1"] : "";
            model.Board[4] = board.Moves.ContainsKey("1,1") ? board.Moves["1,1"] : "";
            model.Board[5] = board.Moves.ContainsKey("2,1") ? board.Moves["2,1"] : "";
            model.Board[6] = board.Moves.ContainsKey("0,2") ? board.Moves["0,2"] : "";
            model.Board[7] = board.Moves.ContainsKey("1,2") ? board.Moves["1,2"] : "";
            model.Board[8] = board.Moves.ContainsKey("2,2") ? board.Moves["2,2"] : "";
            return View(model);
        }

        
        
    }

    public class GameModel
    {
        public string[] Board { get; set; }
    }
}
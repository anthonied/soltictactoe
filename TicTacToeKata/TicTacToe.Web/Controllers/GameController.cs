using jhhwilliams;
using SolTicTacToe.Domain;
using System.Web.Mvc;

namespace TicTacToe.Web.Controllers
{
    public class GameController : Controller
    {
        public ActionResult Index()
        {
            setSession();

            var game = new Game();
            var board = new Board();

            Session["Games"] = (int)Session["Games"] + 1;

            playGame(game, board);

            var model = new GameModel { Board = new string[9] };
            model.Board[0] = board.Moves.ContainsKey("0,0") ? board.Moves["0,0"] : "";
            model.Board[1] = board.Moves.ContainsKey("1,0") ? board.Moves["1,0"] : "";
            model.Board[2] = board.Moves.ContainsKey("2,0") ? board.Moves["2,0"] : "";
            model.Board[3] = board.Moves.ContainsKey("0,1") ? board.Moves["0,1"] : "";
            model.Board[4] = board.Moves.ContainsKey("1,1") ? board.Moves["1,1"] : "";
            model.Board[5] = board.Moves.ContainsKey("2,1") ? board.Moves["2,1"] : "";
            model.Board[6] = board.Moves.ContainsKey("0,2") ? board.Moves["0,2"] : "";
            model.Board[7] = board.Moves.ContainsKey("1,2") ? board.Moves["1,2"] : "";
            model.Board[8] = board.Moves.ContainsKey("2,2") ? board.Moves["2,2"] : "";

            model.Games = (int)Session["Games"];
            model.Xwins = (int)Session["X"];
            model.Owins = (int)Session["O"];
            model.Draws = (int)Session["Draws"];
            return View(model);
        }

        private void setSession()
        {
            if (Session["Games"] == null)
            {
                Session["Games"] = 0;
                Session["X"] = 0;
                Session["O"] = 0;
                Session["Draws"] = 0;
            }
        }

        public ActionResult MultiGame()
        {
            setSession();

            for (int i = 0; i < 50; i++)
            {
                var game = new Game();
                var board = new Board();

                Session["Games"] = (int)Session["Games"] + 1;

                playGame(game, board);
            }
           

            var model = new MultiGameModel();
            model.Games = (int)Session["Games"];
            model.Xwins = (int)Session["X"];
            model.Owins = (int)Session["O"];
            model.Draws = (int)Session["Draws"];
            
            return View(model);
        }

        private void playGame(Game game, Board board)
        {
            var p2 = new JhTttGame();
            var p1 = new TicTacToeQgame.TicTacToeQ { Level =  4};

            while (game.State == GameState.InProgress || game.State == GameState.NotStarted)
            {
                var nextMoveX = p1.MakeMove(board, "X");
                game.ApplyMove(nextMoveX, board, "X");
                game.State = game.CheckBoardState(board);
                if (game.State != GameState.InProgress)
                    break;
                var nextMoveY = p2.MakeMove(board, "O");
                game.ApplyMove(nextMoveY, board, "O");
                game.State = game.CheckBoardState(board);
            }

            switch (game.State)
            {
                case GameState.WonX:
                    Session["X"] = (int)Session["X"] + 1;
                    break;

                case GameState.WonO:
                    Session["O"] = (int)Session["O"] + 1;
                    break;

                case GameState.Draw:
                    Session["Draws"] = (int)Session["Draws"] + 1;
                    break;
            }
        }

        public ActionResult Reset()
        {
            Session["Games"] = 0;
            Session["X"] = 0;
            Session["O"] = 0;
            Session["Draws"] = 0;
            return RedirectToAction("Index");
        }
    }

    public class MultiGameModel
    {
        public int Games { get; set; }

        public int Xwins { get; set; }

        public int Owins { get; set; }

        public int Draws { get; set; }

        public string Player1 { get; set; }

        public string Player2 { get; set; }
    }

    public class GameModel
    {
        public string[] Board { get; set; }

        public int Games { get; set; }

        public int Xwins { get; set; }

        public int Owins { get; set; }

        public int Draws { get; set; }

        public string Player1 { get; set; }

        public string Player2 { get; set; }
    }
}
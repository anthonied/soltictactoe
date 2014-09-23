using System;
using System.Linq;
using Elsi;
using jhhwilliams;
using Microsoft.Owin.Security.Cookies;
using Morpheus;
using MyTickTackToe;
using SolTicTacToe.Domain;
using System.Web.Mvc;
using System.Collections.Generic;

namespace TicTacToe.Web.Controllers
{
    public class GameController : Controller
    {
        public ActionResult Index()
        {
            setSession();

            var p1 = new JhTttGame();
            var p2 = new BigToe();

            var game = new Game();
            var board = new Board();

            Session["Games"] = (int)Session["Games"] + 1;

            playGame(game, board, p1, p2);

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

			model.MovesInOrder = game.MovesInOrder;
            return View(model);
        }

       
        public ActionResult MultiGame()
        {
           //var players = getPlayers();

           // var model = new MultiGameModel {MatchUps = new List<PlayerGame>()};
           // players.ForEach(actionPlayer =>
           // {
           //     var matchup = new PlayerGame
           //     {
           //         PlayerTag = actionPlayer.TicTacTag,
           //         GamesWhereImPlayingFirst = new List<GameResult>(),
           //         GamesWhereImPlayingSecond = new List<GameResult>()
           //     };

           //     players.ForEach(opponent =>
           //     {
           //         var matchupResultsWhereActionPlayerPlaysFirst = new GameResult
           //         {
           //             Draws = 0,
           //             Owins = 0,
           //             Xwins = 0,
           //             Player1Tag = actionPlayer.TicTacTag,
           //             Player2Tag = opponent.TicTacTag
           //         };

           //         var matchupResultsWhereActionPlayerPlaysLast = new GameResult
           //         {
           //             Draws = 0,
           //             Owins = 0,
           //             Xwins = 0,
           //             Player1Tag = opponent.TicTacTag,
           //             Player2Tag = actionPlayer.TicTacTag
           //         };

           //         for (int i = 0; i < 100; i++)
           //         {
           //             var game1 = new Game();
           //             var board1 = new Board();
           //             var game2 = new Game();
           //             var board2 = new Board();

           //             var resultWhereActionIsFirst = playGame(game1, board1, actionPlayer, opponent);
           //             handleResult(matchupResultsWhereActionPlayerPlaysFirst, resultWhereActionIsFirst);

           //             var resultWhereActionIsLast = playGame(game2, board2, opponent, actionPlayer);
           //             handleResult(matchupResultsWhereActionPlayerPlaysLast, resultWhereActionIsLast);
           //         }
           //         matchup.GamesWhereImPlayingFirst.Add(matchupResultsWhereActionPlayerPlaysFirst);
           //         matchup.GamesWhereImPlayingSecond.Add(matchupResultsWhereActionPlayerPlaysLast);
           //     });
           //     calculateTotals(matchup);
           //     model.MatchUps.Add(matchup);
           // });


            
                       return View("");
        }

        private void calculateTotals(PlayerGame matchup)
        {
            matchup.TotalWinFirst = matchup.GamesWhereImPlayingFirst.Sum(game => game.Xwins);
            matchup.TotalLostFirst = matchup.GamesWhereImPlayingFirst.Sum(game => game.Owins);
            matchup.TotalDrawFirst = matchup.GamesWhereImPlayingFirst.Sum(game => game.Draws);

            matchup.TotalWinLast = matchup.GamesWhereImPlayingSecond.Sum(game => game.Owins);
            matchup.TotalLostLast = matchup.GamesWhereImPlayingSecond.Sum(game => game.Xwins);
            matchup.TotalDrawLast = matchup.GamesWhereImPlayingSecond.Sum(game => game.Draws);
        }

        private void handleResult(GameResult resultTotal, GameResult resultOfAGame)
        {
            resultTotal.Draws += resultOfAGame.Draws;
            resultTotal.Xwins += resultOfAGame.Xwins;
            resultTotal.Owins += resultOfAGame.Owins;
        }

        private List<ITicTacToe> getPlayers()
        {
            return  new List<ITicTacToe>
            {
                new TicTacToeQgame.TicTacToeQ {Level = 0},
                new TicTacToeQgame.TicTacToeQ {Level = 1},
                new TicTacToeQgame.TicTacToeQ {Level = 2},
                new TicTacToeQgame.TicTacToeQ {Level = 3},
                new TicTacToeQgame.TicTacToeQ {Level = 4},
                new TicTacJoe.TicTacJoe(),
                new JhTttGame(),
                new TicTacToeSukkel.TicTacToeGame(),
                new MyTickTackToeGame(),
                new BigToe(),
                new TicTacToeGame()
            };
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


		private Board cloneBoard(Board board)
		{
			return new Board { Moves = new Dictionary<string, string>(board.Moves) };
		}

		private GameResult playGame(Game game, Board board, ITicTacToe player1, ITicTacToe player2)
		{
		    var result = new GameResult
		    {
		        Draws = 0,
		        Xwins = 0,
		        Owins = 0,
		        Player1Tag = player1.TicTacTag,
		        Player2Tag = player2.TicTacTag,
		    };

            while (game.State == GameState.InProgress || game.State == GameState.NotStarted)
            {
                var nextMoveX = player1.MakeMove(cloneBoard(board), "X");
                game.ApplyMove(nextMoveX, board, "X");
                game.State = game.CheckBoardState(board);
                if (game.State != GameState.InProgress)
                    break;
                var nextMoveY = player2.MakeMove(cloneBoard(board), "O");
                game.ApplyMove(nextMoveY, board, "O");
                game.State = game.CheckBoardState(board);
            }

            switch (game.State)
            {
                case GameState.WonX:
                   // Session["X"] = (int)Session["X"] + 1;
                    result.Xwins = 1;    
                    break;

                case GameState.WonO:
                 //   Session["O"] = (int)Session["O"] + 1;
                    result.Owins = 1;
                    break;

                case GameState.Draw:
                    //Session["Draws"] = (int)Session["Draws"] + 1;
                    result.Draws = 1;
                    break;
            }
		    return result;
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
       public List<PlayerGame> MatchUps { get; set; } 
    }

    public class PlayerGame
    {
        public string PlayerTag { get; set; }
        public List<GameResult> GamesWhereImPlayingFirst { get; set; }
        public List<GameResult> GamesWhereImPlayingSecond { get; set; }
        public int TotalWinFirst { get; set; }
        public int TotalLostFirst { get; set; }
        public int TotalDrawFirst { get; set; }
        public int TotalWinLast { get; set; }
        public int TotalLostLast { get; set; }
        public int TotalDrawLast { get; set; }

        public int TotalScore
        {
            get { return (TotalWinFirst*2) + TotalDrawFirst + (TotalWinLast*3) + TotalDrawLast; }
        }
    }
    public class   GameResult
    {

        public int Xwins { get; set; }

        public int Owins { get; set; }

        public int Draws { get; set; }

        public string Player1Tag { get; set; }

        public string Player2Tag { get; set; }
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

		public List<Coordinate> MovesInOrder { get; set; }
    }
}
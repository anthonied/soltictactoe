using System;
using System.Collections.Generic;
using System.Linq;
using CoordinateKey = System.String;
using Mark = System.String;

namespace SolTicTacToe.Domain
{
    public class Game : ITicTacToe
    {
        public GameState State { get; set; }

        public Coordinate MakeMove(Board currentBoard, Mark markToMake)
        {
            throw new NotImplementedException();
        }

        public GameState CheckBoardState(Board board)
        {
            if (State == GameState.InvalidX || State == GameState.InvalidO)
                return State;

            var state = board.Moves.Count == 0 ? GameState.NotStarted : GameState.InProgress;

            if (IsWinner(board, "X"))
                return GameState.WonX;

            if (IsWinner(board, "O"))
                return GameState.WonO;

            if (board.Moves.Count == 9)
                return GameState.Draw;

            return state;
        }


        public void ApplyMove(Coordinate moveCoordinate, Board board, Mark markToMake)
        {

            var isLegit = CheckMoveIsLegit(board, moveCoordinate);

            if (!isLegit)
            {
                State = markToMake == "X" ? GameState.InvalidX : GameState.InvalidO;
                return;
            }

            board.Moves[moveCoordinate.Key] = markToMake;

        }


        private static bool _notEnoughMovesForResult(Board board)
        {
            return board.Moves.Count < 5;
        }

        public bool IsWinner(Board board, Mark mark)
        {
            var xCount = new int[4];
            var yCount = new int[4];

            return board.Moves.Any(move => board.Moves[move.Key] == mark && isThreeInRow(xCount, yCount, move.Key[0], move.Key[2]));
        }


        private bool isThreeInRow(int[] xCount, int[] yCount, char xChar, char yChar)
        {
            var x = int.Parse(xChar.ToString());
            var y = int.Parse(yChar.ToString());

            xCount[x]++;
            yCount[y]++;

            if (xCount[x] == 3 || yCount[y] == 3)
                return true;

            if (x == y) xCount[3]++;
            if (x == 2 - y) yCount[3]++;

            return xCount[3] == 3 || yCount[3] == 3;
        }

        public bool CheckMoveIsLegit(Board board, Coordinate newMove)
        {
            if (!XOnBoard(newMove)) return false;

            if (isDuplicate(newMove, board)) return false;

            return true;
        }

        private bool isDuplicate(Coordinate newMove, Board board)
        {
            return board.Moves.ContainsKey(newMove.Key);
        }

        private static bool XOnBoard(Coordinate newMove)
        {
            return newMove.X >= 0;
        }

        public Mark TicTacTag
        {
            get { throw new NotImplementedException(); }
        }
    }



    public interface ITicTacToe
    {
		string TicTacTag { get; }
		Coordinate MakeMove(Board currentBoard, Mark markToMake);
    }

    public class Board
    {
        public Board()
        {
            Moves = new Dictionary<CoordinateKey, Mark>();
        }

        public Dictionary<CoordinateKey, Mark> Moves { get; set; }
    }

    public class Coordinate
    {
        public int X { get; private set; }
        public int Y { get; private set; }

        public Coordinate(int x, int y)
        {
            X = x;
            Y = y;
        }

        public CoordinateKey Key
        {
            get { return string.Format("{0},{1}", X, Y); }
        }
    }

    public enum GameState
    {
        NotStarted,
        InProgress,
        WonX,
        WonO,
        InvalidX,
        InvalidO,
        Draw
    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolTicTacToe.Domain
{
    public class Game : ITicTacToe
    {
        public Coordinate MakeMove(Board CurrentBoard)
        {
            throw new NotImplementedException();
        }
    }

    public interface ITicTacToe
    {
        Coordinate MakeMove(Board CurrentBoard);
    }

    public class Board
    {
        public Dictionary<Coordinate, string> CurrentBoard { get; set; }
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
    }
}

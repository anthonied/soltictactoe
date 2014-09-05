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
        private int _x { get; set; }
        public int _y { get; set; }

        public Coordinate(int x, int y)
        {
            _x = x;
            _y = y;
        }
    }
}

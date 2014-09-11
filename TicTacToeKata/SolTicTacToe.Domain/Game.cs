﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoordinateKey = System.String;
using Mark = System.String;

namespace SolTicTacToe.Domain
{
	//public class Game : ITicTacToe
	//{
	//	public Coordinate MakeMove(Board currentBoard, string markToMake)
	//	{
	//		throw new NotImplementedException();
	//	}
	//}

	public interface ITicTacToe
	{
		Coordinate MakeMove(Board currentBoard, string markToMake);
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
}

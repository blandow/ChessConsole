using ChessConsole.board;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChessConsole.chess
{
    class Tower : Piece
    {
        public Tower(Color color, Board board) : base(color, board) {
        
        }

        public override string ToString()
        {
            return "T";
        }

    }
}

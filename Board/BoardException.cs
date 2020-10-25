using System;
using System.Collections.Generic;
using System.Text;

namespace ChessConsole.board
{
    class BoardException : Exception
    {
        public BoardException(string msg) : base(msg) { }
    }
}

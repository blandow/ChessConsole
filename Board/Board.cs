using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace board
{
    class Board
    {
        public int Lines { get; set; }
        public int Column { get; set; }
        private Piece[,] Pieces;

        public Board(int line, int column)
        {
            Lines = line;
            Column = column;
            Pieces = new Piece[Lines, Column];
        }

        public Piece GetPiece(int line, int column)
        {
            return Pieces[line, column];
        }

        public Piece GetPiece(Position pos)
        {
            return Pieces[pos.Line, pos.Column];
        }
        
        public void PutPiece(Piece p, Position pos)
        {
            
            if (isPieceOnPos(pos))
            {
                throw new BoardException("already exists a piece on this position");
            }
            Pieces[pos.Line, pos.Column] = p;
            p.PiecePos = pos;
        }

        public Piece removePiece(Position pos)
        {
            if (GetPiece(pos) == null)
            {
                return null;
            }
            Piece auxPiece = GetPiece(pos);

            Pieces[pos.Line,pos.Column] = null;

            return auxPiece;
        }

        public bool isPosition(Position pos)
        {
            if(pos.Line<0||pos.Column<0||pos.Line >= Lines||pos.Column >= Column)
            {
                return false;
            }
            return true;
        }

        public void isPositionValid(Position pos)
        {
            if (!isPosition(pos))
            {
                throw new BoardException("invalid position");
            }
        }

        public bool isPieceOnPos(Position pos)
        {
            isPositionValid(pos);
            return GetPiece(pos) != null;
        }
    }
}

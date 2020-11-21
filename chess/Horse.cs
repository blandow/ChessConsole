using System;
using System.Collections.Generic;
using System.Text;
using board;

namespace chess
{
    class Horse : Piece
    {
        public Horse(Color color,Board board):base(color, board) { }
        private bool canMov(Position p)
        {
            Piece aux = this.PieceBoard.GetPiece(p);
            return aux == null || aux.PieceColor != this.PieceColor;

        }
        public override bool[,] possibleMov()
        {
            bool[,] mat = new bool [PieceBoard.Lines, PieceBoard.Column];

            //if (PieceBoard.isPosition(pos) && canMov(pos))
            
            Position pos = new Position(PiecePos.Line + 1, PiecePos.Column + 2);

            if(PieceBoard.isPosition(pos) && canMov(pos))
            {
                mat[pos.Line, pos.Column] = true;
            }
            pos = new Position(PiecePos.Line + 2, PiecePos.Column + 1);

            if (PieceBoard.isPosition(pos) && canMov(pos))
            {
                mat[pos.Line, pos.Column] = true;
            }
            pos = new Position(PiecePos.Line - 1, PiecePos.Column + 2);

            if (PieceBoard.isPosition(pos) && canMov(pos))
            {
                mat[pos.Line, pos.Column] = true;
            }
            pos = new Position(PiecePos.Line + 1, PiecePos.Column - 2);

            if (PieceBoard.isPosition(pos) && canMov(pos))
            {
                mat[pos.Line, pos.Column] = true;
            }
            pos = new Position(PiecePos.Line - 1, PiecePos.Column - 2);

            if (PieceBoard.isPosition(pos) && canMov(pos))
            {
                mat[pos.Line, pos.Column] = true;
            }
            pos = new Position(PiecePos.Line - 2, PiecePos.Column - 1 );

            if (PieceBoard.isPosition(pos) && canMov(pos))
            {
                mat[pos.Line, pos.Column] = true;
            }
            pos = new Position(PiecePos.Line - 2, PiecePos.Column + 1);

            if (PieceBoard.isPosition(pos) && canMov(pos))
            {
                mat[pos.Line, pos.Column] = true;
            }
            return mat;
        }
        public override string ToString()
        {
            return "H ";
        }
    }
}

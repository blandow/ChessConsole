using System;
using System.Collections.Generic;
using System.Text;

namespace board
{
    class Piece
    {
        public Position PiecePos { get; set; }
        public Color PieceColor { get; protected set; }
        public Board PieceBoard { get; protected set; }
        public int QteMoviment { get; protected set; }

        public Piece(Color pieceColor, Board pieceBoard)
        {
            PiecePos = null;
            PieceColor = pieceColor;
            PieceBoard = pieceBoard;
            QteMoviment = 0;
        }

        public void movIncrement()
        {
            QteMoviment++;
        }

    }
}

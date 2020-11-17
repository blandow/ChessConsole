using System;
using System.Collections.Generic;
using System.Text;

namespace board
{
    abstract class Piece
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
        public void movDecrement()
        {
            QteMoviment--;
        }

        public bool isPossibleMov()
        {
            bool[,] mat = possibleMov();

            
            for (int i=0; i<PieceBoard.Lines;i++)
            {
                for (int j = 0; j < PieceBoard.Lines; j++)
                {
                    if (mat[i, j])
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        public bool canMoveToPosition(Position destiny)
        {
            return possibleMov()[destiny.Line, destiny.Column];
        }

        public abstract bool[,] possibleMov(); 
    }
}

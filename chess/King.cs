using board;


namespace chess
{
    class King : Piece
    {
        private ChessMatch KingMatch;
        public King(Color color, Board board, ChessMatch match) : base(color, board)
        {
            KingMatch = match;
        }
        public override string ToString()
        {
            return "R ";
        }
        private bool isTowerRoque(Position pos)
        {
            Piece p = PieceBoard.GetPiece(pos);
            return p != null && p is Tower && p.PieceColor == this.PieceColor && p.QteMoviment == 0;
        }
        private bool canMov(Position p)
        {
            Piece aux = this.PieceBoard.GetPiece(p);
            return aux == null || aux.PieceColor != this.PieceColor;

        }

        public override bool[,] possibleMov()
        {
            Position position = new Position(PiecePos.Line, PiecePos.Column );
            bool[,] mat = new bool[PieceBoard.Lines, PieceBoard.Column];

            for (int i = position.Line - 1; i <= position.Line + 1; i++)
            {
                for (int j = position.Column - 1; j <= position.Column + 1; j++)
                {
                    if (PieceBoard.isPosition(new Position(i, j)) && canMov(new Position(i, j)))
                    {
                        mat[i, j] = true;
                    }
                    else if (PieceBoard.isPosition(new Position(i, j)) && !canMov(new Position(i, j)))
                    {
                        mat[i, j] = false;
                    }
                }
            }
            //#expessial movies
            if(QteMoviment == 0 && !KingMatch.adversaryCheck)
            {
                Position posT = new Position(PiecePos.Line, PiecePos.Column - 3);
                Position posT2 = new Position(PiecePos.Line, PiecePos.Column + 4);
                if (isTowerRoque(posT))
                {
                    Position p1 = new Position(PiecePos.Line, PiecePos.Column - 1);
                    Position p2 = new Position(PiecePos.Line, PiecePos.Column - 2);
                    if(PieceBoard.GetPiece(p1) == null && PieceBoard.GetPiece(p2) == null)
                    {
                        mat[PiecePos.Line, PiecePos.Column - 2] = true;
                    }
                }
                if (isTowerRoque(posT2))
                {
                    Position p1 = new Position(PiecePos.Line, PiecePos.Column + 1);
                    Position p2 = new Position(PiecePos.Line, PiecePos.Column + 2);
                    Position p3 = new Position(PiecePos.Line, PiecePos.Column + 3);
                    if (PieceBoard.GetPiece(p1) == null && PieceBoard.GetPiece(p2) == null && PieceBoard.GetPiece(p3) == null)
                    {
                        mat[PiecePos.Line, PiecePos.Column + 2] = true;
                    }
                }

            }
            return mat;
        }



    }
}

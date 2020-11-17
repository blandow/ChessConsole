using board;


namespace chess
{
    class King : Piece
    {
        public King(Color color, Board board) : base(color, board)
        {

        }
        public override string ToString()
        {
            return "R ";
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

            return mat;
        }



    }
}

using board;


namespace chess
{
    class Tower : Piece
    {
        public Tower(Color color, Board board) : base(color, board) {
        
        }

        public override string ToString()
        {
            return "T ";
        }
        private bool canMov(Position p)
        {
            Piece aux = PieceBoard.GetPiece(p);
            return aux == null || aux.PieceColor != PieceColor;

        }

        public override bool[,] possibleMov()
        {
            bool[,] mat = new bool[PieceBoard.Lines, PieceBoard.Column];


            Position position = new Position(PiecePos.Line-1, PiecePos.Column);
            while (PieceBoard.isPosition(position) && canMov(position))
            {
                mat[position.Line, position.Column] = true;
                if(PieceBoard.GetPiece(position) != null && PieceBoard.GetPiece(position).PieceColor != PieceColor)
                {
                    break;
                }
                position.Line--;
            }
            
            position = new Position(PiecePos.Line + 1, PiecePos.Column);
            while (PieceBoard.isPosition(position) && canMov(position))
            {
                mat[position.Line, position.Column] = true;
                if (PieceBoard.GetPiece(position) != null && PieceBoard.GetPiece(position).PieceColor != PieceColor)
                {
                    break;
                }
                position.Line++;
            }
            
            position = new Position(PiecePos.Line , PiecePos.Column-1);
            while (PieceBoard.isPosition(position) && canMov(position))
            {
                mat[position.Line, position.Column] = true;
                if (PieceBoard.GetPiece(position) != null && PieceBoard.GetPiece(position).PieceColor != PieceColor)
                {
                    break;
                }
                position.Column--;
            }

            position = new Position(PiecePos.Line, PiecePos.Column + 1);
            while (PieceBoard.isPosition(position) && canMov(position))
            {
                mat[position.Line, position.Column] = true;
                if (PieceBoard.GetPiece(position) != null && PieceBoard.GetPiece(position).PieceColor != PieceColor)
                {
                    break;
                }
                position.Column++;
            }

            return mat;
        }
    }
}

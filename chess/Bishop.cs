
using board;
namespace chess
{
    class Bishop : Piece
    {
        public Bishop(Color color, Board board) : base(color, board)
        {

        }
        private bool canMov(Position p)
        {
            Piece aux = PieceBoard.GetPiece(p);
            return aux == null || aux.PieceColor != PieceColor;

        }
        public override bool[,] possibleMov()
        {
            bool[,] mat = new bool[PieceBoard.Lines, PieceBoard.Column];
            Position position = new Position(PiecePos.Line, PiecePos.Column);
            int acum = position.Line + 1;
            int dec = position.Line - 1;
            bool isMoveAcum = true;
            bool isMoveDec = true;
            for (int j = position.Column - 1; j >= 0; j--)
            {

                if (PieceBoard.isPosition(new Position(acum, j)) && canMov(new Position(acum, j))&&isMoveAcum)
                {
                    mat[acum, j] = true;
                }
                else
                {
                    isMoveAcum = false;
                }
                acum++;
                if (PieceBoard.isPosition(new Position(dec, j)) && canMov(new Position(dec, j))&&isMoveDec)
                {
                    mat[dec, j] = true;
                }
                else
                {
                    isMoveDec = false;
                }
                dec--;
            }
            isMoveAcum = true;
            isMoveDec = true;
            position = new Position(PiecePos.Line, PiecePos.Column);
            dec = position.Line - 1;
            acum = position.Line + 1;
            for (int j = position.Column + 1; j < PieceBoard.Column; j++)
            {

                if (PieceBoard.isPosition(new Position(acum, j)) && canMov(new Position(acum, j))&&isMoveAcum)
                {
                    mat[acum, j] = true;
                }
                else
                {
                    isMoveAcum = false;
                }
                acum++;
                if (PieceBoard.isPosition(new Position(dec, j)) && canMov(new Position(dec, j))&&isMoveDec)
                {
                    mat[dec, j] = true;
                }
                else
                {
                    isMoveDec = false;
                }
                dec--;
            }


            return mat;
        }
        public override string ToString()
        {
            return "B ";
        }
    }
}

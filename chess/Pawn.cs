
using board;
namespace chess
{
    class Pawn : Piece
    {
        private ChessMatch _match;
        public Pawn(Color color, Board board, ChessMatch match) : base(color, board) {
            _match = match;
        }
        private bool canMov(Position p)
        {
            Piece aux = this.PieceBoard.GetPiece(p);
            return aux == null;

        }
        private bool canCapture(Position p)
        {
            Piece aux = this.PieceBoard.GetPiece(p);
            if(aux == null)
            {
                return false;
            }
            return aux.PieceColor!=PieceColor;

        }
        private bool isEnemy(Position pos)
        {
            Piece p = PieceBoard.GetPiece(pos);
            return p != null && p.PieceColor != PieceColor;
        }
        public override bool[,] possibleMov()
        {
            bool[,] mat = new bool[PieceBoard.Lines, PieceBoard.Column];
            ChessMatch match = new ChessMatch();
            if (PieceColor == Color.white)
            {
                int line = PiecePos.Line;
                int col = PiecePos.Column + 1;
                int revcol = PiecePos.Column - 1;

                if (PiecePos.Line == 6)
                {

                    while (line > PiecePos.Line - 3)
                    {

                        if (PieceBoard.isPosition(new Position(PiecePos.Line - 1, col)) && canCapture(new Position(PiecePos.Line - 1, col)))
                        {
                            mat[PiecePos.Line - 1, col] = true;
                        }
                        if (PieceBoard.isPosition(new Position(PiecePos.Line - 1, revcol)) && canCapture(new Position(PiecePos.Line - 1, revcol)))
                        {
                            mat[PiecePos.Line - 1, revcol] = true;
                        }
                        if (PieceBoard.isPosition(new Position(line, PiecePos.Column)) && canMov(new Position(line, PiecePos.Column)))
                        {
                            mat[line, PiecePos.Column] = true;
                        }
                        line--;

                    }

                }
                else
                {
                    if (PieceBoard.isPosition(new Position(line - 1, col)) && canCapture(new Position(line - 1, col)))
                    {
                        mat[line - 1, col] = true;
                    }
                    if (PieceBoard.isPosition(new Position(line - 1, revcol)) && canCapture(new Position(line - 1, revcol)))
                    {
                        mat[line - 1, revcol] = true;
                    }
                    if (PieceBoard.isPosition(new Position(line - 1, PiecePos.Column)) && canMov(new Position(line - 1, PiecePos.Column)))
                    {
                        mat[line - 1, PiecePos.Column] = true;
                    }
                }

                //en passant
                if(PiecePos.Line == 3)
                {
                    Position left = new Position(PiecePos.Line, PiecePos.Column - 1);
                    if(PieceBoard.isPieceOnPos(left) && isEnemy(left) && PieceBoard.GetPiece(left) == _match._vulnerableEnPassant)
                    {
                        mat[left.Line - 1, left.Column] = true;
                    }
                    Position right = new Position(PiecePos.Line, PiecePos.Column + 1);
                    if (PieceBoard.isPieceOnPos(right) && isEnemy(right) && PieceBoard.GetPiece(right) == _match._vulnerableEnPassant)
                    {
                        mat[right.Line - 1, right.Column] = true;
                    }
                }

            }
            else
            {
                int line = PiecePos.Line;
                int col = PiecePos.Column + 1;
                int revcol = PiecePos.Column - 1;

                if (PiecePos.Line == 1)
                {

                    while (line < PiecePos.Line + 3)
                    {

                        if (PieceBoard.isPosition(new Position(PiecePos.Line + 1, col)) && canCapture(new Position(PiecePos.Line + 1, col)))
                        {
                            mat[PiecePos.Line + 1, col] = true;
                        }
                        if (PieceBoard.isPosition(new Position(PiecePos.Line + 1, revcol)) && canCapture(new Position(PiecePos.Line + 1, revcol)))
                        {
                            mat[PiecePos.Line + 1, revcol] = true;
                        }
                        if (PieceBoard.isPosition(new Position(line, PiecePos.Column)) && canMov(new Position(line, PiecePos.Column)))
                        {
                            mat[line, PiecePos.Column] = true;
                        }
                        line++;

                    }

                }
                else
                {
                    if (PieceBoard.isPosition(new Position(line + 1, col)) && canCapture(new Position(line + 1, col)))
                    {
                        mat[line + 1, col] = true;
                    }
                    if (PieceBoard.isPosition(new Position(line + 1, revcol)) && canCapture(new Position(line + 1, revcol)))
                    {
                        mat[line + 1, revcol] = true;
                    }
                    if (PieceBoard.isPosition(new Position(line + 1, PiecePos.Column)) && canMov(new Position(line + 1, PiecePos.Column)))
                    {
                        mat[line + 1, PiecePos.Column] = true;
                    }
                }
                //en passant
                if (PiecePos.Line == 4)
                {
                    Position left = new Position(PiecePos.Line, PiecePos.Column - 1);
                    if (PieceBoard.isPieceOnPos(left) && isEnemy(left) && PieceBoard.GetPiece(left) == _match._vulnerableEnPassant)
                    {
                        mat[left.Line + 1, left.Column] = true;
                    }
                    Position right = new Position(PiecePos.Line, PiecePos.Column + 1);
                    if (PieceBoard.isPieceOnPos(right) && isEnemy(right) && PieceBoard.GetPiece(right) == _match._vulnerableEnPassant)
                    {
                        mat[right.Line + 1, right.Column] = true;
                    }
                }

            }

            return mat;
        }
        public override string ToString()
        {
            return "P ";
        }
    }
}

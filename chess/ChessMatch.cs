using board;
using System.Collections.Generic;
namespace chess
{
    class ChessMatch
    {
        public int turn { get; private set; }
        public Color currentPlayer { get; private set; }
        public Board board { get; private set; }
        public bool end { get; private set; }
        private HashSet<Piece> pieces;
        private HashSet<Piece> captured;
        public bool adversaryCheck { get; private set; }

        
        public ChessMatch()
        {
            this.board = new Board(8, 8);
            this.turn = 1;
            this.currentPlayer = Color.white;
            this.end = false;

            pieces = new HashSet<Piece>();
            captured = new HashSet<Piece>();

            placingPiece();
        }

        public Piece executeMov(Position origin, Position destiny)
        {

            Piece piece = board.removePiece(origin);
            piece.movIncrement();

            var pieceCatch = board.removePiece(destiny);

            board.PutPiece(piece, destiny);
            if (pieceCatch != null)
            {
                captured.Add(pieceCatch);
            }

            return pieceCatch;
        }

        public Color adversary(Color color)
        {
            if (color == Color.white)
            {
                return Color.black;
            }
            else
            {
                return Color.white;
            }
        }

        private Piece getKingByColor(Color color)
        {
            foreach (Piece k in inGamePieces(color))
            {
                if (k is King)
                {
                    return k;
                }
            }
            return null;
        }

        public bool isCheck(Color color)
        {
            Piece k = getKingByColor(color);
            if (k == null)
            {
                throw new BoardException($"there is no king of color {color}");
            }
            foreach (Piece p in inGamePieces(adversary(color)))
            {
                bool[,] mat = p.possibleMov();

                if (mat[k.PiecePos.Line, k.PiecePos.Column])
                {
                    return true;
                }

            }
            return false;
        }

        public HashSet<Piece> capturedPieces(Color color)
        {
            HashSet<Piece> aux = new HashSet<Piece>();
            foreach (var piece in captured)
            {
                if (piece.PieceColor == color)
                {
                    aux.Add(piece);
                }
            }
            return aux;
        }

        public HashSet<Piece> inGamePieces(Color color)
        {
            HashSet<Piece> aux = new HashSet<Piece>();
            foreach (var piece in pieces)
            {
                if (piece.PieceColor == color)
                {
                    aux.Add(piece);
                }
            }
            aux.ExceptWith(capturedPieces(color));

            return aux;
        }


        public void accomplishMov(Position origin, Position destiny)
        {
            Piece capturedPiece = executeMov(origin, destiny);
            if (isCheck(currentPlayer))
            {
                undoMove(origin, destiny, capturedPiece);
                throw new BoardException("you can not put yourself in check!");
            }

            if (isCheck(adversary(currentPlayer)))
            {
                adversaryCheck = true;

            }
            else
            {
                adversaryCheck = false;
            }

            if (isCheckmate(adversary(currentPlayer)))
            {
                end = true;
                
            }
            else
            {

                turn++;
                changePlayer();
            }



        }
        private void undoMove(Position origin, Position destiny, Piece capturedPiece)
        {
            Piece p = board.removePiece(destiny);
            p.movDecrement();
            if (capturedPiece != null)
            {
                board.PutPiece(capturedPiece, destiny);
                captured.Remove(capturedPiece);
            }
            board.PutPiece(p, origin);
        }
        private void changePlayer()
        {
            if (currentPlayer == Color.white)
            {
                currentPlayer = Color.black;
            }
            else
            {
                currentPlayer = Color.white;
            }
        }

        public void validOrigin(Position origin)
        {
            if (board.GetPiece(origin) == null)
            {
                throw new BoardException("does not exist a piece in origin position");
            }
            if (currentPlayer != board.GetPiece(origin).PieceColor)
            {
                throw new BoardException("the origin piece is not yours ! ");
            }
            if (!board.GetPiece(origin).isPossibleMov())
            {
                throw new BoardException("there is not possible movement for the selected piece");
            }

        }
        public bool isCheckmate(Color color)
        {
            if (!isCheck(color))
            {
                return false;
            }
            foreach (Piece p in inGamePieces(color))
            {
                bool[,] mat = p.possibleMov();
                for (int i = 0; i < board.Lines; i++)
                {
                    for (int j = 0; j < board.Column; j++)
                    {
                        if (mat[i, j])
                        {
                            Position origin = p.PiecePos;
                            Position destiny = new Position(i, j);
                            Piece capt = executeMov(origin, destiny);
                            bool testCheck = isCheck(color);
                            undoMove(origin, destiny, capt);
                            if (!testCheck)
                            {
                                return false;
                            }
                        }
                    }

                }
            }
            return true;
        }

        public void validDestiny(Position origin, Position destiny)
        {
            if (!board.GetPiece(origin).canMoveToPosition(destiny))
            {
                throw new BoardException("Invalid destiny position");
            }
        }

        public void putNewPieces(char column, int line, Piece piece)
        {
            board.PutPiece(piece, new ChessPosition(column, line).ToPosition());
            pieces.Add(piece);

        }

        private void placingPiece()
        {
            putNewPieces('f', 1, new Tower(Color.white, board));
            putNewPieces('b', 2, new Tower(Color.white, board));
            putNewPieces('d', 1, new Tower(Color.white, board));
            putNewPieces('b', 1, new Tower(Color.white, board));
            putNewPieces('a', 1, new King(Color.white, board));

            putNewPieces('c', 8, new Tower(Color.black, board));
            putNewPieces('d', 8, new Tower(Color.black, board));
            putNewPieces('d', 7, new Tower(Color.black, board));
            putNewPieces('f', 7, new Tower(Color.black, board));
            putNewPieces('e', 8, new King(Color.black, board));

        }


    }
}

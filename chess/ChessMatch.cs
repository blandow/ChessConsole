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
        public Piece _vulnerableEnPassant { get; private set; }


        public ChessMatch()
        {
            this.board = new Board(8, 8);
            this.turn = 1;
            this.currentPlayer = Color.white;
            this.end = false;
            this._vulnerableEnPassant = null;
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
            //small roque
            if (piece is King && destiny.Column == origin.Column + 2)
            {
                Position originT = new Position(origin.Line, origin.Column + 4);
                Position destinyT = new Position(origin.Line, origin.Column + 1);
                Piece tower = board.removePiece(originT);
                tower.movIncrement();
                board.PutPiece(tower, destinyT);
            }
            //big roque
            if (piece is King && destiny.Column == origin.Column - 2)
            {
                Position originT = new Position(origin.Line, origin.Column - 3);
                Position destinyT = new Position(origin.Line, origin.Column - 1);
                Piece tower = board.removePiece(originT);
                tower.movIncrement();
                board.PutPiece(tower, destinyT);
            }
            //En Passant
            if(piece is Pawn)
            {
                if(origin.Column != destiny.Column && pieceCatch == null)
                {
                    Position posP;
                    if(piece.PieceColor == Color.white)
                    {
                        posP = new Position(destiny.Line + 1, destiny.Column);
                    }
                    else
                    {
                        posP = new Position(destiny.Line - 1, destiny.Column);
                    }
                    pieceCatch = board.removePiece(posP);
                    captured.Add(pieceCatch);
                }
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
            
            Piece p = board.GetPiece(destiny);

            //promotion
            if(p is Pawn)
            {
                if((p.PieceColor == Color.white && destiny.Line == 0) || (p.PieceColor == Color.black && destiny.Line == 7))
                {
                    p = board.removePiece(destiny);
                    pieces.Remove(p);
                    Piece queen = new Queen(p.PieceColor, board);
                    board.PutPiece(queen, destiny);
                    pieces.Add(queen);
                }
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

            //en passant
            
            if (p is Pawn && (destiny.Line == origin.Line + 2 || destiny.Line == origin.Line - 2))
            {
                _vulnerableEnPassant = p;
            }
            else
            {
                _vulnerableEnPassant = null;
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

            //small roque
            if (p is King && destiny.Column == origin.Column - 2)
            {
                Position originT = new Position(origin.Line, origin.Column - 3);
                Position destinyT = new Position(origin.Line, origin.Column - 1);
                Piece tower = board.removePiece(destinyT);
                tower.movDecrement();
                board.PutPiece(tower, originT);
            }
            //big roque
            if (p is King && destiny.Column == origin.Column + 2)
            {
                Position originT = new Position(origin.Line, origin.Column + 4);
                Position destinyT = new Position(origin.Line, origin.Column + 1);
                Piece tower = board.removePiece(destinyT);
                tower.movDecrement();
                board.PutPiece(tower, originT);
            }
            //en passant
            if(p is Pawn)
            {
                if(origin.Column != destiny.Column && capturedPiece == _vulnerableEnPassant)
                {
                    Piece pawn = board.removePiece(destiny);
                    Position posP;
                    if(p.PieceColor == Color.white)
                    {
                        posP = new Position(3, destiny.Column);
                    }
                    else
                    {
                        posP = new Position(4, destiny.Column);
                    }
                    board.PutPiece(pawn, posP);
                }
            }
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
            //White Pieces

            //pawn
            putNewPieces('a', 2, new Pawn(Color.white, board, this));
            putNewPieces('b', 2, new Pawn(Color.white, board, this));
            putNewPieces('c', 2, new Pawn(Color.white, board, this));
            putNewPieces('d', 2, new Pawn(Color.white, board, this));
            putNewPieces('e', 2, new Pawn(Color.white, board, this));
            putNewPieces('f', 2, new Pawn(Color.white, board, this));
            putNewPieces('g', 2, new Pawn(Color.white, board, this));
            putNewPieces('h', 2, new Pawn(Color.white, board, this));
            //other
            putNewPieces('a', 1, new Tower(Color.white, board));
            putNewPieces('b', 1, new Horse(Color.white, board));
            putNewPieces('c', 1, new Bishop(Color.white, board));
            putNewPieces('d', 1, new King(Color.white, board, this));
            putNewPieces('e', 1, new Queen(Color.white, board));
            putNewPieces('f', 1, new Bishop(Color.white, board));
            putNewPieces('g', 1, new Horse(Color.white, board));
            putNewPieces('h', 1, new Tower(Color.white, board));

            //black Pieces

            //pawn
            putNewPieces('a', 7, new Pawn(Color.black, board, this));
            putNewPieces('b', 7, new Pawn(Color.black, board, this));
            putNewPieces('c', 7, new Pawn(Color.black, board, this));
            putNewPieces('d', 7, new Pawn(Color.black, board, this));
            putNewPieces('e', 7, new Pawn(Color.black, board, this));
            putNewPieces('f', 7, new Pawn(Color.black, board, this));
            putNewPieces('g', 7, new Pawn(Color.black, board, this));
            putNewPieces('h', 7, new Pawn(Color.black, board, this));

            //other
            putNewPieces('a', 8, new Tower(Color.black, board));
            putNewPieces('b', 8, new Horse(Color.black, board));
            putNewPieces('c', 8, new Bishop(Color.black, board));
            putNewPieces('d', 8, new King(Color.black, board, this));
            putNewPieces('e', 8, new Queen(Color.black, board));
            putNewPieces('f', 8, new Bishop(Color.black, board));
            putNewPieces('g', 8, new Horse(Color.black, board));
            putNewPieces('h', 8, new Tower(Color.black, board));

        }


    }
}

using board;

namespace chess
{
    class ChessMatch
    {
        private int turn;
        private Color currentPlayer;
        public Board board { get; private set; }
        public bool end { get; private set; }

        public ChessMatch()
        {
            this.board = new Board(8,8);
            this.turn = 1;
            this.currentPlayer = Color.white;
            this.end = false;
            placingPiece(); 
        }

        public void executeMov(Position origin, Position destiny)
        {
            Piece piece = board.removePiece(origin);
            piece.movIncrement();
            var pieceCatch = board.removePiece(destiny);
            board.PutPiece(piece, destiny);
        }

        private void placingPiece()
        {
            board.PutPiece(new Tower(Color.white, board), new ChessPosition('c', 1).ToPosition());
        }


    }
}

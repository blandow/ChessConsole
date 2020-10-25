using System;
using ChessConsole.board;
using ChessConsole.chess;

namespace ChessConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Board b = new Board(8, 8);
            try
            {

                b.PutPiece(new King(Color.black, b), new Position(0, 0));
                b.PutPiece(new Tower(Color.black, b), new Position(1, 0));
                b.PutPiece(new King(Color.black, b), new Position(2, 2));
                b.PutPiece(new Tower(Color.black, b), new Position(4, 3));

                Scream.PrintBoard(b);

            }
            catch (BoardException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}

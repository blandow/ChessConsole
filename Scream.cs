using board;
using chess;
using System;


namespace ChessConsole
{
    class Scream
    {
        public static void PrintBoard(Board board)
        {
            for(int i = 0; i < board.Lines; i++)
            {
                Console.Write(8 - i + " ");
                for(int j=0; j<board.Column; j++)
                {
                    if (board.GetPiece(i, j) == null)
                    {
                        Console.Write("_ ");
                    }
                    else
                    {
                        PrintPiece(board.GetPiece(i, j));
                        Console.Write(" ");
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine("  a b c d e f g h");
        }

        public static void PrintPiece(Piece piece)
        {
            if (piece.PieceColor == Color.black)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write(piece);
                Console.ForegroundColor = ConsoleColor.White;
            }
            else
            {
                Console.Write(piece);
            }
        }

        public static ChessPosition ReadChessPos()
        {
            string p = Console.ReadLine();

            char column = p[0];
            int line = int.Parse(p[1] + "");

            return new ChessPosition(column, line);

        }


    }
}

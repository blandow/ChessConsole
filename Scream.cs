using board;
using chess;
using System;
using System.Collections.Generic;
using System.Globalization;


namespace ChessConsole
{
    class Scream
    {
        public static void PrintBoard(Board board)
        {
            for (int i = 0; i < board.Lines; i++)
            {
                Console.Write(8 - i + " ");
                for (int j = 0; j < board.Column; j++)
                {

                    PrintPiece(board.GetPiece(i, j));


                }
                Console.WriteLine();
            }
            Console.WriteLine(" _a _b _c _d _e _f _g _h");

        }

        public static void PrintBoard(Board board, bool[,] possiblePos)
        {
            ConsoleColor orignBackGround = Console.BackgroundColor;
            ConsoleColor markedBackGround = ConsoleColor.DarkGray;

            for (int i = 0; i < board.Lines; i++)
            {
                Console.Write(8 - i + " ");
                for (int j = 0; j < board.Column; j++)
                {
                    if (possiblePos[i, j])
                    {
                        Console.BackgroundColor = markedBackGround;
                    }
                    else
                    {
                        Console.BackgroundColor = orignBackGround;
                    }
                    PrintPiece(board.GetPiece(i, j));
                    Console.BackgroundColor = orignBackGround;

                }
                Console.WriteLine();
            }
            Console.WriteLine(" _a _b _c _d _e _f _g _h");


        }

        public static void PrintPiece(Piece piece)
        {

            if (piece == null)
            {
                Console.Write("- ");
            }
            else
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

            Console.Write(" ");

        }
        public static ChessPosition ReadChessPos()
        {
            string p = Console.ReadLine();

            char column = p[0];
            int line = int.Parse(p[1] + "");

            return new ChessPosition(column, line);

        }

        public static void printMatch(ChessMatch match)
        {
            PrintBoard(match.board);

            Console.WriteLine();
            printCapturedPieces(match);
            
            Console.WriteLine($"\nTurno: {match.turn}");
            
            if(!match.end)
            {
                Console.WriteLine($"Aguardando Jogada de: {match.currentPlayer}");
                if (match.adversaryCheck)
                {
                    Console.WriteLine("XEQUE!");
                }

            }
            else
            {
                Console.WriteLine("XEQUEMATE!!");
                Console.WriteLine($"Vencedor: {match.currentPlayer}");
            }

        }
            public static void printCapturedPieces(ChessMatch match)
        {
            Console.Write($"Peças capturadas:\nBrancas:");
            printSet(match.capturedPieces(Color.white));

            Console.Write($"Peças capturadas:\nPretas:");
            printSet(match.capturedPieces(Color.black));

        }
        public static void printSet(HashSet<Piece> pieces)
        {
            Console.Write("{");
            foreach (var p in pieces)
            {
                if (p.PieceColor == Color.black) Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write(p + " - ");
            }
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("}");
        }
    }
}

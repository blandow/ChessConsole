using ChessConsole.board;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChessConsole
{
    class Scream
    {
        public static void PrintBoard(Board board)
        {
            for(int i = 0; i < board.Lines; i++)
            {
                for(int j=0; j<board.Column; j++)
                {
                    if (board.GetPiece(i, j) == null)
                    {
                        Console.Write("- ");
                    }
                    Console.Write(board.GetPiece(i,j) +" ");
                }
                Console.WriteLine();
            }
        }
    }
}

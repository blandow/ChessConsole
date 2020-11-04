using System;
using board;
using chess;

namespace ChessConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            
            try
            {
                ChessMatch b = new ChessMatch();
                while (!b.end)
                {
                    Console.Clear();

                    Scream.PrintBoard(b.board);
                
                    Console.Write("\nOrigen:");
                    Position origin = Scream.ReadChessPos().ToPosition();

                    Console.Write("Destino:");
                    Position destiny = Scream.ReadChessPos().ToPosition();

                    b.executeMov(origin, destiny);
                }
            }
            catch (BoardException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}

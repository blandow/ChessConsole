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
                ChessMatch match = new ChessMatch();


                while (!match.end)
                {
                    try
                    {
                        Console.Clear();

                        
                        Scream.printMatch(match);
                        Console.Write("\nOrigen:");

                        Position origin = Scream.ReadChessPos().ToPosition();
                        
                        match.validOrigin(origin);

                        bool[,] possiblePos = match.board.GetPiece(origin).possibleMov();

                        Console.Clear();

                        Scream.PrintBoard(match.board, possiblePos);


                        Console.Write("Destino:");
                        Position destiny = Scream.ReadChessPos().ToPosition();
                        
                        match.validDestiny(origin, destiny);

                        match.accomplishMov(origin, destiny);
                    }
                    catch (BoardException e)
                    {
                        Console.WriteLine(e.Message);
                        Console.ReadLine();
                    }
                }
                Console.Clear();
                Scream.printMatch(match);
               
            }
            catch (BoardException e)
            {
                Console.WriteLine(e.Message);
            }


        }
    }
}

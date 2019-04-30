using System;
using Server;
namespace Testing
{
    class Program
    {
        static void Main(string[] args)
        {
            GameLogic gameLogic = new GameLogic();
            gameLogic.gameBoard = new BoardState();
            gameLogic.gameBoard.InitializeBoard();

            for (int i = 0; i < 11; i++)
            {
                gameLogic.gameBoard.board[0, i] = 1;
            }

            Console.WriteLine(gameLogic.GameWon());
            Console.Read();
        }
    }
}

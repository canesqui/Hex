using System;
using System.Collections.Generic;
using System.Text;

namespace Server
{
    public class BoardState
    {
        public int[,] board = new int[11, 11];

        public void InitializeBoard()
        {
            Array.Clear(board, 0, 121);
        }

        public void UpdateBoard(Common.Move move, bool player1)
        {
            if (player1)
            {
                board[move.x, move.y] = 1;
            }
            else
            {
                board[move.x, move.y] = 2;
            }

        }

     }
}

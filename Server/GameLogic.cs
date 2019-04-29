using System;
using System.Collections.Generic;
using System.Text;

namespace Server
{
    public class GameLogic
    {
        Random rng = new Random();
        bool isPlayer1sTurn = true;
        public int winner = 0;
        public BoardState gameBoard;

        public Common.MoveResponse Move(Common.Move move)
        {
            int i = rng.Next(0, 4);

            if (!isPlayer1sTurn) //Get help with this condition
            {
                return Common.MoveResponse.NotYourTurn;
            }
            else if (gameBoard.board[move.x, move.y] == 0)
            {
                return Common.MoveResponse.Sucess;
            }
            else if (gameBoard.board[move.x, move.y] != 0)
            {
                return Common.MoveResponse.IllegalMove;
            }
            return Common.MoveResponse.GameOver;
        }

        public void SwitchTurns()
        {
            isPlayer1sTurn = !isPlayer1sTurn;
        }

        public bool IsPlayer1sTurn()
        {
            return isPlayer1sTurn;
        }

        //Determine if someone has won
        public bool GameWon()
        {
            bool[,] visited = new bool[11, 11];
            bool pathFound = false;

            //Check for left->right path for player 1
            if (isPlayer1sTurn)
            {
                for (int i = 0; i < 11; i++)
                {
                    Array.Clear(visited, 0, 121);
                    if (gameBoard.board[i, 0] == 1)
                    {
                        if (isPath(i, 0, visited, 1))
                        {
                            pathFound = true;
                            winner = 1;
                            break;
                        }
                    }
                }
            }

            //Check for bottom->top path for player 2
            else if (!isPlayer1sTurn)
            {
                for (int i = 0; i < 11; i++)
                {
                    Array.Clear(visited, 0, 121);
                    if (gameBoard.board[0, i] == 2 && !visited[i, 0])
                    {
                        if (isPath(0, i, visited, 2))
                        {
                            pathFound = true;
                            winner = 2;
                            break;
                        }
                    }
                }
            }
            return pathFound;
        }

        public bool isSafe(int i, int j)
        {
            if (i >= 0 && i < 11 && j >= 0 && j < 11)
                return true;
            return false;
        }

        public bool isPath(int i, int j, bool[,] visited, int player)
        {
            if (isSafe(i, j) && gameBoard.board[i, j] == player)
            {
                visited[i, j] = true;

                if (player == 1)
                {
                    //If it's player 1, check to see if it's gone all the way to the right
                    if (j == 10)
                    {
                        return true;
                    }
                }
                else if (player == 2)
                {
                    //If it's player 2, check to see if it's gone all the way to the top
                    if (i == 10)
                    {
                        return true;
                    }
                }

                //check up 
                bool up = isPath(i++, j, visited, player);
                if (up)
                    return true;

                //check down
                bool down = isPath(i--, j, visited, player);
                if (down)
                    return true;

                //check left
                bool left = isPath(i, j--, visited, player);
                if (left)
                    return true;

                //check right
                bool right = isPath(i, j++, visited, player);
                if (right)
                    return true;
            }
            return false;
        }

    }
}
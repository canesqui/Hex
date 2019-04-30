using Common;
using System;
using System.Collections.Generic;
namespace Server
{
    public class Server
    {
        private static Player player1;
        private static Player player2;
        private static GameLogic gameLogic;
        private static GameType gameType;
        private static bool isAIInitialized = false;
        private static List<string> AIMoves = new List<string>();
        private static void CreateAI()
        {
            if (player1 == null) { player1 = new AI(); }
            else if (player2 == null) { player2 = new AI(); }
        }

        private void CreateHuman()
        {
            if (player1 == null) { player1 = new Human(); }
            else if (player2 == null) { player2 = new Human(); }
        }

        public void CreateBoard(GameType GameType)
        {
            gameType = GameType;

            if (GameType == GameType.PlayerVsAI)
            {
                player1 = new Human();
                player2 = new AI();
                
            }
            else if (GameType == GameType.PlayerVsPlayer)
            {
                player1 = new Human();
                player2 = new Human();
            }
            else
            {
                player1 = new AI();
                player2 = new AI();
            }

            gameLogic = new GameLogic();
            gameLogic.gameBoard = new BoardState();
            gameLogic.gameBoard.InitializeBoard();
        }

        public Common.MoveResult Move(Common.Move move)
        {
            var response = new Common.MoveResult();
            response.OpponentMove = new Move();
            response.PlayerMoveResult = gameLogic.Move(move);
            if (response.PlayerMoveResult == Common.MoveResponse.Sucess)
            {
                gameLogic.gameBoard.UpdateBoard(move, gameLogic.IsPlayer1sTurn());
                gameLogic.SwitchTurns();

                if (isAIInitialized)
                {
                    ((AI)player2).UpdateBoard(move.x, move.y, 1);
                    int[] xy = AIMove();//((AI)player2).Move(2, 1);
                    response.OpponentMove.x = xy[0];
                    response.OpponentMove.y = xy[1];
                    gameLogic.gameBoard.UpdateBoard(response.OpponentMove, false);
                }
                else
                {
                    ((AI)player2).UpdateBoard(move.x, move.y, 1);
                    int[] xy = ((AI)(player2)).Initialize(2, (int)DateTime.Now.Ticks);//send time as seed
                    response.OpponentMove.x = xy[0];
                    response.OpponentMove.y = xy[1];
                    gameLogic.gameBoard.UpdateBoard(response.OpponentMove, false);
                    isAIInitialized = true;
                }

                gameLogic.SwitchTurns();
            }

            if (gameLogic.GameWon())
            {
                response.PlayerMoveResult = Common.MoveResponse.GameOver;
                return response;
            }

            return response;
        }

        public GameResult GameStatus()
        {
            if(gameLogic.winner == 1)
            {
                return GameResult.Win;
            }
            else if(gameLogic.winner == 2)
            {
                return GameResult.Lose;
            }
            return GameResult.InProgress;
        }

        public Player CurrentPlayersTurn()
        {
            if(gameLogic.IsPlayer1sTurn())
            {
                return player1;
            }
            else
            {
                return player2;
            }
        }

        private int[] AIMove()
        {
            int[] xy = ((AI)player2).Move(2, 1);
            string move = "" + xy[0] + "" + xy[1];

            while(AIMoves.Contains(move))
            {
                xy = ((AI)player2).Move(2, 1);
                move = "" + xy[0] + "" + xy[1];
            }
            AIMoves.Add(move);

            return xy;
        }
    }
}

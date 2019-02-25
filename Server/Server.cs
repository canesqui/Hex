using Common;
using System;

namespace Server
{
    public class Server
    {
        private Player player1;
        private Player player2;
        private GameLogicPlaceholder gameLogic;
        private GameType gameType;

        private void CreateAI()
        {
            if (player1 == null) { player1 = new AI(); }
            else if (player2 == null) { player2 = new AI(); }
        }

        private void CreateHuman()
        {
            if (player1 == null) { player1 = new Human(); }
            else if (player2 == null) { player2 = new Human(); }
        }

        public void CreateBoard(GameType gameType)
        {
            this.gameType = gameType;

            if(gameType == GameType.PlayerVsAI)
            {
                player1 = new Human();
                player2 = new AI();
            }
            else if(gameType == GameType.PlayerVsPlayer)
            {
                player1 = new Human();
                player2 = new Human();
            }
            else
            {
                player1 = new AI();
                player2 = new AI();
            }

            gameLogic = new GameLogicPlaceholder();
            UpdateBoard();
        }

        public void UpdateBoard()
        {
            //Send board state
        }

        public Common.MoveResult Move(Common.Move move)
        {
            var response = new Common.MoveResult();
            response.PlayerMoveResult = gameLogic.Move(move);
            if (response.PlayerMoveResult == Common.MoveResponse.Sucess)
            {
                response.OpponentMove = ((AI)player2).MakeMove(gameLogic);
            }

            if(gameLogic.IsGameOver())
            {
                response.PlayerMoveResult = Common.MoveResponse.GameOver;
                return response;
            }

            return response;
        }

        public void NewBoardState(GameLogicPlaceholder gameLogic)
        {
            this.gameLogic = gameLogic;
            UpdateBoard();
        }

        public bool DidHumanWin()
        {
            return gameLogic.Winner == 1;
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
    }
}

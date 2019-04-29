using Common;
using System;

namespace Server
{
    public class Server
    {
        private Player player1;
        private Player player2;
        private GameLogic gameLogic;
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

            if (gameType == GameType.PlayerVsAI)
            {
                player1 = new Human();
                player2 = new AI();
            }
            else if (gameType == GameType.PlayerVsPlayer)
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
            response.PlayerMoveResult = gameLogic.Move(move);
            if (response.PlayerMoveResult == Common.MoveResponse.Sucess)
            {
                gameLogic.gameBoard.UpdateBoard(move, gameLogic.IsPlayer1sTurn()); //Update the board through the logic
                gameLogic.SwitchTurns(); //Moved this to this file so that it could all be done in one place
                response.OpponentMove = ((AI)player2).MakeMove(gameLogic);
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
    }
}

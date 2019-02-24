using System;

namespace Server
{
    public class Server
    {
        private Player player1;
        private Player player2;
        private GameLogicPlaceholder gameLogic;

        public void CreateAI()
        {
            if (player1 == null) { player1 = new AI(); }
            else if (player2 == null) { player2 = new AI(); }
        }

        public void CreateHuman()
        {
            if (player1 == null) { player1 = new Human(); }
            else if (player2 == null) { player2 = new Human(); }
        }

        public void CreateBoard()
        {
            gameLogic = new GameLogicPlaceholder();
            UpdateBoard();
        }

        public void UpdateBoard()
        {
            //Send board state
        }

        public Common.MoveResult Move(Common.Move move)
        {
            var m = new Common.MoveResult();
            m.PlayerMoveResult = gameLogic.Move(move);

            if (m.PlayerMoveResult == Common.MoveResponse.Sucess)
            {
                m.OpponentMove = ((AI)player2).MakeMove(gameLogic);
            }

            return m;
        }

        public void NewBoardState(GameLogicPlaceholder gameLogic)
        {
            this.gameLogic = gameLogic;
            UpdateBoard();
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

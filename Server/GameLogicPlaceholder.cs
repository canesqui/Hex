using System;
using System.Collections.Generic;
using System.Text;

namespace Server
{
    public class GameLogicPlaceholder
    {
        Random rng = new Random();
        bool isPlayer1sTurn = true;

        public Common.MoveResponse Move(Common.Move move)
        {
            int i = rng.Next(0, 4);

            if (i == 0)
            {
                SwitchTurns();
                return Common.MoveResponse.Sucess;
            }
            else if(i==1)
            {
                return Common.MoveResponse.NotYourTurn;
            }
            else if(i==2)
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
    }
}

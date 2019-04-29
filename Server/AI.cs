using System;
using System.Collections.Generic;
using System.Text;

namespace Server
{
    class AI : Player
    {
        public Common.Move MakeMove(GameLogic game)
        {
            return Think();
        }

        public Common.Move Think()
        {
            Common.Move move = new Common.Move();
            return move;
        }
    }
}

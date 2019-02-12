using System;
using System.Collections.Generic;
using System.Text;

namespace Common
{
    public interface IHexServer
    {
        bool CreateGame(GameType gameType, string HumanplayerId);        
        MoveResult Move(string playerId, Move move);
        GameResult GameResult(string playerId);
    }
}

using System;

namespace Common
{
    public class Move
    {
        public int x { get; set; }
        public int y { get; set; }
    }

    public enum MoveResponse { Sucess, IllegalMove, NotYourTurn, GameOver }
    public enum GameResult { Win, Lose, InProgress }
    public enum GameType
    {
        PlayerVsAI,
        PlayerVsPlayer,
        AIVsAI
    }

}

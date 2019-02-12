using System;
using System.Collections.Generic;
using System.Text;

namespace Common
{
    public class MoveResult
    {
        public Move OpponentMove { get; set; }
        public MoveResponse PlayerMoveResult { get; set; }
    }
}

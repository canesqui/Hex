using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AI;

namespace TEST_AI
{
    class Program
    {
        static void Main(string[] args)
        {
            int red = 2;
            int blue = 1;
            var red_test = new AI.AI();   // blue
            var blue_test = new AI.AI(); // red
            red_test.Initialize(blue, 1);
            blue_test.Initialize(red, 1);
            //test.UpdateBoard(i, j, blue);
            while (true)
            {
                int[] mv_blue = blue_test.Move(blue, red);
                red_test.UpdateBoard(mv_blue[0], mv_blue[1], red);      // update the board for the opponent(red)

                int[] mv_red = red_test.Move(red, blue);
                blue_test.UpdateBoard(mv_red[0], mv_red[1], blue);    // update the board for the opponent(blue)
            }
            
        }
    }
}

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
            
            int[] b = blue_test.Initialize(blue);
            int[] r = red_test.Initialize(red);
            
            blue_test.UpdateBoard(r[0], r[1], red);      // update the red board with blue's move
            red_test.UpdateBoard(b[0], b[1], blue);      // update the red board with blue's move
            //test.UpdateBoard(i, j, blue);
            do
            {
                int[] mv_blue = blue_test.Move(blue, red);              // puts the blue move into mv_blue
                red_test.UpdateBoard(mv_blue[0], mv_blue[1], blue);     // update the red board with blue's move

                int[] mv_red = red_test.Move(red, blue);                // puts the red move into mv_red
                blue_test.UpdateBoard(mv_red[0], mv_red[1], red);       // update the blue board with red's move
            } while (!(blue_test.TestGoal() || red_test.TestGoal()));
            Console.WriteLine("End");
            
        }
    }
}

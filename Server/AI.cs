using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Server
{
    public class AI : Player
    {
        // USE DEFINES OR VARIABLES FOR -12 +1 -11 etc=====================================================NAH
        private static readonly int red = 2;
        private static readonly int blue = 1;
        private static readonly int empty = 0;
        private static readonly int V = 121;
        private static readonly int v = 11;
        private static readonly int NO_PARENT = -1;
        private static readonly int connected = 1;
        private static readonly int not_connected = 10000;  // changed from 0
        private static readonly int[,] translation = new int[,]
           {{0, 11, 22, 33, 44, 55, 66, 77, 88,  99, 110},
            {1, 12, 23, 34, 45, 56, 67, 78, 89, 100, 111},
            {2, 13, 24, 35, 46, 57, 68, 79, 90, 101, 112},
            {3, 14, 25, 36, 47, 58, 69, 80, 91, 102, 113},
            {4, 15, 26, 37, 48, 59, 70, 81, 92, 103, 114},
            {5, 16, 27, 38, 49, 60, 71, 82, 93, 104, 115},
            {6, 17, 28, 39, 50, 61, 72, 83, 94, 105, 116},
            {7, 18, 29, 40, 51, 62, 73, 84, 95, 106, 117},
            {8, 19, 30, 41, 52, 63, 74, 85, 96, 107, 118},
            {9, 20, 31, 42, 53, 64, 75, 86, 97, 108, 119},
           {10, 21, 32, 43, 54, 65, 76, 87, 98, 109, 120}
            };
        //    private int[,] translation_UNUSED = new int[,]
        //       {{0,  1,  2,  3,  4,  5,  6,  7,  8,  9,  10},
        //        {11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21},
        //        {22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32},
        //        {33, 34, 35, 36, 37, 38, 39, 40, 41, 42, 43},
        //        {44, 45, 46, 47, 48, 49, 50, 51, 52, 53, 54},
        //        {55, 56, 57, 58, 59, 60, 61, 62, 63, 64, 65},
        //        {66, 67, 68, 69, 70, 71, 72, 73, 74, 75, 76},
        //        {77, 78, 79, 80, 81, 82, 83, 84, 85, 86, 87},
        //        {88, 89, 90, 91, 92, 93, 94, 95, 96, 97, 98},
        //        {99, 100, 101, 102, 103, 104, 105, 106, 107, 108, 109},
        //        {110, 111, 112, 113, 114, 115, 116, 117, 118, 119, 120}
        //};
        private static readonly int[,] indices = new int[,]
           {{0,0}, {1,0}, {2,0}, {3,0}, {4,0}, {5,0}, {6,0}, {7,0}, {8,0}, {9,0}, {10,0},
            {0,1}, {1,1}, {2,1}, {3,1}, {4,1}, {5,1}, {6,1}, {7,1}, {8,1}, {9,1}, {10,1},
            {0,2}, {1,2}, {2,2}, {3,2}, {4,2}, {5,2}, {6,2}, {7,2}, {8,2}, {9,2}, {10,2},
            {0,3}, {1,3}, {2,3}, {3,3}, {4,3}, {5,3}, {6,3}, {7,3}, {8,3}, {9,3}, {10,3},
            {0,4}, {1,4}, {2,4}, {3,4}, {4,4}, {5,4}, {6,4}, {7,4}, {8,4}, {9,4}, {10,4},
            {0,5}, {1,5}, {2,5}, {3,5}, {4,5}, {5,5}, {6,5}, {7,5}, {8,5}, {9,5}, {10,5},
            {0,6}, {1,6}, {2,6}, {3,6}, {4,6}, {5,6}, {6,6}, {7,6}, {8,6}, {9,6}, {10,6},
            {0,7}, {1,7}, {2,7}, {3,7}, {4,7}, {5,7}, {6,7}, {7,7}, {8,7}, {9,7}, {10,7},
            {0,8}, {1,8}, {2,8}, {3,8}, {4,8}, {5,8}, {6,8}, {7,8}, {8,8}, {9,8}, {10,8},
            {0,9}, {1,9}, {2,9}, {3,9}, {4,9}, {5,9}, {6,9}, {7,9}, {8,9}, {9,9}, {10,9},
            {0,10}, {1,10}, {2,10}, {3,10}, {4,10}, {5,10}, {6,10}, {7,10}, {8,10}, {9,10}, {10,10}
            };
        private static readonly int[] cells = new int[]
        { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10,
          11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21,
          22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32,
          33, 34, 35, 36, 37, 38, 39, 40, 41, 42, 43,
          44, 45, 46, 47, 48, 49, 50, 51, 52, 53, 54,
          55, 56, 57, 58, 59, 60, 61, 62, 63, 64, 65,
          66, 67, 68, 69, 70, 71, 72, 73, 74, 75, 76,
          77, 78, 79, 80, 81, 82, 83, 84, 85, 86, 87,
          88, 89, 90, 91, 92, 93, 94, 95, 96, 97, 98,
          99, 100, 101, 102, 103, 104, 105, 106, 107, 108, 109,
          110, 111, 112, 113, 114, 115, 116, 117, 118, 119, 120
        };
        private static readonly List<int> red_top_borders = new List<int> { 110, 111, 112, 113, 114, 115, 116, 117, 118, 119, 120 };
        private static readonly List<int> red_bot_borders = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
        private List<int> red_borders = new List<int>();
        private static List<int> red_border = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 110, 111, 112, 113, 114, 115, 116, 117, 118, 119, 120 };
        private readonly List<int> blue_left_borders = new List<int> { 0, 11, 22, 33, 44, 55, 66, 77, 88, 99, 110 };
        private readonly List<int> blue_right_borders = new List<int> { 10, 21, 32, 43, 54, 65, 76, 87, 98, 109, 120 };
        private List<int> blue_borders = new List<int>();
        private static readonly List<int> blue_border = new List<int> { 0, 11, 22, 33, 44, 55, 66, 77, 88, 99, 110, 10, 21, 32, 43, 54, 65, 76, 87, 98, 109, 120 };
        private List<int> red_tiles = new List<int>();
        private List<int> blue_tiles = new List<int>();
        private int[,] board = new int[v, v];
        private int[,] red_graph = new int[V, V];
        private int[,] blue_graph = new int[V, V];
        //static List<int> path = new List<int>();
        private static readonly int _i = 0;
        private static readonly int _j = 1;
        // need to make the board  and others a global variable, maybe add private or something like that *shrug*

        // I NEED TO FIND WHAT NEIGHBOR(S) IS ON THE SHORTEST PATH. 
        // MEANING I NEED TO HAVE A LIST OF NODES THAT MAKE UP THE SHORTEST PATH
        // I THEN PICK EITHER THE FIRST ONE OR THE ONE THAT IS A NEIGHBOR

        public int[] Initialize(int player, int seed = 0)   // default for seed == 0; sets the seed to be System.Time
        {
            red_graph = MakeGraph();
            blue_graph = MakeGraph();
            int[] mv = new int[2]; // 2 is number of indices in the return value
            mv = FirstMove(player, seed);
            int[] tile = GetPlayerTiles(player);
            SetBorder(tile[0], player);
            return mv;
        }

        public int[] Move(int player, int opposing_player)  // red = 2, blue = 1, empty = 0
        {
            if (player == red)
            {
                //Console.Write("Red ");
            }
            else if (player == blue)
            {
                //Console.Write("Blue ");
            }

            List<int> path = new List<int>();
            int[] out_parents = new int[V]; // graph.getlength(0)
            int smallest = int.MaxValue;   // what should I initialize this to?
            int out_source = int.MaxValue;
            List<int> out_dist = new List<int>();
            int[,] _graph = new int[V, V];
            List<int> tiles = new List<int>();

            if (player == red)
            {
                RemoveOpponentGraph(ref red_graph, player, opposing_player); // this edits out the opposing tiles so there are no connections between red and blue
                _graph = red_graph;
                tiles = red_tiles;
            }
            else if (player == blue)
            {
                RemoveOpponentGraph(ref blue_graph, player, opposing_player); // this edits out the opposing tiles so there are no connections between red and blue
                _graph = blue_graph;
                tiles = blue_tiles;
            }
            object sync = new Object();



            Parallel.For(0, tiles.Count, i =>
            {
                int source = tiles[i];
                int[] parents = new int[V];
                List<int> dist = DijkstraS(_graph, source, ref parents);
                if (player == red)
                {
                    dist = RemoveNonBorderDists(dist, red);
                }
                else if (player == blue)
                {
                    dist = RemoveNonBorderDists(dist, blue);
                }
                lock (sync)
                {
                    dist[Array.IndexOf(dist.ToArray(), dist.Min())] = int.MaxValue; // this makes sure that 0 isn't picked as the smallest
                    if (dist.Min() < smallest)
                    {
                        smallest = Array.IndexOf(dist.ToArray(), dist.Min());//dist.Min();
                        out_source = source;
                        out_dist = dist;
                        out_parents = parents;
                    }
                }
            }); // Parallel.For  

            // change the board by adding a player tile that gives the shortest distance

            PrintPath(out_source, out_dist, out_parents, ref path, smallest);
            //Console.Write("\n");

            smallest = path[path.Count - 2];  // gets the next tile in the chain after the initial tile// path[last] == start
            int[] move = new int[] { indices[smallest, _i], indices[smallest, _j] };

            if (player == red)
            {
                board[indices[smallest, _i], indices[smallest, _j]] = red;
                red_tiles.Add(smallest);    // MAKE SURE THIS IS THE RIGHT INDEX======================================
            }
            else if (player == blue)
            {
                board[indices[smallest, _i], indices[smallest, _j]] = blue;
                blue_tiles.Add(smallest);
            }

            //PrintBoard();   // Print out the board
            //PrintGraph(player); // Print graph to file
            return move;
        }
        public void UpdateBoard(int i, int j, int player)   // this sets the player given, to the indices given
        {
            if (player == red)
            {
                board[i, j] = red;
            }
            else if (player == blue)
            {
                board[i, j] = blue;
            }
            else if (player == empty)    // default
            {
                board[i, j] = blue;
            }

        }
        int[] FirstMove(int player, int seed)
        {
            if (seed == 0)
            {
                seed = (int)DateTime.Now.Ticks;
            }
            Random rand = new Random(seed);
            int i = 0;
            int j = 0;
            int[] mv = new int[2];  // 2 is the number of indices in the return value
            if (player == red)
            {
                i = indices[red_border[rand.Next(red_border.Count())], _i];
                j = indices[red_border[rand.Next(red_border.Count())], _j];
                red_tiles.Add(translation[i, j]);
            }
            else if (player == blue)
            {
                i = indices[blue_border[rand.Next(blue_border.Count())], _i];
                j = indices[blue_border[rand.Next(blue_border.Count())], _j];
                blue_tiles.Add(translation[i, j]);
            }
            mv[_i] = i;
            mv[_j] = j;
            UpdateBoard(i, j, player);
            return mv;
        }
        public bool TestGoal()
        {
            foreach (var tile in red_tiles)
            {
                if (red_borders.Contains(tile)) // if the first tile is in the top, set the goal border set to the bottom etc
                {
                    return true;
                }
            }
            foreach (var tile in blue_tiles)
            {
                if (blue_borders.Contains(tile))
                {
                    return true;
                }
            }
            return false;   // if neither has a tile in the goal return false
        }
        void SetBorder(int tile, int player)    // This sets which side the AI is trying to get to
        {
            if (player == red)
            {
                if (red_top_borders.Contains(tile)) // if the first tile is in the top, set the goal border set to the bottom etc
                {
                    red_borders = red_bot_borders;
                }
                else if (red_bot_borders.Contains(tile))
                {
                    red_borders = red_top_borders;
                }
            }
            else if (player == blue)
            {
                if (blue_left_borders.Contains(tile))
                {
                    blue_borders = blue_right_borders;
                }
                else if (blue_right_borders.Contains(tile))
                {
                    blue_borders = blue_left_borders;
                }
            }
        }
        private void PrintBoard()
        {
            int i = 0;
            for (int j = v - 1; j >= 0; j--)
            {
                for (i = 0; i < v; i++)
                {
                    if (board[i, j] == red)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                    }
                    else if (board[i, j] == blue)
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                    }
                    else if (board[i, j] == empty)
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    Console.Write(board[i, j] + "   ");
                }
                switch (j)
                {
                    case 10:
                        Console.Write("\n" + "  ");
                        break;
                    case 9:
                        Console.Write("\n" + "    ");
                        break;
                    case 8:
                        Console.Write("\n" + "      ");
                        break;
                    case 7:
                        Console.Write("\n" + "        ");
                        break;
                    case 6:
                        Console.Write("\n" + "          ");
                        break;
                    case 5:
                        Console.Write("\n" + "            ");
                        break;
                    case 4:
                        Console.Write("\n" + "              ");
                        break;
                    case 3:
                        Console.Write("\n" + "                ");
                        break;
                    case 2:
                        Console.Write("\n" + "                  ");
                        break;
                    case 1:
                        Console.Write("\n" + "                    ");
                        break;
                    case 0:
                        Console.Write("\n" + "                      ");
                        break;
                }
            }
            Console.Write("\n");
        }
        private void PrintGraph(int player)
        {
            string file_path = @"C:\Users\Able Sankovik\Visual Studio Enterprise 2017\repos\MyProjects\ZTestFilesSelfKey\hex_graph.txt";

            File.WriteAllText(file_path, "");   // to clear the file
            for (int j = 0; j < V; j++)
            {
                string str = System.String.Empty;
                for (int i = 0; i < V; i++)
                {
                    if (player == red)
                    {
                        str += red_graph[i, j];
                        //str += " ";
                    }
                    else if (player == blue)
                    {
                        str += blue_graph[i, j];
                        //str += " ";
                    }
                }
                str += "Z\r\n";
                File.AppendAllText(file_path, str);
                //File.AppendAllText(file_path, "Z\n");
            }
        }
        private List<int> RemoveNonBorderDists(List<int> dist, int player)  // blue = 1, red = 2, empty = 0
        {
            List<int> ret_dist = dist;
            object sync_dist = new Object();
            for (int i = 0; i < dist.Count; i++)
            {
                if (player == red)
                {
                    if (!red_borders.Contains(i)) // borders does not contain
                    {
                        //dist.Remove(i);
                        dist[i] = int.MaxValue;
                    }
                }
                else if (player == blue)
                {
                    if (!blue_borders.Contains(i))    // if borders(including player tiles) does not contain the tile, remove it
                    {
                        //dist.Remove(i);
                        dist[i] = int.MaxValue;
                    }
                }
            }
            //Parallel.For(0, dist.Count, i =>
            //{
            //    if (player == red)
            //    {
            //        if (!red_borders.Contains(i)) // borders does not contain
            //        {
            //            lock (sync_dist)
            //            {
            //                //dist.Remove(i);
            //                dist[i] = int.MaxValue;
            //            }
            //        }
            //    }
            //    else if (player == blue)
            //    {
            //        if (!blue_borders.Contains(i))    // if borders(including player tiles) does not contain the tile, remove it
            //        {
            //            lock (sync_dist)
            //            {
            //                //dist.Remove(i);
            //                dist[i] = int.MaxValue;
            //            }
            //        }
            //    }
            //});
            return ret_dist;
        }
        //void GetBorderTiles(int[,] board, int player) // 0 = red, 1 = blue, 2 = empty
        //{
        //    for (int i = 0; i < v; i++)
        //    {
        //        for (int j = 0;  j < v;  j++)
        //        {
        //            if(board[i,j] == player)
        //            {
        //                if(player == red)
        //                {
        //                    red_borders.Add(translation[i, j]);
        //                }
        //                else if(player == blue)
        //                {
        //                    blue_borders.Add(translation[i, j]);
        //                }
        //            }
        //        }
        //    }
        //}
        private int[] GetPlayerTiles(int player)    // This may be unneeded
        {
            List<int> tiles = new List<int>();
            for (int i = 0; i < v; i++)
            {
                for (int j = 0; j < v; j++)
                {
                    if (board[i, j] == player)
                    {
                        tiles.Add(translation[i, j]);
                    }
                }
            }
            return tiles.ToArray();
        }
        private void RemoveOpponentGraph(ref int[,] graph, int player, int opposing_player)   // correct way of ref'ing an array?
        {
            for (int i = 0; i < v; i++)
            {
                for (int j = 0; j < v; j++)
                {
                    int z = translation[i, j];
                    if (board[i, j] == opposing_player)
                    {
                        if (z < 11)  // bottom
                        {
                            if (z == 0)
                            {
                                graph[z + 11, z] = not_connected; //== 0
                                graph[z + 12, z] = not_connected;
                                graph[z + 1, z] = not_connected;

                                graph[z, z + 11] = not_connected;
                                graph[z, z + 12] = not_connected;
                                graph[z, z + 1] = not_connected;
                            }
                            else if (z == 10)
                            {
                                graph[z - 1, z] = not_connected;
                                graph[z + 11, z] = not_connected;

                                graph[z, z - 1] = not_connected;
                                graph[z, z + 11] = not_connected;
                            }
                            else
                            {
                                graph[z - 1, z] = not_connected;
                                graph[z + 11, z] = not_connected;
                                graph[z + 12, z] = not_connected;
                                graph[z + 1, z] = not_connected;

                                graph[z, z - 1] = not_connected;
                                graph[z, z + 11] = not_connected;
                                graph[z, z + 12] = not_connected;
                                graph[z, z + 1] = not_connected;
                            }
                        }
                        else if (z % 11 == 0) // left
                        {
                            if (z == 110)
                            {
                                //graph[z - 12, z] = not_connected;
                                graph[z + 1, z] = not_connected;
                                graph[z - 11, z] = not_connected;
                                //graph[z + 12, z] = not_connected;

                                //graph[z, z - 12] = not_connected;
                                graph[z, z + 1] = not_connected;
                                graph[z, z - 11] = not_connected;
                                //graph[z, z + 12] = not_connected;
                            }
                            else
                            {
                                graph[z + 11, z] = not_connected;
                                graph[z + 12, z] = not_connected;
                                graph[z + 1, z] = not_connected;
                                graph[z - 11, z] = not_connected;

                                graph[z, z + 11] = not_connected;
                                graph[z, z + 12] = not_connected;
                                graph[z, z + 1] = not_connected;
                                graph[z, z - 11] = not_connected;
                            }
                        }
                        else if (z == 21 | z == 32 | z == 43 | z == 54 | z == 65 | z == 76 | z == 87 | z == 98 | z == 109 | z == 120)    // right
                        {
                            if (z == 120)
                            {
                                graph[z - 12, z] = not_connected;
                                graph[z - 1, z] = not_connected;
                                graph[z - 11, z] = not_connected;

                                graph[z, z - 12] = not_connected;
                                graph[z, z - 1] = not_connected;
                                graph[z, z - 11] = not_connected;
                            }
                            else
                            {
                                graph[z - 12, z] = not_connected;
                                graph[z - 1, z] = not_connected;
                                graph[z + 11, z] = not_connected;
                                graph[z - 11, z] = not_connected;

                                graph[z, z - 12] = not_connected;
                                graph[z, z - 1] = not_connected;
                                graph[z, z + 11] = not_connected;
                                graph[z, z - 11] = not_connected;
                            }
                        }
                        else if (z > 109)    // top  // doesn't include 120 or 110
                        {
                            graph[z - 12, z] = not_connected;
                            graph[z - 1, z] = not_connected;
                            graph[z + 1, z] = not_connected;
                            graph[z - 11, z] = not_connected;

                            graph[z, z - 12] = not_connected;
                            graph[z, z - 1] = not_connected;
                            graph[z, z + 1] = not_connected;
                            graph[z, z - 11] = not_connected;
                        }
                        else    // middle
                        {
                            graph[z - 12, z] = not_connected;
                            graph[z - 1, z] = not_connected;
                            graph[z + 11, z] = not_connected;
                            graph[z + 12, z] = not_connected;
                            graph[z + 1, z] = not_connected;
                            graph[z - 11, z] = not_connected;

                            graph[z, z - 12] = not_connected;
                            graph[z, z - 1] = not_connected;
                            graph[z, z + 11] = not_connected;
                            graph[z, z + 12] = not_connected;
                            graph[z, z + 1] = not_connected;
                            graph[z, z - 11] = not_connected;
                        }
                    }
                }
            }
        }
        private int[,] MakeGraph()
        {
            int[,] graph = new int[V, V];

            for (int i = 0; i < V; i++)
            {
                if (i < 11)  // bottom
                {
                    if (i == 0)
                    {
                        graph[i + 11, i] = connected;
                        graph[i + 12, i] = connected;
                        graph[i + 1, i] = connected;

                        graph[i, i + 11] = connected;
                        graph[i, i + 12] = connected;
                        graph[i, i + 1] = connected;
                    }
                    else if (i == 10)
                    {
                        graph[i - 1, i] = connected;
                        graph[i + 11, i] = connected;

                        graph[i, i - 1] = connected;
                        graph[i, i + 11] = connected;
                    }
                    else
                    {
                        graph[i - 1, i] = connected;
                        graph[i + 11, i] = connected;
                        graph[i + 12, i] = connected;
                        graph[i + 1, i] = connected;

                        graph[i, i - 1] = connected;
                        graph[i, i + 11] = connected;
                        graph[i, i + 12] = connected;
                        graph[i, i + 1] = connected;
                    }
                }
                else if (i % 11 == 0) // left
                {
                    if (i == 110)
                    {
                        //graph[i - 12, i] = connected;
                        graph[i + 1, i] = connected;
                        graph[i - 11, i] = connected;
                        //graph[i + 12, i] = connected;

                        //graph[i, i - 12] = connected;
                        graph[i, i + 1] = connected;
                        graph[i, i - 11] = connected;
                        // graph[i, i + 12] = connected;
                    }
                    else
                    {
                        graph[i + 11, i] = connected;
                        graph[i + 12, i] = connected;
                        graph[i + 1, i] = connected;
                        graph[i - 11, i] = connected;

                        graph[i, i + 11] = connected;
                        graph[i, i + 12] = connected;
                        graph[i, i + 1] = connected;
                        graph[i, i - 11] = connected;
                    }
                }
                else if (i == 21 | i == 32 | i == 43 | i == 54 | i == 65 | i == 76 | i == 87 | i == 98 | i == 109 | i == 120)    // right
                {
                    if (i == 120)
                    {
                        graph[i - 12, i] = connected;
                        graph[i - 1, i] = connected;
                        graph[i - 11, i] = connected;

                        graph[i, i - 12] = connected;
                        graph[i, i - 1] = connected;
                        graph[i, i - 11] = connected;
                    }
                    else
                    {
                        graph[i - 12, i] = connected;
                        graph[i - 1, i] = connected;
                        graph[i + 11, i] = connected;
                        graph[i - 11, i] = connected;

                        graph[i, i - 12] = connected;
                        graph[i, i - 1] = connected;
                        graph[i, i + 11] = connected;
                        graph[i, i - 11] = connected;
                    }
                }
                else if (i > 109)    // top  // doesn't include 120 or 110
                {
                    graph[i - 12, i] = connected;
                    graph[i - 1, i] = connected;
                    graph[i + 1, i] = connected;
                    graph[i - 11, i] = connected;

                    graph[i, i - 12] = connected;
                    graph[i, i - 1] = connected;
                    graph[i, i + 1] = connected;
                    graph[i, i - 11] = connected;
                }
                else    // middle
                {
                    graph[i - 12, i] = connected;
                    graph[i - 1, i] = connected;
                    graph[i + 11, i] = connected;
                    graph[i + 12, i] = connected;
                    graph[i + 1, i] = connected;
                    graph[i - 11, i] = connected;

                    graph[i, i - 12] = connected;
                    graph[i, i - 1] = connected;
                    graph[i, i + 11] = connected;
                    graph[i, i + 12] = connected;
                    graph[i, i + 1] = connected;
                    graph[i, i - 11] = connected;
                }
            }
            return graph;
        }
        //void UpdateGraph(int player, int tile)
        //{
        //    int[,] graph = new int[V,V];
        //    if (player == red)
        //    {
        //        graph = red_graph;
        //    }
        //    else if(player == blue)
        //    {
        //        graph = blue_graph;
        //    }
        //    int i = tile;
        //    if (i < 11)  // bottom
        //    {
        //        if (i == 0)
        //        {
        //            graph[i + 11, i] = connected;
        //            graph[i + 12, i] = connected;
        //            graph[i + 1, i] = connected;

        //            graph[i, i + 11] = connected;
        //            graph[i, i + 12] = connected;
        //            graph[i, i + 1] = connected;
        //        }
        //        else if (i == 10)
        //        {
        //            graph[i - 1, i] = connected;
        //            graph[i + 11, i] = connected;

        //            graph[i, i - 1] = connected;
        //            graph[i, i + 11] = connected;
        //        }
        //        else
        //        {
        //            graph[i - 1, i] = connected;
        //            graph[i + 11, i] = connected;
        //            graph[i + 12, i] = connected;
        //            graph[i + 1, i] = connected;

        //            graph[i, i - 1] = connected;
        //            graph[i, i + 11] = connected;
        //            graph[i, i + 12] = connected;
        //            graph[i, i + 1] = connected;
        //        }
        //    }
        //    else if (i % 11 == 0) // left
        //    {
        //        if (i == 110)
        //        {
        //            //graph[i - 12, i] = connected;
        //            graph[i + 1, i] = connected;
        //            graph[i - 11, i] = connected;
        //            //graph[i + 12, i] = connected;

        //            //graph[i, i - 12] = connected;
        //            graph[i, i + 1] = connected;
        //            graph[i, i - 11] = connected;
        //            // graph[i, i + 12] = connected;
        //        }
        //        else
        //        {
        //            graph[i + 11, i] = connected;
        //            graph[i + 12, i] = connected;
        //            graph[i + 1, i] = connected;
        //            graph[i - 11, i] = connected;

        //            graph[i, i + 11] = connected;
        //            graph[i, i + 12] = connected;
        //            graph[i, i + 1] = connected;
        //            graph[i, i - 11] = connected;
        //        }
        //    }
        //    else if (i == 21 | i == 32 | i == 43 | i == 54 | i == 65 | i == 76 | i == 87 | i == 98 | i == 109 | i == 120)    // right
        //    {
        //        if (i == 120)
        //        {
        //            graph[i - 12, i] = connected;
        //            graph[i - 1, i] = connected;
        //            graph[i - 11, i] = connected;

        //            graph[i, i - 12] = connected;
        //            graph[i, i - 1] = connected;
        //            graph[i, i - 11] = connected;
        //        }
        //        else
        //        {
        //            graph[i - 12, i] = connected;
        //            graph[i - 1, i] = connected;
        //            graph[i + 11, i] = connected;
        //            graph[i - 11, i] = connected;

        //            graph[i, i - 12] = connected;
        //            graph[i, i - 1] = connected;
        //            graph[i, i + 11] = connected;
        //            graph[i, i - 11] = connected;
        //        }
        //    }
        //    else if (i > 109)    // top  // doesn't include 120 or 110
        //    {
        //        graph[i - 12, i] = connected;
        //        graph[i - 1, i] = connected;
        //        graph[i + 1, i] = connected;
        //        graph[i - 11, i] = connected;

        //        graph[i, i - 12] = connected;
        //        graph[i, i - 1] = connected;
        //        graph[i, i + 1] = connected;
        //        graph[i, i - 11] = connected;
        //    }
        //    else    // middle
        //    {
        //        graph[i - 12, i] = connected;
        //        graph[i - 1, i] = connected;
        //        graph[i + 11, i] = connected;
        //        graph[i + 12, i] = connected;
        //        graph[i + 1, i] = connected;
        //        graph[i - 11, i] = connected;

        //        graph[i, i - 12] = connected;
        //        graph[i, i - 1] = connected;
        //        graph[i, i + 11] = connected;
        //        graph[i, i + 12] = connected;
        //        graph[i, i + 1] = connected;
        //        graph[i, i - 11] = connected;
        //    }
        //}
        // This function stores transpose of A[][] in B[][] 
        //static void Transpose()
        //{
        //    int[,] B = translation;
        //    int i, j;
        //    for (i = 0; i < v; i++)
        //    {
        //        for (j = 0; j < v; j++)
        //        {
        //            B[i, j] = translation[j, i];
        //        }
        //    }
        //    translation = B;
        //}
        private static void PrintPath(int startVertex, List<int> dist, int[] parents, ref List<int> path, int smallest)
        {
            int nVertices = dist.Count;
            //Console.Write("Vertex\t Distance\tPath");
            for (int vertexIndex = 0; vertexIndex < nVertices; vertexIndex++)
            {
                if (vertexIndex != startVertex && vertexIndex == smallest)    // dist.Min()
                {
                    //Console.Write("\n" + startVertex + " -> ");
                    //Console.Write(vertexIndex + "   ");
                    //Console.Write(dist[vertexIndex] + "    ");
                    MakePath(vertexIndex, parents, ref path);
                }
            }
        }
        private static void MakePath(int currentVertex, int[] parents, ref List<int> path)
        {
            // Base case : Source node has been processed  
            if (currentVertex == NO_PARENT)
            {
                return;
            }
            path.Add(currentVertex);
            //Console.Write(currentVertex + " ");
            MakePath(parents[currentVertex], parents, ref path);
        }
        private static List<int> DijkstraS(int[,] graph, int startVertex, ref int[] parents)
        {
            int nVertices = V; // graph.GetLength(0);     // Change this to V=========================================================!!!!????

            // shortestDistances[i] will hold the shortest distance from src to i  
            List<int> dist = new List<int>(new int[nVertices]);

            // added[i] will be true if vertex i is included in shortest path tree or shortest distance from src to i is finalized  
            bool[] added = new bool[nVertices];

            // Initialize all distances as INFINITE and added[] as false  
            for (int vertexIndex = 0; vertexIndex < nVertices; vertexIndex++)
            {
                dist[vertexIndex] = int.MaxValue;
                added[vertexIndex] = false;
            }

            dist[startVertex] = 0;  // Distance of source vertex from itself is always 0 

            //int[] parents = new int[nVertices];   // Parent array to store shortest path tree  

            parents[startVertex] = NO_PARENT;   // The starting vertex does not have a parent

            for (int i = 1; i < nVertices; i++) // Find shortest path for all vertices 
            {
                // Pick the minimum distance vertex from the set of vertices not yet processed. 
                // nearestVertex is always equal to startNode in first iteration.  
                int nearestVertex = -1;
                int shortestDistance = int.MaxValue;
                int vertexIndex1 = 0;
                for (vertexIndex1 = 0; vertexIndex1 < nVertices; vertexIndex1++)
                {
                    if (!added[vertexIndex1] && dist[vertexIndex1] < shortestDistance)
                    {
                        nearestVertex = vertexIndex1;
                        shortestDistance = dist[vertexIndex1];

                    }
                    //else if (!added[vertexIndex1])
                    //{
                    //    Console.WriteLine(vertexIndex1 + " !added[vertexIndex1]");
                    //}
                    //else if (dist[vertexIndex1] < shortestDistance)
                    //{
                    //    //nearestVertex = -1;
                    //    Console.WriteLine(vertexIndex1 + " dist[vertexIndex1] < shortestDistance");
                    //}
                    //else
                    //{
                    //    Console.WriteLine(vertexIndex1 + " added[vertexIndex1] && dist[vertexIndex1] >= shortestDistance");
                    //}
                }
                // Mark the picked vertex as processed  
                added[nearestVertex] = true;

                // Update distance value of the adjacent vertices of the picked vertex.
                int vertexIndex2 = 0;
                for (vertexIndex2 = 0; vertexIndex2 < nVertices; vertexIndex2++)
                {
                    int edgeDistance = graph[nearestVertex, vertexIndex2];

                    if (edgeDistance > 0 && ((shortestDistance + edgeDistance) < dist[vertexIndex2]))
                    {
                        parents[vertexIndex2] = nearestVertex;
                        dist[vertexIndex2] = shortestDistance + edgeDistance;
                    }
                }
            }
            //PrintSolution(startVertex, shortestDistances, parents);
            return dist;
        }

        //for(int i = 0; i < tiles.Count; i++)
        //{
        //    int source = tiles[i];
        //    int[] parents = new int[V];
        //    List<int> dist = DijkstraS(_graph, source, ref parents);
        //    if(player == red)
        //    {
        //        dist = RemoveNonBorderDists(dist, red); 
        //    }
        //    else if(player == blue)
        //    {
        //        dist = RemoveNonBorderDists(dist, blue);
        //    }

        //    dist[Array.IndexOf(dist.ToArray(), dist.Min())] = int.MaxValue; // this makes sure that 0 isn't picked as the smallest
        //    if (dist.Min() < smallest)
        //    {
        //        smallest = Array.IndexOf(dist.ToArray(), dist.Min());//dist.Min();
        //        out_source = source;
        //        out_dist = dist;
        //        out_parents = parents;
        //    }
        //} // Serial.For  
    }
}

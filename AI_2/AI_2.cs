using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AI_2
{
    public class AI_2
    {
        // USE DEFINES OR VARIABLES FOR -12 +1 -11 etc=====================================================
        static readonly int red = 2;
        static readonly int blue = 1;
        static readonly int empty = 0;
        static readonly int V = 121;
        static readonly int v = 11;
        private static readonly int NO_PARENT = -1;
        //static readonly int[,] translation = new int [,] 
        //   {{0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10},
        //    {11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21},
        //    {22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32},
        //    {33, 34, 35, 36, 37, 38, 39, 40, 41, 42, 43},
        //    {44, 45, 46, 47, 48, 49, 50, 51, 52, 53, 54},
        //    {55, 56, 57, 58, 59, 60, 61, 62, 63, 64, 65},
        //    {66, 67, 68, 69, 70, 71, 72, 73, 74, 75, 76},
        //    {77, 78, 79, 80, 81, 82, 83, 84, 85, 86, 87},
        //    {88, 89, 90, 91, 92, 93, 94, 95, 96, 97, 98},
        //    {99, 100, 101, 102, 103, 104, 105, 106, 107, 108, 109},
        //    {110, 111, 112, 113, 114, 115, 116, 117, 118, 119, 120}
        //    };
        static int[,] translation = new int[,]
            {{0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10},
            {11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21},
            {22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32},
            {33, 34, 35, 36, 37, 38, 39, 40, 41, 42, 43},
            {44, 45, 46, 47, 48, 49, 50, 51, 52, 53, 54},
            {55, 56, 57, 58, 59, 60, 61, 62, 63, 64, 65},
            {66, 67, 68, 69, 70, 71, 72, 73, 74, 75, 76},
            {77, 78, 79, 80, 81, 82, 83, 84, 85, 86, 87},
            {88, 89, 90, 91, 92, 93, 94, 95, 96, 97, 98},
            {99, 100, 101, 102, 103, 104, 105, 106, 107, 108, 109},
            {110, 111, 112, 113, 114, 115, 116, 117, 118, 119, 120}
    };
        static readonly int[,] indices = new int[,]
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
        static readonly int[] cells = new int[]
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
        static List<int> red_top_borders = new List<int> { 110, 111, 112, 113, 114, 115, 116, 117, 118, 119, 120 };
        static List<int> red_bot_borders = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
        static List<int> red_borders = new List<int>();
        static List<int> red_border = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 110, 111, 112, 113, 114, 115, 116, 117, 118, 119, 120 };
        static List<int> blue_left_borders = new List<int> { 0, 11, 22, 33, 44, 55, 66, 77, 88, 99, 110 };
        static List<int> blue_right_borders = new List<int> { 10, 21, 32, 43, 54, 65, 76, 87, 98, 109, 120 };
        static List<int> blue_borders = new List<int>();
        static readonly List<int> blue_border = new List<int> { 0, 11, 22, 33, 44, 55, 66, 77, 88, 99, 110, 10, 21, 32, 43, 54, 65, 76, 87, 98, 109, 120 };
        static List<int> red_tiles = new List<int>();
        static List<int> blue_tiles = new List<int>();
        static int[,] board = new int[v, v];
        static int[,] red_graph = new int[V, V];
        static int[,] blue_graph = new int[V, V];   // blue is always AI
        //static List<int> path = new List<int>();
        static readonly int _i = 0;
        static readonly int _j = 1;
        // need to make the board  and others a global variable, maybe add private or something like that *shrug*

        //
        // I NEED TO FIND WHAT NEIGHBOR(S) IS ON THE SHORTEST PATH. 
        // MEANING I NEED TO HAVE A LIST OF NODES THAT MAKE UP THE SHORTEST PATH
        // I THEN PICK EITHER THE FIRST ONE OR THE ONE THAT IS A NEIGHBOR
        //
        // THESE ARE UNUSED
        /*
        List<int> BreadthFirstTraversal(int[,] graph, int start) // borrowed from: https://stackoverflow.com/questions/15312273/traverse-a-graph-represented-in-an-adjacency-matrix
        {
            var queue = new Queue();
            var mark_set = new List<int>();
            queue.Enqueue(start);
            mark_set.Add(start);
            while (queue.Count != 0)
            {
                var vertex = queue.Dequeue();
                List<int> neighbors = ListNeighbours((int)vertex);
                for(int neighbor = 0; neighbor < neighbors.Count(); neighbor++)
                {
                    if (!mark_set.Contains(neighbor))
                    {
                        mark_set.Add(neighbor);
                        queue.Enqueue(neighbor);
                    }
                }
            }
            return mark_set;
        }
        List<int> ListNeighbours(int vertex)
        {
            // create a new list 
            // for each vertex j in the graph 
            // if j is a neighbour of i then add it to the list 
            // return the list 
            int z = vertex; // lazinesses
            List<int> neighbors = new List<int>();

            if (z < 11)  // bottom
            {
                if (z == 0)
                {
                    neighbors.Add(cells[z + 11]);
                    neighbors.Add(cells[z + 12]);
                    neighbors.Add(cells[z + 1]);

                }
                else if (z == 10)
                {
                    neighbors.Add(cells[z - 1]);
                    neighbors.Add(cells[z + 11]);
                }
                else
                {
                    neighbors.Add(cells[z - 1]);
                    neighbors.Add(cells[z + 11]);
                    neighbors.Add(cells[z + 12]);
                    neighbors.Add(cells[z + 1]);
                }
            }
            else if (z % 11 == 0) // left
            {
                if (z == 110)
                {
                    neighbors.Add(cells[z - 12]);
                    neighbors.Add(cells[z - 1]);
                    neighbors.Add(cells[z + 11]);
                    neighbors.Add(cells[z + 12]);
                }
                else
                {
                    neighbors.Add(cells[z + 11]);
                    neighbors.Add(cells[z + 12]);
                    neighbors.Add(cells[z + 1]);
                    neighbors.Add(cells[z - 11]);
                }
            }
            else if (z == 21 | z == 32 | z == 43 | z == 54 | z == 65 | z == 76 | z == 87 | z == 98 | z == 109 | z == 120)    // right
            {
                if (z == 120)
                {
                    neighbors.Add(cells[z - 12]);
                    neighbors.Add(cells[z - 1]);
                    neighbors.Add(cells[z - 11]);
                }
                else
                {
                    neighbors.Add(cells[z - 12]);
                    neighbors.Add(cells[z - 1]);
                    neighbors.Add(cells[z + 11]);
                    neighbors.Add(cells[z - 11]);
                }
            }
            else if (z > 109)    // top  // doesn't include 120 or 110
            {
                neighbors.Add(cells[z - 12]);
                neighbors.Add(cells[z - 1]);
                neighbors.Add(cells[z + 1]);
                neighbors.Add(cells[z - 11]);
            }
            else    // middle
            {
                neighbors.Add(cells[z - 12]);
                neighbors.Add(cells[z - 1]);
                neighbors.Add(cells[z + 11]);
                neighbors.Add(cells[z + 12]);
                neighbors.Add(cells[z + 1]);
                neighbors.Add(cells[z - 11]);
            }
            return neighbors;
        }
        void DFS(int vertex, ref List<int> traversal, ref int[] visited, int[,]graph)
        {
            int next;
            traversal.Add(vertex);
            visited[vertex] = 1;
            for (next = 0; next < V; next++)
            {
                if (visited[next] != 0 && graph[vertex, next] == 1)    // if it's not in visited and they're connected, do // Make sure this is right?
                {
                    DFS(next, ref traversal, ref visited, graph);
                }
            }
        }
        ////////////////////////////////////////////////////////////////////////////////////
        */

        public void Initialize(int first_player, int seed)
        {
            Transpose();
            red_graph = MakeGraph();    // unused
            blue_graph = MakeGraph();
            GetBorderTiles(board, blue);
            GetBorderTiles(board, red); // unused
            FirstMove(first_player, seed);
            int[] tile = GetPlayerTiles(blue);
            SetBorder(tile[0], blue);
        }
        public void UpdateBoard(int i, int j, int player)   // if the player is "empty" then we are making our first move
        {
            if (player == red)
            {
                board[i, j] = blue;
            }
            else if (player == blue)
            {
                board[i, j] = red;
            }
            else if (player == empty)
            {
                board[i, j] = blue;
            }

        }
        void FirstMove(int player, int seed)
        {
            Random rand = new Random(seed);
            int i = 0;
            int j = 0;
            if (player == red)
            {
                i = indices[red_border[rand.Next(red_border.Count())], _i];
                j = indices[red_border[rand.Next(red_border.Count())], _j];
            }
            else if (player == blue)
            {
                i = indices[blue_border[rand.Next(blue_border.Count())], _i];
                j = indices[blue_border[rand.Next(blue_border.Count())], _j];
            }

            UpdateBoard(i, j, empty);   // sending empty because it is the first move
        }
        void SetBorder(int tile, int player)
        {
            if (player == red)
            {
                if (red_top_borders.Contains(tile))
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
        public int[] Move(int player, int opposing_player)
        {
            List<int> path = new List<int>();
            int[] out_parents = new int[V]; // graph.getlength(0)
            int smallest = int.MaxValue;   // what should I initialize this to?
            int out_source = int.MaxValue;
            List<int> out_dist = new List<int>();

            if (player == red)
            {
                RemoveOpponentTiles(ref red_graph, player, opposing_player); // this edits out the opposing tiles so there are no connections between red and blue
            }
            else if (player == blue)
            {
                RemoveOpponentTiles(ref blue_graph, player, opposing_player); // this edits out the opposing tiles so there are no connections between red and blue
            }

            RemoveOpponentTiles(ref blue_graph, player, opposing_player); // this edits out the opposing tiles so there are no connections between red and blue
            object sync = new Object();
            // I need to make children for Dijkstra in parallel
            int[] tiles = GetPlayerTiles(blue);

            for (int i = 0; i < tiles.Length; i++)
            {
                int source = tiles[i];
                int[] parents = new int[V];
                List<int> dist = DijkstraS(blue_graph, source, ref parents);

                dist = RemoveExtraDists(dist, blue);
                dist[Array.IndexOf(dist.ToArray(), dist.Min())] = int.MaxValue; // this makes sure that 0 isn't picked as the smallest
                if (dist.Min() < smallest)
                {
                    smallest = Array.IndexOf(dist.ToArray(), dist.Min());//dist.Min();
                    out_source = source;
                    out_dist = dist;
                    out_parents = parents;
                }
            } // Serial.For  

            //Parallel.For(0, tiles.Length, i =>
            //{
            //int source = tiles[i];
            //int[] parents = new int[V];
            //List<int> dist = DijkstraS(blue_graph, source, ref parents);

            //dist = RemoveExtraDists(dist, blue);
            //dist[Array.IndexOf(dist.ToArray(), dist.Min())] = int.MaxValue; // this makes sure that 0 isn't picked as the smallest
            //if (dist.Min() < smallest)
            //{
            //lock (sync)
            //{
            //    smallest = Array.IndexOf(dist.ToArray(), dist.Min());//dist.Min();
            //    out_source = source;
            //    out_dist = dist;
            //    out_parents = parents;
            //}
            //}
            //}); // Parallel.For  

            // change the board by adding a player tile that gives the shortest distance
            if (player == red)
            {
                Console.Write("Red ");
            }
            else if (player == blue)
            {
                Console.Write("Blue ");
            }
            PrintSolution(out_source, out_dist, out_parents, ref path, smallest);
            smallest = path[path.Count - 2];  // gets the next tile in the chain after the initial tile// path[last] == start
            int[] move = new int[] { indices[smallest, _i], indices[smallest, _j] };
            board[indices[smallest, _i], indices[smallest, _j]] = blue;
            return move;
        }
        void PrintBoard()
        {
            return;
        }
        List<int> RemoveExtraDists(List<int> dist, int player)
        {
            List<int> ret_dist = dist;
            object sync_dist = new Object();
            Parallel.For(0, dist.Count, i =>
            {
                if (player == red)
                {
                    if (!red_borders.Contains(i)) // borders does not contain
                    {
                        lock (sync_dist)
                        {
                            //dist.Remove(i);
                            dist[i] = int.MaxValue;
                        }
                    }
                }
                else if (player == blue)
                {
                    if (!blue_borders.Contains(i))    // if borders(including player tiles) does not contain the tile, remove it
                    {
                        lock (sync_dist)
                        {
                            //dist.Remove(i);
                            dist[i] = int.MaxValue;
                        }
                    }
                }
            });
            return ret_dist;
        }
        void GetBorderTiles(int[,] board, int player) // 0 = red, 1 = blue, 2 = empty
        {
            for (int i = 0; i < v; i++)
            {
                for (int j = 0; j < v; j++)
                {
                    if (board[i, j] == player)
                    {
                        if (player == 0)
                        {
                            red_borders.Add(translation[i, j]);
                        }
                        else if (player == 1)
                        {
                            blue_borders.Add(translation[i, j]);
                        }
                    }
                }
            }
        }
        int[] GetPlayerTiles(int player)
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
        void RemoveOpponentTiles(ref int[,] graph, int player, int opposing_player)   // correct way of referencing an array?
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
                                graph[z + 11, z] = 0;
                                graph[z + 12, z] = 0;
                                graph[z + 1, z] = 0;

                                graph[z, z + 11] = 0;
                                graph[z, z + 12] = 0;
                                graph[z, z + 1] = 0;
                            }
                            else if (z == 10)
                            {
                                graph[z - 1, z] = 0;
                                graph[z + 11, z] = 0;

                                graph[z, z - 1] = 0;
                                graph[z, z + 11] = 0;
                            }
                            else
                            {
                                graph[z - 1, z] = 0;
                                graph[z + 11, z] = 0;
                                graph[z + 12, z] = 0;
                                graph[z + 1, z] = 0;

                                graph[z, z - 1] = 0;
                                graph[z, z + 11] = 0;
                                graph[z, z + 12] = 0;
                                graph[z, z + 1] = 0;
                            }
                        }
                        else if (z % 11 == 0) // left
                        {
                            if (z == 110)
                            {
                                //graph[z - 12, z] = 0;
                                graph[z + 1, z] = 0;
                                graph[z - 11, z] = 0;
                                //graph[z + 12, z] = 0;

                                //graph[z, z - 12] = 0;
                                graph[z, z + 1] = 0;
                                graph[z, z - 11] = 0;
                                //graph[z, z + 12] = 0;
                            }
                            else
                            {
                                graph[z + 11, z] = 0;
                                graph[z + 12, z] = 0;
                                graph[z + 1, z] = 0;
                                graph[z - 11, z] = 0;

                                graph[z, z + 11] = 0;
                                graph[z, z + 12] = 0;
                                graph[z, z + 1] = 0;
                                graph[z, z - 11] = 0;
                            }
                        }
                        else if (z == 21 | z == 32 | z == 43 | z == 54 | z == 65 | z == 76 | z == 87 | z == 98 | z == 109 | z == 120)    // right
                        {
                            if (z == 120)
                            {
                                graph[z - 12, z] = 0;
                                graph[z - 1, z] = 0;
                                graph[z - 11, z] = 0;

                                graph[z, z - 12] = 0;
                                graph[z, z - 1] = 0;
                                graph[z, z - 11] = 0;
                            }
                            else
                            {
                                graph[z - 12, z] = 0;
                                graph[z - 1, z] = 0;
                                graph[z + 11, z] = 0;
                                graph[z - 11, z] = 0;

                                graph[z, z - 12] = 0;
                                graph[z, z - 1] = 0;
                                graph[z, z + 11] = 0;
                                graph[z, z - 11] = 0;
                            }
                        }
                        else if (z > 109)    // top  // doesn't include 120 or 110
                        {
                            graph[z - 12, z] = 0;
                            graph[z - 1, z] = 0;
                            graph[z + 1, z] = 0;
                            graph[z - 11, z] = 0;

                            graph[z, z - 12] = 0;
                            graph[z, z - 1] = 0;
                            graph[z, z + 1] = 0;
                            graph[z, z - 11] = 0;
                        }
                        else    // middle
                        {
                            graph[z - 12, z] = 0;
                            graph[z - 1, z] = 0;
                            graph[z + 11, z] = 0;
                            graph[z + 12, z] = 0;
                            graph[z + 1, z] = 0;
                            graph[z - 11, z] = 0;

                            graph[z, z - 12] = 0;
                            graph[z, z - 1] = 0;
                            graph[z, z + 11] = 0;
                            graph[z, z + 12] = 0;
                            graph[z, z + 1] = 0;
                            graph[z, z - 11] = 0;
                        }
                    }
                }
            }
        }
        int[,] MakeGraph()
        {
            int[,] graph = new int[V, V];

            for (int i = 0; i < V; i++)
            {
                if (i < 11)  // bottom
                {
                    if (i == 0)
                    {
                        graph[i + 11, i] = 1;
                        graph[i + 12, i] = 1;
                        graph[i + 1, i] = 1;

                        graph[i, i + 11] = 1;
                        graph[i, i + 12] = 1;
                        graph[i, i + 1] = 1;
                    }
                    else if (i == 10)
                    {
                        graph[i - 1, i] = 1;
                        graph[i + 11, i] = 1;

                        graph[i, i - 1] = 1;
                        graph[i, i + 11] = 1;
                    }
                    else
                    {
                        graph[i - 1, i] = 1;
                        graph[i + 11, i] = 1;
                        graph[i + 12, i] = 1;
                        graph[i + 1, i] = 1;

                        graph[i, i - 1] = 1;
                        graph[i, i + 11] = 1;
                        graph[i, i + 12] = 1;
                        graph[i, i + 1] = 1;
                    }
                }
                else if (i % 11 == 0) // left
                {
                    if (i == 110)
                    {
                        //graph[i - 12, i] = 1;
                        graph[i + 1, i] = 1;
                        graph[i - 11, i] = 1;
                        //graph[i + 12, i] = 1;

                        //graph[i, i - 12] = 1;
                        graph[i, i + 1] = 1;
                        graph[i, i - 11] = 1;
                        // graph[i, i + 12] = 1;
                    }
                    else
                    {
                        graph[i + 11, i] = 1;
                        graph[i + 12, i] = 1;
                        graph[i + 1, i] = 1;
                        graph[i - 11, i] = 1;

                        graph[i, i + 11] = 1;
                        graph[i, i + 12] = 1;
                        graph[i, i + 1] = 1;
                        graph[i, i - 11] = 1;
                    }
                }
                else if (i == 21 | i == 32 | i == 43 | i == 54 | i == 65 | i == 76 | i == 87 | i == 98 | i == 109 | i == 120)    // right
                {
                    if (i == 120)
                    {
                        graph[i - 12, i] = 1;
                        graph[i - 1, i] = 1;
                        graph[i - 11, i] = 1;

                        graph[i, i - 12] = 1;
                        graph[i, i - 1] = 1;
                        graph[i, i - 11] = 1;
                    }
                    else
                    {
                        graph[i - 12, i] = 1;
                        graph[i - 1, i] = 1;
                        graph[i + 11, i] = 1;
                        graph[i - 11, i] = 1;

                        graph[i, i - 12] = 1;
                        graph[i, i - 1] = 1;
                        graph[i, i + 11] = 1;
                        graph[i, i - 11] = 1;
                    }
                }
                else if (i > 109)    // top  // doesn't include 120 or 110
                {
                    graph[i - 12, i] = 1;
                    graph[i - 1, i] = 1;
                    graph[i + 1, i] = 1;
                    graph[i - 11, i] = 1;

                    graph[i, i - 12] = 1;
                    graph[i, i - 1] = 1;
                    graph[i, i + 1] = 1;
                    graph[i, i - 11] = 1;
                }
                else    // middle
                {
                    graph[i - 12, i] = 1;
                    graph[i - 1, i] = 1;
                    graph[i + 11, i] = 1;
                    graph[i + 12, i] = 1;
                    graph[i + 1, i] = 1;
                    graph[i - 11, i] = 1;

                    graph[i, i - 12] = 1;
                    graph[i, i - 1] = 1;
                    graph[i, i + 11] = 1;
                    graph[i, i + 12] = 1;
                    graph[i, i + 1] = 1;
                    graph[i, i - 11] = 1;
                }
            }
            return graph;
        }
        // This function stores transpose of A[][] in B[][] 
        static void Transpose()
        {
            int[,] B = translation;
            int i, j;
            for (i = 0; i < v; i++)
            {
                for (j = 0; j < v; j++)
                {
                    B[i, j] = translation[j, i];
                }
            }
            translation = B;
        }
        private static void PrintSolution(int startVertex, List<int> dist, int[] parents, ref List<int> path, int smallest)
        {
            int nVertices = dist.Count;
            //Console.Write("Vertex\t Distance\tPath");
            for (int vertexIndex = 0; vertexIndex < nVertices; vertexIndex++)
            {
                if (vertexIndex != startVertex && vertexIndex == smallest)    // dist.Min()
                {
                    Console.Write("\n" + startVertex + " -> ");
                    Console.Write(vertexIndex + "   ");
                    Console.Write(dist[vertexIndex] + "    ");
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
            Console.Write(currentVertex + " ");
            MakePath(parents[currentVertex], parents, ref path);
        }
        private static List<int> DijkstraS(int[,] graph, int startVertex, ref int[] parents)
        {
            int nVertices = graph.GetLength(0);

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
                for (int vertexIndex = 0; vertexIndex < nVertices; vertexIndex++)
                {
                    if (!added[vertexIndex] && dist[vertexIndex] < shortestDistance)
                    {
                        nearestVertex = vertexIndex;
                        shortestDistance = dist[vertexIndex];
                    }
                    //Console.WriteLine(vertexIndex);
                }
                // Mark the picked vertex as processed  
                added[nearestVertex] = true;

                // Update dist value of the adjacent vertices of the picked vertex.  
                for (int vertexIndex = 0; vertexIndex < nVertices; vertexIndex++)
                {
                    int edgeDistance = graph[nearestVertex, vertexIndex];

                    if (edgeDistance > 0 && ((shortestDistance + edgeDistance) < dist[vertexIndex]))
                    {
                        parents[vertexIndex] = nearestVertex;
                        dist[vertexIndex] = shortestDistance + edgeDistance;
                    }
                }
            }
            //PrintSolution(startVertex, shortestDistances, parents);
            return dist;
        }
    }
}

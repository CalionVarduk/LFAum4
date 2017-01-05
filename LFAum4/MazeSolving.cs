using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace LFAum4
{
    public static class MazeSolving
    {
        public static GraphPath RandomMouse(MazeGraph maze)
        {
            Random rng = new Random();
            GraphPath path = new GraphPath();
            int direction = (int)StartingDirection(maze);

            GraphVertex v = maze.EntrancePoint;
            path.Start(v);

            while(v.Location != maze.Exit)
            {
                int connections = v.Degree;
                if (v.Location == maze.Entrance) ++connections;

                if (connections == 1)
                    direction = TurnAround(direction);
                else if (connections == 2)
                {
                    int newDirection = ContinueAlongThePath(direction, v);
                    direction = (newDirection != -1) ? newDirection : TurnAround(direction);
                }
                else                            // at junction - pick random direction (without turning back)
                {
                    int newDirection = rng.Next(4);
                    while (!IsAdvancing(v, newDirection, direction))
                        newDirection = (newDirection + 1) & 3;
                    direction = newDirection;
                }
                v = path.Next(direction);
            }
            return path;
        }

        public static GraphPath WallFollower(MazeGraph maze, bool leftHandRule)
        {
            GraphPath path = new GraphPath();
            int direction = (int)StartingDirection(maze);
            int dirChange = leftHandRule ? -1 : 1;

            GraphVertex v = maze.EntrancePoint;
            path.Start(v);

            while (v.Location != maze.Exit)
            {
                int newDirection = (direction + dirChange) & 3;

                if (v.Edges[newDirection] != null)              // can go left/right
                    direction = newDirection;
                else if (v.Edges[direction] == null)            // can't go forward
                {
                    newDirection = (direction - dirChange) & 3;     // if can go right/left, do so - otherwise turn back
                    direction = (v.Edges[newDirection] != null) ? newDirection : TurnAround(direction);
                }
                v = path.Next(direction);
            }
            return path;
        }

        public static GraphPath Tremaux(MazeGraph maze)
        {
            Random rng = new Random();
            int direction = (int)StartingDirection(maze);
            byte[,] marks = new byte[maze.Columns, maze.Rows];

            GraphVertex v = maze.EntrancePoint;

            while (v.Location != maze.Exit)
            {
                int connections = v.Degree;
                if (v.Location == maze.Entrance) ++connections;

                if (connections == 1)
                {
                    direction = TurnAround(direction);
                    marks[v.X, v.Y] = 2;
                }
                else if (connections == 2)
                {
                    direction = ContinueAlongThePath(direction, v);
                    ++marks[v.X, v.Y];
                }
                else                                                // at junction
                {
                    byte minMarks = 3;
                    for (int i = 0; i < 4; ++i)
                    {
                        if (v.Edges[i] != null)
                        {
                            GraphVertex n = v.NeighbourAt(i);
                            if (minMarks > marks[n.X, n.Y]) minMarks = marks[n.X, n.Y];
                        }
                    }

                    if (minMarks == 0)                              // if there is an unvisited path at this junction - take it (if more than one - choose random)
                    {
                        direction = TremauxRandomDirection(v, marks, minMarks, rng);
                        if (marks[v.X, v.Y] == 0) ++marks[v.X, v.Y];        // mark the junction as visited if it hasn't been already
                    }
                    else                                            // if all paths at this junction have been visited at least once
                    {
                        GraphVertex prev = v.NeighbourAt(TurnAround(direction));

                        if (marks[prev.X, prev.Y] == 1)
                            direction = TurnAround(direction);      // if moving forward, turn back
                        else
                        {                                           // if backtracking, pick a random direction
                            direction = TremauxRandomDirection(v, marks, minMarks, rng);

                            int count = 0;
                            for (int i = 0; i < 4; ++i)
                            {
                                if (v.Edges[i] != null)
                                {
                                    GraphVertex n = v.NeighbourAt(i);
                                    if (marks[n.X, n.Y] == 1) ++count;
                                }
                            }
                            if (count == 1) ++marks[v.X, v.Y];      // if there is only one path left that has been visited only once, mark this junction as fully explored
                        }
                    }
                }
                v = v.NeighbourAt(direction);
            }
            return TremauxPath(maze, v, marks);
        }

        private static int TremauxRandomDirection(GraphVertex v, byte[,] marks, byte minMarks, Random rng)
        {
            int direction = rng.Next(4);

            for(int i = 0; i < 4; ++i)
            {
                int k = (direction + i) & 3;
                if(v.Edges[k] != null)
                {
                    GraphVertex n = v.NeighbourAt(k);
                    if(marks[n.X, n.Y] == minMarks)
                        return k;
                }
            }
            return -1;
        }

        private static GraphPath TremauxPath(MazeGraph maze, GraphVertex current, byte[,] marks)
        {
            int direction = (int)ExitDirection(maze);

            GraphPath path = new GraphPath();
            path.Start(current);

            while (current.Location != maze.Entrance)
            {
                for (int i = 0; i < 4; ++i)
                {
                    if (IsAdvancing(current, i, direction))
                    {
                        GraphVertex n = current.NeighbourAt(i);
                        if (marks[n.X, n.Y] == 1)
                        {
                            direction = i;
                            break;
                        }
                    }
                }
                current = path.Next(direction);
            }
            path.Reverse();

            return path;
        }

        public static GraphPath FloodFill(MazeGraph maze)
        {
            int direction = (int)StartingDirection(maze);
            byte[,] reachedFrom = new byte[maze.Columns, maze.Rows];
            bool[,] flooded = new bool[maze.Columns, maze.Rows];
            flooded[maze.Exit.X, maze.Exit.Y] = true;

            List<GraphVertex> frontier = new List<GraphVertex>();
            frontier.Add(maze.ExitPoint);
            
            while (frontier.Count > 0)
            {
                GraphVertex p = frontier[0];
                if (p.Location == maze.Entrance) break;

                frontier.RemoveAt(0);
                
                for (int i = 0; i < 4; ++i)
                {
                    if (p.Edges[i] != null)
                    {
                        GraphVertex n = p.NeighbourAt(i);
                        if(!flooded[n.X, n.Y])
                        {
                            flooded[n.X, n.Y] = true;
                            reachedFrom[n.X, n.Y] = (byte)((i + 2) & 3);
                            frontier.Add(n);
                        }
                    }
                }
            }
            return FloodFillPath(maze, frontier[0], reachedFrom);
        }

        private static GraphPath FloodFillPath(MazeGraph maze, GraphVertex current, byte[,] reachedFrom)
        {
            GraphPath path = new GraphPath();
            path.Start(current);

            while (current.Location != maze.Exit)
                current = path.Next(reachedFrom[current.X, current.Y]);

            return path;
        }

        public static GraphPath Dijkstra(MazeGraph maze)
        {
            ShortestPathData data = new ShortestPathData(maze, (v1, v2) => 0);
            return PathFromData(maze, data);
        }

        public static GraphPath AStar(MazeGraph maze)
        {
            ShortestPathData data = new ShortestPathData(maze, (v1, v2) => (Math.Abs(v1.X - v2.X) + Math.Abs(v1.Y - v2.Y)));
            return PathFromData(maze, data);
        }

        private static GraphPath PathFromData(MazeGraph maze, ShortestPathData data)
        {
            GraphPath path = new GraphPath();
            int x = maze.Exit.X, y = maze.Exit.Y;
            path.Start(maze[x, y]);

            while (x != maze.Entrance.X || y != maze.Entrance.Y)
            {
                path.Next((int)data.ReachedFrom(x, y));

                switch (data.ReachedFrom(x, y))
                {
                    case Direction.Left:
                        --x; break;

                    case Direction.Right:
                        ++x; break;

                    case Direction.Top:
                        --y; break;

                    case Direction.Bottom:
                        ++y; break;
                }
            }
            path.Reverse();

            return path;
        }

        private static int TurnAround(int direction)
        {
            return ((direction + 2) & 3);
        }

        private static int ContinueAlongThePath(int direction, GraphVertex v)
        {
            for (int i = 0; i < 4; ++i)
                if (IsAdvancing(v, i, direction))
                    return i;
            return -1;
        }

        private static bool IsAdvancing(GraphVertex v, int iEdge, int direction)
        {
            return (v.Edges[iEdge] != null && TurnAround(direction) != iEdge);
        }

        private static Direction StartingDirection(MazeGraph maze)
        {
            return (maze.Entrance.Y == 0) ? Direction.Bottom :
                   (maze.Entrance.Y == maze.Rows - 1) ? Direction.Top :
                   (maze.Entrance.X == 0) ? Direction.Right :
                                            Direction.Left;
        }

        private static Direction ExitDirection(MazeGraph maze)
        {
            return (maze.Exit.Y == 0) ? Direction.Bottom :
                   (maze.Exit.Y == maze.Rows - 1) ? Direction.Top :
                   (maze.Exit.X == 0) ? Direction.Right :
                                        Direction.Left;
        }
    }
}

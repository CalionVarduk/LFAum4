using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace LFAum4
{
    public class MazeGraph
    {
        private GridVertex[,] points;
        public GridVertex this[int x, int y]
        { get { return points[x, y]; } }

        private Point entrance;
        public Point Entrance
        {
            get { return entrance; }
            set
            {
                if ((value.X != 0 && value.X != Columns - 1) ||
                    (value.Y != 0 && value.Y != Rows - 1))
                    throw new ArgumentOutOfRangeException("Entrance point must be on the maze's edge.");

                entrance = value;
            }
        }

        private Point exit;
        public Point Exit
        {
            get { return exit; }
            set
            {
                if ((value.X != 0 && value.X != Columns - 1) ||
                    (value.Y != 0 && value.Y != Rows - 1))
                    throw new ArgumentOutOfRangeException("Exit point must be on the maze's edge.");

                exit = value;
            }
        }

        public GridVertex EntrancePoint { get { return points[entrance.X, entrance.Y]; } }
        public GridVertex ExitPoint { get { return points[exit.X, exit.Y]; } }

        public int Columns { get { return points.GetLength(0); } }
        public int Rows { get { return points.GetLength(1); } }
        public int Count { get { return points.Length; } }

        public MazeGraph(int columns, int rows)
        {
            points = new GridVertex[columns, rows];

            for (int i = 0; i < columns; ++i)
                for (int j = 0; j < rows; ++j)
                    points[i, j] = new GridVertex(i, j);

            int columnsM1 = columns - 1;
            int rowsM1 = rows - 1;

            for (int i = 0; i < columns; ++i)
            {
                for (int j = 0; j < rows; ++j)
                {
                    GridVertex v = points[i, j];

                    if (i > 0)
                        v.LeftEdge = points[i - 1, j].RightEdge;

                    if (i < columnsM1)
                    {
                        v.RightEdge = new GraphEdge(1);
                        v.RightEdge.LinkVertices(v, points[i + 1, j]);
                    }

                    if (j > 0)
                        v.TopEdge = points[i, j - 1].BottomEdge;

                    if (j < rowsM1)
                    {
                        v.BottomEdge = new GraphEdge(1);
                        v.BottomEdge.LinkVertices(v, points[i, j + 1]);
                    }
                }
            }

            entrance = Point.Empty;
            exit = new Point(columnsM1, rowsM1);
        }

        public bool HasWallAt(int x, int y, Direction direction)
        {
            if (direction == Direction.Left)
            {
                if (x == 0)
                {
                    if ((entrance.X == x && entrance.Y == y) || (exit.X == x && exit.Y == y))
                        return (y == 0 || y == Rows - 1);
                    return true;
                }
                return !points[x, y].HasLeft;
            }
            if (direction == Direction.Top)
            {
                if (y == 0)
                    return ((entrance.Y != 0 || entrance.X != x) && (exit.Y != 0 || exit.X != x));
                return !points[x, y].HasTop;
            }
            if (direction == Direction.Right)
            {
                if (x == Columns - 1)
                {
                    if ((entrance.X == x && entrance.Y == y) || (exit.X == x && exit.Y == y))
                        return (y == 0 || y == Rows - 1);
                    return true;
                }
                return !points[x, y].HasRight;
            }
            if (y == Rows - 1)
                return ((entrance.Y != y || entrance.X != x) && (exit.Y != y || exit.X != x));
            return !points[x, y].HasBottom;
        }

        public void CreateWallAt(int x, int y, Direction direction)
        {
            switch (direction)
            {
                case Direction.Left: CreateWallLeft(x, y); break;
                case Direction.Top: CreateWallUp(x, y); break;
                case Direction.Right: CreateWallRight(x, y); break;
                case Direction.Bottom: CreateWallDown(x, y); break;
            }
        }

        public void RemoveWallAt(int x, int y, Direction direction)
        {
            switch (direction)
            {
                case Direction.Left: RemoveWallLeft(x, y); break;
                case Direction.Top: RemoveWallUp(x, y); break;
                case Direction.Right: RemoveWallRight(x, y); break;
                case Direction.Bottom: RemoveWallDown(x, y); break;
            }
        }

        private void CreateWallUp(int x, int y)
        {
            GraphEdge e = points[x, y].TopEdge;
            points[x, y].TopEdge = null;
            points[x, y - 1].BottomEdge = null;
            if (e != null) e.UnlinkVertices();
        }

        private void CreateWallDown(int x, int y)
        {
            GraphEdge e = points[x, y].BottomEdge;
            points[x, y].BottomEdge = null;
            points[x, y + 1].TopEdge = null;
            if (e != null) e.UnlinkVertices();
        }

        private void CreateWallLeft(int x, int y)
        {
            GraphEdge e = points[x, y].LeftEdge;
            points[x, y].LeftEdge = null;
            points[x - 1, y].RightEdge = null;
            if (e != null) e.UnlinkVertices();
        }

        private void CreateWallRight(int x, int y)
        {
            GraphEdge e = points[x, y].RightEdge;
            points[x, y].RightEdge = null;
            points[x + 1, y].LeftEdge = null;
            if (e != null) e.UnlinkVertices();
        }

        private void RemoveWallUp(int x, int y)
        {
            if (!points[x, y].HasTop)
            {
                GraphEdge e = new GraphEdge(1);
                points[x, y].TopEdge = e;
                points[x, y - 1].BottomEdge = e;
                e.LinkVertices(points[x, y], points[x, y - 1]);
            }
        }

        private void RemoveWallDown(int x, int y)
        {
            if (!points[x, y].HasBottom)
            {
                GraphEdge e = new GraphEdge(1);
                points[x, y].BottomEdge = e;
                points[x, y + 1].TopEdge = e;
                e.LinkVertices(points[x, y], points[x, y + 1]);
            }
        }

        private void RemoveWallLeft(int x, int y)
        {
            if (!points[x, y].HasLeft)
            {
                GraphEdge e = new GraphEdge(1);
                points[x, y].LeftEdge = e;
                points[x - 1, y].RightEdge = e;
                e.LinkVertices(points[x, y], points[x - 1, y]);
            }
        }

        private void RemoveWallRight(int x, int y)
        {
            if (!points[x, y].HasRight)
            {
                GraphEdge e = new GraphEdge(1);
                points[x, y].RightEdge = e;
                points[x + 1, y].LeftEdge = e;
                e.LinkVertices(points[x, y], points[x + 1, y]);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace LFAum4
{
    public class MazePanel : Panel
    {
        public MazeGraph Maze { get; set; }
        public GraphPath Path { get; set; }

        private int tileSize;
        public int TileSize
        {
            get { return tileSize - 1; }
            set
            {
                tileSize = (value < 5) ? 6 : value + 1;
                if ((tileSize & 1) == 1) ++tileSize;
            }
        }

        private Pen wallPen;
        public Brush WallBrush
        {
            get { return wallPen.Brush; }
            set { wallPen.Brush = value; }
        }

        private Pen pathPen;
        public Brush PathBrush
        {
            get { return pathPen.Brush; }
            set { pathPen.Brush = value; }
        }

        public Brush EntranceBrush { get; set; }
        public Brush ExitBrush { get; set; }

        public Size MazeSize
        {
            get
            {
                Size size = new Size(1, 1);

                if (Maze != null)
                {
                    if (Maze.Columns > 0) size.Width += Maze.Columns * tileSize;
                    if (Maze.Rows > 0) size.Height += Maze.Rows * tileSize;
                }
                return size;
            }
        }

        public MazePanel()
        {
            DoubleBuffered = true;
            tileSize = 6;
            wallPen = new Pen(new SolidBrush(Color.Black), 1);
            pathPen = new Pen(new SolidBrush(Color.Blue), 1);
            EntranceBrush = new SolidBrush(Color.Red);
            ExitBrush = new SolidBrush(Color.Green);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (Maze != null) DrawMaze(e.Graphics);
        }

        private void DrawMaze(Graphics g)
        {
            int columns = Maze.Columns;
            int rows = Maze.Rows;

            for (int i = 0; i < columns; ++i)
            {
                if (Maze.HasWallAt(i, 0, Direction.Top))
                {
                    int x = Maze[i, 0].X * tileSize;
                    g.DrawLine(wallPen, x, 0, x + tileSize, 0);
                }
            }

            for (int i = 0; i < rows; ++i)
            {
                if (Maze.HasWallAt(0, i, Direction.Left))
                {
                    int y = Maze[0, i].Y * tileSize;
                    g.DrawLine(wallPen, 0, y, 0, y + tileSize);
                }
            }

            for (int i = 0; i < columns; ++i)
            {
                for (int j = 0; j < rows; ++j)
                {
                    if (Maze.HasWallAt(i, j, Direction.Right))
                    {
                        int x = (Maze[i, j].X + 1) * tileSize;
                        int y = Maze[i, j].Y * tileSize;
                        g.DrawLine(wallPen, x, y, x, y + tileSize);
                    }

                    if (Maze.HasWallAt(i, j, Direction.Bottom))
                    {
                        int x = Maze[i, j].X * tileSize;
                        int y = (Maze[i, j].Y + 1) * tileSize;
                        g.DrawLine(wallPen, x, y, x + tileSize, y);
                    }
                }
            }

            g.FillRectangle(EntranceBrush, Maze.Entrance.X * tileSize + 2, Maze.Entrance.Y * tileSize + 2, tileSize - 3, tileSize - 3);
            g.FillRectangle(ExitBrush, Maze.Exit.X * tileSize + 2, Maze.Exit.Y * tileSize + 2, tileSize - 3, tileSize - 3);

            if (Path != null && Path.VertexCount > 0) DrawPath(g);
        }

        private void DrawPath(Graphics g)
        {
            int sizeHalf = tileSize >> 1;
            int count = Path.VertexCount;

            for (int i = 1; i < count; ++i)
            {
                GraphVertex p0 = Path.Vertex(i - 1);
                GraphVertex p1 = Path.Vertex(i);

                int x0 = p0.X * tileSize + sizeHalf;
                int y0 = p0.Y * tileSize + sizeHalf;

                int x1 = p1.X * tileSize + sizeHalf;
                int y1 = p1.Y * tileSize + sizeHalf;

                g.DrawLine(pathPen, x0, y0, x1, y1);
            }
        }
    }
}

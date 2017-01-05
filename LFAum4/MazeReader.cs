using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;

namespace LFAum4
{
    public static class MazeReader
    {
        public static MazeGraph FromFile(string fileName)
        {
            MazeGraph maze = null;

            using (FileStream fs = new FileStream(fileName, FileMode.Open))
            using (StreamReader sr = new StreamReader(fs))
            {
                int columns, rows;
                Point entrance, exit;
                InitialRead(sr, out columns, out rows, out entrance, out exit);

                maze = new MazeGraph(columns, rows);
                maze.Entrance = entrance;
                maze.Exit = exit;

                while (!sr.EndOfStream)
                {
                    string[] line = sr.ReadLine().Split(' ');

                    if (line.Length == 3 && line[2].Length > 0)
                    {
                        int x = int.Parse(line[0]);
                        int y = int.Parse(line[1]);

                        if (line[2][0] == 'P')
                        {
                            InterpretCodeP(maze, x, y);
                            if (line[2].Length > 1 && line[2][1] == 'D')
                                InterpretCodeD(maze, x, y);
                        }
                        else if (line[2][0] == 'D')
                            InterpretCodeD(maze, x, y);
                    }
                }
            }
            return maze;
        }

        private static void InterpretCodeP(MazeGraph maze, int x, int y)
        {
            if (y > 0)
                maze.CreateWallAt(x, y - 1, Direction.Bottom);
        }

        private static void InterpretCodeD(MazeGraph maze, int x, int y)
        {
            if (x > 0)
                maze.CreateWallAt(x - 1, y, Direction.Right);
        }

        private static void InitialRead(StreamReader sr, out int columns, out int rows, out Point entrance, out Point exit)
        {
            columns = 0;
            rows = 0;
            entrance = Point.Empty;
            exit = Point.Empty;

            if (!sr.EndOfStream)
            {
                string[] line = sr.ReadLine().Split(' ');
                int c = int.Parse(line[0]);
                int r = int.Parse(line[1]);

                columns = Math.Max(columns, c);
                rows = Math.Max(rows, r);
                entrance = new Point(c, r);

                while (!sr.EndOfStream)
                {
                    line = sr.ReadLine().Split(' ');
                    c = int.Parse(line[0]);
                    r = int.Parse(line[1]);

                    columns = Math.Max(columns, c);
                    rows = Math.Max(rows, r);
                }
                exit = new Point(c, r);
            }

            ++columns;
            ++rows;
            sr.BaseStream.Seek(0, SeekOrigin.Begin);
        }
    }
}

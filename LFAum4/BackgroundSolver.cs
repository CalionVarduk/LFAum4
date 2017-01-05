using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.ComponentModel;

namespace LFAum4
{
    public enum MazeSolvingAlgoritm
    {
        RandomMouse,
        WallFollowerLeft,
        WallFollowerRight,
        Tremaux,
        FloodFill,
        Dijkstra,
        AStar
    }

    public sealed class BackgroundSolver
    {
        private BackgroundWorker worker;
        private Stopwatch watch;

        public MazeGraph Maze { get; private set; }
        public GraphPath Path { get; private set; }

        public long ElapsedMs { get { return watch.ElapsedMilliseconds; } }
        public bool IsSolving { get { return worker.IsBusy; } }
        public MazeSolvingAlgoritm Algorithm { get; private set; }

        public event Action<BackgroundSolver> Solved;

        public BackgroundSolver()
        {
            watch = new Stopwatch();
            worker = new BackgroundWorker();
            worker.DoWork += this.DoWork;
            worker.RunWorkerCompleted += this.WorkDone;
        }

        public void Solve(MazeGraph maze, MazeSolvingAlgoritm algorithm)
        {
            Maze = maze;
            Algorithm = algorithm;
            worker.RunWorkerAsync();
        }

        private void DoWork(object sender, DoWorkEventArgs e)
        {
            watch.Restart();

            switch (Algorithm)
            {
                case MazeSolvingAlgoritm.RandomMouse:
                    Path = MazeSolving.RandomMouse(Maze); break;

                case MazeSolvingAlgoritm.WallFollowerLeft:
                    Path = MazeSolving.WallFollower(Maze, true); break;

                case MazeSolvingAlgoritm.WallFollowerRight:
                    Path = MazeSolving.WallFollower(Maze, false); break;

                case MazeSolvingAlgoritm.Tremaux:
                    Path = MazeSolving.Tremaux(Maze); break;

                case MazeSolvingAlgoritm.FloodFill:
                    Path = MazeSolving.FloodFill(Maze); break;

                case MazeSolvingAlgoritm.Dijkstra:
                    Path = MazeSolving.Dijkstra(Maze); break;

                case MazeSolvingAlgoritm.AStar:
                    Path = MazeSolving.AStar(Maze); break;
            }
        }

        private void WorkDone(object sender, RunWorkerCompletedEventArgs e)
        {
            watch.Stop();
            if (Solved != null) Solved(this);
        }
    }
}

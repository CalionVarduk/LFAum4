using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LFAum4
{
    public sealed class ShortestPathData
    {
        private Tuple<Direction, int>[,] data;

        public Tuple<Direction, int> this[int x, int y]
        { get { return data[x, y]; } }

        public Func<GridVertex, GridVertex, int> Heuristic { get; private set; }
        public MazeGraph Maze { get; private set; }

        public int Rows { get { return data.GetLength(1); } }
        public int Columns { get { return data.GetLength(0); } }
        public int Count { get { return data.Length; } }

        public ShortestPathData(MazeGraph maze, Func<GridVertex, GridVertex, int> heuristic)
        {
            Maze = maze;
            Heuristic = heuristic;
            var heap = Init();

            bool[,] evaluated = new bool[Maze.Columns, Maze.Rows];

            while (!heap.IsEmpty)
            {
                VertexTreeData c = heap.Extract();
                GridVertex p = c.Vertex;
                data[p.X, p.Y] = c.Data;

                if (p.X == Maze.Exit.X && p.Y == Maze.Exit.Y)
                    break;

                evaluated[p.X, p.Y] = true;

                if (p.HasLeft && !evaluated[p.Left.X, p.Left.Y])
                    HandleNeighbour(p, p.Left, Direction.Right, p.LeftEdge.Weight, heap);

                if (p.HasRight && !evaluated[p.Right.X, p.Right.Y])
                    HandleNeighbour(p, p.Right, Direction.Left, p.RightEdge.Weight, heap);

                if (p.HasTop && !evaluated[p.Top.X, p.Top.Y])
                    HandleNeighbour(p, p.Top, Direction.Bottom, p.TopEdge.Weight, heap);

                if (p.HasBottom && !evaluated[p.Bottom.X, p.Bottom.Y])
                    HandleNeighbour(p, p.Bottom, Direction.Top, p.BottomEdge.Weight, heap);
            }
        }

        public Direction ReachedFrom(int x, int y)
        {
            return data[x, y].Item1;
        }

        public int Cost(int x, int y)
        {
            return data[x, y].Item2;
        }

        private MinHeap<VertexTreeData> Init()
        {
            int columns = Maze.Columns;
            int rows = Maze.Rows;

            data = new Tuple<Direction, int>[columns, rows];

            var heap = new MinHeap<VertexTreeData>();
            heap.Insert(new VertexTreeData(Maze[Maze.Entrance.X, Maze.Entrance.Y]));
            UpdateHeap(heap, Maze[Maze.Entrance.X, Maze.Entrance.Y], Direction.Left, 0);
            return heap;
        }

        private void UpdateHeap(MinHeap<VertexTreeData> heap, GridVertex v, Direction d, int cost)
        {
            VertexTreeData c = heap.Remove(new VertexTreeData(v));
            if (c == null) c = new VertexTreeData(v);

            c.ReachedFrom = d;
            c.Cost = cost;
            c.HeuristicValue = Heuristic(v, Maze[Maze.Exit.X, Maze.Exit.Y]);
            heap.Insert(c);
        }

        private void HandleNeighbour(GridVertex v, GridVertex n, Direction conDir, int conWeight, MinHeap<VertexTreeData> heap)
        {
            int cost = Cost(v.X, v.Y) + conWeight;
            if (data[n.X, n.Y] == null || cost < Cost(n.X, n.Y))
            {
                UpdateHeap(heap, n, conDir, cost);
                data[n.X, n.Y] = Tuple.Create(conDir, cost);
            }
        }
    }
}

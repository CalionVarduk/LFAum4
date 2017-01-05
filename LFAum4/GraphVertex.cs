using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace LFAum4
{
    public class GraphVertex
    {
        List<GraphEdge> edges;
        public List<GraphEdge> Edges { get { return edges; } }

        public int Degree
        {
            get
            {
                int count = edges.Count;
                int degree = 0;

                for (int i = 0; i < count; ++i)
                    if (edges[i] != null) ++degree;
                return degree;
            }
        }

        private Point location;
        public Point Location
        {
            get { return location; }
            set { location = value; }
        }

        public int X
        {
            get { return location.X; }
            set { location.X = value; }
        }

        public int Y
        {
            get { return location.Y; }
            set { location.Y = value; }
        }

        public GraphVertex()
            : this(0, 0)
        { }

        public GraphVertex(int x, int y)
        {
            edges = new List<GraphEdge>();
            Location = new Point(x, y);
        }

        public GraphVertex NeighbourAt(int iEdge)
        {
            return edges[iEdge].OtherOf(this);
        }
    }
}

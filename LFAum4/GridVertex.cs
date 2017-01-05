using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace LFAum4
{
    public sealed class GridVertex : GraphVertex
    {
        public GraphEdge LeftEdge
        {
            get { return Edges[(int)Direction.Left]; }
            set { Edges[(int)Direction.Left] = value; }
        }

        public GraphEdge RightEdge
        {
            get { return Edges[(int)Direction.Right]; }
            set { Edges[(int)Direction.Right] = value; }
        }

        public GraphEdge TopEdge
        {
            get { return Edges[(int)Direction.Top]; }
            set { Edges[(int)Direction.Top] = value; }
        }

        public GraphEdge BottomEdge
        {
            get { return Edges[(int)Direction.Bottom]; }
            set { Edges[(int)Direction.Bottom] = value; }
        }

        public bool HasLeft { get { return (LeftEdge != null); } }
        public bool HasRight { get { return (RightEdge != null); } }
        public bool HasTop { get { return (TopEdge != null); } }
        public bool HasBottom { get { return (BottomEdge != null); } }

        public GridVertex Left { get { return (GridVertex)NeighbourAt((int)Direction.Left); } }
        public GridVertex Right { get { return (GridVertex)NeighbourAt((int)Direction.Right); } }
        public GridVertex Top { get { return (GridVertex)NeighbourAt((int)Direction.Top); } }
        public GridVertex Bottom { get { return (GridVertex)NeighbourAt((int)Direction.Bottom); } }

        public GridVertex()
            : this(0, 0)
        { }

        public GridVertex(int x, int y)
            : base(x, y)
        {
            Edges.AddRange(new GraphEdge[4]);
        }
    }
}

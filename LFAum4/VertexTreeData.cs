using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LFAum4
{
    public sealed class VertexTreeData : IComparable<VertexTreeData>
    {
        public Tuple<Direction, int> Data { get { return Tuple.Create(ReachedFrom, Cost); } }
        public GridVertex Vertex { get; private set; }

        public Direction ReachedFrom { get; set; }
        public int Cost { get; set; }
        public int HeuristicValue { get; set; }
        public long Priority { get { return (long)Cost + HeuristicValue; } }

        public VertexTreeData(GridVertex v)
        {
            ReachedFrom = Direction.Left;
            Cost = int.MaxValue;
            HeuristicValue = int.MaxValue;
            Vertex = v;
        }

        public int CompareTo(VertexTreeData other)
        {
            if (Vertex.Location == other.Vertex.Location) return 0;
            return (Priority.CompareTo(other.Priority) < 0) ? -1 : 1;
        }
    }
}

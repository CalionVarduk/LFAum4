using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LFAum4
{
    public class GraphEdge
    {
        public GraphVertex V1 { get; private set; }
        public GraphVertex V2 { get; private set; }

        public bool IsLinking { get { return (V1 != null && V2 != null); } }

        private int weight;
        public int Weight
        {
            get { return weight; }
            set
            {
                if (value < 0)
                    throw new ArgumentException("Weight must be greater than or equal to 0.");
                weight = value;
            }
        }

        public GraphEdge()
            : this(0)
        { }

        public GraphEdge(int weight)
        {
            Weight = weight;
        }

        public GraphVertex OtherOf(GraphVertex v)
        {
            return (v == V1) ? V2 : V1;
        }

        public void LinkVertices(GraphVertex v1, GraphVertex v2)
        {
            if (v1 == null || v2 == null)
                throw new ArgumentNullException();

            V1 = v1;
            V2 = v2;
        }

        public void UnlinkVertices()
        {
            V1 = null;
            V2 = null;
        }
    }
}

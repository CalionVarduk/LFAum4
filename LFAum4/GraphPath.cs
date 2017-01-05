using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LFAum4
{
    public sealed class GraphPath
    {
        private List<GraphVertex> vertices;
        private List<GraphEdge> edges;

        public int EdgeCount { get { return edges.Count; } }
        public int VertexCount { get { return vertices.Count; } }

        public bool IsCycle { get { return (edges.Count > 0 && vertices[0] == vertices[edges.Count]); } }

        public int Cost
        {
            get
            {
                int count = edges.Count;
                if (count == 0) return -1;

                int weight = 0;
                for (int i = 0; i < count; ++i)
                    weight += edges[i].Weight;

                return weight;
            }
        }

        public GraphPath()
        {
            vertices = new List<GraphVertex>();
            edges = new List<GraphEdge>();
        }

        public GraphEdge Edge(int i)
        {
            return edges[i];
        }

        public GraphVertex Vertex(int i)
        {
            return vertices[i];
        }

        public void Reverse()
        {
            vertices.Reverse();
            edges.Reverse();
        }

        public void Start(GraphVertex v)
        {
            vertices.Clear();
            edges.Clear();
            vertices.Add(v);
        }

        public GraphVertex Next(int iEdge)
        {
            GraphVertex last = vertices[edges.Count];
            if (iEdge >= last.Edges.Count)
                throw new IndexOutOfRangeException("The last vertex in the path must have at least iEdge + 1 incident edges.");

            GraphEdge edge = last.Edges[iEdge];
            if (edge == null)
                throw new ArgumentNullException("The chosen edge is null and therefore doesn't lead to another vertex.");

            edges.Add(edge);
            vertices.Add(last.NeighbourAt(iEdge));
            return vertices[edges.Count];
        }
    }
}

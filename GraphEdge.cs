using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace yens_algorithm
{
    public class GraphEdge
    {
        public int weight;
        public GraphVertex vertex1;
        public GraphVertex vertex2;

        public GraphEdge(GraphVertex vertex1, GraphVertex vertex2, int weight)
        {
            this.weight = weight;
            this.vertex1 = vertex1;
            this.vertex2 = vertex2;
        }

        public GraphEdge()
        {
            this.weight = 0;
            this.vertex1 = null;
            this.vertex2 = null;
        }

        static public void CopyTo(GraphEdge first, GraphEdge second)
        {
            second.vertex1 = first.vertex1;
            second.vertex2 = first.vertex2;
            second.weight = first.weight;
        }
    }
}


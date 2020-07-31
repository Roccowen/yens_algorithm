using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yen_sAlgorithm
{
    public class GraphEdge
    {
        public int weight;
        public GraphVertex vertex1;
        public GraphVertex vertex2;
        /// <summary>
        /// Graph edge with args
        /// </summary>
        /// <param name="vertex1">First vertex</param>
        /// <param name="vertex2">Second vertex</param>
        /// <param name="weight">Weight</param>
        public GraphEdge(GraphVertex vertex1, GraphVertex vertex2, int weight)
        {
            this.weight = weight;
            this.vertex1 = vertex1;
            this.vertex2 = vertex2;
        }
        /// <summary>
        /// Empty graph edge
        /// </summary>
        public GraphEdge()
        {
            this.weight = 0;
            this.vertex1 = null;
            this.vertex2 = null;
        }
        /// <summary>
        /// Сopy the values ​​of the first edge to the second
        /// </summary>
        /// <param name="first">Copied GraphEdge</param>
        /// <param name="second">Changed GraphEdge</param>
        static public void CopyTo(GraphEdge first, GraphEdge second)
        {
            second.vertex1 = first.vertex1;
            second.vertex2 = first.vertex2;
            second.weight = first.weight;
        }
    }
}


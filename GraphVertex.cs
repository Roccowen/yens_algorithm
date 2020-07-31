using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yen_sAlgorithm
{
    public class GraphVertex
    {
        public bool visit;
        public int distanceToVertex;
        public string name;
        public List<GraphEdge> incidiencyGraphEdges;
        public List<GraphVertex> path;
        /// <summary>
        /// Vertex
        /// </summary>
        /// <param name="name">Vertex name</param>
        public GraphVertex(string name)
        {
            visit = false;
            distanceToVertex = int.MaxValue;
            this.name = name;
            incidiencyGraphEdges = new List<GraphEdge>();
            path = new List<GraphVertex>();
        }
    }
}

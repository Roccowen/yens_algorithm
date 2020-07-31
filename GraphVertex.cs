using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace yens_algorithm
{
    public class GraphVertex
    {
        public bool visit;
        public int distanceToVertex;
        public string name;
        public List<GraphEdge> incidiencyGraphEdges;
        public List<GraphVertex> path;

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

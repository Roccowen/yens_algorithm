using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using yens_algorithm.GraphLibrary;

namespace yens_algorithm
{
    class VertexInfo
    {
        public Vertex Vertex { get; }
        public bool IsVisited;
        public int DistanceToVertex;
        public List<Vertex> Path;

        public VertexInfo(Vertex v, bool isVis = false, int dist = 0)
        {
            Vertex = v;
            Path = new List<Vertex>();
        }
    }
}

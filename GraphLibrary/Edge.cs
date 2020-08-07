using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace yens_algorithm.GraphLibrary
{
    public class Edge
    {
        public int Weight { get; }
        public Vertex Vertex1 { get; }
        public Vertex Vertex2 { get; }

        public Edge(Vertex v1 = null, Vertex v2 = null, int w = 0)
        {           
            Vertex1 = v1;
            Vertex2 = v2;
            Weight = w;
        }
        public Edge Clone() => new Edge(Vertex1, Vertex2, Weight);
    }
}


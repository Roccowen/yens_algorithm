using System.Collections.Generic;
using yens_algorithm.GraphD;

namespace yens_algorithm
{
    class VertexInfo
    {
        public Vertex Vertex { get; }
        public bool IsVisited;
        public int DistanceToVertex;
        public List<Vertex> Path;

        public VertexInfo(Vertex v, bool isVis = false, int dist = int.MaxValue, List<Vertex> path = null)
        {
            Vertex = v;
            IsVisited = false;
            DistanceToVertex = dist;
            Path = path is null
                ? new List<Vertex>() 
                : path;
        }
    }
}

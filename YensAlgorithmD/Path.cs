using System.Collections.Generic;
using yens_algorithm.GraphD;

namespace yens_algorithm.YensAlgorithmD
{
    public class Path
    {
        public List<Vertex> Route;
        public int Weight;
        public (Vertex vertex1, Vertex vertex2, int weight) DeletedEdge;

        public Path(List<Vertex> route, int weight, (Vertex vertex1, Vertex vertex2, int weight) deletedEdge)
        {
            this.Route = route;
            this.Weight = weight;
            this.DeletedEdge = deletedEdge;
        }

        public Path()
        {
            this.Route = new List<Vertex>();
            this.Weight = 0;
        }
    }
}

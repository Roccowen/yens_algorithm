using System.Collections.Generic;

namespace yens_algorithm.GraphD
{
    public class Path
    {
        public List<Vertex> Route;
        public int Weight;
        public Edge DeletedEdge;

        public Path(List<Vertex> route, int weight, Edge deletedEdge)
        {
            this.Route = route;
            this.Weight = weight;
            this.DeletedEdge = deletedEdge;
        }

        public Path(int w = 0)
        {
            this.Route = new List<Vertex>();
            this.Weight = w;
            this.DeletedEdge = new Edge();
        }
    }
}

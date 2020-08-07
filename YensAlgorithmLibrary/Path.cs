using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace yens_algorithm.GraphLibrary
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

        public Path()
        {
            this.Route = new List<Vertex>();
            this.Weight = 0;
            this.DeletedEdge = new Edge();
        }
    }
}

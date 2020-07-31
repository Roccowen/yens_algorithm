using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace yens_algorithm
{
    public class Path
    {
        public List<GraphVertex> route;
        public int weight;
        public GraphEdge deletedEdge;

        public Path(List<GraphVertex> route, int weight, GraphEdge deletedEdge)
        {
            this.route = route;
            this.weight = weight;
            this.deletedEdge = deletedEdge;
        }

        public Path()
        {
            this.route = new List<GraphVertex>();
            this.weight = 0;
            this.deletedEdge = new GraphEdge();
        }
    }
}

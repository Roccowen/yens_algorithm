using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yen_sAlgorithm
{
    public class Path
    {
        public List<GraphVertex> route;
        public int weight;
        public GraphEdge deletedEdge;

        /// <summary>
        /// Path with args
        /// </summary>
        /// <param name="route">Path</param>
        /// <param name="weight">Weight</param>
        /// <param name="deletedEdge">Deleted vertex</param>
        public Path(List<GraphVertex> route, int weight, GraphEdge deletedEdge)
        {
            this.route = route;
            this.weight = weight;
            this.deletedEdge = deletedEdge;
        }
        /// <summary>
        /// Empty path
        /// </summary>
        public Path()
        {
            this.route = new List<GraphVertex>();
            this.weight = 0;
            this.deletedEdge = new GraphEdge();
        }
    }
}

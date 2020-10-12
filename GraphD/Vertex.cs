using System.Collections.Generic;

namespace yens_algorithm.GraphD
{
    public class Vertex
    {
        public string Name { get; set; }
        public List<Edge> IncidiencyGraphEdges { get; set; }

        public Vertex(string n)
        {
            Name = n;
            IncidiencyGraphEdges = new List<Edge>();
        }
    }
}

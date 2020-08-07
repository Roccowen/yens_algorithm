using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace yens_algorithm.GraphLibrary
{
    public class Vertex
    {
        public string Name { get; }
        public List<Edge> IncidiencyGraphEdges { get; }
        
        public Vertex(string n)
        {
            Name = n;
            IncidiencyGraphEdges = new List<Edge>();
        }
    }
}

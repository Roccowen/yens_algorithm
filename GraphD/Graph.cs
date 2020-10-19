using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace yens_algorithm.GraphD
{
    public class Graph
    {       
        public Dictionary<string, Vertex> Vertices { get; }
        public Dictionary<Vertex, Dictionary<Vertex, int>> Struct { get; }
        bool Bidirectional;

        public Graph(bool bidirectional)
        {
            Bidirectional = bidirectional;
            Vertices = new Dictionary<string, Vertex>();
            Struct = new Dictionary<Vertex, Dictionary<Vertex, int>>();
        }
        public void AddVertex(string vertexName) 
        {
            if (!Vertices.ContainsKey(vertexName))
            {
                Vertices[vertexName] = new Vertex(vertexName);
                Struct[Vertices[vertexName]] = new Dictionary<Vertex, int>();
            }              
        } 
        public Vertex GetVertex(string vertexName) => Vertices[vertexName];
        public void AddEdge((Vertex vertex1, Vertex vertex2, int weight) v) => AddEdge(v.vertex1, v.vertex2, v.weight);
        public void AddEdge(string vertex1, string vertex2, string weight) => AddEdge(Vertices[vertex1], Vertices[vertex2], Convert.ToInt32(weight));
        public void AddEdge(Vertex vertex1, Vertex vertex2, int weight)
        {
            Struct[vertex1].Add(vertex2, weight);
            if (Bidirectional)
                Struct[vertex2].Add(vertex1, weight);
        }       
        public (Vertex vertex1, Vertex vertex2, int weight) RemoveEdge(Vertex v1, Vertex v2)
        {
            var deletedEdgeWeight = Struct[v1][v2];
            Struct[v1].Remove((v2));
            if (Bidirectional)
                Struct[v2].Remove((v1));
            return (v1, v2, deletedEdgeWeight);
        }   
    }
}

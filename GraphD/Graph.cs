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
        public Dictionary<(Vertex vertex1, Vertex vertex2), Edge> Edges;
        public Dictionary<string, Vertex> Vertices;

        public Graph(int vertexCount)
        {
            Vertices = new Dictionary<string, Vertex>();
            Edges = new Dictionary<(Vertex vertex1, Vertex vertex2), Edge>();

            CreateVertices(vertexCount);
        }       
        public void AddVertex(string vertexName) => Vertices.Add(vertexName, new Vertex(vertexName));
        public Vertex GetVertex(string vertexName) => Vertices[vertexName];
        public void AddEdge(Vertex vertex1, Vertex vertex2, int weight) => Edges.Add((vertex1, vertex2), new Edge(vertex1, vertex2, weight));
        public Edge RemoveEdge(Vertex v1, Vertex v2)
        {
            var deletedEdge = Edges[(v1, v2)];
            Edges.Remove((v1, v2));
            return deletedEdge;
        }
        public Edge GetEdge(Vertex v1, Vertex v2) => Edges[(v1, v2)];
        void CreateVertices(int cnt)
        {
            for (int i = 0; i < cnt; i++)
                this.AddVertex(i.ToString());
        }      
    }
}

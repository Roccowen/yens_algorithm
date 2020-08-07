using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using yens_algorithm.GraphLibrary;

namespace yens_algorithm
{
    class DataReader
    {
        Graph Graph;
        public DataReader(Graph g)
        {
            Graph = g;
        }
        static void GraphFilling(string path, Graph g)
        {
            StreamReader sr = new StreamReader(path);
            string[] read = new string[3];
            string line = sr.ReadLine();
            while ((line = sr.ReadLine()) != null)
            {
                read = line.Split();
                g.AddEdge(g.GetVertex(read[0]), g.GetVertex(read[1]), Convert.ToInt32(read[2]));
                g.AddEdge(g.GetVertex(read[1]), g.GetVertex(read[0]), Convert.ToInt32(read[2]));
            }
            g.FillingIncidiencyGraphEdges();
        }
        public void FillingIncidiencyGraphEdges()
        {
            foreach (var edge in Graph.Edges)
                edge.Vertex1.IncidiencyGraphEdges.Add(edge);
        }
        static public int GetDataVertexCount(string pathToData) => Convert.ToInt32(new StreamReader(pathToData).ReadLine().Split()[0]);
    }
}

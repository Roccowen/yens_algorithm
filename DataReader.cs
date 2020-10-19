using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using yens_algorithm.GraphD;

namespace yens_algorithm
{
    static class DataReader
    {
        public static Graph ReadGraph(string path, bool _bidirectional)
        {
            var graph = new Graph(_bidirectional);
            var sr = new StreamReader(path);
            string[] read = new string[3];
            string line = sr.ReadLine();
            while ((line = sr.ReadLine()) != null)
            {
                read = line.Split();
                graph.AddVertex(read[0]);
                graph.AddVertex(read[1]);
                
                graph.AddEdge(read[0], read[1], read[2]);
            }
            return graph;
        }
        public static int GetDataVertexCount(string pathToData) => Convert.ToInt32(new StreamReader(pathToData).ReadLine().Split()[0]);
    }
}

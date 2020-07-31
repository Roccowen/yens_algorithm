using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yen_sAlgorithm
{
    class Program
    {
        static int VertexCount(string path)
        {
            StreamReader sR = new StreamReader(path);

            string line = sR.ReadLine();
            string[] read = line.Split();
            sR.Close();
            int vertexCount = Convert.ToInt32(read[0]);
            return vertexCount;
        }
        static void Main(string[] args)
        {
            System.Diagnostics.Stopwatch myStopwatch = new System.Diagnostics.Stopwatch();
            myStopwatch.Start();

            string path = "data/data3.txt"; //data3.txt

            var g = new Graph();

            for (int i = 0; i < VertexCount(path); i++)
            {
                string name = i.ToString();
                g.AddVertex(name);
            }

            int vertexCount = VertexCount(path);

            StreamReader sr = new StreamReader(path);
            string[] read = new string[3];
            string line;
            sr.ReadLine();
            while ((line = sr.ReadLine()) != null)
            {
                read = line.Split();
                g.AddEdge(g.FindVertex(read[0]), g.FindVertex(read[1]), Convert.ToInt32(read[2]));
                g.AddEdge(g.FindVertex(read[1]), g.FindVertex(read[0]), Convert.ToInt32(read[2]));
            }
            g.FillingIncidiencyGraphEdges();

            Console.WriteLine(" Vertex count - {0}", VertexCount(path));
            Console.Write(" Start vertex: ");
            string start = Console.ReadLine();
            Console.Write(" Final vertex: ");
            string end = Console.ReadLine();
            Console.Write(" Path count: ");
            int count = Convert.ToInt32(Console.ReadLine());

            string pathInput = "";

            int costDijekstra = 0;
            List<GraphVertex> DijekstraPath = new List<GraphVertex>();
            var startVertex = g.FindVertex(start);
            var endVertex = g.FindVertex(end);

            g.Dijkstra(startVertex);

            costDijekstra = endVertex.distanceToVertex;
            DijekstraPath.AddRange(endVertex.path);

            //shortest path
            foreach (var item in DijekstraPath)
            {
                pathInput += item.name + " ";
            }

            Console.WriteLine(" Shortest path: {0}", pathInput);
            Console.WriteLine(" Weight: {0}", costDijekstra);

            //Yen's alg
            Console.WriteLine("\n===========================================================");
            List<Path> temp1 = new List<Path>();
            temp1.AddRange(g.Yen(startVertex, endVertex, count));

            foreach (var item in temp1)
            {
                int ves = 0;
                string pyt = " ";

                foreach (var jtem in item.route)
                {
                    pyt += jtem.name + " ";
                }

                ves = item.weight;
                Console.WriteLine(" Path:{0}      Path weight: {1}.    Remote edge: {2} - > {3}.",
                    pyt, ves, item.deletedEdge.vertex1.name, item.deletedEdge.vertex2.name);
            }

            myStopwatch.Stop();
            int time = Convert.ToInt32(myStopwatch.Elapsed.TotalMilliseconds);
            Console.WriteLine("\n Lead time: {0} ms", time);
        }
    }
}

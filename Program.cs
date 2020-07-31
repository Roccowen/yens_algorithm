using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace yens_algorithm
{
    class Program
    {
        static string path;
        static string GetDataName()
        {
            var msg = " Input data name: ";
            tryAgain:
            Console.Write(msg);
            var dataName = "data/" + Console.ReadLine() + ".txt";
            try
            {
                new StreamReader(dataName).ReadLine();
            }
            catch
            {
                msg = " Input exist data name: ";
                goto tryAgain;               
            }
            return dataName;

        }
        static (string start, string end, int count) GetInfo()
        {
            Console.WriteLine(" Vertex count - {0}", Graph.GetVertexCount(path)); //написать методы
            Console.Write(" Start vertex: ");
            string start = Console.ReadLine();
            Console.Write(" Final vertex: ");
            string end = Console.ReadLine();
            Console.Write(" Path count: ");
            int count = Convert.ToInt32(Console.ReadLine());
            return (start, end, count);
        }

        static void GraphFilling(string path, Graph g)
        {
            StreamReader sr = new StreamReader(path);
            string[] read = new string[3];
            string line = sr.ReadLine();
            while ((line = sr.ReadLine()) != null)
            {
                read = line.Split();
                g.AddEdge(g.FindVertex(read[0]), g.FindVertex(read[1]), Convert.ToInt32(read[2]));
                g.AddEdge(g.FindVertex(read[1]), g.FindVertex(read[0]), Convert.ToInt32(read[2]));
            }
            g.FillingIncidiencyGraphEdges();
        }
        static void Main(string[] args)
        {
            System.Diagnostics.Stopwatch myStopwatch = new System.Diagnostics.Stopwatch();
            myStopwatch.Start();
            path = GetDataName();
            var info = GetInfo();
            var g = new Graph(path);      
            GraphFilling(path, g);
            string pathInput = "";
            int costDijekstra = 0;
            List<GraphVertex> DijekstraPath = new List<GraphVertex>();
            var startVertex = g.FindVertex(info.start);
            var endVertex = g.FindVertex(info.end);
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
            temp1.AddRange(g.Yen(startVertex, endVertex, info.count));

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

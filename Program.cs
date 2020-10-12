using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using yens_algorithm.GraphD;

namespace yens_algorithm
{
    class Program
    {
        static string Path;
        static string GetDataName()
        {
            var msg = " Input data name: ";
            tryAgain:
            Console.Write(msg);
            var dataName = "data_all/" + Console.ReadLine() + ".txt";
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
            Console.WriteLine(" Vertex count - {0}", DataReader.GetDataVertexCount(Path));
            Console.Write(" Start vertex: ");
            string start = Console.ReadLine();
            Console.Write(" Final vertex: ");
            string end = Console.ReadLine();
            Console.Write(" Path count: ");
            int count = Convert.ToInt32(Console.ReadLine());
            return (start, end, count);
        }

        static void Main(string[] args)
        {
            System.Diagnostics.Stopwatch myStopwatch = new System.Diagnostics.Stopwatch();
            
            Path = GetDataName();
            var info = GetInfo();
            myStopwatch.Start();
            var g = new Graph(Path);      
            GraphFilling(Path, g);
            string pathInput = "";
            int costDijekstra = 0;
            List<Vertex> DijekstraPath = new List<Vertex>();
            var startVertex = g.GetVertex(info.start);
            var endVertex = g.GetVertex(info.end);
            g.Dijkstra(startVertex);

            costDijekstra = endVertex.DistanceToVertex;
            DijekstraPath.AddRange(endVertex.Path);

            foreach (var item in DijekstraPath)
            {
                pathInput += item.Name + " ";
            }

            Console.WriteLine(" Shortest path: {0}", pathInput);
            Console.WriteLine(" Weight: {0}", costDijekstra);

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

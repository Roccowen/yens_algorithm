using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using yens_algorithm.GraphD;

namespace yens_algorithm.YensAlgorithmLibrary
{
    class YensAlgorithm
    {
        Graph YenGraph;
        Dictionary<Vertex, VertexInfo> VerteciesInfo;
        
        public YensAlgorithm(Graph g)
        {
            YenGraph = g;
            foreach (Vertex vertex in g.Vertices.Values)
                VerteciesInfo.Add(vertex, new VertexInfo(vertex));
        }

        public Vertex FindUnvisitedMin() //O(n) 
        {
            Vertex nearestVertex = null;
            int minDistance = int.MaxValue;
            foreach (VertexInfo vertexInfo in VerteciesInfo.Values)
            {
                if (vertexInfo.IsVisited == false)
                {
                    if (minDistance > vertexInfo.DistanceToVertex)
                    {
                        minDistance = vertexInfo.DistanceToVertex;
                        nearestVertex = vertexInfo.Vertex;
                    }
                }
            }
            return nearestVertex;
        }
        public Edge FindEdge(Vertex vertex1, Vertex vertex2) => YenGraph.GetEdge(vertex1, vertex2);

        public void FillingDistanceAndPathInNears(Vertex graphVertex)
        {
            int pathWeight = 0;
            List<Vertex> verticesPath = new List<Vertex>();
            verticesPath.AddRange(VerteciesInfo[graphVertex].Path);

            foreach (var item in graphVertex.IncidiencyGraphEdges)
            {
                var nearVertex = item.Vertex2;
                pathWeight = VerteciesInfo[graphVertex].DistanceToVertex + item.Weight;

                if (pathWeight < VerteciesInfo[nearVertex].DistanceToVertex)
                {                    
                    VerteciesInfo[nearVertex].Path.Clear();
                    VerteciesInfo[nearVertex].Path.AddRange(verticesPath);
                    VerteciesInfo[nearVertex].Path.Add(nearVertex);
                    VerteciesInfo[nearVertex] = new VertexInfo(nearVertex, false, pathWeight, VerteciesInfo[nearVertex].Path);
                }
            }
        }
        public void ReturnEdgeInGraph(Edge deadEdge)
        {
            Vertex vertexR1 = deadEdge.Vertex1;
            Vertex vertexR2 = deadEdge.Vertex2;
            int weightR = deadEdge.Weight;

            Edge returnEdge = new Edge(vertexR1, vertexR2, weightR);

            YenGraph.AddEdge(vertexR1, vertexR2, weightR);

            vertexR1.IncidiencyGraphEdges.Add(returnEdge);

        }
        public void Dijkstra(Vertex start)
        {
            var current = start;
            VerteciesInfo[current].Path.Add(current);
            VerteciesInfo[current] = new VertexInfo(current, false, 0, VerteciesInfo[current].Path);           

            while (current != null)
            {
                g.FillingDistanceAndPathInNears(current);
                VerteciesInfo[current].IsVisited = true;
                current = FindUnvisitedMin();
            }
        }
        public List<Path> YenAlg(Vertex start, Vertex end)
        {
            List<Path> Gen = new List<Path>();
            List<Vertex> shortestPathInGen = new List<Vertex>();
            shortestPathInGen.AddRange(VerteciesInfo[end].Path);


            for (int i = 0; i < shortestPathInGen.Count - 1; i++)
            {
                Edge deadEdge = new Edge(YenGraph.RemoveEdge(shortestPathInGen[i], shortestPathInGen[i + 1]).Vertex1,
                    YenGraph.RemoveEdge(shortestPathInGen[i], shortestPathInGen[i + 1]).Vertex2);

                foreach (var item in YenGraph.Vertices)
                {
                    VerteciesInfo[item.Value].IsVisited = false;
                    VerteciesInfo[item.Value].DistanceToVertex = int.MaxValue;
                    VerteciesInfo[item.Value].Path.Clear();
                }

                Dijkstra(start);

                Path temp = new Path();

                temp.Route.AddRange(VerteciesInfo[end].Path);
                temp.Weight = VerteciesInfo[end].DistanceToVertex;

                temp.DeletedEdge.Vertex1 = deadEdge.Vertex1;
                temp.DeletedEdge.Vertex2 = deadEdge.Vertex2;
                temp.DeletedEdge.Weight = deadEdge.Weight;

                Gen.Add(temp);

                ReturnEdgeInGraph(deadEdge);
                deadEdge = null;
            }
            return Gen;
        }
        public List<Path> Yen(Vertex start, Vertex end, int count)
        {

            List<Path> Generation = new List<Path>();
            List<Path> Best = new List<Path>();
            List<Edge> deadList = new List<Edge>();

            for (int i = 0; i < count; i++)
            {
                Generation.AddRange(YenAlg(start, end));

                int minLength = int.MaxValue;
                Path bestItem = new Path();

                foreach (var item in Generation)
                {

                    if (item.Weight < minLength)
                    {
                        minLength = item.Weight;

                        bestItem.Route.Clear();
                        bestItem.Route.AddRange(item.Route);
                        bestItem.Weight = item.Weight;

                        bestItem.DeletedEdge.Vertex1 = item.DeletedEdge.Vertex1;
                        bestItem.DeletedEdge.Vertex2 = item.DeletedEdge.Vertex2;
                    }
                }
                if (minLength == int.MaxValue)
                {
                    Console.WriteLine(" There are only {0} paths to the final vertex: \n", Best.Count);
                    break;
                }

                Generation.Clear();

                YenGraph.RemoveEdge(bestItem.DeletedEdge.Vertex1, bestItem.DeletedEdge.Vertex2);

                VerteciesInfo[end].Path.Clear();
                VerteciesInfo[end].Path.AddRange(bestItem.Route);

                Best.Add(bestItem);
            }
            return Best;
        }
    }
}

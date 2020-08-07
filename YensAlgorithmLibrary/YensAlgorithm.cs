using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using yens_algorithm.GraphLibrary;

namespace yens_algorithm.YensAlgorithmLibrary
{
    class YensAlgorithm
    {
        Graph Graph;
        Dictionary<Vertex, VertexInfo> VerteciesInfo;
        
        public YensAlgorithm(Graph g)
        {
            Graph = g;
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
        public Edge FindEdge(Vertex vertex1, Vertex vertex2) => Graph.GetEdge(vertex1, vertex2);

        public void FillingDistanceAndPathInNears(Vertex graphVertex)
        {
            int pathWeight = 0;
            List<Vertex> verticesPath = new List<Vertex>();
            verticesPath.AddRange(graphVertex.Path);

            foreach (var item in graphVertex.IncidiencyGraphEdges)
            {
                var nearVertex = item.Vertex2;
                pathWeight = graphVertex.DistanceToVertex + item.Weight;

                if (pathWeight < nearVertex.DistanceToVertex)
                {
                    nearVertex.DistanceToVertex = pathWeight;
                    nearVertex.Path.Clear();
                    nearVertex.Path.AddRange(verticesPath);
                    nearVertex.Path.Add(nearVertex);
                }
            }
        }
        public void ReturnEdgeInGraph(Edge deadEdge)
        {
            Vertex vertexR1 = deadEdge.Vertex1;
            Vertex vertexR2 = deadEdge.Vertex2;
            int weightR = deadEdge.Weight;

            Edge returnEdge = new Edge(vertexR1, vertexR2, weightR);

            AddEdge(vertexR1, vertexR2, weightR);

            vertexR1.IncidiencyGraphEdges.Add(returnEdge);

        }
        public void Dijkstra(Vertex start)
        {
            var current = start;
            current.Path.Add(current);
            current.DistanceToVertex = 0;

            while (current != null)
            {
                g.FillingDistanceAndPathInNears(current);
                current.IsVisited = true;
                current = FindUnvisitedMin();
            }
        }
        public List<Path> YenAlg(Vertex start, Vertex end)
        {
            List<Path> Gen = new List<Path>();
            List<Vertex> shortestPathInGen = new List<Vertex>();
            shortestPathInGen.AddRange(end.Path);


            for (int i = 0; i < shortestPathInGen.Count - 1; i++)
            {
                Edge deadEdge = Graph.RemoveEdge(shortestPathInGen[i], shortestPathInGen[i + 1]).Clone();

                foreach (var item in Graph.Vertices)
                {
                    item.Value.visit = false;
                    item.Value.distanceToVertex = int.MaxValue;
                    item.Value.path.Clear();
                }

                Dijkstra(start);

                Path temp = new Path();

                temp.Route.AddRange(end.Path);
                temp.Weight = end.DistanceToVertex;

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

                RemoveEdgeFromGraph(bestItem.DeletedEdge.Vertex1, bestItem.DeletedEdge.Vertex2);

                end.Path.Clear();
                end.Path.AddRange(bestItem.Route);

                Best.Add(bestItem);
            }
            return Best;
        }
    }
}

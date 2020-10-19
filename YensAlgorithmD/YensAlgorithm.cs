using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using yens_algorithm.GraphD;

namespace yens_algorithm.YensAlgorithmD
{
    class YensAlgorithm
    {
        Graph YenGraph;
        public Dictionary<Vertex, VertexInfo> _VerteciesInfo;

        public YensAlgorithm(Graph g)
        {
            YenGraph = g;
            _VerteciesInfo = new Dictionary<Vertex, VertexInfo>();
            foreach (var vertex in g.Vertices.Values)
                _VerteciesInfo.Add(vertex, new VertexInfo(vertex));
        }

        public Vertex FindUnvisitedMin()
        {
            Vertex nearestVertex = null;
            int minDistance = int.MaxValue;
            foreach (VertexInfo vertexInfo in _VerteciesInfo.Values)
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

        public void FillingDistanceAndPathInNears(Vertex graphVertex)
        {
            int pathWeight = 0;
            List<Vertex> verticesPath = new List<Vertex>();
            verticesPath.AddRange(_VerteciesInfo[graphVertex].Path);

            foreach (var vertex in YenGraph.Struct[graphVertex])
            {
                var nearVertex = vertex.Key;
                pathWeight = _VerteciesInfo[graphVertex].DistanceToVertex + vertex.Value;

                if (pathWeight < _VerteciesInfo[nearVertex].DistanceToVertex)
                {
                    _VerteciesInfo[nearVertex].Path.Clear();
                    _VerteciesInfo[nearVertex].Path.AddRange(verticesPath);
                    _VerteciesInfo[nearVertex].Path.Add(nearVertex);
                    _VerteciesInfo[nearVertex].DistanceToVertex = pathWeight;
                }
            }
        }
        public void Dijkstra(string vertex) => Dijkstra(YenGraph.GetVertex(vertex));
        public void Dijkstra(Vertex start)
        {
            var current = start;
            _VerteciesInfo[current].Path.Add(current);
            _VerteciesInfo[current].DistanceToVertex = 0;

            while (current != null)
            {
                FillingDistanceAndPathInNears(current);
                _VerteciesInfo[current].IsVisited = true;
                current = FindUnvisitedMin();
            }
        }
        public List<Path> YenAlg(Vertex start, Vertex end)
        {
            List<Path> Gen = new List<Path>();
            List<Vertex> shortestPathInGen = new List<Vertex>();
            shortestPathInGen.AddRange(_VerteciesInfo[end].Path);


            for (int i = 0; i < shortestPathInGen.Count - 1; i++)
            {
                var deadEdge = YenGraph.RemoveEdge(shortestPathInGen[i], shortestPathInGen[i + 1]);

                foreach (var item in YenGraph.Vertices)
                {
                    _VerteciesInfo[item.Value].IsVisited = false;
                    _VerteciesInfo[item.Value].DistanceToVertex = int.MaxValue;
                    _VerteciesInfo[item.Value].Path.Clear();
                }

                Dijkstra(start);

                Path temp = new Path();

                temp.Route.AddRange(_VerteciesInfo[end].Path);
                temp.Weight = _VerteciesInfo[end].DistanceToVertex;

                temp.DeletedEdge.vertex1 = deadEdge.vertex1;
                temp.DeletedEdge.vertex2 = deadEdge.vertex2;
                temp.DeletedEdge.weight = deadEdge.weight;

                Gen.Add(temp);

                YenGraph.AddEdge(deadEdge);
            }
            return Gen;
        }
        public List<Path> Yen(Vertex start, Vertex end, int count)
        {

            List<Path> Generation = new List<Path>();
            List<Path> Best = new List<Path>();
            List<(Vertex vertex1, Vertex vertex2, int weight)> deadList
                = new List<(Vertex vertex1, Vertex vertex2, int weight)>();

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

                        bestItem.DeletedEdge.vertex1 = item.DeletedEdge.vertex1;
                        bestItem.DeletedEdge.vertex2 = item.DeletedEdge.vertex2;
                    }
                }
                if (minLength == int.MaxValue)
                {
                    Console.WriteLine(" There are only {0} paths to the final vertex: \n", Best.Count);
                    break;
                }

                Generation.Clear();

                YenGraph.RemoveEdge(bestItem.DeletedEdge.vertex1, bestItem.DeletedEdge.vertex2);

                _VerteciesInfo[end].Path.Clear();
                _VerteciesInfo[end].Path.AddRange(bestItem.Route);

                Best.Add(bestItem);
            }
            return Best;
        }
    }
}

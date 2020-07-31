using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yen_sAlgorithm
{
    public class Graph
    {
        public static List<GraphVertex> Vertices;
        public static List<GraphEdge> Edges;
        /// <summary>
        /// Graph
        /// </summary>
        public Graph()
        {
            Vertices = new List<GraphVertex>();
            Edges = new List<GraphEdge>();
        }
        /// <summary>
        /// Add vertex to vertices list
        /// </summary>
        /// <param name="vertexName">vertex name</param>
        public void AddVertex(string vertexName)
        {
            Vertices.Add(new GraphVertex(vertexName));
        }
        /// <summary>
        /// Vertex search
        /// </summary>
        /// <param name="vertexName">Name</param>
        /// <returns>Vertex instance</returns>
        public GraphVertex FindVertex(string vertexName)
        {
            foreach (var v in Vertices)
            {
                if (vertexName == v.name)
                {
                    return v;
                }
            }
            return null;
        }
        /// <summary>
        /// Add edge to edges list
        /// </summary>
        /// <param name="vertex1">Начальная вершина</param>
        /// <param name="vertex2">Конечная вершина</param>
        /// <param name="weight">Вес</param>
        public void AddEdge(GraphVertex vertex1, GraphVertex vertex2, int weight)
        {
            Edges.Add(new GraphEdge(vertex1, vertex2, weight));
        }
        /// <summary>
        /// Dijekstra algorithm
        /// </summary>
        /// <param name="start">Начальная вершина</param>       
        public void Dijkstra(GraphVertex start)
        {
            var current = start;
            current.path.Add(current);
            current.distanceToVertex = 0;

            while (true)
            {
                FillingDistanceAndPathInNears(current);
                current.visit = true;
                current = FindUnvisitedMin();
                if (current == null)
                {
                    break;
                }
            }
        }
        /// <summary>
        /// Filling list with incident edges at vertices in a graph
        /// </summary>
        public void FillingIncidiencyGraphEdges()
        {
            foreach (var edge in Edges)
            {
                edge.vertex1.incidiencyGraphEdges.Add(edge);
            }
        }
        /// <summary>
        /// Search for unvisited vertex with minimal weight
        /// </summary>
        /// <returns>Vertex</returns>
        public GraphVertex FindUnvisitedMin()
        {
            int visitFalseCount = 0;
            string vertexMinName = "";
            int minValue = int.MaxValue;
            foreach (var i in Graph.Vertices)
            {
                if (i.visit == false)
                {
                    if (minValue > i.distanceToVertex)
                    {
                        minValue = i.distanceToVertex;
                        vertexMinName = i.name;
                    }
                    ++visitFalseCount;
                }
            }
            if (visitFalseCount == 0)
            {
                return null;
            }
            else return FindVertex(vertexMinName);
        }
        /// <summary>
        /// Finding an edge between two vertices
        /// </summary>
        /// <param name="vertex1">Первая вершина</param>
        /// <param name="vertex2">Вторая вершина</param>
        /// <returns>Edge between then</returns>
        public GraphEdge FindIncidientGraphsEdge(GraphVertex vertex1, GraphVertex vertex2)
        {
            foreach (var item in vertex1.incidiencyGraphEdges)
            {
                if (item.vertex2.name == vertex2.name)
                {
                    return item;
                }
            }
            return null;
        }
        /// <summary>
        /// Filling the distance and path to the nearest peaks
        /// </summary>
        /// <param name="graphVertex1">Vertex</param>
        public void FillingDistanceAndPathInNears(GraphVertex graphVertex)
        {
            int pathWeight = 0;
            List<GraphVertex> verticesPath = new List<GraphVertex>();
            verticesPath.AddRange(graphVertex.path);

            foreach (var item in graphVertex.incidiencyGraphEdges)
            {
                var nearVertex = item.vertex2;
                pathWeight = graphVertex.distanceToVertex + item.weight;

                if (pathWeight < nearVertex.distanceToVertex)
                {
                    nearVertex.distanceToVertex = pathWeight;
                    nearVertex.path.Clear();
                    nearVertex.path.AddRange(verticesPath);
                    nearVertex.path.Add(nearVertex);
                }
            }
        }
        /// <summary>
        /// Remove edge from graph
        /// </summary>
        /// <param name="v1">First vertex</param>
        /// <param name="v2">second vertex</param>
        /// <returns>removed edge</returns>
        public GraphEdge RemoveEdgeFromGraph(GraphVertex v1, GraphVertex v2)
        {
            int temp = 0;
            foreach (GraphEdge item in Edges)
            {
                if (item.vertex1.name == v1.name && item.vertex2.name == v2.name)
                {
                    temp = Edges.IndexOf(item);
                    break;
                }
            }
            GraphEdge saveEdge = new GraphEdge();
            GraphEdge.CopyTo(Edges[temp], saveEdge);
            Edges.RemoveAt(temp);
            temp = 0;
            foreach (var item in v1.incidiencyGraphEdges)
            {
                if (item.vertex2.name == v2.name)
                {
                    temp = v1.incidiencyGraphEdges.IndexOf(item);
                    break;
                }
            }
            v1.incidiencyGraphEdges.RemoveAt(temp);

            return saveEdge;
        }
        /// <summary>
        /// Return edge
        /// </summary>
        /// <param name="deadEdge">Edge</param>
        public void ReturnEdgeInGraph(GraphEdge deadEdge)
        {
            GraphVertex vertexR1 = deadEdge.vertex1;
            GraphVertex vertexR2 = deadEdge.vertex2;
            int weightR = deadEdge.weight;

            GraphEdge returnEdge = new GraphEdge(vertexR1, vertexR2, weightR);

            AddEdge(vertexR1, vertexR2, weightR);

            vertexR1.incidiencyGraphEdges.Add(returnEdge);

        }
        /// <summary>
        /// Delete and Insert
        /// </summary>
        /// <param name="start">First vertex</param>
        /// <param name="end">Second vertex</param>
        public List<Path> YenAlg(GraphVertex start, GraphVertex end)
        {
            List<Path> Gen = new List<Path>();
            List<GraphVertex> shortestPathInGen = new List<GraphVertex>();
            shortestPathInGen.AddRange(end.path);


            for (int i = 0; i < shortestPathInGen.Count - 1; i++)
            {
                GraphEdge deadEdge = new GraphEdge();
                GraphEdge.CopyTo(RemoveEdgeFromGraph(shortestPathInGen[i], shortestPathInGen[i + 1]), deadEdge);

                foreach (var item in Vertices)
                {
                    item.visit = false;
                    item.distanceToVertex = int.MaxValue;
                    item.path.Clear();
                }

                Dijkstra(start);

                Path temp = new Path();

                temp.route.AddRange(end.path);
                temp.weight = end.distanceToVertex;

                temp.deletedEdge.vertex1 = deadEdge.vertex1;
                temp.deletedEdge.vertex2 = deadEdge.vertex2;
                temp.deletedEdge.weight = deadEdge.weight;

                Gen.Add(temp);

                ReturnEdgeInGraph(deadEdge);
                deadEdge = null;
            }
            return Gen;
        }
        /// <summary>
        /// Yens algorithm
        /// </summary>
        /// <param name="start"> First vertex</param>
        /// <param name="end"> Second vertex</param>
        /// <param name="count"> How many shortest paths you need to find</param>
        /// <returns>Лист путей</returns>
        public List<Path> Yen(GraphVertex start, GraphVertex end, int count)
        {

            List<Path> Generation = new List<Path>();
            List<Path> Best = new List<Path>();
            List<GraphEdge> deadList = new List<GraphEdge>();

            for (int i = 0; i < count; i++)
            {
                Generation.AddRange(YenAlg(start, end));

                int minLength = int.MaxValue;
                Path bestItem = new Path();

                foreach (var item in Generation)
                {

                    if (item.weight < minLength)
                    {
                        minLength = item.weight;

                        bestItem.route.Clear();
                        bestItem.route.AddRange(item.route);
                        bestItem.weight = item.weight;

                        bestItem.deletedEdge.vertex1 = item.deletedEdge.vertex1;
                        bestItem.deletedEdge.vertex2 = item.deletedEdge.vertex2;
                    }
                }
                if (minLength == int.MaxValue)
                {
                    Console.WriteLine(" There are only {0} paths to the final vertex: \n", Best.Count);
                    break;
                }

                Generation.Clear();

                RemoveEdgeFromGraph(bestItem.deletedEdge.vertex1, bestItem.deletedEdge.vertex2);

                end.path.Clear();
                end.path.AddRange(bestItem.route);

                Best.Add(bestItem);
            }
            return Best;
        }
    }
}

using System;
using System.Collections.Generic;
using yens_algorithm.GraphD;

namespace yens_algorithm.YensAlgorithmD
{
    class VertexInfo : IComparable<VertexInfo>
    {
        public Vertex Vertex { get; }
        public bool IsVisited;
        public int DistanceToVertex;
        public List<Vertex> Path;

        public VertexInfo(Vertex v, int dist = int.MaxValue, List<Vertex> path = null)
        {            
            Vertex = v;           
            DistanceToVertex = dist;
            Path = path is null
                ? new List<Vertex>() 
                : path;
            IsVisited = false;
        }

        int IComparable<VertexInfo>.CompareTo(VertexInfo other)
        {
            if (DistanceToVertex != other.DistanceToVertex)
            {
                return this.DistanceToVertex.CompareTo(other.DistanceToVertex);
            }
            else return this.Vertex.Name.CompareTo(other.Vertex.Name);
        }
    }
}

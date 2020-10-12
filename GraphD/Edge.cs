namespace yens_algorithm.GraphD
{
    public class Edge
    {
        public int Weight { get; set; }
        public Vertex Vertex1 { get; set; }
        public Vertex Vertex2 { get; set; }

        public Edge(Vertex v1 = null, Vertex v2 = null, int w = 0)
        {           
            Vertex1 = v1;
            Vertex2 = v2;
            Weight = w;
        }        
    }
}


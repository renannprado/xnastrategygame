using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace DummyProject.Primitives
{
    /// <summary>
    /// Classe que representa uma aresta
    /// </summary>
    public class Edge
    {
        public Vector2 StartVertex { get; set; }
        public Vector2 EndVertex { get; set; }

        public Edge(Vector2 StartVertex, Vector2 EndVertex)
        { 
            this.StartVertex = StartVertex;
            this.EndVertex = EndVertex;
        }

        public Edge()
        { 
        
        }

        public override string ToString()
        {
            return "{ " + StartVertex.ToString() + ", " + EndVertex.ToString() + " }";
        }
    }
}

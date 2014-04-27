using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace DummyProject.Primitives
{
    public class Polygon
    {
        /// <summary>
        /// Encapsular o add dentro de um método.
        /// </summary>
        public List<Edge> Edges { get; set; }

        public Polygon()
        {
            Edges = new List<Edge>();
        }

        public static bool PointInPolygon(Point point, Polygon polygon)
        {
            bool inside = false;

            foreach (var side in polygon.Edges)
            {
                if (point.Y > Math.Min(side.StartVertex.Y, side.EndVertex.Y))
                    if (point.Y <= Math.Max(side.StartVertex.Y, side.EndVertex.Y))
                        if (point.X <= Math.Max(side.StartVertex.X, side.EndVertex.X))
                        {
                            float xIntersection = side.StartVertex.X + ((point.Y - side.StartVertex.Y) / (side.EndVertex.Y - side.StartVertex.Y)) * (side.EndVertex.X - side.StartVertex.X);
                            if (point.X <= xIntersection)
                                inside = !inside;
                        }
            }

            return inside;
        }

        /// <summary>
        /// Só serve se for um losango
        /// </summary>
        /// <param name="p">Losango</param>
        /// <returns>O ponto central do losango</returns>
        public static Point CalculateCenter(Polygon p)
        {
            Edge e1 = p.Edges[0];
            Edge e2 = p.Edges[1];
            Edge e3 = p.Edges[2];

            //int x = Convert.ToInt32(Math.Ceiling(e1.StartVertex.X + (e2.EndVertex.X / 2)));
            //int y = Convert.ToInt32(Math.Ceiling(e3.EndVertex.Y - (e2.StartVertex.Y / 2)));

            int x = Convert.ToInt32(e1.StartVertex.X + (e2.EndVertex.X / 2));

            int y1 = Convert.ToInt32(e1.EndVertex.Y > 0 ? e1.EndVertex.Y : e1.EndVertex.Y * -1);
            int y2 = Convert.ToInt32(e3.EndVertex.Y > 0 ? e3.EndVertex.Y : e3.EndVertex.Y * -1);

            int y = Convert.ToInt32((y1 + y2) / 2);

            //Vector2.dis

            return new Point(x, y);
        }
    }
}

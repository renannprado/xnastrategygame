using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using DummyProject.Primitives;

namespace DummyProject
{
    public static class Camera
    {
        public static Point Location = Point.Zero;

        public static Edge RelativeToCamera(Edge e)
        {
            return new Edge
                        (
                            new Vector2
                            (
                                e.StartVertex.X + Camera.Location.X,
                                e.StartVertex.Y + Camera.Location.Y
                            ),
                            new Vector2
                            (
                                e.EndVertex.X + Camera.Location.X,
                                e.EndVertex.Y + Camera.Location.Y
                            )
                        );
        }

        public static Polygon RelativeToCamera(Polygon p)
        {
            Polygon tmpPol = new Polygon();

            foreach (Edge e in p.Edges)
            {
                tmpPol.Edges.Add(Camera.RelativeToCamera(e));
            }

            return tmpPol;
        }

        public static Rectangle RelativeToCamera(Rectangle r)
        {
            r.X += Camera.Location.X;
            r.Y += Camera.Location.Y;

            return r;
        }

        public static Point RelativeToCamera(Point p)
        {
            p.X += Camera.Location.X;
            p.Y += Camera.Location.Y;

            return p;
        }
    }
}

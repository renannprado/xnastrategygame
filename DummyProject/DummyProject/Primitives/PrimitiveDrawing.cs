using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;

namespace DummyProject.Primitives
{
    public static class PrimitiveDrawing
    {
        public static void DrawLine(SpriteBatch batch, Texture2D blank, float width, Color color, Vector2 point1, Vector2 point2)
        {
            float angle = (float)Math.Atan2(point2.Y - point1.Y, point2.X - point1.X);
            float length = Vector2.Distance(point1, point2);

            batch.Draw(blank, point1, null, color,
                       angle, Vector2.Zero, new Vector2(length, width),
                       SpriteEffects.None, 0);
        }

        public static void DrawLine(SpriteBatch batch, Texture2D blank, float width, Color color, Edge e)
        {
            float angle = (float)Math.Atan2(e.EndVertex.Y - e.StartVertex.Y,
                e.EndVertex.X - e.StartVertex.X);
            float length = Vector2.Distance(e.StartVertex, e.EndVertex);

            batch.Draw(blank, e.StartVertex, null, color,
                       angle, Vector2.Zero, new Vector2(length, width),
                       SpriteEffects.None, 0);
        }

        public static void DrawPolygon(SpriteBatch batch, Texture2D blank, float width, Color color, Polygon p)
        {
            foreach (Edge e in p.Edges)
            {
                DrawLine(batch, blank, width, color, e);
            }
        }
    }
}

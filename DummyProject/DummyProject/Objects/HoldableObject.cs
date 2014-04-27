using Microsoft.Xna.Framework.Graphics;

namespace DummyProject.Objects
{
    public abstract class HoldableObject
    {
        public Texture2D Texture { get;  set; }
        public HoldableObjectType Type { get; protected set; }

        public HoldableObject(Texture2D Texture, HoldableObjectType Type)
        {
            this.Texture = Texture;
            this.Type = Type;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using DummyProject.Types;

namespace DummyProject.Objects
{
    public class Resource
    {
        public String Name { get; private set; }
        public ResourceType Type { get; private set; }
        public int Quantity { get; private set; }
        public Texture2D Image { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rt">Tipo de recurso</param>
        /// <param name="qtd">Quantidade do recurso</param>
        public Resource(ResourceType rt, int qtd, Texture2D img)
        {
            Type = rt;
            Quantity = qtd;
            Image = img;
            Name = Enum.GetName(typeof(ResourceType), Type);
        }
    }
}

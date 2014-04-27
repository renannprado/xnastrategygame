using System;
using DummyProject.Objects;
using DummyProject.Types;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace DummyProject.Factories
{
    public class ResourceFactory
    {
        private static ResourceFactory FactoryInstance = null;
        public static ContentManager ContentLoader = null;

        private ResourceFactory()
        { 
            
        }

        public static ResourceFactory getInstance()
        {
            if (FactoryInstance == null)
            {
                FactoryInstance = new ResourceFactory();
            }

            return FactoryInstance;
        }

        public Resource createResouce(ResourceType tp, int qtd)
        {
            String imgPath = @"Resources\";

            switch(tp)
            {
                case ResourceType.Wood:
                {
                    return new Resource(tp, qtd, ContentLoader.Load<Texture2D>(imgPath + "woodmini"));
                }
                case ResourceType.Money:
                {
                    return new Resource(tp, qtd, ContentLoader.Load<Texture2D>(imgPath + "moneymini"));
                }
                case ResourceType.Water:
                {
                    return new Resource(tp, qtd, ContentLoader.Load<Texture2D>(imgPath + "watermini"));
                }
                default:
                {
                    //mudar isso depois
                    return new Resource(tp, qtd, ContentLoader.Load<Texture2D>(imgPath + "watermini"));
                }
            }
        }
    }
}

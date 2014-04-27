using System;
using System.Collections.Generic;
using DummyProject.Factories;
using DummyProject.Helpers;
using DummyProject.Objects.Buildings.DataLayer;
using DummyProject.Primitives;
using DummyProject.Types;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DummyProject.Objects
{
    public class DrawableCityBlock //: DrawableGameComponent
    {
        public CityBlock CityBlock { get; private set; }

        public bool IsHighlighted { get; set; }
        public bool IsPainted { get; set; }
        public List<Resource> Resources { get; private set; }
        public Texture2D Texture { get; set; }
        public GenericBuilding Building { get; private set; }
        public bool IsBuildable { get; private set; }
        public bool HasBuilding { get { return Building == null ? false : true; } }
        public bool HighLightBuilding { get; set; }
        public String Coordinates { get; set; }
        
        public bool IsReferenceForBuilding { get; private set; }

        public DrawableCityBlock(CityBlock CityBlock)
        {
            IsHighlighted = false;
            this.IsPainted = false;
            IsBuildable = true;
            HighLightBuilding = false;
            this.CityBlock = CityBlock;

            IsReferenceForBuilding = false;

            Building = null; //não há estrutura construida nesse bloco

            RandomizeResouces();
        }
        
        public GenericBuilding Unbuild()
        {
            GenericBuilding b = this.Building;
            this.Building = null;
            return b;
        }

        private void RandomizeResouces()
        {
            Resources = new List<Resource>();
            Random r = new Random(Convert.ToInt32(DateTime.Now.Millisecond));

            Resources.Add(ResourceFactory.getInstance().createResouce(ResourceType.Wood, r.Next(0, 100)));
            Resources.Add(ResourceFactory.getInstance().createResouce(ResourceType.Sand, r.Next(0, 100)));
            Resources.Add(ResourceFactory.getInstance().createResouce(ResourceType.Water, r.Next(0, 100)));

            if (Resources[0].Quantity > Resources[1].Quantity &&
                Resources[0].Quantity > Resources[2].Quantity)
            {

                this.Texture = AssetLoader.Content.Load<Texture2D>(@"CityMap\Cubes\cubo");
            }
            else if (Resources[1].Quantity > Resources[0].Quantity &&
                     Resources[1].Quantity > Resources[2].Quantity)
            {
                this.Texture = AssetLoader.Content.Load<Texture2D>(@"CityMap\Cubes\cubo_terra");
            }
            else
            {
                this.Texture = AssetLoader.Content.Load<Texture2D>(@"CityMap\Cubes\cubo_agua");
            }
        }
    }
}

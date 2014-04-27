using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using DummyProject.Types;

namespace StrategyGameLibrary
{
    public class GenericBuildingTestcs : LoadableGameComponent
    {
        //public List<Resource> RequiredResourceList1 { get; private set; }
        //public int WaterPerTurn { get; set; }
        //public int EnergyPerTurn { get; set; }
        public Point Area { get; protected set; }
        public Rectangle Location { get; private set; }
        private int _assetID;
        private string _rootAssetPath;
        public Dictionary<ResourceType, int> RequiredResourceList { get; set; }
        public Dictionary<ResourceType, int> ConsumedResourcesPerTurn { get; set; }
        //public HoldableObjectType HoldeableType = HoldableObjectType.Nothing;

        public GenericBuildingTestcs()//Point Area)//, HoldableObjectType Type)
        {
            //this.RequiredResourceList1 = RequiredResourceList;
            //this.WaterPerTurn = WaterPerTurn;
            //this.EnergyPerTurn = EnergyPerTurn;
            //this.Area = Area;
            this.AssetID = 2;
            this.RootAssetPath = @"Teste\";
            this.RequiredResourceList = new Dictionary<ResourceType, int>();
            this.RequiredResourceList.Add(ResourceType.Money, 10);
            this.RequiredResourceList.Add(ResourceType.Sand, 30);
            this.RequiredResourceList.Add(ResourceType.Wood, 50);

            this.ConsumedResourcesPerTurn = new Dictionary<ResourceType, int>();
            this.ConsumedResourcesPerTurn.Add(ResourceType.Money, 10);
            //this.ConsumedResourcesPerTurn.Add(ResourceType.Sand, 30);
            //this.ConsumedResourcesPerTurn.Add(ResourceType.Wood, 50);
            this.ConsumedResourcesPerTurn.Add(ResourceType.Food, 23);
        }

        public string RootAssetPath
        {
            get
            {
                return _rootAssetPath;
            }
            set
            {
                _rootAssetPath = value;
            }
        }

        public int AssetID
        {
            get
            {
                return _assetID;
            }
            set
            {
                _assetID = value;
            }
        }
    }
}

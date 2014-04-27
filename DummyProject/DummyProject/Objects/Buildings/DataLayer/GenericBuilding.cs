using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StrategyGameLibrary;

namespace DummyProject.Objects.Buildings.DataLayer
{
    public abstract class GenericBuilding : HoldableObject, LoadableGameComponent
    {
        public List<Resource> RequiredResourceList { get; set; }
        public int WaterPerTurn { get; set; }
        public int EnergyPerTurn { get; set; }
        public Point Area { get; protected set; }
        public Rectangle Location { get; set; }
        private int _assetID;
        private string _rootAssetPath;

        public GenericBuilding(List<Resource> RequiredResourceList, int WaterPerTurn,
                                int EnergyPerTurn, Texture2D Texture, Point BlockOccupation, HoldableObjectType Type)
            : base(Texture, Type)
        {
            this.RequiredResourceList = RequiredResourceList;
            this.WaterPerTurn = WaterPerTurn;
            this.EnergyPerTurn = EnergyPerTurn;
            this.Texture = Texture;
            this.Area = BlockOccupation;
        }

        public abstract GenericBuilding GetNextLevel();

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

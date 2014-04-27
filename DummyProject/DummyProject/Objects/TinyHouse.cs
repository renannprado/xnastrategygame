using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using DummyProject.Objects.Buildings.DataLayer;

namespace DummyProject.Objects
{
    public class TinyHouse : GenericBuilding
    {
        public TinyHouse(List<Resource> RequiredResourceList, int WaterPerTurn,
                                int EnergyPerTurn, Texture2D Texture, Point BlockOccupation, HoldableObjectType Type)
                        : base(RequiredResourceList, WaterPerTurn,
                                EnergyPerTurn, Texture, BlockOccupation, Type)
        {
            
        }


        public override GenericBuilding GetNextLevel()
        {
            throw new NotImplementedException();
        }
    }
}

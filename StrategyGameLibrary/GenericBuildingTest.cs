using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Intermediate;
using System.Xml;

namespace StrategyGameLibrary
{
    public class GenericBuildingTest
    {
        public int WaterPerTurn { get; set; }
        public int EnergyPerTurn { get; set; }
        public Point Area { get; protected set; }
        public Rectangle Location { get; set; }
        public TipoTeste TesteTipo { get; set; }

        public GenericBuildingTest(int WaterPerTurn, int EnergyPerTurn, Point BlockOccupation)
        {
            this.WaterPerTurn = WaterPerTurn;
            this.EnergyPerTurn = EnergyPerTurn;
            this.Area = BlockOccupation;
            this.TesteTipo = TipoTeste.Tipo2;
        }

        public static void Teste()
        {
            GenericBuildingTestcs test = new GenericBuildingTestcs();

            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;

            using (XmlWriter writer = XmlWriter.Create(@"C:\teste\example.xml", settings))
            {
                IntermediateSerializer.Serialize(writer, test, null);
            }
        }

        public static void Teste(object o)
        {
            //GenericBuildingTest test = new GenericBuildingTest(1, 2, new Point(3, 5));

            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;

            using (XmlWriter writer = XmlWriter.Create(@"C:\teste\example" + o.GetType().ToString() + ".xml", settings))
            {
                IntermediateSerializer.Serialize(writer, o, null);
            }
        }
    }
}

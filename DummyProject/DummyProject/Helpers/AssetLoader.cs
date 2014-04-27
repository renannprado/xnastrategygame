using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using DummyProject.Objects;
using System.Xml;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Intermediate;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.IO;
using System.Windows;
using StrategyGameLibrary;

namespace DummyProject.Helpers
{
    public static class AssetLoader//<T> where T : LoadableGameComponent
    {
        private static Game _game;

        public static Game Game
        {
            private get
            {
                return _game;
            }
            set
            {
                if (_game == null)
                    _game = value;
            }
        }

        public static ContentManager Content
        { 
            get 
            {
                return AssetLoader.Game.Content;
            } 
        }

        public static T Load<T>(String path, String asset) where T : LoadableGameComponent
        {
            T t = ((T)Activator.CreateInstance(typeof(T)));

            //o RootAssetPath deve conter a barra no final, ex: Buildings\
            return Game.Content.Load<T>(path + asset);            
        }

        public static Texture2D DynamicTextureLoad(String path, String asset)
        { 
            try
            {
                return Texture2D.FromStream(Game.GraphicsDevice, new FileStream(path + asset + ".png", FileMode.Open));
            }
            catch(FileNotFoundException)
            {
                MessageBox.Show("Caminho ou nome do arquivo incorretos\n\n" + path + asset + ".png");
                return null;
            }

        }

        public static void Teste(object value)
        {
            //GenericBuildingTest test = new GenericBuildingTest(1, 2, new Point(3, 5));

            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;

            using (XmlWriter writer = XmlWriter.Create(@"C:\teste\"+ value.GetType().ToString() + ".xml", settings))
            {
                IntermediateSerializer.Serialize(writer, value, null);
            }
        }
    }
}

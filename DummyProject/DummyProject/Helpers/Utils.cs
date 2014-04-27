using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using DummyProject.Objects;
using DummyProject.Objects.Buildings.DataLayer;

namespace DummyProject.Helpers
{
    /// <summary>
    /// Classe com algumas coisas que serão úteis para todo o jogo
    /// </summary>
    public static class Utils
    {
        public static Texture2D BlankTexture = null;
        private static int LastComponentID = 0;
        public static int HeightOfSelectionTexture = 50;

        public static int GetComponentID()
        {
            return LastComponentID++;
        }

        public static Point GetCenterOfRectangle(Rectangle r)
        {
            return new Point(r.Width / 2, r.Height / 2);
        }

        public static bool IsAllValuesNotNull(object[] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] == null)
                    return false;
            }

            return true;
        }

        /// <summary>
        /// retorna verdadeiro se o primeiro bloco estiver mais a direita do que o segundo
        /// </summary>
        /// <param name="?"></param>
        /// <returns></returns>
        public static bool RightSideBlock(CityBlock cb1, CityBlock cb2)
        {
            return (cb1.MatrixPosition.X >= cb2.MatrixPosition.X 
                && cb1.MatrixPosition.Y <= cb2.MatrixPosition.Y);
        }
    }
}

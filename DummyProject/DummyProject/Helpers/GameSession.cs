using System.Collections.Generic;
using DummyProject.Objects.Buildings.DataLayer;
using DummyProject.Types;

namespace DummyProject.Helpers
{
    public static class GameSession
    {
        public static GameState State = GameState.CityView;
        public static InputCache InputCache = null;
        public static MouseHolding MouseHolding = new MouseHolding();
        private static Dictionary<ResourceType,int> PlayerResources = new Dictionary<ResourceType,int>();

        public static event SelectedBuildingDelegate OnSelectBuilding;

        public static void CallOnSelectBuilding(GenericBuilding b)
        {
            GameSession.OnSelectBuilding(b);
        }

        public static void AddResources(ResourceType rt, int qtd)
        {
            PlayerResources[rt] += qtd;
        }

        public static int CheckResource(ResourceType rt)
        {
            return PlayerResources[rt];
        }

        /// <summary>
        /// Retorna -1 se o player não tiver resources suficientes
        /// </summary>
        /// <param name="rt"></param>
        /// <param name="qtd"></param>
        public static int ConsumeResource(ResourceType rt, int qtd)
        {
            if (PlayerResources[rt] >= qtd)
            {
                return PlayerResources[rt] -= qtd;
            }
            else
            {
                return -1;
            }
            
        }
    }
}

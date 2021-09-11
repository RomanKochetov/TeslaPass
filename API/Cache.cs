using System.Collections.Generic;
using Exiled.API.Features;

namespace TeslaPass.API
{
    public class Cache
    {
        private static Cache _instance;
        public readonly List<Player> PlayersInCoolDown = new List<Player>();
        public readonly Dictionary<TeslaGate, int> TeslaGatesIds = new Dictionary<TeslaGate, int>();
        public readonly Dictionary<TeslaGate, bool> TeslaGatesStates = new Dictionary<TeslaGate, bool>();

        private Cache()
        {
        }

        public void Clear()
        {
            TeslaGatesIds.Clear();
            TeslaGatesStates.Clear();
            PlayersInCoolDown.Clear();
        }

        public static Cache GetInstance()
        {
            return _instance ?? (_instance = new Cache());
        }
    }
}

using System.Collections.Generic;
using Exiled.API.Features;
using MEC;

namespace TeslaPass.API
{
    using static TeslaPass;

    public static class PlayerCoolDown
    {
        private static readonly Cache Data = Cache.GetInstance();

        public static bool PlayerInCoolDown(Player player)
        {
            return Data.PlayersInCoolDown.Contains(player);
        }

        public static IEnumerator<float> SetPlayerCoolDown(Player player)
        {
            if (PlayerInCoolDown(player)) yield break;
            Data.PlayersInCoolDown.Add(player);
            yield return Timing.WaitForSeconds(Instance.Config.CoolDown);
            Data.PlayersInCoolDown.Remove(player);
        }
    }
}

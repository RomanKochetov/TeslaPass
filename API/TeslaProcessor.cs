using System.Collections.Generic;
using Exiled.API.Features;
using MEC;

namespace TeslaPass.API
{
    using static TeslaPass;

    public static class TeslaProcessor
    {
        private static readonly Cache Data = Cache.GetInstance();

        public static IEnumerator<float> DisableTesla(TeslaGate tesla, float delay)
        {
            Data.TeslaGatesStates[tesla] = false;
            var id = Data.TeslaGatesIds[tesla];
            Cassie.Message(Instance.Config.TeslaShutDownCassieMessage.Replace("%TESLA%", id.ToString()));

            if (Instance.Config.ShowBroadcasts)
                Map.Broadcast(10, Instance.Config.TeslaShutDownBroadCast
                    .Replace("%TESLA%", id.ToString()));

            yield return Timing.WaitForSeconds(delay);

            Data.TeslaGatesStates[tesla] = true;

            Cassie.Message(Instance.Config.TeslaOperationalCassieMessage.Replace("%TESLA%", id.ToString()));

            if (Instance.Config.ShowBroadcasts)
                Map.Broadcast(10, Instance.Config.TeslaOperationalBroadCast
                    .Replace("%TESLA%", id.ToString()));
        }
    }
}

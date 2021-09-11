using Exiled.API.Features;
using TeslaPass.API;

namespace TeslaPass.Events
{
    using static TeslaPass;

    public class ServerEventHandler
    {
        private readonly Cache _data = Cache.GetInstance();

        public void OnRoundStarted()
        {
            for (var i = 0; i < Map.TeslaGates.Count; i++)
            {
                var tesla = Map.TeslaGates[i];
                if (tesla == null) continue;

                _data.TeslaGatesIds.Add(tesla, i + 1);
                _data.TeslaGatesStates.Add(tesla, true);
                Log.Debug($"Registered Tesla gate {tesla.name}", Instance.Config.Debug);
            }
        }
    }
}

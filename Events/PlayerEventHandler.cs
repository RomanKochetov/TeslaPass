using System.Collections.Generic;
using System.Linq;
using Exiled.API.Features;
using Exiled.Events.EventArgs;
using MEC;
using UnityEngine;
using Cache = TeslaPass.API.Cache;

namespace TeslaPass.Events
{
    using static TeslaPass;

    public class PlayerEventHandler
    {
        private readonly Cache _data = Cache.GetInstance();
        private CoroutineHandle _handle;

        public void OnTriggeringTesla(TriggeringTeslaEventArgs ev)
        {
            if (!Instance.Config.AllowedRoles.Contains(ev.Player.Role)) return;

            var teslaGates = Map.TeslaGates
                .Where(element => element.PlayerInRange(ev.Player.ReferenceHub)).ToList();

            if (!teslaGates.Any()) return;
            var tesla = teslaGates.First();
            if (tesla == null) return;

            if (!_data.TeslaGatesStates.ContainsKey(tesla)) return;

            if (!_handle.IsRunning)
                _handle = Timing.RunCoroutine(ShowPlayerMessage(ev.Player, ev.Player.CurrentRoom,
                    _data.TeslaGatesIds[tesla]));

            if (!_data.TeslaGatesStates[tesla]) ev.IsTriggerable = false;
        }

        private static IEnumerator<float> ShowPlayerMessage(Player player, Object room, int teslaNumber)
        {
            while (player.CurrentRoom == room)
            {
                player.ShowHint(Instance.Config.TeslaRoomMessage.Replace("%TESLA%", 
                    teslaNumber.ToString()), 1);
                yield return Timing.WaitForSeconds(1);
            }
        }
    }
}

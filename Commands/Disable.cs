using System;
using System.Linq;
using CommandSystem;
using Exiled.API.Features;
using MEC;
using RemoteAdmin;
using TeslaPass.API;

namespace TeslaPass.Commands
{
    using static TeslaPass;

    [CommandHandler(typeof(ClientCommandHandler))]
    public class Disable : ICommand
    {
        private readonly Cache _data = Cache.GetInstance();
        public string Command { get; } = "disable";
        public string[] Aliases { get; }
        public string Description { get; } = "Disables tesla in player's current room";


        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            response = "You're not a player";

            if (!(sender is PlayerCommandSender playerCommandSender)) return false;

            var player = Player.Get(playerCommandSender.PlayerId);
            if (player == null) return false;

            if (!Instance.Config.AllowedRoles.Contains(player.Role))
            {
                response = "You're not allowed to do this";
                return false;
            }

            if (PlayerCoolDown.PlayerInCoolDown(player))
            {
                response = "Please, try again later. You're on cooldown";
                return false;
            }

            var tesla = Map.TeslaGates.FirstOrDefault(element => element.PlayerInRange(player.ReferenceHub));

            if (tesla == null)
            {
                response = "You must be in same room with tesla gate";
                return false;
            }

            if (!_data.TeslaGatesStates.TryGetValue(tesla, out var state) || !_data.TeslaGatesIds.ContainsKey(tesla))
            {
                response = "Tesla gate is undefined";
                return false;
            }

            if (!state)
            {
                response = "Tesla gate is already shut down";
                return false;
            }

            Timing.RunCoroutine(PlayerCoolDown.SetPlayerCoolDown(player));

            Timing.RunCoroutine(TeslaProcessor.DisableTesla(tesla, Instance.Config.TeslaDisableTime));

            response = $"Done. Wait for {Instance.Config.CoolDown} second(-s) to use this command again";
            return true;
        }
    }
}

using System;
using Exiled.API.Enums;
using Exiled.API.Features;
using TeslaPass.API;
using TeslaPass.Events;
using Player = Exiled.Events.Handlers.Player;
using Server = Exiled.Events.Handlers.Server;

namespace TeslaPass
{
    public class TeslaPass : Plugin<Config>
    {
        private readonly Cache _data = Cache.GetInstance();

        private PlayerEventHandler _playerEventHandler;
        private ServerEventHandler _serverEventHandler;

        private TeslaPass()
        {
        }

        public override string Name => "TeslaPass";
        public override string Author => "Roman Kochetov";
        public override PluginPriority Priority => PluginPriority.Default;
        public override Version Version { get; } = new Version(1, 0, 0, 0);
        public override Version RequiredExiledVersion { get; } = new Version(3, 0, 0);

        public static TeslaPass Instance { get; } = new TeslaPass();

        private void RegisterEvents()
        {
            _playerEventHandler = new PlayerEventHandler();
            _serverEventHandler = new ServerEventHandler();
            Server.RoundStarted += _serverEventHandler.OnRoundStarted;
            Server.RestartingRound += _data.Clear;
            Player.TriggeringTesla += _playerEventHandler.OnTriggeringTesla;

            Log.Info("Events have been registered");
        }

        private void UnRegisterEvents()
        {
            Player.TriggeringTesla -= _playerEventHandler.OnTriggeringTesla;
            Server.RoundStarted -= _serverEventHandler.OnRoundStarted;
            Server.RestartingRound -= _data.Clear;
            _playerEventHandler = null;
            _serverEventHandler = null;

            _data.Clear();
            Log.Info("Events have been unregistered");
        }

        public override void OnEnabled()
        {
            base.OnEnabled();
            RegisterEvents();
        }

        public override void OnDisabled()
        {
            base.OnDisabled();
            UnRegisterEvents();
        }
    }
}

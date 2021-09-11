using System.Collections.Generic;
using System.ComponentModel;
using Exiled.API.Interfaces;

namespace TeslaPass
{
    public sealed class Config : IConfig
    {
        [Description("%TESLA% - Tesla gate ID")]
        public string TeslaRoomMessage { get; set; } = "<b>You're next to Tesla gate <color=red>%TESLA%</color></b>" +
                                                       "\nUse <color=blue>.disable</color> command to disable tesla";

        public string TeslaShutDownCassieMessage { get; set; } = "TESLA GATE %TESLA% HAS BEEN DEACTIVATED";

        public string TeslaOperationalCassieMessage { get; set; } = "TESLA GATE %TESLA% IS NOW OPERATIONAL";

        [Description("Cooldown after player disables tesla, in seconds")]
        public float CoolDown { get; set; } = 120f;

        public float TeslaDisableTime { get; set; } = 10f;

        public bool ShowBroadcasts { get; set; } = true;

        public string TeslaShutDownBroadCast { get; set; } = "Tesla gate %TESLA% has been deactivated";

        public string TeslaOperationalBroadCast { get; set; } = "Tesla gate %TESLA% is now operational";

        [Description("Roles that are allowed to disable tesla gate")]
        public List<RoleType> AllowedRoles { get; set; } = new List<RoleType>
        {
            RoleType.NtfSergeant,
            RoleType.NtfSpecialist,
            RoleType.NtfCaptain,
            RoleType.FacilityGuard
        };

        public bool Debug { get; set; } = false;

        public bool IsEnabled { get; set; } = true;
    }
}

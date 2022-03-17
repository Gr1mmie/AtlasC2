using System;

using Implant.Models;

namespace Implant.Tasks.Execute
{
    internal class Exit : ImplantCommands
    {
        public override string Name => "KillSwitch";

        public override string Execute(ImplantTask task)
        {

            Environment.Exit(0);
            return $"Implant Shutdown";
        }
    }
}

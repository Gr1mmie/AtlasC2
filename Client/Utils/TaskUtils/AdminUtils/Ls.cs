using Client.Models;

using static Client.Models.Client;

namespace Client.Utils.TaskUtils.AdminUtils
{
    class Ls : Models.AdminTask
    {
        private string path { get; set; }
        public override string TaskName => "Ls";

        public override string Desc => "list the contents of the current directory";

        public override string AdminUtilExec(string[] opts)
        {
            try
            {

                if (opts is null) { throw new AtlasException($"[*] Usage: Ls [Path]\n"); }
                if (opts.Length > 2) { throw new AtlasException($"[*] Usage: Ls [Path]\n"); }
                if (CurrentImplant is null) { throw new AtlasException("[-] No connected implant"); }

                path = opts[1];

                return TaskOps.sendAdminUtil(TaskName, path);

            }
            catch (AtlasException e) { return e.Message; }
        }
    }
}

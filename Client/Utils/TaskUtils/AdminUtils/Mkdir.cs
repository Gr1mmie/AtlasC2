using Client.Models;

using static Client.Models.Client;

namespace Client.Utils
{
    class Mkdir : Models.AdminTask
    {
        private string newDir { get; set; }
        public override string TaskName => "MkDir";

        public override string Desc => "Create a directory";

        public override string AdminUtilExec(string[] opts)
        {
            try
            {

                if (opts is null) { throw new AtlasException($"[*] Usage: MkDir [newDir]\n"); }
                if (!(opts.Length == 2)) { throw new AtlasException($"[*] Usage: MkDir [newDir]\n"); }
                if (CurrentImplant is null) { throw new AtlasException("[-] No connected implant"); }

                newDir = opts[1];

                return TaskOps.sendAdminUtil(TaskName, newDir);

            }
            catch (AtlasException e) { return e.Message; }
        }
    }
}

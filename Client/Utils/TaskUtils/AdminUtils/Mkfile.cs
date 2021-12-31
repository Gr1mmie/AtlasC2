using Client.Models;

using static Client.Models.Client;


namespace Client.Utils.TaskUtils.AdminUtils
{
    class Mkfile : Models.AdminTask
    {
        private string newFile { get; set; }
        public override string TaskName => "MkFile";

        public override string Desc => "Create a file";

        public override string AdminUtilExec(string[] opts)
        {
            try
            {

                if (opts is null) { throw new AtlasException($"[*] Usage: MkFile [newFile]\n"); }
                if (!(opts.Length == 2)) { throw new AtlasException($"[*] Usage: MkDir [newFile]\n"); }
                if (CurrentImplant is null) { throw new AtlasException("[-] No connected implant"); }
                
                newFile = opts[1];

                return TaskOps.sendAdminUtil(TaskName, newFile);
            }
            catch (AtlasException e) { return e.Message; }
        }
    }
}

using Client.Models;

using static Client.Models.Client;

namespace Client.Utils.TaskUtils
{
    class Cd : Models.AdminTask
    {
        private string newDir { get; set; }

        public override string TaskName => "Cd";

        public override string Desc => "Change directory";

        public override string AdminUtilExec(string[] opts)
        {
            try 
            {

                if (opts is null) { throw new AtlasException($"[*] Usage: Cd [Path]\n"); }
                if (opts.Length > 2) { throw new AtlasException($"[*] Usage: Cd [Path]\n"); }            
                if(CurrentImplant is null) { throw new AtlasException("[-] No connected implant"); }

                newDir = opts[1];

                return TaskOps.sendAdminUtil(TaskName, newDir);
                
            } catch (AtlasException e) { return e.Message; }
        }
    }
}

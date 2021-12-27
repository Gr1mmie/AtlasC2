using System;
using System.Linq;
using Client.Models;

using static Client.Models.Client;
using static Client.Utils.Init;

namespace Client.Util
{

    class SetTask : Models.Util
    {
        string cTaskName {get;set;}
        public override string UtilName => "SetTask";
        public override string Desc => "Set a task";
        public override string UtilExecute(string[] opts)
        {
            try
            {
                if (opts is null) { throw new AtlasException($"[-] No parameters passed\nUsage: SetTask [taskName]"); }

                cTaskName = opts[1];

                if (_opts.Count == 0) { OptInit(); }

                Task util = _opts.FirstOrDefault(u => u.TaskName.Equals(cTaskName, StringComparison.InvariantCultureIgnoreCase));

                if (util is null) { throw new AtlasException($"[-] {cTaskName} is an invalid task\n"); }
                else{ TaskName = cTaskName; return $"[*] Task set to {TaskName}\n"; }

            } catch (AtlasException e) { return $"{e.Message}\n"; }
        }
    }
}

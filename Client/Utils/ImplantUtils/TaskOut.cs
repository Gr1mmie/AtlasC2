using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Client.Models;

using static Client.Models.Client;

namespace Client.Utils.ImplantUtils
{
    class TaskOut : Models.Util
    {
        private string taskId { get; set; }
        public override string UtilName => "TaskOut";

        public override string Desc => "Returns the output of a specified task";

        public override string UtilExecute(string[] opts)
        {
            try
            {
                if (opts is null) { throw new AtlasException($"[-] No parameters passed\nUsage: TaskOut [taskId]\n"); }
                if (opts.Length > 2) { throw new AtlasException($"[*] Incorrect parameters passed\nUsage: TaskOut [taskId]\n"); }

                taskId = opts[1];

                StringBuilder _out = new StringBuilder();

                var taskOut = Comms.comms.SendGET($"{TeamServerAddr}/Implants/{CurrentImplant}/{taskId}");
                var parsedTaskOut = JSONOps.ReturnTaskData(taskOut);

                _out.AppendLine(parsedTaskOut.TaskOut);

                return _out.ToString();
            }
            catch (AtlasException e) { return e.Message; }
            catch (System.Net.WebException) { return $"[-] Connection to teamserver could not be established, verify teamserver is active\n"; }
        }
    }
}

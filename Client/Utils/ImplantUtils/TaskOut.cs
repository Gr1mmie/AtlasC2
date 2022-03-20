using System.Text;

using Client.Models;

using static Client.JSON.Classes;
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
                if (opts is null) { throw new AtlasException($"[*] Usage: TaskOut [taskId]\n"); }
                if (opts.Length > 2) { throw new AtlasException($"[*] Usage: TaskOut [taskId]\n"); }

                taskId = opts[1];

                StringBuilder _out = new StringBuilder();

                string taskOut = Comms.comms.SendGET($"{TeamServerAddr}/Implants/{CurrentImplant}/tasks/{taskId}")
                    .Replace("\\\\\"", "\"");
                TaskRecvOut parsedTaskOut = JSONOps.ReturnTaskData(taskOut);
                ArgsRecv parsedArgs = JSONOps.ReturnTaskArgs(parsedTaskOut.TaskArgs);

                _out.AppendLine($"TaskName: {parsedTaskOut.TaskName}");
                if (parsedArgs != null) {
                    _out.AppendLine($"\nParams:");
                    foreach (TaskArgs param in parsedArgs.Params) { _out.AppendLine($"\t{param.OptionName} - {param.OptionValue}"); }
                }
                _out.AppendLine($"Output:\n{parsedTaskOut.TaskOut}");

                return _out.ToString();
            }
            catch (AtlasException e) { return e.Message; }
            catch (System.Net.WebException) { return $"[-] Connection to teamserver could not be established, verify teamserver is active\n"; }
        }
    }
}

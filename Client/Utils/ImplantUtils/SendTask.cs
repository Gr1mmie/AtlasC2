using System.Text;
using System.Threading;

using Client.Models;

using static System.Console;

using static Client.Models.Client;

namespace Client.Utils
{
    class SendTask : Models.Util
    {
        public override string UtilName => "SendTask";
        public override string Desc => "Push a task to implant for execution";
        public override string UtilExecute(string[] opts)
        {
            try {

                StringBuilder _out = new StringBuilder();

                argData = JSONOps.PackTaskArgs().Replace('"'.ToString(),"\\\"");
                sendData = JSONOps.PackTaskData(argData);
                
                var tasksendOut = Comms.comms.SendPOST($"{TeamServerAddr}/Implants/{CurrentImplant}", sendData).TrimStart('[').TrimEnd(']');
                var taskId = JSONOps.ReturnTaskID(tasksendOut);
                WriteLine($"Task {taskId.Id} Initialized");
                Thread.Sleep(3000);
                
                var taskOut = Comms.comms.SendGET($"{TeamServerAddr}/Implants/{CurrentImplant}/tasks/{taskId.Id}");
                var taskOutrecv = JSONOps.ReturnTaskData(taskOut);
                WriteLine($"Task {taskId.Id} Complete\n");
                Thread.Sleep(1000);

                _out.AppendLine(taskOutrecv.TaskOut);

                return _out.ToString();

            }
            catch (AtlasException e) { return e.Message; }
            //catch (System.Net.WebException) { return $"[-] Connection to teamserver could not be established, verify teamserver is active\n"; }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Newtonsoft.Json;

using Client.Models;

using static Client.Models.Client;

namespace Client.Utils.ImplantUtils
{
    class TasksOut : Models.Util
    {
        private JSON.Classes.TaskRecvOut taskData { get; set; } 

        public override string UtilName => "TasksOut";

        public override string Desc => "Return all tasks for given implant";

        public override string UtilExecute(string[] opts)
        {
            try
            {
                StringBuilder _out = new StringBuilder();

                var tasks = Comms.comms.SendGET($"{TeamServerAddr}/Implants/{CurrentImplant}/tasks").TrimStart('[').TrimEnd(']');
                if (tasks.Length == 0) { throw new AtlasException("[*] No tasks to view\n"); }

                if (tasks.Contains("},{")) { tasks = tasks.Replace("},{", "}&{"); }

                var taskList = tasks.Split('&');

                foreach (var _task in taskList)
                {
                    taskData = JsonConvert.DeserializeObject<JSON.Classes.TaskRecvOut>(_task);
                    _out.AppendLine($"{taskData.Id} {TaskName}");
                }

                return _out.ToString();

            } catch (AtlasException e) { return e.Message; }
            catch (System.Net.WebException) { return $"[-] Connection to teamserver could not be established or no implant currently set\n"; }

        }
    }
}

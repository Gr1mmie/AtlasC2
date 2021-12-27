using System.Text;

using Client.Models;

using static Client.Utils.TaskOps;
using static Client.Models.Client;

namespace Client.Utils
{
    class TaskOpts : Models.Util
    {
        public override string UtilName => "TaskOpts";
        public override string Desc => "View task options";
        public override string UtilExecute(string[] opts)
        {
            try
            {
                StringBuilder _out = new StringBuilder();

                var options = ReturnMethod();

                _out.AppendLine($"Task Options ({TaskName})\n");
                _out.AppendLine($"{"Name",-25} {"Value",-35} {"Description",-50}");
                _out.AppendLine($"{"----",-25} {"-----",-35} {"-----------",-50}");

                foreach (var opt in options)
                {
                    _out.AppendLine($"{opt.GetPropertyValue("Name"),-25} {opt.GetPropertyValue("Value"),-35} {opt.GetPropertyValue("Desc"),-50}");
                }

                _out.AppendLine();

                return _out.ToString().Trim('\n');

            } catch (AtlasException e) { return e.Message; }
        }
    }
}

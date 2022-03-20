using System.Text;

using static Client.Models.Client;

namespace Client.Utils
{
    class AdminTasks : Models.Util
    {
        public override string UtilName => "Admin";
        public override string Desc => "List Admin tasks";
        public override string UtilExecute(string[] opts)
        {
            StringBuilder _out = new StringBuilder();

            if (_adminTask.Count == 0) { Init.OptInit(); }

            foreach (Models.AdminTask admTask in _adminTask) { _out.AppendLine($"{admTask.TaskName,-25} {admTask.Desc}"); }

            return _out.ToString();
        }
    }
}

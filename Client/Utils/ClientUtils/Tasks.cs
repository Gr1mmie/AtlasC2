using System.Text;

using static Client.Models.Client;

namespace Client.Utils
{
    class Tasks : Models.Util
    {
        public override string UtilName => "Tasks";
        public override string Desc => "List tasks";
        public override string UtilExecute(string[] opts)
        {
            StringBuilder _out = new StringBuilder();

            if (_opts.Count == 0) { Init.OptInit(); }

            foreach (Models.Task task in _opts) { _out.AppendLine($"{task.TaskName,-25} {task.Desc}"); }

            return _out.ToString();
        }
    }
}

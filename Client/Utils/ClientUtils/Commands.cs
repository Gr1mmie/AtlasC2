using System.Text;

using static Client.Models.Client;

namespace Client.Utils
{
    public class Commands : Models.Util
    {
        public override string UtilName => "Commands";

        public override string Desc => "List available commands";

        public override string UtilExecute(string[] opts)
        {
            StringBuilder _out = new StringBuilder();

            if (_utils.Count == 0) { Init.UtilInit(); }
            if (_opts.Count == 0 ) { Init.OptInit(); }
            if (_adminTask.Count == 0) { Init.AdminUtilInit(); }    

            _out.AppendLine("\nClient Utils\n____________\n");
            foreach (Models.Util cmd in _utils){ _out.AppendLine($"{cmd.UtilName,-25} {cmd.Desc}"); }

            _out.AppendLine("\nImplant Tasks\n_____________\n");
            foreach (Models.Task cmd in _opts) { _out.AppendLine($"{cmd.TaskName, -25} {cmd.Desc}"); }

            _out.AppendLine("\nImplant Admin Tasks\n___________________\n");
            foreach(Models.AdminTask cmd in _adminTask) { _out.AppendLine($"{cmd.TaskName,-25} {cmd.Desc}"); }

            return _out.ToString();
        }
    }
}

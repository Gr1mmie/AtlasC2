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

            foreach (Models.Util cmd in _utils){ _out.AppendLine($"{cmd.UtilName,-25} {cmd.Desc}"); }

            // separate based on usage
            /* ie
             * ImplantUtils
             * ------------
             * xyz
             * 
             * ListenerUtils
             * -------------
             * xyz
             * 
             * etc
             */

            return _out.ToString();
        }
    }
}

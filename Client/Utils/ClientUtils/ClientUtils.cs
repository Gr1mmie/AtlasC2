using System.Text;

using static Client.Models.Client;

namespace Client.Utils
{
    class ClientUtil : Models.Util
    {
        public override string UtilName => "Utils";
        public override string Desc => "List available utils";
        public override string UtilExecute(string[] opts)
        {
            StringBuilder _out = new StringBuilder();

            if (_utils.Count == 0) { Init.OptInit(); }

            foreach (Models.Util util in _utils) { _out.AppendLine($"{util.UtilName,-25} {util.Desc}"); }

            return _out.ToString();
        }
    }
}

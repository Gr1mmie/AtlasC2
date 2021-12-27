using System.Text;

using Client.Models;

using static Client.Models.Client;
using static Client.Comms.comms;
using Newtonsoft.Json;

namespace Client.Utils
{
    class ViewListener : Models.Util
    {
        private string ListenerName { get; set; }
        private JSON.Classes.ListenerData ListenerData { get; set; }
        public override string UtilName => "ViewListener";

        public override string Desc => "Return data of specified listener";

        public override string UtilExecute(string[] opts)
        {
            try {

                StringBuilder _out = new StringBuilder();

                if (opts is null) { throw new AtlasException($"[*] Usage: ViewListener [ListenerName]\n"); }

                ListenerName = opts[1];

                var listener = SendGET($"{TeamServerAddr}/Listeners/{ListenerName}");
                ListenerData = JsonConvert.DeserializeObject<JSON.Classes.ListenerData>(listener);

                _out.AppendLine($"{"Name",-25} {"Port",-25}");
                _out.AppendLine($"{"----",-25} {"----",-25}");
                _out.AppendLine($"{ListenerData.Name,-25} {ListenerData.bindPort, -25}");

                return _out.ToString();

            } catch (AtlasException e) { return $"{e.Message}\n"; }
            catch (System.Net.WebException) { return $"[-] Connection to teamserver could not be established, verify teamserver is active\n"; }

        }
    }
}

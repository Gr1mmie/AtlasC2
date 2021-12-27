using System;
using System.Text;


using Client.Models;

using static Client.Models.Client;
using static Client.Comms.comms;

namespace Client.Utils
{
    class StartListener : Models.Util
    {
        private string ListenerName { get; set; }
        private int ListenerPort { get; set; }

        public override string UtilName => "StartListener";

        public override string Desc => "create a new listener";

        public override string UtilExecute(string[] opts)
        {
            try
            {
                StringBuilder _out = new StringBuilder();

                if (opts is null) { throw new AtlasException($"[*] Usage: StartListener [ListenerName] [ListenerPort]\n"); }

                ListenerName = opts[1];
                ListenerPort = Int32.Parse(opts[2]);

                if(ListenerPort < 0 && ListenerPort > 65535) { throw new AtlasException($"[-] ListenerPort must be a valid port"); }

                SendPOST($"{TeamServerAddr}/Listeners", JSONOps.PackStartListenerData(ListenerName, ListenerPort));
                _out.AppendLine($"[*] Started listener {ListenerName} running on port {ListenerPort}");

                return _out.ToString();

            } catch (AtlasException e) { return $"{e.Message}\n"; }
            catch (System.Net.WebException) { return $"[-] Connection to teamserver could not be established, verify teamserver is active\n"; }

        }
    }
}

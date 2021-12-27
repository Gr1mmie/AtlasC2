using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Client.Models;

using static Client.Models.Client;
using static Client.Comms.comms;

namespace Client.Utils
{
    class RemoveListener : Models.Util
    {
        private string ListenerName { get; set; }
        public override string UtilName => "RemoveListener";

        public override string Desc => "Remove a listener";

        public override string UtilExecute(string[] opts)
        {
            try{
                StringBuilder _out = new StringBuilder();

                if (opts is null) { throw new AtlasException($"[*] Usage: ViewListener [ListenerName]\n"); }

                ListenerName = opts[1];

                var _listener = ListenerList.FirstOrDefault(listener => listener.Equals(ListenerName));
                if (_listener is null) { throw new AtlasException($"[-] Listener {ListenerName} does not exist\n"); }

                SendDELETE($"{TeamServerAddr}/Listeners?name={ListenerName}");
                _out.AppendLine($"[*] Removed listener {ListenerName}");

                return _out.ToString();

            } catch (AtlasException e) { return $"{e.Message}"; }
            catch (System.Net.WebException) { return $"[-] Connection to teamserver could not be established, verify teamserver is active\n"; }

        }
    }
}

using System.Linq;
using System.Text;

using Newtonsoft.Json;

using Client.Models;

using static Client.Models.Client;

namespace Client.Utils
{
    class Listeners : Models.Util
    {
        private JSON.Classes.Listeners Listener { get; set; }
        public override string UtilName => "Listeners";

        public override string Desc => "list active listeners";

        public override string UtilExecute(string[] opts)
        {
            try
            {
                StringBuilder _out = new StringBuilder();

                var listeners = Comms.comms.SendGET($"{TeamServerAddr}/Listeners").TrimStart('[').TrimEnd(']');

                if (listeners.Length == 0) { throw new AtlasException("[*] No active listeners"); }

                _out.AppendLine("Name");
                _out.AppendLine("----");

                if (listeners.Contains("},{")) { listeners = listeners.Replace("},{", "}&{"); }
                var listenersList = listeners.Split('&');

                foreach (var listener in listenersList) {
                    Listener = JsonConvert.DeserializeObject<JSON.Classes.Listeners>(listener);
                    ListenerList.Add(Listener.Name);
                    _out.AppendLine(Listener.Name); }

                ListenerList = ListenerList.Distinct().ToList();
                return _out.ToString();

            } catch (AtlasException e) { return $"{e.Message}\n"; }
            catch (System.Net.WebException) { return $"[-] Connection to teamserver could not be established, verify teamserver is active\n"; }

        }
    }
}

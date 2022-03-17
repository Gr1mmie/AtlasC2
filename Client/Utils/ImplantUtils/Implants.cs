using System.Text;
using System.Linq;

using Newtonsoft.Json;

using Client.Models;

using static Client.Models.Client;

namespace Client.Utils
{
    class Implants : Models.Util
    {
        public JSON.Classes.ImplantData ImplantData { get; set; }
        
        public override string UtilName => "Implants";
        public override string Desc => "List connected implants";
        public override string UtilExecute(string[] opts)
        {
            try
            {
                StringBuilder _out = new StringBuilder();
                
                string implants = Comms.comms.SendGET($"{TeamServerAddr}/Implants").TrimStart('[').TrimEnd(']');
                if (implants.Length == 0) { throw new AtlasException("[*] No active implants\n"); }

                _out.AppendLine($"{"ImplantId",-20} {"Hostname",-25} {"Intergity",-20} {"LastSeen",-20}");
                _out.AppendLine($"{"----------",-20} {"--------",-25} {"---------",-20} {"--------",-20}");

                if (implants.Contains("},{")) { implants = implants.Replace("},{", "}&{"); }

                var implantList = implants.Split('&');

                foreach(var _implant in implantList){
                    ImplantData = JsonConvert.DeserializeObject<JSON.Classes.ImplantData>(_implant);
                    string lastSeen = $"{ImplantData.lastSeen.Split("T")[1].Split(".")[0]} {ImplantData.lastSeen.Split("T")[0]}";
                    ImplantList.Add(ImplantData.data.id);
                    _out.AppendLine($"{ImplantData.data.id,-20} {ImplantData.data.hostName, -25} {ImplantData.data.integrity, -20}" +
                        $" {lastSeen, -20} ");
                }

                ImplantList = ImplantList.Distinct().ToList();
                return _out.ToString();

            }
            catch (AtlasException e) { return $"{e.Message}"; }
            catch (System.Net.WebException) { return $"[-] Connection to teamserver could not be established, verify teamserver is active\n"; } 

        }

    }
}

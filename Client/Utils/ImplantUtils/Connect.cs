using System.Linq;
using System.Text;

using Newtonsoft.Json;

using Client.Models;

using static Client.Models.Client;

namespace Client.Utils
{
    class Connect : Models.Util
    {
        public override string UtilName => "Connect";
        public override string Desc => "Select an implant to interface with";
        public override string UtilExecute(string[] opts){
            try
            {
                if (opts is null) { throw new AtlasException($"[-] No parameters passed\nUsage: Connect [implantName]\n"); }
                if(opts.Length > 2) { throw new AtlasException($"[*] Incorrect parameters passed\nUsage: Connect [implantName]\n"); }

                StringBuilder _out = new StringBuilder();

                var implantName = opts[1];
                var _implant = ImplantList.FirstOrDefault(implant => implant.Equals(implantName));
                if(_implant is null) { throw new AtlasException($"[-] Implant {implantName} does not exist"); }

                var _implantJSON = Comms.comms.SendGET($"{TeamServerAddr}/implants/{_implant}").TrimStart('[').TrimEnd(']');
                var _implantData = JsonConvert.DeserializeObject<JSON.Classes.ImplantData>(_implantJSON);

                CurrentImplant = _implant;
                ImplantAddr = _implantData.data.IPAddr;

                _out.AppendLine($"[*] Set current implant to {CurrentImplant}");
                return _out.ToString();

            } catch (AtlasException e) { return $"{e.Message}"; }

        }
    }
}

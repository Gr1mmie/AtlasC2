using System.Text;

using Newtonsoft.Json;

using Client.Models;

using static Client.Models.Client;

namespace Client.Utils.ImplantUtils
{
    class ViewImplant : Models.Util
    {
        public override string UtilName => "ViewImplant";

        public override string Desc => "Returns all data from specified implant";

        public override string UtilExecute(string[] opts)
        {
            try
            {
                if (CurrentImplant is null) { throw new AtlasException($"[-] No implant selected"); }

                StringBuilder _out = new StringBuilder();

                var _implantJSON = Comms.comms.SendGET($"{TeamServerAddr}/implants/{CurrentImplant}").TrimStart('[').TrimEnd(']');
                var _implantData = JsonConvert.DeserializeObject<JSON.Classes.ImplantData>(_implantJSON);

                _out.AppendLine($"{nameof(_implantData.data.id),-25} {_implantData.data.id}\n" +
                    $"{nameof(_implantData.data.user),-25} {_implantData.data.user}\n" +
                    $"{nameof(_implantData.data.hostName),-25} {_implantData.data.hostName}\n" +
                    $"{nameof(_implantData.data.integrity),-25} {_implantData.data.integrity}\n" +
                    $"{nameof(_implantData.data.arch),-25} {_implantData.data.arch}\n" +
                    $"{nameof(_implantData.data.procID),-25} {_implantData.data.procID}\n" +
                    $"{nameof(_implantData.data.procName),-25} {_implantData.data.procName}\n" +
                    $"{nameof(_implantData.data.IPAddr),-25} {_implantData.data.IPAddr}\n" +
                    $"{nameof(_implantData.lastSeen),-25} {_implantData.lastSeen}"
                    );

                return _out.ToString();
            } catch (AtlasException e) { return e.Message; }
            catch (System.Net.WebException) { return $"[-] Connection to teamserver could not be established, verify teamserver is active\n"; }

        }
    }
}

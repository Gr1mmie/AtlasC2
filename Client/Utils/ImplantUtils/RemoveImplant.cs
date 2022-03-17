using System;
using System.Text;
using System.Linq;

using Client.Models;

using static Client.Models.Client;

namespace Client.Utils
{
    public class RemoveImplant : Models.Util
    {
        public override string UtilName => "RemoveImplant";

        public override string Desc => "Remove implant from implant list";

        public override string UtilExecute(string[] opts)
        {
            if (opts is null) { throw new AtlasException($"[-] No parameters passed\nUsage: RemoveImplant [implantName]\n"); }
            if (opts.Length > 2) { throw new AtlasException($"[*] Incorrect parameters passed\nUsage: RemoveImplant [implantName]\n"); }

            StringBuilder outData = new StringBuilder();

            var implantName = opts[1];
            var _implant = ImplantList.FirstOrDefault(implant => implant.Equals(implantName));
            if (_implant is null) { throw new AtlasException($"[-] Implant {implantName} does not exist"); }
            
            //TaskOps.sendAdminUtil("KillSwitch");

            Comms.comms.SendDELETE($"{TeamServerAddr}/Implants/{implantName}");

            outData.AppendLine($"[*] Implant {implantName} successfully removed");

            return outData.ToString();
        }
    }
}

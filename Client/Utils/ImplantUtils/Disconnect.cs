using Client.Models;

namespace Client.Utils
{
    class Disconnect : Models.Util
    {
        public override string UtilName => "Disconnect";
        public override string Desc => "Disconnect an implant";
        public override string UtilExecute(string[] opts)
        {
            try
            {
                if(Models.Client.CurrentImplant is null) { throw new AtlasException($"[-] No current implant set\n"); }

                Models.Client.CurrentImplant = null;
                Models.Client.ImplantAddr = null;
                return "";

            } catch (AtlasException e) { return e.Message; }
        }
    }
}

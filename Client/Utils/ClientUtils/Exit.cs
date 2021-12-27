using System;

namespace Client.Utils
{
    class Exit : Models.Util
    {
        public override string UtilName => "Exit";
        public override string Desc => "Exit AtlasC2";
        public override string UtilExecute(string[] opts){ Environment.Exit(0); return ""; }
    }
}

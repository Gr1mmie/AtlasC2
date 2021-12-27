using System;

namespace Client.Utils
{
    class Clear : Models.Util
    {
        public override string UtilName => "Clear";
        public override string Desc => "Clears screen";
        public override string UtilExecute(string[] opts) { Console.Clear(); return ""; }
    }
}

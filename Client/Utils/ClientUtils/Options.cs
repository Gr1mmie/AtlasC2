using System.Text;

using static Client.Models.Client;

namespace Client.Utils
{
    class Options : Models.Util
    {
        public override string UtilName => "Options";
        public override string Desc => "List AtlasC2 Options";
        public override string UtilExecute(string[] opts)
        {
            StringBuilder _out = new StringBuilder();

            _out.AppendLine(
                "TeamServer:\n" +
                $"\t{nameof(TeamServerAddr)}:{TeamServerAddr, 25}\n" +
                "Implant:\n" +
                $"\t{nameof(CurrentImplant)}:{CurrentImplant, 12}\n" +
                $"\t{nameof(ImplantAddr)}:{ImplantAddr, 19}\n"+
                "Tasks:\n" +
                $"\t{nameof(TaskName)}:{TaskName, 14}\n"
                //$"\t{nameof(Debug)}:{Debug, 18}"
            );


            return _out.ToString();
        }
    }
}

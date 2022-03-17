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
                $"\t{nameof(TeamServerAddr), -15}: {TeamServerAddr}\n" +
                "Implant:\n" +
                $"\t{nameof(CurrentImplant), -15}: {CurrentImplant}\n" +
                $"\t{nameof(ImplantAddr), -15}: {ImplantAddr}\n" +
                "Tasks:\n" +
                $"\t{nameof(TaskName), -15}: {TaskName}\n"
                );

            return _out.ToString();
        }
    }
}

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

            var len = nameof(TeamServerAddr).Length + 25;

            _out.AppendLine(
                "TeamServer:\n" +
                $"\t{nameof(TeamServerAddr)} : {TeamServerAddr.Align(len) }\n" +
                "Implant:\n" +
                $"\t{nameof(CurrentImplant)} : {CurrentImplant.Align(len)}\n" +
                $"\t{nameof(ImplantAddr)} : {ImplantAddr.Align(len)}\n"
                );

            if (TaskName is not null)
            {
                _out.AppendLine(
                    "Tasks:\n" +
                    $"\t{nameof(TaskName)} : {TaskName.Align(len)}\n"
                );
            }

            return _out.ToString();
        }
    }
}

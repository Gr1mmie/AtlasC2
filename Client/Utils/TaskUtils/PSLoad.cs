using System.Collections.Generic;

using static Client.Models.TaskOptions;

namespace Client.Utils
{
    class PSLoad : Models.Task
    {
        public override string TaskName => "PSLoad";

        public override string Desc => "Load a PowerShell file into the implant process";

        public override List<object> OptList { get; } = new List<object> { psFile };
    }
}

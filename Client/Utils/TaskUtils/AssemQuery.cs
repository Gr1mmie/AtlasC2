using System.Collections.Generic;

using static Client.Models.TaskOptions;

namespace Client.Utils
{
    class AssemQuery : Models.Task
    {
        public override string TaskName => "AssemQuery";
        public override string Desc => "Return all assem type and methods in the implant process";
        public override List<object> OptList { get; } = new List<object> { };
    }
}

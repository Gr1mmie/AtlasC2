using System.Collections.Generic;

using static Client.Models.TaskOptions;

namespace Client.Utils
{
    class ExecuteAssem : Models.Task
    {
        public override string TaskName => "ExecuteAssem";
        public override string Desc => "Execute a specifed assem type";
        public override List<object> OptList { get; } = new List<object> { assemType };
    }
}

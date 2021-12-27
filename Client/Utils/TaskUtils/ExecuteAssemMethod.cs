using System;
using System.Collections.Generic;

using static Client.Models.TaskOptions;

namespace Client.Utils
{
    class ExecuteAssemMethod : Models.Task
    {
        public override string TaskName => "ExecuteAssemMethod";
        public override string Desc => "Executes specified method belonging to a loaded assem type";
        public override List<Object> OptList { get; } = new List<object> { assemType, assemMethod };
    }
}

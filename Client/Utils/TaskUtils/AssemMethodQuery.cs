using System.Collections.Generic;

using static Client.Models.TaskOptions;

namespace Client.Utils
{
    class AssemMethodQuery : Models.Task
    {
        public override string TaskName => "AssemMethodQuery";
        public override string Desc => "Returns all methods belonging to the specified assem name";
        public override List<object> OptList { get; } = new List<object> { assemName };
    }
}

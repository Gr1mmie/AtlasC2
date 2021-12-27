using System.Collections.Generic;

using static Client.Models.TaskOptions;

namespace Client.Utils
{
    class PSShell : Models.Task
    {
        public override string TaskName => "PSShell";
        public override string Desc => "Execute a PS command using the PS DLLs";
        public override List<object> OptList { get; } = new List<object> { command, encode };
    }
}

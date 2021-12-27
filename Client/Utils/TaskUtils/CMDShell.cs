using System.Collections.Generic;

using static Client.Models.TaskOptions;

namespace Client.Utils
{
    class CMDShell : Models.Task
    {
        public override string TaskName => "CMDShell";
        public override string Desc => "Executes a command in the context of cmd.exe";
        public override List<object> OptList { get; } = new List<object> { command };
    }
}

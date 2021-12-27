using System.Collections.Generic;

using Implant.Models;

using static Implant.Models.ImplantTaskOptions;

namespace Implant.Tasks.Options
{
    class CMDShell : ImplantOptions
    {
        public override string TaskName => "CMDShell";

        public override List<object> Data => new List<object> { command };
    }
}

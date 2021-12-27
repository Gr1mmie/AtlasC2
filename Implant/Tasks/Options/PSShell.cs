using System.Collections.Generic;

using Implant.Models;

using static Implant.Models.ImplantTaskOptions;

namespace Implant.Tasks.Options
{
    class PSShell : ImplantOptions
    {
        public override string TaskName => "PSShell";

        public override List<object> Data => new List<object> { command };
    }
}
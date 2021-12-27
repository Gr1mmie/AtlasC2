using Implant.Models;
using System;
using System.Collections.Generic;

using static Implant.Models.ImplantTaskOptions;

namespace Implant.Tasks.Options
{
    class AssemQuery : ImplantOptions
    {
        public override string TaskName => "AssemQuery";

        public override List<object> Data => new List<object> { assemType };
    }
}

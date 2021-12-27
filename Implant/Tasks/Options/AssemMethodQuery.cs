using Implant.Models;
using System;
using System.Collections.Generic;

using static Implant.Models.ImplantTaskOptions;

namespace Implant.Tasks.Options
{
    class AssemMethodQuery : ImplantOptions
    {
        public override string TaskName => "AssemMethodQuery";

        public override List<object> Data => new List<object> { assemType, assemMethod};
    }
}

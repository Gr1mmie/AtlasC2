using Implant.Models;
using System;
using System.Collections.Generic;

using static Implant.Models.ImplantTaskOptions;

namespace Implant.Tasks.Options
{
    class ExecuteAssemMethod : ImplantOptions
    {
        public override string TaskName => "ExecuteAssemMethod";         public override List<object> Data => new List<object> { assemType, assemMethod };    }
}

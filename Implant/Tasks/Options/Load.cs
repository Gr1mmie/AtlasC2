using Implant.Models;
using System;
using System.Collections.Generic;

using static Implant.Models.ImplantTaskOptions;

namespace Implant.Tasks.Options
{
    class Load : ImplantOptions
    {
        public override string TaskName => "Load";

        public override List<object> Data => new List<object> { assemPath };
    }
}

using Implant.Models;
using System;
using System.Collections.Generic;

using static Implant.Models.ImplantTaskOptions;

namespace Implant.Tasks.Options
{
    class ExecuteAssem : ImplantOptions
    {
        public override string TaskName => "ExecuteAssem";
        public override List<object> Data => new List<object> { assemName, assemParams };    }
}

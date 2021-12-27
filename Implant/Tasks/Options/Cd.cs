using System.Collections.Generic;

using Implant.Models;

using static Implant.Models.ImplantTaskOptions;

namespace Implant.Tasks.Options
{
    class Cd : ImplantOptions
    {
        public override string TaskName => "Cd";
        public override List<object> Data => new List<object> { path };
    }
}

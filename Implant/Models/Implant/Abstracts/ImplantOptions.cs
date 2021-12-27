using System;
using System.Collections.Generic;

namespace Implant.Models
{
    public abstract class ImplantOptions
    {
        public abstract string TaskName { get; }
        public abstract List<Object> Data { get; }
    }
}

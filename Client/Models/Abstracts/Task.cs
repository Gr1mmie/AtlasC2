using System;
using System.Collections.Generic;

namespace Client.Models
{
    abstract class Task
    {
        public abstract string TaskName { get; }
        public abstract string Desc { get; }
        public abstract List<Object> OptList { get; }

    }
}
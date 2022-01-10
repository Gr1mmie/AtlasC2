using System;
using System.Collections.Generic;

namespace Implant.JSON
{
    public class Classes
    {
        [Serializable]
        public class TaskArgs
        {
            public string OptionName { get; set; }
            public string OptionValue { get; set; }
        }

        [Serializable]
        public class ArgsRecv
        {
            public List<TaskArgs> Params { get; set; }
        }

    }
}

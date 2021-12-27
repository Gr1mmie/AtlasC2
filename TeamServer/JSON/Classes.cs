using System.Collections.Generic;

namespace TeamServer.JSON
{
    public class Classes
    {
        public class TaskArgs
        {
            public string OptionName { get; set; }
            public string OptionValue { get; set; }
        }

        public class ArgsRecv
        {
            public List<TaskArgs> Params { get; set; }
        }
    }
}

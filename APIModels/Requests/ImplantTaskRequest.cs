using System;
using System.Collections.Generic;

namespace APIModels.Requests
{
    [Serializable]
    public class TaskArgs
    {
        public string OptionName { get; set; }
        public string OptionValue { get; set; }
    }

    [Serializable]
    public class ArgsRecv{
        public List<TaskArgs> Params { get; set; }
    }

    public class ImplantTaskRequest
    {
        public string Command { get; set; }
        public string Args { get; set; }
        public string File { get; set; }
    }
}

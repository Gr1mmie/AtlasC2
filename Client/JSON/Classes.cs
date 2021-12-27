using System;
using System.Collections.Generic;
using System.Net;

namespace Client.JSON
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

        [Serializable]
        public class ArgsData
        {
            //public string Taskname { get; set; }
            public List<TaskArgs> Params { get; set; }
        }

        [Serializable]
        public class TaskSend
        {
            public string Command { get; set; }
            public string Args { get; set; }
            public string File { get; set; }
            //public byte[] File { get; set; }
        }

        [Serializable]
        public class TaskSendOut
        {
            public string Id { get; set; }
            public string Command { get; set; }
            public string Args { get; set; }
            public string File { get; set; }
            //public byte[] File { get; set; }

        }

        [Serializable]
        public class TaskRecvOut
        {
            public string Id { get; set; }
            public string TaskOut { get;set; }
        }

        [Serializable]
        public class StartListenerData
        {
            public string name { get; set; }
            public int bindPort { get; set; }
        }

        [Serializable]
        public class IData
        {
            public string id { get; set; }
            public string hostName { get; set; }
            public string user { get; set; }
            public string procName { get; set; }
            public string procID { get; set; }
            public string integrity { get; set; }
            public string arch { get; set; }
            public string IPAddr { get; set; }
        }

        [Serializable]
        public class ImplantData
        {
            public IData data { get; set; }
            public string lastSeen { get; set; }
        }

        [Serializable]
        public class Listeners
        {
            public string Name { get; set; }
        }

        [Serializable]
        public class ListenerData
        {
            public string Name { get; set; }
            public int bindPort { get; set; }
        }
    }
}

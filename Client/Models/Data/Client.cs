using System;
using System.Collections.Generic;
using System.Net;

namespace Client.Models
{
    class Client
    {
        public static bool Debug { get; set; } = false;
        public static string TaskName { get; set; }
        public static string argData { get; set; }
        public static string sendData { get; set; }
        public static byte[] assemBytes { get; set; }
        public static string TeamServerAddr { get; set; }
        public static string ConnectAddr { get; set; }
        public static string CurrentImplant { get; set; }
        public static string ImplantAddr { get; set; }
        //public static bool ImplantStatus { get; set; }

        public static bool running = true;
        public static string Prompt = "> ";
        public const string Ver = "v0.1";

        public static List<String> ImplantList = new List<String>();
        public static List<String> ListenerList = new List<String>();
        public static List<String> Tasks = new List<String>();

        public static readonly List<Util> _utils = new List<Util>();
        public static readonly List<Task> _opts = new List<Task>();
        public static readonly List<AdminTask> _adminTask = new List<AdminTask>();

    }
}

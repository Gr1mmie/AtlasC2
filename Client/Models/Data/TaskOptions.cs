namespace Client.Models
{
    class TaskOptions
    {
        // https://www.youtube.com/watch?v=C6lhpNkw6H4

        public static object assemName = new AssemName();
        public static object assemType = new AssemType();
        public static object assemMethod = new AssemMethod();
        public static object assemParams = new AssemParams();
        public static object retryCount = new Retry_count();
        public static object timeout = new Timeout();
        public static object command = new Command();
        public static object assemBytes = new AssemBytes();
        public static object psFile = new PSFile();
        public static object debug = new Debug();
        public static object encode = new Encode();

        public class Encode
        {
            public string Name { get; set; } = nameof(encode);
            public bool Value { get; set; } = false;
            public string Desc { get; set; } = "encode PowerShell command";
        }

        public class Debug
        {
            public string Name { get; set; } = nameof(debug);
            public bool Value { get; set; } = false;
            public string Desc { get; set; } = "verbose output";
        }

        public class PSFile
        {
            public string Name { get; set; } = nameof(psFile);
            public string Value { get; set; } = "";
            public string Desc { get; set; } = "path to PowerShell file to load into implant process";
        }

        public class AssemBytes
        {
            public string Name { get; set; } = nameof(assemBytes);
            public byte[] Value { get; set; } = Client.assemBytes;
            public string Desc { get; set; } = "byte array to load into implant process";
        }

        public class Command
        {
            public string Name { get; set; } = nameof(command);
            public string Value { get; set; } = "";
            public string Desc { get; set; } = "command to execute";
        }

        public class Retry_count {
            public string Name { get; set; } = nameof(retryCount);
            public int Value { get; set; } = 3;
            public string Desc { get; set; } = "set retry count";
        }
        public class Timeout {
            public string Name { get; set; } = nameof(timeout);
            public int Value { get; set; } = 5;
            public string Desc { get; set; } = "set timeout";
        }

        public class AssemName
        {
            public string Name { get; set; } = nameof(assemName);
            public string Value { get; set; } = "";
            public string Desc { get; set; } = "select assembly name";
        }

        public class AssemType {
            public string Name { get; set; } = nameof(assemType);
            public string Value { get; set; } = "";
            public string Desc { get; set; } = "select assembly type";
        }
        
        public class AssemMethod {

            public string Name { get; set; } = nameof(assemMethod);
            public string Value { get; set; } = "";
            public string Desc { get; set; } = "select assembly method";
        }

        public class AssemParams
        {
            public string Name { get; set; } = nameof(assemParams);
            public string Value { get; set; } = "";
            public string Desc { get; set; } = "parameters to pass to loaded assembly";
        }

    }
}

namespace Implant.Models
{
    public class ImplantTaskOptions
    {
        public static object command = new Command();
        public static object path = new Path();
        public static object assemName = new AssemName();
        public static object assemPath = new AssemPath();
        public static object assemType = new AssemType();
        public static object assemParams = new AssemParams();
        public static object assemMethod = new AssemMethod();

        public class Command
        {
            public string Name { get; set; } = "command";
            public string Value { get; set; } = "";
        }

        public class Path
        {
            public string Name { get; set; } = "path";
            public string Value { get; set; } = "";
        }

        public class AssemPath
        {
            public string Name { get; set; } = "assemPath";
            public string Value { get; set; } = "";
        }

        public class AssemName
        {
            public string Name { get; set; } = "assemName";
            public string Value { get; set; } = "";
        }

        public class AssemType
        {
            public string Name { get; set; } = "assemType";
            public string Value { get; set; } = "";
        }

        public class AssemMethod
        {
            public string Name { get; set; } = "assemMethod";
            public string Value { get; set; } = "";
        }

        public class AssemParams
        {
            public string Name { get; set; } = "assemParams";
            public string Value { get; set; } = "";
        }

    }
}

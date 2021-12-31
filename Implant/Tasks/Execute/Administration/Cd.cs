using Implant.Models;
using System;
using System.IO;
using System.Text;

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

                if (path is null || path == ""){
                    path = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
                }

                Directory.SetCurrentDirectory(path);

                return $"[*] Path set to {Directory.GetCurrentDirectory()}";
            } catch (DirectoryNotFoundException) { return $"{path} is not a valid path"; }
        }
    }
}

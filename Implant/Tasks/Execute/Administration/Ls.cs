using System.IO;
using System.Text;

using Implant.Models;

using static Implant.Utils.Extensions;

namespace Implant.Tasks.Execute
{
    class Ls : ImplantCommands
    {
        private string targetPath { get; set; }     
        
        private int fileNameLen { get; set; }
        private int fileSizeLen { get; set; }
        private int dirNameLen { get; set; }
        
        public override string Name => "Ls";

        public override string Execute(ImplantTask task)
        {

            targetPath = task.Args;

            StringBuilder _out = new StringBuilder();

            if (targetPath is null || targetPath == ""){
                targetPath = Directory.GetCurrentDirectory();
            }


            var dirs = Directory.GetDirectories(targetPath);
            var files = Directory.GetFiles(targetPath);

            dirNameLen = lsParse.getMaxDirLen(dirs);
            fileNameLen = lsParse.getMaxFileLen(files);

            if (dirNameLen < fileNameLen) {
                _out.AppendLine($"{"Name".Align(fileNameLen)} {"Length".Align(fileNameLen + 5)}");
                _out.AppendLine($"{"----".Align(fileNameLen)} {"------".Align(fileNameLen + 5)}");
            } else {
                _out.AppendLine($"{"Name".Align(dirNameLen)} {"Length".Align(dirNameLen + 5)}");
                _out.AppendLine($"{"----".Align(dirNameLen)} {"------".Align(dirNameLen + 5)}");
            }

            foreach (var dir in dirs){
                var dirData = new DirectoryInfo(dir);
                _out.AppendLine($"{dirData.Name.Align(dirNameLen)}");
            }

            foreach (var file in files) { 
                var fileData = new FileInfo(file);

                if (dirNameLen < fileNameLen){ _out.AppendLine($"{fileData.Name.Align(fileNameLen)} {fileData.Length.Align(fileSizeLen + 5)}"); }
                else{ _out.AppendLine($"{fileData.Name.Align(dirNameLen)} {fileData.Length.Align(dirNameLen + 5)}"); }

            }

            _out.AppendLine();  
            return _out.ToString();
        }
    }

    public sealed class lsParse
    {
        public static int getMaxDirLen(string[] dirs) {
            int maxDirNameLen = 0;

            foreach (var dir in dirs) {
                var dirData = new DirectoryInfo(dir);
                if (dirData.Name.Length > maxDirNameLen) { 
                    maxDirNameLen = dir.Length;
                }
            }

            return maxDirNameLen + 5;
        }

        public static int getMaxFileLen(string[] files) {
            int maxfileNameLen = 0;
            
            foreach (var file in files) {
                var fileData = new FileInfo(file);
                if(fileData.Name.Length > maxfileNameLen){
                    maxfileNameLen = fileData.Name.Length;
                }   
            }

            return maxfileNameLen + 5; 
        }
    }
}

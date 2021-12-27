using Implant.Models;
using Implant.Utils;

namespace Implant.Tasks.Execute
{
    class ExecuteAssem : ImplantCommands
    {
        private string assemType { get; set; }

        public override string Name => "ExecuteAssem";
        public override string Execute(ImplantTask task)
        {
            var opts = ImplantOptionUtils.ReturnMethod(task);
            var args = ImplantOptionUtils.ParseArgs(task.Args);

            foreach (var opt in opts)
            {
                foreach (var _params in args.Params)
                {
                    if ((_params.OptionName.ToLower() is "assemtype")
                        && (_params.OptionName.ToLower() == opt.GetPropertyValue("Name").ToString().ToLower()))
                    {
                        assemType = _params.OptionValue;
                    }
                }
            }

            return assemType;
        }    }
}

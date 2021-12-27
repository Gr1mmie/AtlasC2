using System.Text;

using Implant.Models;

namespace Implant.Tasks.Execute
{
    class AssemQuery : ImplantCommands
    {
        public override string Name => "AssemQuery";

        public override string Execute(ImplantTask task)
        {
            StringBuilder _out = new StringBuilder();

            _out.AppendLine(LoadFunctions.GetAssems());
            return _out.ToString();
        }
    }
}

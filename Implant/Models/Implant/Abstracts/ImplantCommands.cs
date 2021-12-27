namespace Implant.Models
{
    public abstract class ImplantCommands
    {
        public abstract string Name { get; }
        public abstract string Execute(ImplantTask task);
    }
}

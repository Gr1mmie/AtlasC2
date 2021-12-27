using System.Threading.Tasks;

using TeamServer.Services;

namespace TeamServer.Models
{
    public abstract class Listener
    {
        public abstract string Name { get; }
        protected IImplantService ImplantSvc { get; set; }

        public void Init(IImplantService implantSvc) { ImplantSvc = implantSvc; }

        public abstract Task Start();
        public abstract void Stop();
    }
}

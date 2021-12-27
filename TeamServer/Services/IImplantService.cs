using System.Collections.Generic;
using System.Linq;
using TeamServer.Models;

namespace TeamServer.Services
{
    public interface IImplantService{
        void AddImplant(Implant implant);
        IEnumerable<Implant> GetImplants();
        Implant GetImplant(string id);
        void PurgeImplant(Implant implant);
    }

    public class ImplantService : IImplantService
    {
        private readonly List<Implant> _implants = new();
        public void AddImplant(Implant implant) { _implants.Add(implant); }

        public Implant GetImplant(string id) { return GetImplants().FirstOrDefault(implant => implant.Data.ID.Equals(id)); }

        public IEnumerable<Implant> GetImplants() { return _implants; }

        public void PurgeImplant(Implant implant) { _implants.Remove(implant); }
        
    }

}

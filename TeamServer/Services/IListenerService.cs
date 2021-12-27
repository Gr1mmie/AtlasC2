using System;
using System.Collections.Generic;
using System.Linq;

using TeamServer.Models;

namespace TeamServer.Services
{
    public interface IListenerService{
        void AddListener(Listener listener);
        IEnumerable<Listener> GetListeners();
        Listener GetListener(string name);
        void PurgeListener(Listener listener);
    }

    public class ListenerService : IListenerService
    {
        //add to files or something later
        public static readonly List<Listener> _listeners = new List<Listener>();
        public void AddListener(Listener listener) { _listeners.Add(listener); }

        public Listener GetListener(string name){
            return GetListeners().FirstOrDefault(listener => listener.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase));
        }

        public IEnumerable<Listener> GetListeners(){ return _listeners; }

        public void PurgeListener(Listener listener){ _listeners.Remove(listener); }
    }
}

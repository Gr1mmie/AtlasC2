using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Implant.Models
{
    public abstract class Comms
    {
        public abstract Task Start();
        public abstract void Stop();

        protected ConcurrentQueue<ImplantTask> TaskInbound = new ConcurrentQueue<ImplantTask>();
        protected ConcurrentQueue<ImplantTaskOut> TaskOut = new ConcurrentQueue<ImplantTaskOut>();

        protected ImplantData _implantData;

        public virtual void ImplantInit(ImplantData implantData) { _implantData = implantData; }

        protected IEnumerable<ImplantTaskOut> GetTaskOut() {
            var taskOut = new List<ImplantTaskOut>();

            while (TaskOut.TryDequeue(out var tasks)) { taskOut.Add(tasks); }

            return taskOut;
        }

        public bool DataRecv(out IEnumerable<ImplantTask> tasks) {
            if (TaskInbound.IsEmpty) { tasks = null; return false; }

            var taskList = new List<ImplantTask>();

            while(TaskInbound.TryDequeue(out var task)) { taskList.Add(task); }

            tasks = taskList;
            return true;
        }

        public void DataSend(ImplantTaskOut taskOut) { TaskOut.Enqueue(taskOut); }

    }
}

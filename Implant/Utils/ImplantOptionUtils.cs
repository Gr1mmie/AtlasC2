using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using Implant.Models;
using Newtonsoft.Json;
using static Implant.JSON.Classes;
using static Implant.Models.ImplantTaskData;

namespace Implant.Utils
{
    public class ImplantOptionUtils
    {

        public static void OptsInit()
        {
            foreach (Type type in Assembly.GetExecutingAssembly().GetTypes())
            {
                if (type.IsSubclassOf(typeof(ImplantOptions)))
                {
                    ImplantOptions function = Activator.CreateInstance(type) as ImplantOptions;
                    _opts.Add(function);
                }
            }
        }

        public static List<Object> ReturnMethod(ImplantTask task)
        {
            if (_opts.Count == 0) { OptsInit(); }

            ImplantOptions opt = _opts.FirstOrDefault(u => u.TaskName.Equals(task.Command, StringComparison.InvariantCultureIgnoreCase));

            return opt.Data;
        }

        public static ArgsRecv ParseArgs(string jsonData){
            return JsonConvert.DeserializeObject<ArgsRecv>(jsonData);
        }

        /*
        public static ArgsRecv ParseArgs(byte[] jsonData)
        {
            return JsonConvert.DeserializeObject<ArgsRecv>(jsonData);
        }
        */
        
    }
}

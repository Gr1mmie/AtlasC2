using System;

using Microsoft.AspNetCore.Mvc;

using APIModels.Requests;

using TeamServer.Services;
using TeamServer.Models;

namespace TeamServer.Controllers.Implants
{
    [ApiController]
    [Route("[controller]")]
    public class ImplantsController : ControllerBase
    {
        private readonly IImplantService _implants;

        public ImplantsController(IImplantService implants) { _implants = implants; }

        [HttpGet]
        public IActionResult GetImplants(){
            var implants = _implants.GetImplants();
            return Ok(implants);
        }

        [HttpGet("{implantId}")]
        public IActionResult GetImplant(string implantId)
        {
            var implant = _implants.GetImplant(implantId);
            if (implant is null) { return NotFound($"{implantId} not found"); }

            return Ok(implant);
        }

        [HttpGet("{implantId}/tasks")]
        public IActionResult ReturnTasks(string implantId)
        {
            var implant = _implants.GetImplant(implantId);
            if (implant is null) { return NotFound($"{implantId} not found"); }

            var taskOuts = implant.GetTaskOut();
            return Ok(taskOuts);
        }

        [HttpGet("{implantId}/tasks/{taskId}")]
        public IActionResult ReturnTask(string implantId, string taskId)
        {
            var implant = _implants.GetImplant(implantId);
            if (implant is null) { return NotFound($"{implantId} not found"); }

            var taskOut = implant.ReturnTaskOut(taskId);
            if(taskOut is null) { return NotFound($"{taskId} not found"); }

            return Ok(taskOut);
        }

        [HttpPost("{implantId}")]
        public IActionResult TaskImplant(string implantId, [FromBody] ImplantTaskRequest req)
        {
            var implant = _implants.GetImplant(implantId);
            if(implant is null) { return NotFound($"{implantId} not found"); }

            var task = new ImplantTask() { Id = Guid.NewGuid().ToString() , Command = req.Command , Args = req.Args, File = req.File};
            implant.TaskQueue(task);

            var root = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}{HttpContext.Request.Path}";
            var path = $"{root}/tasks/{task.Id}";

            return Created(path, task);
        }

    }
}

using Microsoft.AspNetCore.Mvc;

using APIModels.Requests;

using TeamServer.Services;
using TeamServer.Models;

namespace TeamServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ListenersController : ControllerBase
    {
        private readonly IListenerService _listeners;
        private readonly IImplantService _implantSvc;

        public ListenersController(IListenerService listeners, IImplantService implantsvc){ 
            _listeners = listeners;
            _implantSvc = implantsvc;
        }

        [HttpGet]
        public IActionResult GetListeners(){
            var listeners = _listeners.GetListeners();
            return Ok(listeners);
        }

        [HttpGet("{name}")]
        public IActionResult GetListener(string name) {
            var listener = _listeners.GetListener(name);

            if (listener is null) { return NotFound($"listener {name} not found"); }

            return Ok(listener);
        }

        [HttpPost]
        public IActionResult StartListener([FromBody] StartHTTPListenerRequest request) {
            var listener = new HTTPListener(request.Name, request.BindPort);

            _listeners.AddListener(listener);

            listener.Init(_implantSvc);
            listener.Start();

            var root = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}{HttpContext.Request.Path}";
            var path = $"{root}/{listener}";

            return Created(path, listener);
        }

        [HttpDelete]
        public IActionResult PurgeListener(string name)
        {
            var listener = _listeners.GetListener(name);
            if(listener is null) { return NotFound($"{listener} not found"); }

            listener.Stop();

            _listeners.PurgeListener(listener);

            return Ok($"{listener.Name} stopped");
        }
    }
}

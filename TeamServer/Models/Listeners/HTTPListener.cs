using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using System.Threading;
using System.Threading.Tasks;

namespace TeamServer.Models
{
    public class HTTPListener : Listener
    {
        public override string Name { get; }
        public int BindPort { get; }

        public CancellationTokenSource _cancelToken;

        public HTTPListener(string name, int port){
            Name = name;
            BindPort = port;
        }

        public override async Task Start()
        {
            var Builder = new HostBuilder().ConfigureWebHostDefaults(host =>
            {
                host.UseUrls($"http://0.0.0.0:{BindPort}");
                host.Configure(ConfigureApplication);
                host.ConfigureServices(ConfigureServices);
            });

            var host = Builder.Build();

            _cancelToken = new CancellationTokenSource();
            host.RunAsync(_cancelToken.Token);
        }

        private void ConfigureServices(IServiceCollection services) { 
            services.AddControllers();
            services.AddSingleton(ImplantSvc);
        }

        private void ConfigureApplication(IApplicationBuilder app){
            app.UseRouting();
            app.UseEndpoints(endpoint => {
                // could have it so that TS only accept conns to xyz paths
                endpoint.MapControllerRoute("/", "/", new { controller = "HTTPListener", action = "HandleImplant"});
            });
        }

        public override void Stop(){ _cancelToken.Cancel(); } 
    }
}

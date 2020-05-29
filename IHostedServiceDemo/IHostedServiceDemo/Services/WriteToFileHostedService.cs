using Microsoft.Extensions.Hosting;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace IHostedServiceDemo.Services
{
    public class WriteToFileHostedService : IHostedService, IDisposable
    {
        private readonly IHostingEnvironment environment;
        private readonly string fileName = "File_1.txt";
        private Timer timer;
        public WriteToFileHostedService(IHostingEnvironment environment)
        {
            this.environment = environment;
        }

        public void Dispose()
        {
            timer?.Dispose();
        }

        /**
         * Se ejecuta cuando se inicializa la aplicación
         */
        public Task StartAsync(CancellationToken cancellationToken)
        {
            WriteToFile("WriteToFileHostedService: Process Started");
            timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(5));
            return Task.CompletedTask;
        }
        /**
         * Se ejecuta cuando se detiene la aplicación
         */
        public Task StopAsync(CancellationToken cancellationToken)
        {
            WriteToFile("WriteToFileHostedService: Process Stopped");
            timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }
        private void DoWork(object state)
        {
            WriteToFile("WriteToFileHostedService: Doing some work at "+DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss"));
        }
        private void WriteToFile(string message)
        {
            var path = $@"{environment.ContentRootPath}\wwwroot\{fileName}";
            using(StreamWriter writer = new StreamWriter(path, append: true))
            {
                writer.WriteLine(message);
            }
        }

    }
}

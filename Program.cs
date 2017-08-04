using System;
using System.IO;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.AspNetCore.Http.Abstractions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;

namespace webserver
{
    class Program
    {
        static void Main(string[] args)
        {
            var path = Directory.GetCurrentDirectory();
            if (args.Length > 0)
            {
                path = args[0];
            }

            var host = new WebHostBuilder()
                    .UseKestrel()
                    .UseContentRoot(path)
                    .UseWebRoot(path)
                    .UseUrls("http://localhost:5000")
                    .UseStartup<Startup>()
                    .Build();

            host.Run();
        }
    }

    internal class Startup
    {
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(LogLevel.Information);

            var fileServerOptions = new FileServerOptions();
            fileServerOptions.EnableDefaultFiles = true;
            fileServerOptions.EnableDirectoryBrowsing = true;
            fileServerOptions.FileProvider = env.WebRootFileProvider;
            fileServerOptions.StaticFileOptions.ServeUnknownFileTypes = true;
            app.UseFileServer(fileServerOptions);
        }
    }
}
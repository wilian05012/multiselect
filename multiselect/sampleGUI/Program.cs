﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace sampleGUI {
    public class Program {
        public static void Main(string[] args) {
            CreateWebHostBuilder(args)
                .ConfigureLogging(logginBuilder => {
                    logginBuilder.AddConsole();
                    logginBuilder.AddDebug();
                })
                .Build()
                .Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}

using Microsoft.AspNetCore.Hosting;
using System.Net.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Quartz;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Quartz.Impl;

namespace MoviesForEveryone
{
    public class Program
    {
        
        public static async void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();

            //Create the scheduler for the moviequeue
            StdSchedulerFactory factory = new StdSchedulerFactory();
            IScheduler scheduler = await factory.GetScheduler();

            await scheduler.Start();

            ////Define the job
            //IJobDetail job = JobBuilder.Create<CreateMovieQueueJob>()
            //    .WithIdentity("");
            //Trigger the job to run now, and repeat every week

            //Schedule the job
                   
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}

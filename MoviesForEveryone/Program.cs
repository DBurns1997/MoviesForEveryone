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
using MoviesForEveryone.Models;

namespace MoviesForEveryone
{
    public class Program
    {
        
        public  static void Main(string[] args)
        {
            CreateQueueScheduler().GetAwaiter().GetResult(); //This allows us to call asynchronous methods from main!       
            CreateHostBuilder(args).Build().Run();                     
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

        private static async Task<bool> CreateQueueScheduler()
        {
            //Create the scheduler for the moviequeue
            StdSchedulerFactory factory = new StdSchedulerFactory();
            //Get a scheduler
            IScheduler scheduler = await factory.GetScheduler();
            await scheduler.Start();

            //Define the job and tie it to our PopulateMovieQueueJob class
            IJobDetail job = JobBuilder.Create<PopulateMovieQueueJob>()
                .WithIdentity("PopulateQueue", "group1")
                .Build();

            //Trigger the job to run now, and then every week (we're doing it daily right now just to prove that it works)
            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("QueueTrigger", "group1")
                .StartNow()
                .WithSimpleSchedule(x => x
                .WithIntervalInHours(24)
                .RepeatForever())
                .Build();

            await scheduler.ScheduleJob(job, trigger);
            return true;
        }
    }
}

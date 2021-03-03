using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Quartz;
using MoviesForEveryone.Pages;

namespace MoviesForEveryone.Models
{
    public class PopulateMovieQueueJob : IJob
    {
       public async Task Execute(IJobExecutionContext context)
        {           
            await MovieQueuePageModel.PopulateQueue();
        }
    }
}

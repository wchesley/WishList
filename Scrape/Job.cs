using System;
using Quartz;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace WishList
{
    public class scrapeJob : IJob
    { 
        async Task IJob.Execute(IJobExecutionContext context)
        { 
            Console.Out.WriteLine("Begin Scheduled Job...");
            var scraper = new scrape();
            await Task.Run(scraper.Scrape());   
        }
    }
}
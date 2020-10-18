using System;
using Quartz;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace WishList
{
    public class scrapeJob : IJob
    { 
        private IServiceProvider services {get; }
        public scrapeJob(IServiceProvider service)
        {
            services = service; 
        }
        async Task IJob.Execute(IJobExecutionContext context)
        { 
            Console.Out.WriteLine("Begin Scheduled Job...");
            using(var scope = services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ProductContext>(); 
                var scraper = new scrape();
                await Task.Run(scraper.Scrape(dbContext));  
            }
             
        }
    }
}
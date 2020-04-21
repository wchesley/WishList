using System; 
using Quartz; 
using Quartz.Impl;
using System.Threading.Tasks; 
using System.Collections.Specialized;

namespace WishList
{
    /*
    Schedules jobs to be run every 24hrs at midnight: 
    utalizes Quartz for scheduling, this class configures and runs quartz
    largly taken from: http://www.mikesdotnetting.com/Article/254/Scheduled-Tasks-In-ASP.NET-With-Quartz.Net
    && Quartz Docs: https://www.quartz-scheduler.net/documentation/quartz-3.x/tutorial/using-quartz.html 
    */
    public class JobScheduler
    {
        public static async void Start()
        {
            NameValueCollection props = new NameValueCollection
            {
                {"quartz.serializer.type", "binary"}
            };
            StdSchedulerFactory factor = new StdSchedulerFactory(props); 
            IScheduler scheduler = await factor.GetScheduler(); 
            await scheduler.Start(); 

            //Tells quartz which job to run: in this case it will be scrape.Execute()
            IJobDetail job = JobBuilder.Create<scrapeJob>().Build();

            //schedule for everyday at midnight: 
            ITrigger trigger = TriggerBuilder.Create()
                .WithDailyTimeIntervalSchedule
                (s => 
                    s.WithIntervalInHours(24)
                    .OnEveryDay()
                    .StartingDailyAt(TimeOfDay.HourAndMinuteOfDay(0,0))
                    )
                .Build();
            
            await scheduler.ScheduleJob(job, trigger);
            
        }
    }
}
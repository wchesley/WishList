using System; 
using Quartz; 
using Quartz.Impl;
using System.Threading.Tasks; 

namespace WishList
{
    /*
    Schedules jobs to be run every 24hrs at midnight: 
    utalizes Quartz for scheduling, this class configures and runs quartz
    largly taken from: http://www.mikesdotnetting.com/Article/254/Scheduled-Tasks-In-ASP.NET-With-Quartz.Net
    */
    public class JobScheduler
    {
        public static void Start()
        {
            IScheduler scheduler = (IScheduler)StdSchedulerFactory.GetDefaultScheduler();
            scheduler.Start(); 

            //Tells quartz which job to run: in this case it will be scrape.Execute()
            IJobDetail job = JobBuilder.Create<scrape>().Build();

            //schedule for everyday at midnight: 
            ITrigger trigger = TriggerBuilder.Create()
                .WithDailyTimeIntervalSchedule
                (s => 
                    s.WithIntervalInHours(24)
                    .OnEveryDay()
                    .StartingDailyAt(TimeOfDay.HourAndMinuteOfDay(0,0))
                    )
                .Build();
            
            scheduler.ScheduleJob(job, trigger); 
        }
    }
}
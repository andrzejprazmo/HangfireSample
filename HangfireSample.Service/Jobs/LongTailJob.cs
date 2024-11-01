using Hangfire;
using HangfireSample.Service.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace HangfireSample.Service.Jobs
{
    public class LongTailJob
    {
        private readonly IHubContext<LongTailHub> hubContext;

        public LongTailJob(IHubContext<LongTailHub> hubContext)
        {
            this.hubContext = hubContext;
        }

        public int Progress { get; set; }

        public void DoWork(string connectionId)
        {
            int counter = 0;
            while (counter++ < 10)
            {
                Progress = counter * 10;
                Console.WriteLine($"Hello from Hangfire Job: {counter}");
                this.hubContext.Clients.All.SendAsync("JobProgress", Progress);
                Thread.Sleep(5000);
            }
            Progress = 0;
        }
    }
}

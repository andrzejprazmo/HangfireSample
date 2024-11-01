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

        public void DoWork(string connectionId)
        {
            int counter = 0;
            while (counter++ < 10)
            {
                Console.WriteLine($"Hello from Hangfire Job: {counter}");
                this.hubContext.Clients.Client(connectionId).SendAsync("JobProgress", counter * 10);
                Thread.Sleep(1000);
            }
        }
    }
}

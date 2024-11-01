using Hangfire;
using HangfireSample.Service.Jobs;
using Microsoft.AspNetCore.SignalR;
using System.Reflection;

namespace HangfireSample.Service.Hubs
{
    public class LongTailHub : Hub
    {
        private readonly IBackgroundJobClient jobClient;
        private readonly IServiceProvider services;

        public LongTailHub(IBackgroundJobClient jobClient, IServiceProvider services)
        {
            this.jobClient = jobClient;
            this.services = services;
        }

        public void StartJob()
        {
            jobClient.Enqueue<LongTailJob>(x => x.DoWork(Context.ConnectionId));
            Clients.All.SendAsync("StartJob", Context.ConnectionId);
        }
    }
}

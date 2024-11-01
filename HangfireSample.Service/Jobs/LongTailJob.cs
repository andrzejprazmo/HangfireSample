namespace HangfireSample.Service.Jobs
{
    public class LongTailJob
    {
        public void DoWork()
        {
            int counter = 0;
            while (counter++ < 10)
            {
                Console.WriteLine($"Hello from Hangfire Job: {counter}");
                Thread.Sleep(1000);
            }
        }
    }
}

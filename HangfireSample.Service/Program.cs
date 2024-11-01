using Hangfire;
using HangfireSample.Service.Jobs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHangfire(config => 
{ 
    config.UseSimpleAssemblyNameTypeSerializer().UseRecommendedSerializerSettings().UseInMemoryStorage();
});
builder.Services.AddHangfireServer(config => config.SchedulePollingInterval = TimeSpan.FromSeconds(10));

builder.Services.AddTransient<LongTailJob>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseHangfireDashboard();
}


app.MapGet("/job", (IBackgroundJobClient jobClient, LongTailJob job) =>
{
    jobClient.Enqueue(() => job.DoWork());

    return Results.Ok("Hello jobs");
})
.WithName("StartJob")
.WithOpenApi();

app.Run();

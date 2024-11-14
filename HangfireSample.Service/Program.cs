using Hangfire;
using HangfireSample.Service.Hubs;
using HangfireSample.Service.Jobs;

var webApplicationOptions = new WebApplicationOptions
{
    Args = args,
    ApplicationName = typeof(Program).Assembly.FullName,
    ContentRootPath = Directory.GetCurrentDirectory(),
    WebRootPath = "wwwroot/browser"
};
var builder = WebApplication.CreateBuilder(webApplicationOptions);

// TEST
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHangfire(config =>
{
    config.UseSimpleAssemblyNameTypeSerializer().UseRecommendedSerializerSettings().UseInMemoryStorage();
});
builder.Services.AddHangfireServer(config => config.SchedulePollingInterval = TimeSpan.FromSeconds(10));

builder.Services.AddSingleton<LongTailJob>();

builder.Services.AddSignalR();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseHangfireDashboard();
}
app.UseStaticFiles();

app.MapGet("/api/job", (IBackgroundJobClient jobClient, LongTailJob job) =>
{
    

    return Results.Ok("Hello jobs");
})
.WithName("StartJob")
.WithOpenApi();

app.MapHub<LongTailHub>("/hub");

app.MapFallbackToFile("index.html");
app.Run();

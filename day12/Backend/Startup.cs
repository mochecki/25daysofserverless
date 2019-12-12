using System;
using Backend.Services;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Octokit;

[assembly: FunctionsStartup(typeof(Backend.Startup))]
namespace Backend
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddHttpClient();
            builder.Services.AddDistributedRedisCache(options =>
            {
                options.Configuration = Environment.GetEnvironmentVariable("RedisConnection"); 
                options.InstanceName = "master";
            });

            builder.Services.AddScoped<ICardService, CardService>();
            builder.Services.AddScoped<IGistService, GistService>();
            builder.Services.AddScoped<GitHubClient>(provider => new GitHubClient(new ProductHeaderValue("dos24-moc")));
        }
    }
}
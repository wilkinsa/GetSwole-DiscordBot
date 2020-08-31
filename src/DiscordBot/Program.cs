using System;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Application;
using Application.DevSeeds;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using DiscordBot.Services;
using Infrastructure;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;

namespace DiscordBot
{
    class Program
    {
        private ILogger<Program> _logger;
        public IServiceProvider Services { get; } 
        
        static void Main(string[] args)
            => new Program().MainAsync().GetAwaiter().GetResult();

        public async Task MainAsync()
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .WriteTo.Seq("http://swole-seq:5341")
                .CreateLogger();

            try 
            {
                var serviceCollection = new ServiceCollection();
                ConfigureServices(serviceCollection);

                var serviceProvider = serviceCollection.BuildServiceProvider();
                var client = serviceProvider.GetRequiredService<DiscordSocketClient>();

                _logger = serviceProvider.GetService<ILogger<Program>>();

                _logger.LogInformation("Discord Bot Started...");

                //TODO: Move to secure location
                var token = Environment.GetEnvironmentVariable("DISCORD_TOKEN");

                await client.LoginAsync(TokenType.Bot, token);
                await client.StartAsync();

                var context = serviceProvider.GetRequiredService<ApplicationDbContext>();
                await serviceProvider.GetRequiredService<CommandHandlingService>().InitializeAsync();
                await serviceProvider.GetRequiredService<ReactionHandlingService>().InitializeAsync();
                serviceProvider.GetRequiredService<WorkoutPostingService>().Start();

                await context.Database.MigrateAsync();
                
                if(!context.Campaigns.Any(c => c.Name == "30 Day Ab Challenge"))
                {
                    SeedData.GetSeedCampaign(context);
                }
                
                await client.GetUser(Convert.ToUInt64(Environment.GetEnvironmentVariable("BOT_ADMIN")))
                    .SendMessageAsync("Ready for work!");

                await Task.Delay(Timeout.Infinite);
            }
            catch (Exception e)
            {
                Log.Fatal(e, "Discord Bot Crashed");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddLogging(configure => configure.AddSerilog(Log.Logger));
            services.AddSingleton<DiscordSocketClient>();
            services.AddSingleton<CommandService>();
            services.AddSingleton<CommandHandlingService>();
            services.AddSingleton<ReactionHandlingService>();
            services.AddSingleton<WorkoutPostingService>();
            services.AddSingleton<HttpClient>();
            services.AddInfrastructure();
            services.AddApplication();
        }

        //Used for design time migrations
        static IServiceProvider BuildServiceProvider(string[] args)
        {
            IServiceCollection serviceProvider = new ServiceCollection();
            ConfigureServices(serviceProvider);
            return serviceProvider.BuildServiceProvider();
        }
        
        static object BuildWebHost(string[] args) =>
            new { Services = BuildServiceProvider(args) };
    }
}

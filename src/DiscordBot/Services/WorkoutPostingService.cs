using System;
using System.Timers;
using Application.Common;
using Application.Common.Interfaces;
using Application.Workouts.Commands.MarkWorkoutAsPosted;
using Application.Workouts.Queries.GetWorkoutReadyForPost;
using Discord;
using Discord.WebSocket;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace DiscordBot.Services
{
    public class WorkoutPostingService
    {
        private readonly DiscordSocketClient _discord;
        private readonly IServiceProvider _services;
        private readonly ILogger<ReactionHandlingService> _logger;
        private readonly IMediator _mediator;
        private readonly IMemeGenerator _memeGenerator;
        private Timer timer;
        public WorkoutPostingService(IServiceProvider services)
        {
            _discord = services.GetRequiredService<DiscordSocketClient>();
            _logger = services.GetRequiredService<ILogger<ReactionHandlingService>>();
            _mediator = services.GetRequiredService<IMediator>();
            _memeGenerator = services.GetRequiredService<IMemeGenerator>();
            _services = services;
        }

        public void Start()
        {
            timer = new Timer();
            timer.Elapsed += CheckWorkouts;
            timer.Interval = 60000; // change to 300000
            timer.AutoReset = true;
            timer.Start();
        }

        private async void CheckWorkouts(object sender, EventArgs e)
        {
            var workouts = await _mediator.Send(new GetWorkoutReadyForPostQuery());

            foreach(var workout in workouts)
            {
                var image = await _memeGenerator.GetWorkoutMeme();
                var embededMessage = MessageTemplates.WorkoutMessage(workout.Campaign, workout, image);

                var message = await _discord
                    .GetGuild(Convert.ToUInt64(Environment.GetEnvironmentVariable("GUILDID")))
                    .GetTextChannel(Convert.ToUInt64(Environment.GetEnvironmentVariable("CHANNELID")))
                    .SendMessageAsync("", false, embededMessage);

                await _mediator.Send(new MarkWorkoutAsPostedCommand(workout.Id, message.Id));

                await message.AddReactionAsync(new Emoji(Emojis.white_check_mark));
            }
        }

    }
}
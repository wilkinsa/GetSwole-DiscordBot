using System;
using System.Linq;
using System.Threading.Tasks;
using Application;
using Application.Campaigns.Commands.CreateCampaign;
using Application.Campaigns.Queries.GetCampaignByName;
using Application.Common;
using Application.Common.Interfaces;
using Application.Workouts;
using Application.Workouts.Commands.MarkWorkoutAsPosted;
using Discord;
using Discord.Commands;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace DiscordBot.Modules
{
    public class WorkoutModule : ModuleBase<SocketCommandContext>
    {
        private readonly ILogger<WorkoutModule> _logger;
        private readonly IMediator _mediator;
        private readonly IMemeGenerator _memeGenerator;
        private static readonly WorkoutData Workouts = WorkoutData.GetWorkouts();
        
        public WorkoutModule(IServiceProvider services)
        {
            _logger = services.GetRequiredService<ILogger<WorkoutModule>>();
            _mediator = services.GetRequiredService<IMediator>();
            _memeGenerator = services.GetRequiredService<IMemeGenerator>();
        }

        [Command("get-workout")]
        [RequireUserPermission(GuildPermission.Administrator)]
        public async Task ChallengeAsync([Remainder] string data) 
        {
            try 
            {
                var args = data.Split(",");

                if(args.Length < 2) return;

                Campaign campaign = await _mediator.Send(new GetCampaignByNameQuery(args[0]));
                Workout workout = campaign.Workouts.FirstOrDefault(w => w.Name.ToLowerInvariant() == args[1].Trim().ToLowerInvariant());

                if(campaign == null) 
                {
                    await ReplyAsync($"Campaign '{args[0]}' not found");
                    return;
                }
                if(workout == null) 
                {
                    await ReplyAsync($"Workout '{args[1]}' not found for campaign {campaign.Name}");
                    return;
                }
                if(workout.Posted) 
                {
                    await ReplyAsync("That workout has already been posted");
                    return;
                }
                var image = await _memeGenerator.GetWorkoutMeme();
                var embededMessage = MessageTemplates.WorkoutMessage(campaign, workout, image);

                var message = await ReplyAsync("", false, embededMessage);

                await _mediator.Send(new MarkWorkoutAsPostedCommand(workout.Id, message.Id));

                await message.AddReactionAsync(new Emoji(Emojis.white_check_mark));
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
            }
        }

        [Command("get-campaign")]
        [RequireUserPermission(GuildPermission.Administrator)]
        public async Task GetStatus([Remainder] string data)
        {

            Campaign campaign = await _mediator.Send(new GetCampaignByNameQuery(data));

            if(campaign == null) 
            {
                await ReplyAsync($"Campaign '{data}' not found");
                return;
            }

            if(campaign.PostId != default)
            {
                await ReplyAsync($"Campaign already posted");
                return;
            }
            
            var embededMessage = MessageTemplates.CampaignCreated(campaign);
            var message = await ReplyAsync("", false, embededMessage);
            await _mediator.Send(new MarkCampaignAsPostedCommand(campaign.Id, message.Id));
            await message.AddReactionAsync(new Emoji(Emojis.white_check_mark));
        }

        [Command("create-campaign")]
        [RequireUserPermission(GuildPermission.Administrator)]
        public async Task CreateCampaign([Remainder] string data)
        {
            var args = data.Split(",");

            if(args.Length < 3) 
            {
                await ReplyAsync("Format should be '{name}, {description}, {date (mm/dd/yyyy)}'");
                return;
            }
            string name = args[0];
            string description = args[1];
            DateTimeOffset startDate;
            DateTimeOffset.TryParse(args[2], out startDate);

            Campaign campaign = await _mediator.Send(new CreateCampaignCommand(name, description, startDate));

            var embededMessage = MessageTemplates.CampaignCreated(campaign);
            var message = await ReplyAsync("", false, embededMessage);
            await message.AddReactionAsync(new Emoji(Emojis.white_check_mark));
        }

        [Command("quick-fix")]
        [RequireUserPermission(GuildPermission.Administrator)]
        public async Task QuickFix([Remainder] string data)
        {
            var updates = await _mediator.Send(new QuickFix());

            await ReplyAsync($"Updated {updates} workouts");
        }
    }
}
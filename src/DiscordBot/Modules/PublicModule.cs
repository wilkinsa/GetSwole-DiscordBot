using System;
using System.Linq;
using System.Threading.Tasks;
using Application;
using Application.Campaigns.Queries.GetCampaignByName;
using Application.Common;
using Application.Common.Interfaces;
using Application.Workouts.Commands.MarkWorkoutAsPosted;
using Discord;
using Discord.Commands;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace DiscordBot.Modules
{
    public class PublicModule : ModuleBase<SocketCommandContext>
    {
        private readonly ILogger<PublicModule> _logger;
        private readonly IMediator _mediator;
        private readonly IMemeGenerator _memeGenerator;
        private static readonly WorkoutData Workouts = WorkoutData.GetWorkouts();
        
        public PublicModule(IServiceProvider services)
        {
            _logger = services.GetRequiredService<ILogger<PublicModule>>();
            _mediator = services.GetRequiredService<IMediator>();
            _memeGenerator = services.GetRequiredService<IMemeGenerator>();
        }

        // [Command("ping")]
        // [Alias("pong", "hello")]
        // public Task PingAsync()
        //     => ReplyAsync("pong!");

        // // Get info on a user, or the user who invoked the command if one is not specified
        // [Command("userinfo")]
        // public async Task UserInfoAsync(IUser user = null)
        // {
        //     try 
        //     {
        //         user = user ?? Context.User;

        //         await ReplyAsync(user.ToString());
        //     }
        //     catch (Exception e)
        //     {
        //         _logger.LogError(e, e.Message);
        //     }
        // }

        // // Ban a user
        // [Command("ban")]
        // [RequireContext(ContextType.Guild)]
        // // make sure the user invoking the command can ban
        // [RequireUserPermission(GuildPermission.BanMembers)]
        // // make sure the bot itself can ban
        // [RequireBotPermission(GuildPermission.BanMembers)]
        // public async Task BanUserAsync(IGuildUser user, [Remainder] string reason = null)
        // {
        //     await user.Guild.AddBanAsync(user, reason: reason);
        //     await ReplyAsync("ok!");
        // }

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

        [Command("get-status")]
        [RequireUserPermission(GuildPermission.Administrator)]
        public async Task GetStatus([Remainder] string data)
        {

            Campaign campaign = await _mediator.Send(new GetCampaignByNameQuery(data));

            if(campaign == null) 
            {
                await ReplyAsync($"Campaign '{data}' not found");
                return;
            }

            var embededMessage = MessageTemplates.CampaignStatus(campaign);
            var message = await ReplyAsync("", false, embededMessage);
        }


        // // [Remainder] takes the rest of the command's arguments as one argument, rather than splitting every space
        // [Command("echo")]
        // public Task EchoAsync([Remainder] string text)
        //     // Insert a ZWSP before the text to prevent triggering other bots!
        //     => ReplyAsync('\u200B' + text);

        // // 'params' will parse space-separated elements into a list
        // [Command("list")]
        // public Task ListAsync(params string[] objects)
        //     => ReplyAsync("You listed: " + string.Join("; ", objects));

        // // Setting a custom ErrorMessage property will help clarify the precondition error
        // [Command("guild_only")]
        // [RequireContext(ContextType.Guild, ErrorMessage = "Sorry, this command must be ran from within a server, not a DM!")]
        // public Task GuildOnlyCommand()
        //     => ReplyAsync("Nothing to see here!");
    }
}
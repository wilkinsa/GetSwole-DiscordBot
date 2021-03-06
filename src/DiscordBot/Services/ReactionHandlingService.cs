using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Application.Campaigns.Commands.AddCampaignParticipant;
using Application.Campaigns.Queries.GetCampaignById;
using Application.Common;
using Application.Common.Interfaces;
using Application.Workouts.Commands.WorkoutAddCompletedUser;
using Application.Workouts.Commands.WorkoutRemoveCompletedUser;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace DiscordBot.Services
{
    public class ReactionHandlingService
    {
        private readonly CommandService _commands;
        private readonly DiscordSocketClient _discord;
        private readonly IServiceProvider _services;
        private readonly ILogger<ReactionHandlingService> _logger;
        private readonly IMediator _mediator;
        private readonly IMemeGenerator _memeGenerator;

        public ReactionHandlingService(IServiceProvider services)
        {
            _commands = services.GetRequiredService<CommandService>();
            _discord = services.GetRequiredService<DiscordSocketClient>();
            _logger = services.GetRequiredService<ILogger<ReactionHandlingService>>();
            _mediator = services.GetRequiredService<IMediator>();
            _memeGenerator = services.GetRequiredService<IMemeGenerator>();
            _services = services;

            _discord.ReactionAdded += ReactionAddedAsync;
            _discord.ReactionRemoved += ReactionRemovedAsync;
        }

        public async Task InitializeAsync()
        {
            // Register modules that are public and inherit ModuleBase<T>.
            await _commands.AddModulesAsync(Assembly.GetEntryAssembly(), _services);
        }

        //TODO: REFACTOR
        public async Task ReactionAddedAsync(Cacheable<IUserMessage, UInt64> message, ISocketMessageChannel channel, SocketReaction reaction)
        {
            if(reaction.User.Value.IsBot) return;

            var orginalMessage = await message.DownloadAsync();

            if(!orginalMessage.Author.IsBot) return;

            var image = orginalMessage.Embeds.Select(e => e.Image).FirstOrDefault().GetValueOrDefault().Url;
            var id = Guid.Parse(orginalMessage.Embeds.Select(e => e.Footer).FirstOrDefault().GetValueOrDefault().Text);
            if(string.IsNullOrWhiteSpace(image)) image = await _memeGenerator.GetWorkoutMeme();

            //if(reaction.Emote.Name !=  Emojis.white_check_mark && reaction.Emote.Name !=  Emojis.droplet) await orginalMessage.RemoveReactionAsync(reaction.Emote, reaction.User.GetValueOrDefault());

            var campaign = await _mediator.Send(new GetCampaignByIdQuery(id));

            if(campaign != null)
            {
                if(reaction.Emote.Name == Emojis.white_check_mark)  
                {   
                    campaign = await _mediator.Send(new AddCampaignParticipantCommand(campaign.Id, reaction.User.Value.Id, reaction.User.Value.Username));
                    await orginalMessage.ModifyAsync(msg => msg.Embed = MessageTemplates.CampaignCreated(campaign));
                }
            }

            if(campaign == null && reaction.Emote.Name == Emojis.white_check_mark)  
            {   
                _logger.LogDebug("reaction to workout");
                Workout workout = await _mediator.Send(new WorkoutAddCompletedUserCommand(orginalMessage.Id, reaction.UserId, reaction.User.Value.Username));
                await orginalMessage.ModifyAsync(msg => msg.Embed = MessageTemplates.WorkoutMessage(workout.Campaign, workout, image));
            }
        }

        //TODO: REFACTOR
        public async Task ReactionRemovedAsync(Cacheable<IUserMessage, UInt64> message, ISocketMessageChannel channel, SocketReaction reaction)
        {
            var orginalMessage = await message.DownloadAsync();
            if(!orginalMessage.Author.IsBot) return;
            var image = orginalMessage.Embeds.Select(e => e.Image).FirstOrDefault().GetValueOrDefault().Url;
            var id = Guid.Parse(orginalMessage.Embeds.Select(e => e.Footer).FirstOrDefault().GetValueOrDefault().Text);

            var campaign = await _mediator.Send(new GetCampaignByIdQuery(id));

            if(campaign != null)
            {
                if(reaction.Emote.Name == Emojis.white_check_mark)  
                {   
                    campaign = await _mediator.Send(new RemoveCampaignParticipantCommand(campaign.Id, reaction.User.Value.Id, reaction.User.Value.Username));
                    await orginalMessage.ModifyAsync(msg => msg.Embed = MessageTemplates.CampaignCreated(campaign));
                }
            }

            if(string.IsNullOrWhiteSpace(image)) image = await _memeGenerator.GetWorkoutMeme();
            if(reaction.Emote.Name == Emojis.white_check_mark)
            {
                Workout workout = await _mediator.Send(new WorkoutRemoveCompletedUserCommand(orginalMessage.Id, reaction.UserId, reaction.User.Value.Username));
                await orginalMessage.ModifyAsync(msg => msg.Embed = MessageTemplates.WorkoutMessage(workout.Campaign, workout, image));
            }
        }
    }
}
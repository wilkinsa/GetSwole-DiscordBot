using System;
using System.Linq;
using Discord;
using Domain.Entities;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace DiscordBot
{
    public class MessageTemplates
    {
        public static Embed WorkoutMessage(Campaign campaign, Workout workout, string image)
        {
            EmbedBuilder builder = new EmbedBuilder();
            EmbedFooterBuilder footer = new EmbedFooterBuilder();

            builder.WithColor(166, 82, 187);
            builder.WithTitle($"Campaign: {campaign.Name}");
            builder.WithDescription($"Participants: {string.Join(", ", campaign.Participants?.Select(p => $"<@!{p.UserId}>"))}");
            builder.AddField("Workout", workout.Name, true);
            builder.AddField("Date", workout.WorkoutDate.Date.ToShortDateString(), true);
            builder.AddField("Completed by", $"{(workout.CompletedBy.Any() ? string.Join(", ", workout.CompletedBy.Select(u => u.UserName)) : "none")}");
            builder.WithImageUrl(image);
            foreach (Exercise ex in workout.Exercises)
            {
                builder.AddField(ex.Name, ex.Value, false);
            }
            builder.WithFooter($"{workout.Id}");

            return builder.Build();
        }

        public static Embed CampaignStatus(Campaign campaign)
        {
            EmbedBuilder builder = new EmbedBuilder();
            EmbedFooterBuilder footer = new EmbedFooterBuilder();

            builder.WithColor(166, 82, 187);
            builder.WithTitle($"Campaign Status: {campaign.Name}");
            builder.WithFooter($"{campaign.Id}");
            foreach(Workout workout in campaign.Workouts)
            {
                string status;
                if(workout.Posted) status = $"Completed by: {string.Join(", ", workout.CompletedBy.Select(u => u.UserName))}";
                else status = "workout not posted yet";

                builder.AddField($"{workout.Name} on {workout.WorkoutDate.Date.ToShortDateString()}", $"Completed by: {string.Join(", ", workout.CompletedBy.Select(u => u.UserName))}", false);
            }

            return builder.Build();
        }

        public static Embed CampaignCreated(Campaign campaign)
        {

            EmbedBuilder builder = new EmbedBuilder();
            EmbedFooterBuilder footer = new EmbedFooterBuilder();

            builder.WithColor(166, 82, 187);
            builder.WithTitle($"Campaign Created: {campaign.Name}");
            builder.WithFooter($"{campaign.Id}");
            builder.WithDescription(campaign.Description);
            builder.AddField("Start date", campaign.StartDate.Date.ToShortDateString());
            if(campaign.Participants.Any()) builder.AddField("Participants", $"{string.Join(", ", campaign.Participants?.Select(u => u?.UserName))}");
            else builder.AddField("Participants", "none");

            return builder.Build();
        }
    }
}
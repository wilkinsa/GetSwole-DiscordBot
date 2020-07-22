using System;
using System.Linq;
using Discord;
using Domain.Entities;

namespace DiscordBot
{
    public static class MessageTemplates
    {
        public static Embed WorkoutMessage(Campaign campaign, Workout workout, string image)
        {
            EmbedBuilder builder = new EmbedBuilder();
            EmbedFooterBuilder footer = new EmbedFooterBuilder();

            footer.Text = $"Completed by: {string.Join(", ", workout.CompletedBy.Select(u => u.UserName))}";

            builder.WithColor(166, 82, 187);
            builder.WithTitle($"Campaign: {campaign.Name}");
            builder.AddField("Workout", workout.Name, true);
            builder.AddField("Date", workout.WorkoutDate.Date.ToShortDateString(), true);
            builder.WithImageUrl(image);
            foreach (Exercise ex in workout.Exercises)
            {
                builder.AddField(ex.Name, ex.Value, false);
            }
            builder.WithFooter(footer);

            return builder.Build();
        }

        public static Embed CampaignStatus(Campaign campaign)
        {
            EmbedBuilder builder = new EmbedBuilder();
            EmbedFooterBuilder footer = new EmbedFooterBuilder();

            builder.WithColor(166, 82, 187);
            builder.WithTitle($"Status for campaign: {campaign.Name}");
            foreach(Workout workout in campaign.Workouts)
            {
                string status;
                if(workout.Posted) status = $"Completed by: {string.Join(", ", workout.CompletedBy.Select(u => u.UserName))}";
                else status = "workout not posted yet";

                builder.AddField($"{workout.Name} on {workout.WorkoutDate.Date.ToShortDateString()}", $"Completed by: {string.Join(", ", workout.CompletedBy.Select(u => u.UserName))}", false);
            }

            return builder.Build();
        }
    }
}
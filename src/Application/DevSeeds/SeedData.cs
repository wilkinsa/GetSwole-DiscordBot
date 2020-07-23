using System;
using System.Threading;
using Application.Common.Interfaces;
using Domain.Entities;

namespace Application.DevSeeds
{
    public static class SeedData
    {
        private static readonly WorkoutData Workouts = WorkoutData.GetWorkouts();

        public static void GetSeedCampaign(IApplicationDbContext context)
        {
            context.Campaigns.Add(new Campaign
            {
                Id = Guid.NewGuid(),
                StartDate = DateTimeOffset.Parse("07/24/2020"),
                Name = "30 Day Challenge",
                Description = $@"<#{Environment.GetEnvironmentVariable("CHANNELID")}> Hit the check mark reaction below to be added as a participant and you will be notified when each days workout is posted.
                
Yooo, the moves will be below! If you have any questions just let me know. If anything bothers throughout the course of the challenge, let me know! Everything can be adjusted. I'll have some more details on the check ins we'll do in discord come Friday before stream!

https://www.youtube.com/watch?v=wy-wJUpwr2I - Back Ext.

https://www.youtube.com/watch?v=IODxDxX7oi4 - Push ups

https://www.youtube.com/watch?v=RClKKQqsvXA - Squat

https://www.youtube.com/watch?v=F-nQ_KJgfCY - Plank",
                Workouts = Workouts.GetWorkoutList()
            });
            context.SaveChangesAsync(CancellationToken.None);
        }
    }
}
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
                StartDate = DateTimeOffset.Parse("08/31/2020"),
                Name = "30 Day Ab Challenge",
                Description = $@"<#{Environment.GetEnvironmentVariable("CHANNELID")}> Hit the check mark reaction below to be added as a participant and you will be notified when each days workout is posted.
                
https://www.youtube.com/watch?v=TbYX0bYSk8s - Sit ups

https://www.youtube.com/watch?v=LIw00LmKd98 - Heel Taps

https://www.youtube.com/watch?v=WRnq49TAv-w - Flutter Kicks

https://www.youtube.com/watch?v=r65E3D2Zi68 - Side Plank",
                Workouts = Workouts.GetWorkoutList()
            });
            context.SaveChangesAsync(CancellationToken.None);
        }
    }
}
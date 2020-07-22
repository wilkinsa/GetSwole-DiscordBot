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
                StartDate = DateTimeOffset.Now.AddDays(2),
                Name = "Test",
                Workouts = Workouts.GetWorkoutList()
            });
            context.SaveChangesAsync(CancellationToken.None);
        }
    }
}
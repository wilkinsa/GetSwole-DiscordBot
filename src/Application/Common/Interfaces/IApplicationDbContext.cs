using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Campaign> Campaigns { get; set; }
        DbSet<Workout> Workouts { get; set; }
        DbSet<Exercise> Exercises { get; set; }
        DbSet<ExerciseOption> ExerciseOptions { get; set; }
        DbSet<User> Users { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
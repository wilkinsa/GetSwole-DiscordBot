using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Application.Workouts
{
    public class QuickFix : IRequest<int>
    {

    }

    public class QuickFixHandler : IRequestHandler<QuickFix, int>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly ILogger<QuickFixHandler> _logger;

        public QuickFixHandler(IServiceScopeFactory services)
        {
            _dbContext = services.CreateScope().ServiceProvider.GetService<IApplicationDbContext>();
            _logger = services.CreateScope().ServiceProvider.GetService<ILogger<QuickFixHandler>>();
        }
        public async Task<int> Handle(QuickFix request, CancellationToken cancellationToken)
        {
            var Workouts = await _dbContext.Workouts.ToListAsync();

            foreach(var workout in Workouts)
            {
                workout.Posted = false;
                workout.PostId = 0;
                workout.WorkoutDate = workout.WorkoutDate.AddHours(5);
            }

            return await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
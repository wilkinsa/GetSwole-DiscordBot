using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Application.Workouts.Queries.GetWorkoutReadyForPost
{
    public class GetWorkoutReadyForPostQueryHandler : IRequestHandler<GetWorkoutReadyForPostQuery, List<Workout>>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly ILogger<GetWorkoutReadyForPostQueryHandler> _logger;

        public GetWorkoutReadyForPostQueryHandler(IServiceScopeFactory services)
        {
            _dbContext = services.CreateScope().ServiceProvider.GetService<IApplicationDbContext>();
            _logger = services.CreateScope().ServiceProvider.GetService<ILogger<GetWorkoutReadyForPostQueryHandler>>();
        }
        public async Task<List<Workout>> Handle(GetWorkoutReadyForPostQuery request, CancellationToken cancellationToken)
        {
            try 
            {
                List<Workout> workouts = await _dbContext.Workouts
                    .Include(w => w.Campaign)
                        .ThenInclude(c => c.Participants)
                    .Include(w => w.Exercises)
                    .Where(workout => !workout.Posted && workout.WorkoutDate <= DateTimeOffset.Now)
                    .ToListAsync();
                
                return workouts;
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                throw e;
            }
        }
    }
}
using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Application.Workouts.Queries.GetWorkoutByPostId
{
    public class GetWorkoutByPostIdQueryHandler : IRequestHandler<GetWorkoutByPostIdQuery, Workout>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly ILogger<GetWorkoutByPostIdQueryHandler> _logger;

        public GetWorkoutByPostIdQueryHandler(IServiceScopeFactory services)
        {
            _dbContext = services.CreateScope().ServiceProvider.GetService<IApplicationDbContext>();
            _logger = services.CreateScope().ServiceProvider.GetService<ILogger<GetWorkoutByPostIdQueryHandler>>();
        }
        public async Task<Workout> Handle(GetWorkoutByPostIdQuery request, CancellationToken cancellationToken)
        {
            try 
            {
                return await _dbContext.Workouts
                    .Include(w => w.Campaign)
                    .Include(w => w.Exercises)
                    .FirstOrDefaultAsync(w => w.PostId == request.PostId);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                throw e;
            }
        }
    }
}
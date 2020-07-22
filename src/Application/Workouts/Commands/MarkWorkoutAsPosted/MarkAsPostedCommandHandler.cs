using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Application.Workouts.Commands.MarkWorkoutAsPosted
{
    public class MarkWorkoutAsPostedCommandHandler : IRequestHandler<MarkWorkoutAsPostedCommand, Guid>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly ILogger<MarkWorkoutAsPostedCommandHandler> _logger;

        public MarkWorkoutAsPostedCommandHandler(IServiceScopeFactory services)
        {
            _dbContext = services.CreateScope().ServiceProvider.GetService<IApplicationDbContext>();
            _logger = services.CreateScope().ServiceProvider.GetService<ILogger<MarkWorkoutAsPostedCommandHandler>>();
        }
        public async Task<Guid> Handle(MarkWorkoutAsPostedCommand request, CancellationToken cancellationToken)
        {
            try 
            {
                Workout workout = await _dbContext.Workouts.FirstOrDefaultAsync(workout => workout.Id == request.WorkoutId);

                if (workout == null) throw new NotFoundException(nameof(workout), request.WorkoutId);

                workout.Posted = true;
                workout.PostId = request.PostId;

                await _dbContext.SaveChangesAsync(cancellationToken);

                return workout.Id;
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                throw e;
            }
        }
    }
}
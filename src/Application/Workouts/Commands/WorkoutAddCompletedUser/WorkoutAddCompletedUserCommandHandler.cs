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

namespace Application.Workouts.Commands.WorkoutAddCompletedUser
{
    public class WorkoutAddCompletedUserCommandHandler : IRequestHandler<WorkoutAddCompletedUserCommand, Workout>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly ILogger<WorkoutAddCompletedUserCommandHandler> _logger;

        public WorkoutAddCompletedUserCommandHandler(IServiceScopeFactory services)
        {
            _dbContext = services.CreateScope().ServiceProvider.GetService<IApplicationDbContext>();
            _logger = services.CreateScope().ServiceProvider.GetService<ILogger<WorkoutAddCompletedUserCommandHandler>>();
        }
        public async Task<Workout> Handle(WorkoutAddCompletedUserCommand request, CancellationToken cancellationToken)
        {
            try 
            {
                Workout workout = await _dbContext.Workouts
                    .Include(w => w.CompletedBy)
                    .Include(w => w.Campaign)
                        .ThenInclude(c => c.Participants)
                    .Include(w => w.Exercises)
                    .FirstOrDefaultAsync(w => w.PostId == request.PostId);

                if(workout == null) throw new NotFoundException(nameof(workout), request.PostId);

                if(!workout.CompletedBy.Any(u => u.UserId == request.UserId))
                    workout.CompletedBy.Add(new User(request.UserId, request.UserName));

                await _dbContext.SaveChangesAsync(cancellationToken);

                return workout;
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                throw e;
            }
        }
    }
}
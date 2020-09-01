using System;
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
    public class QuickFix : IRequest<DateTimeOffset?>
    {
        
    }

    public class QuickFixHandler : IRequestHandler<QuickFix, DateTimeOffset?>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly ILogger<QuickFixHandler> _logger;

        public QuickFixHandler(IServiceScopeFactory services)
        {
            _dbContext = services.CreateScope().ServiceProvider.GetService<IApplicationDbContext>();
            _logger = services.CreateScope().ServiceProvider.GetService<ILogger<QuickFixHandler>>();
        }
        public async Task<DateTimeOffset?> Handle(QuickFix request, CancellationToken cancellationToken)
        {
            var data = await _dbContext.Workouts.Where(w => w.WorkoutDate > DateTime.Now).ToListAsync();

            foreach (var w in data)
            {
                w.WorkoutDate.AddHours(4);
            }

            await _dbContext.SaveChangesAsync(cancellationToken);

            return data.First().WorkoutDate;
        }
    }
}
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
            var campaign = await _dbContext.Campaigns.FirstOrDefaultAsync(c => c.Name == "30 Day Ab Challenge");

            campaign.StartDate = DateTimeOffset.Parse("08/31/2020");

            await _dbContext.SaveChangesAsync(cancellationToken);

            return campaign.StartDate;
        }
    }
}
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Application.Campaigns.Queries.GetCampaignByName
{
    public class GetCampaignByNameQueryHandler : IRequestHandler<GetCampaignByNameQuery, Campaign>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly ILogger<GetCampaignByNameQueryHandler> _logger;

        public GetCampaignByNameQueryHandler(IServiceScopeFactory services)
        {
            _dbContext = services.CreateScope().ServiceProvider.GetService<IApplicationDbContext>();
            _logger = services.CreateScope().ServiceProvider.GetService<ILogger<GetCampaignByNameQueryHandler>>();
        }

        public async Task<Campaign> Handle(GetCampaignByNameQuery request, CancellationToken cancellationToken)
        {
            try 
            {
                Campaign campaign = await _dbContext.Campaigns
                    .Include(campaign => campaign.Workouts)
                        .ThenInclude(w => w.Exercises)
                    .Include(campaign => campaign.Workouts)
                        .ThenInclude(w => w.CompletedBy)
                    .FirstOrDefaultAsync(c => c.Name == request.Name);

                return campaign;
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                throw e;
            }
        }
    }
}
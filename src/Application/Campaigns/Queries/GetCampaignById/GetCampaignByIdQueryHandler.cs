using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Application.Campaigns.Queries.GetCampaignById
{
    public class GetCampaignByIdQueryHandler : IRequestHandler<GetCampaignByIdQuery, Campaign>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly ILogger<GetCampaignByIdQueryHandler> _logger;

        public GetCampaignByIdQueryHandler(IServiceScopeFactory services)
        {
            _dbContext = services.CreateScope().ServiceProvider.GetService<IApplicationDbContext>();
            _logger = services.CreateScope().ServiceProvider.GetService<ILogger<GetCampaignByIdQueryHandler>>();
        }

        public async Task<Campaign> Handle(GetCampaignByIdQuery request, CancellationToken cancellationToken)
        {
            try 
            {
                
                Campaign campaign = await _dbContext.Campaigns
                    .Include(campaign => campaign.Workouts)
                        .ThenInclude(w => w.Exercises)
                    .Include(campaign => campaign.Workouts)
                        .ThenInclude(w => w.CompletedBy)
                    .Include(campaign => campaign.Participants)
                    .FirstOrDefaultAsync(campaign => campaign.Id == request.CampaignId);

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
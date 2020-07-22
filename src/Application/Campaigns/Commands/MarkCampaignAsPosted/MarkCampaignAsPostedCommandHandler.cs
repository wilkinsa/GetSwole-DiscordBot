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
    public class MarkCampaignAsPostedCommandHandler : IRequestHandler<MarkCampaignAsPostedCommand, Guid>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly ILogger<MarkCampaignAsPostedCommandHandler> _logger;

        public MarkCampaignAsPostedCommandHandler(IServiceScopeFactory services)
        {
            _dbContext = services.CreateScope().ServiceProvider.GetService<IApplicationDbContext>();
            _logger = services.CreateScope().ServiceProvider.GetService<ILogger<MarkCampaignAsPostedCommandHandler>>();
        }
        public async Task<Guid> Handle(MarkCampaignAsPostedCommand request, CancellationToken cancellationToken)
        {
            try 
            {
                Campaign campaign = await _dbContext.Campaigns.FirstOrDefaultAsync(campaign => campaign.Id == request.CampaignId);

                if (campaign == null) throw new NotFoundException(nameof(campaign), request.CampaignId);

                campaign.PostId = request.PostId;

                await _dbContext.SaveChangesAsync(cancellationToken);

                return campaign.Id;
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                throw e;
            }
        }
    }
}
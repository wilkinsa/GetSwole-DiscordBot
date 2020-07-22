using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Application.Campaigns.Commands.AddCampaignParticipant
{
    public class RemoveCampaignParticipantCommandHandler : IRequestHandler<RemoveCampaignParticipantCommand, Campaign>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly ILogger<RemoveCampaignParticipantCommandHandler> _logger;

        public RemoveCampaignParticipantCommandHandler(IServiceScopeFactory services)
        {
            _dbContext = services.CreateScope().ServiceProvider.GetService<IApplicationDbContext>();
            _logger = services.CreateScope().ServiceProvider.GetService<ILogger<RemoveCampaignParticipantCommandHandler>>();
        }
        public async Task<Campaign> Handle(RemoveCampaignParticipantCommand request, CancellationToken cancellationToken)
        {
            try 
            {
                Campaign campaign = await _dbContext.Campaigns
                    .Include(c => c.Participants)
                    .FirstOrDefaultAsync(c => c.Id == request.CampaignId);

                campaign.Participants.RemoveAll(u => u.UserId == request.UserId);

                await _dbContext.SaveChangesAsync(cancellationToken);

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
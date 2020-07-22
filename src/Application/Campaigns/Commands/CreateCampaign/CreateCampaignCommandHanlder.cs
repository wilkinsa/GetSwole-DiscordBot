using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Application.Campaigns.Commands.CreateCampaign
{
    public class CreateCampaignCommandHandler : IRequestHandler<CreateCampaignCommand, Guid>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly ILogger<CreateCampaignCommandHandler> _logger;

        public CreateCampaignCommandHandler(IServiceScopeFactory services)
        {
            _dbContext = services.CreateScope().ServiceProvider.GetService<IApplicationDbContext>();
            _logger = services.CreateScope().ServiceProvider.GetService<ILogger<CreateCampaignCommandHandler>>();
        }
        public async Task<Guid> Handle(CreateCampaignCommand request, CancellationToken cancellationToken)
        {
            try 
            {
                Campaign campaign = new Campaign
                {
                    Name = request.Name,
                    StartDate = request.StartDate
                };

                _dbContext.Campaigns.Add(campaign);
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
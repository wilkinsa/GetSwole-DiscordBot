using System;
using Domain.Entities;
using MediatR;

namespace Application.Campaigns.Commands.CreateCampaign
{
    public class CreateCampaignCommand : IRequest<Guid>
    {
        public string Name { get; set; }
        public DateTimeOffset StartDate { get; set; }
    }
}
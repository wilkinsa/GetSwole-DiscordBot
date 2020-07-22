using System;
using Domain.Entities;
using MediatR;

namespace Application.Campaigns.Queries.GetCampaignById
{
    public class GetCampaignByIdQuery : IRequest<Campaign>
    {
        public Guid CampaignId { get; set; }

        public GetCampaignByIdQuery(Guid campaignId)
        {
            CampaignId = campaignId;
        }
    }
}
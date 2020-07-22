using System;
using Domain.Entities;
using MediatR;

namespace Application.Campaigns.Commands.AddCampaignParticipant
{
    public class AddCampaignParticipantCommand : IRequest<Campaign>
    {
        public Guid CampaignId { get; set; }
        public ulong UserId { get; set; }
        public string UserName { get; set; }

        public AddCampaignParticipantCommand(Guid campaignId, ulong userId, string userName)
        {
            CampaignId = campaignId;
            UserId = userId;
            UserName = userName;
        }
    }
}
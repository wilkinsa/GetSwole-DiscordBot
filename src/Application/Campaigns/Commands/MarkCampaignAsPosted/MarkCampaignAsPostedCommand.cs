using System;
using MediatR;

namespace Application.Workouts.Commands.MarkWorkoutAsPosted
{
    public class MarkCampaignAsPostedCommand : IRequest<Guid>
    {
        public Guid CampaignId { get; set; }
        public ulong PostId { get; set; }

        public MarkCampaignAsPostedCommand(Guid campaignId, ulong postId)
        {
            CampaignId = campaignId;
            PostId = postId;
        }
    }
}
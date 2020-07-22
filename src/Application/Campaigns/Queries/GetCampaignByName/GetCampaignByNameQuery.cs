using Domain.Entities;
using MediatR;

namespace Application.Campaigns.Queries.GetCampaignByName
{
    public class GetCampaignByNameQuery : IRequest<Campaign>
    {
        public string Name { get; set; }

        public GetCampaignByNameQuery(string name)
        {
            Name = name;
        }
    }
    
}
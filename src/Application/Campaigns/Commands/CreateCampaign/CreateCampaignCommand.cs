using System;
using Domain.Entities;
using MediatR;

namespace Application.Campaigns.Commands.CreateCampaign
{
    public class CreateCampaignCommand : IRequest<Campaign>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTimeOffset StartDate { get; set; }

        public CreateCampaignCommand(){}

        public CreateCampaignCommand(string name, string description, DateTimeOffset startDate)
        {
            Name = name;
            Description = description;
            StartDate = startDate;
        }
    }
}
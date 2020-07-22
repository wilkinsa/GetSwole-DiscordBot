using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class Campaign
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public ulong PostId { get; set; }
        public List<User> Participants { get; set; }
        public List<Workout> Workouts { get; set; }

        public Campaign()
        {
            Workouts = new List<Workout>();
            Participants = new List<User>();
        }
    }
}
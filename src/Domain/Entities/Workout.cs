using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class Workout 
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool Posted { get; set; } = false;
        public ulong PostId { get; set; }
        public DateTimeOffset WorkoutDate { get; set; }
        public List<Exercise> Exercises { get; set; }
        public List<User> CompletedBy {get; set;} = new List<User>();

        public Campaign Campaign { get; set; }

        public Workout()
        {
            Exercises = new List<Exercise>();
            CompletedBy = new List<User>();
        }
    }
}
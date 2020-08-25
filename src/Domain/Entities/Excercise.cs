using System;

namespace Domain.Entities
{
    public class Exercise
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public string MuscleGroup { get; set; }
    }
}

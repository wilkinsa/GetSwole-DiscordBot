using System;
using MediatR;

namespace Application.Workouts.Commands.MarkWorkoutAsPosted
{
    public class MarkWorkoutAsPostedCommand : IRequest<Guid>
    {
        public Guid WorkoutId { get; set; }
        public ulong PostId { get; set; }

        public MarkWorkoutAsPostedCommand(Guid workoutId, ulong postId)
        {
            WorkoutId = workoutId;
            PostId = postId;
        }
    }
}
using Domain.Entities;
using MediatR;

namespace Application.Workouts.Commands.WorkoutRemoveCompletedUser
{
    public class WorkoutRemoveCompletedUserCommand : IRequest<Workout>
    {
        public ulong PostId { get; set; }
        public ulong UserId { get; set; }
        public string UserName { get; set; }

        public WorkoutRemoveCompletedUserCommand(ulong postId, ulong userId, string userName)
        {
            PostId = postId;
            UserId = userId;
            UserName = userName;
        }
    }
}
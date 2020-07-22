using Domain.Entities;
using MediatR;

namespace Application.Workouts.Commands.WorkoutAddCompletedUser
{
    public class WorkoutAddCompletedUserCommand : IRequest<Workout>
    {
        public ulong PostId { get; set; }
        public ulong UserId { get; set; }
        public string UserName { get; set; }

        public WorkoutAddCompletedUserCommand(ulong postId, ulong userId, string userName)
        {
            PostId = postId;
            UserId = userId;
            UserName = userName;
        }
    }
}
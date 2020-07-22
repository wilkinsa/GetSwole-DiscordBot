using Domain.Entities;
using MediatR;

namespace Application.Workouts.Queries.GetWorkoutByPostId
{
    public class GetWorkoutByPostIdQuery : IRequest<Workout>
    {
        public ulong PostId { get; set; }

        public GetWorkoutByPostIdQuery(ulong postId)
        {
            PostId = postId;   
        }
    }
}
using System.Collections.Generic;
using Domain.Entities;
using MediatR;

namespace Application.Workouts.Queries.GetWorkoutReadyForPost
{
    public class GetWorkoutReadyForPostQuery : IRequest<List<Workout>>
    {
        
    }
}
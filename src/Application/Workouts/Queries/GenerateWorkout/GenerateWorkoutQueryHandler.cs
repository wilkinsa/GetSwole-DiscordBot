using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

public class GenerateWorkoutQueryHandler : IRequestHandler<GenerateWorkoutQuery, Workout>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly ILogger<GenerateWorkoutQueryHandler> _logger;

    public GenerateWorkoutQueryHandler(IServiceScopeFactory services)
    {
        _dbContext = services.CreateScope().ServiceProvider.GetService<IApplicationDbContext>();
        _logger = services.CreateScope().ServiceProvider.GetService<ILogger<GenerateWorkoutQueryHandler>>();
    }

    public async Task<Workout> Handle(GenerateWorkoutQuery request, CancellationToken cancellationToken)
    {
        try
        {
            if(!_dbContext.ExerciseOptions.Any()) {
                _dbContext.ExerciseOptions.AddRange(new List<ExerciseOption>
                {
                    new ExerciseOption {Name = "Pushups", Value = "reps", MuscleGroup = "Chest" },
                    new ExerciseOption {Name = "Squats", Value = "reps", MuscleGroup = "Legs" },
                    new ExerciseOption {Name = "Lunges", Value = "reps", MuscleGroup = "Legs" },
                    new ExerciseOption {Name = "Rows", Value = "reps", MuscleGroup = "Back" },
                    new ExerciseOption {Name = "Plank", Value = "seconds", MuscleGroup = "Core" },
                    new ExerciseOption {Name = "Sit Ups", Value = "reps", MuscleGroup = "Core" },
                    new ExerciseOption {Name = "Incline Pushups", Value = "reps", MuscleGroup = "Chest" },
                });
                await _dbContext.SaveChangesAsync(cancellationToken);
            }
            Random rnd = new Random();
            var excercises = await _dbContext.ExerciseOptions.ToListAsync();
            var ChestWorkout = excercises.Where(e => e.MuscleGroup == "Chest").ToArray()[rnd.Next(excercises.Where(e => e.MuscleGroup == "Chest").Count()) - 1];
            var BackWorkout = excercises.Where(e => e.MuscleGroup == "Back").ToArray()[rnd.Next(excercises.Where(e => e.MuscleGroup == "Back").Count()) - 1];
            var LegWorkout = excercises.Where(e => e.MuscleGroup == "Legs").ToArray()[rnd.Next(excercises.Where(e => e.MuscleGroup == "Legs").Count()) - 1];
            var CoreWorkout = excercises.Where(e => e.MuscleGroup == "Core").ToArray()[rnd.Next(excercises.Where(e => e.MuscleGroup == "Core").Count()) - 1];

            var Workout = new Workout
            {
                Name = "Daily Workout",
                WorkoutDate = DateTimeOffset.Now.Date,
                Exercises = new List<Exercise>
                {
                   new Exercise 
                   {
                       Name = ChestWorkout.Name,
                       Value = $"{rnd.Next(20, 50)} {ChestWorkout.Value}",
                       MuscleGroup = ChestWorkout.MuscleGroup
                   },
                   new Exercise
                   {
                       Name = BackWorkout.Name,
                       Value = $"{rnd.Next(20, 50)} {BackWorkout.Value}",
                       MuscleGroup = BackWorkout.MuscleGroup
                   },
                   new Exercise
                   {
                       Name = LegWorkout.Name,
                       Value = $"{rnd.Next(20, 50)} {LegWorkout.Value}",
                       MuscleGroup = LegWorkout.MuscleGroup
                   },
                   new Exercise
                   {
                       Name = CoreWorkout.Name,
                       Value = $"{rnd.Next(20, 50)} {CoreWorkout.Value}",
                       MuscleGroup = CoreWorkout.MuscleGroup
                   }
                }
            };

            _dbContext.Workouts.Add(Workout);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return Workout;
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            throw;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Entities;

namespace Application
{
    public class WorkoutData 
    {
        private static readonly WorkoutData _instance = new WorkoutData();
        private List<Workout> workouts = new List<Workout>();
        
        private WorkoutData()
        {
            workouts.AddRange(GetList());
        }

        public static WorkoutData GetWorkouts()
        {
            return _instance;
        }

        public Workout GetWorkout(ulong postId) 
        {
            return workouts.FirstOrDefault(w => w.PostId == postId);
        }

        public Workout GetWorkoutByName(string name)
        {
            return workouts.FirstOrDefault(w => w.Name.ToLowerInvariant() == name.ToLowerInvariant());
        }

        public List<Workout> GetWorkoutList() => workouts;
        private static List<Workout> GetList() 
        {
            return new List<Workout> 
            {
                new Workout 
                {
                    Name = "Day One",
                    WorkoutDate = DateTimeOffset.Now,
                    Exercises = new List<Exercise>
                    {
                        new Exercise
                        {
                            Name = "Squats",
                            Value = "20"
                        },
                        new Exercise
                        {
                            Name = "Push ups",
                            Value = "20"
                        },
                        new Exercise
                        {
                            Name = "Back ext. rows",
                            Value = "20"
                        },
                        new Exercise
                        {
                            Name = "Plank",
                            Value = "30 seconds"
                        }
                    },
                },
                new Workout 
                {
                    Name = "Day Three",
                    WorkoutDate = DateTimeOffset.Now,
                    Exercises = new List<Exercise>
                    {
                        new Exercise
                        {
                            Name = "Squats",
                            Value = "20"
                        },
                        new Exercise
                        {
                            Name = "Push ups",
                            Value = "20"
                        },
                        new Exercise
                        {
                            Name = "Back ext. rows",
                            Value = "20"
                        },
                        new Exercise
                        {
                            Name = "Plank",
                            Value = "30 seconds"
                        }
                    },
                },
                new Workout 
                {
                    Name = "Day Five",
                    WorkoutDate = DateTimeOffset.Now,
                    Exercises = new List<Exercise>
                    {
                        new Exercise
                        {
                            Name = "Squats",
                            Value = "25"
                        },
                        new Exercise
                        {
                            Name = "Push ups",
                            Value = "25"
                        },
                        new Exercise
                        {
                            Name = "Back ext. rows",
                            Value = "25"
                        },
                        new Exercise
                        {
                            Name = "Plank",
                            Value = "35 seconds"
                        }
                    },
                }
            };
        }
    }
}
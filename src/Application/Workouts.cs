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
            DateTimeOffset startDate = DateTimeOffset.Parse("07/24/2020");
            return new List<Workout> 
            {
                new Workout 
                {
                    Name = "Day 1",
                    WorkoutDate = startDate,
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
                    Name = "Day 3",
                    WorkoutDate = startDate.AddDays(2),
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
                    Name = "Day 5",
                    WorkoutDate = startDate.AddDays(4),
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
                },
                new Workout 
                {
                    Name = "Day 7",
                    WorkoutDate = startDate.AddDays(6),
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
                },
                new Workout 
                {
                    Name = "Day 9",
                    WorkoutDate = startDate.AddDays(8),
                    Exercises = new List<Exercise>
                    {
                        new Exercise
                        {
                            Name = "Squats",
                            Value = "30"
                        },
                        new Exercise
                        {
                            Name = "Push ups",
                            Value = "30"
                        },
                        new Exercise
                        {
                            Name = "Back ext. rows",
                            Value = "30"
                        },
                        new Exercise
                        {
                            Name = "Plank",
                            Value = "40 seconds"
                        }
                    },
                },
                new Workout 
                {
                    Name = "Day 11",
                    WorkoutDate = startDate.AddDays(10),
                    Exercises = new List<Exercise>
                    {
                        new Exercise
                        {
                            Name = "Squats",
                            Value = "30"
                        },
                        new Exercise
                        {
                            Name = "Push ups",
                            Value = "30"
                        },
                        new Exercise
                        {
                            Name = "Back ext. rows",
                            Value = "30"
                        },
                        new Exercise
                        {
                            Name = "Plank",
                            Value = "40 seconds"
                        }
                    },
                },
                new Workout 
                {
                    Name = "Day 13",
                    WorkoutDate = startDate.AddDays(12),
                    Exercises = new List<Exercise>
                    {
                        new Exercise
                        {
                            Name = "Squats",
                            Value = "35"
                        },
                        new Exercise
                        {
                            Name = "Push ups",
                            Value = "35"
                        },
                        new Exercise
                        {
                            Name = "Back ext. rows",
                            Value = "35"
                        },
                        new Exercise
                        {
                            Name = "Plank",
                            Value = "45 seconds"
                        }
                    },
                },
                new Workout 
                {
                    Name = "Day 15",
                    WorkoutDate = startDate.AddDays(14),
                    Exercises = new List<Exercise>
                    {
                        new Exercise
                        {
                            Name = "Squats",
                            Value = "35"
                        },
                        new Exercise
                        {
                            Name = "Push ups",
                            Value = "35"
                        },
                        new Exercise
                        {
                            Name = "Back ext. rows",
                            Value = "35"
                        },
                        new Exercise
                        {
                            Name = "Plank",
                            Value = "45 seconds"
                        }
                    },
                },
                new Workout 
                {
                    Name = "Day 17",
                    WorkoutDate = startDate.AddDays(16),
                    Exercises = new List<Exercise>
                    {
                        new Exercise
                        {
                            Name = "Squats",
                            Value = "40"
                        },
                        new Exercise
                        {
                            Name = "Push ups",
                            Value = "40"
                        },
                        new Exercise
                        {
                            Name = "Back ext. rows",
                            Value = "40"
                        },
                        new Exercise
                        {
                            Name = "Plank",
                            Value = "50 seconds"
                        }
                    },
                },
                new Workout 
                {
                    Name = "Day 19",
                    WorkoutDate = startDate.AddDays(18),
                    Exercises = new List<Exercise>
                    {
                        new Exercise
                        {
                            Name = "Squats",
                            Value = "40"
                        },
                        new Exercise
                        {
                            Name = "Push ups",
                            Value = "40"
                        },
                        new Exercise
                        {
                            Name = "Back ext. rows",
                            Value = "40"
                        },
                        new Exercise
                        {
                            Name = "Plank",
                            Value = "50 seconds"
                        }
                    },
                },
                new Workout 
                {
                    Name = "Day 21",
                    WorkoutDate = startDate.AddDays(20),
                    Exercises = new List<Exercise>
                    {
                        new Exercise
                        {
                            Name = "Squats",
                            Value = "45"
                        },
                        new Exercise
                        {
                            Name = "Push ups",
                            Value = "45"
                        },
                        new Exercise
                        {
                            Name = "Back ext. rows",
                            Value = "45"
                        },
                        new Exercise
                        {
                            Name = "Plank",
                            Value = "55 seconds"
                        }
                    },
                },
                new Workout 
                {
                    Name = "Day 23",
                    WorkoutDate = startDate.AddDays(22),
                    Exercises = new List<Exercise>
                    {
                        new Exercise
                        {
                            Name = "Squats",
                            Value = "45"
                        },
                        new Exercise
                        {
                            Name = "Push ups",
                            Value = "45"
                        },
                        new Exercise
                        {
                            Name = "Back ext. rows",
                            Value = "45"
                        },
                        new Exercise
                        {
                            Name = "Plank",
                            Value = "55 seconds"
                        }
                    },
                },
                new Workout 
                {
                    Name = "Day 25",
                    WorkoutDate = startDate.AddDays(24),
                    Exercises = new List<Exercise>
                    {
                        new Exercise
                        {
                            Name = "Squats",
                            Value = "50"
                        },
                        new Exercise
                        {
                            Name = "Push ups",
                            Value = "50"
                        },
                        new Exercise
                        {
                            Name = "Back ext. rows",
                            Value = "50"
                        },
                        new Exercise
                        {
                            Name = "Plank",
                            Value = "60 seconds"
                        }
                    },
                },
                new Workout 
                {
                    Name = "Day 27",
                    WorkoutDate = startDate.AddDays(26),
                    Exercises = new List<Exercise>
                    {
                        new Exercise
                        {
                            Name = "Squats",
                            Value = "50"
                        },
                        new Exercise
                        {
                            Name = "Push ups",
                            Value = "50"
                        },
                        new Exercise
                        {
                            Name = "Back ext. rows",
                            Value = "50"
                        },
                        new Exercise
                        {
                            Name = "Plank",
                            Value = "60 seconds"
                        }
                    },
                },
                new Workout 
                {
                    Name = "Day 29",
                    WorkoutDate = startDate.AddDays(28),
                    Exercises = new List<Exercise>
                    {
                        new Exercise
                        {
                            Name = "Squats",
                            Value = "60"
                        },
                        new Exercise
                        {
                            Name = "Push ups",
                            Value = "60"
                        },
                        new Exercise
                        {
                            Name = "Back ext. rows",
                            Value = "60"
                        },
                        new Exercise
                        {
                            Name = "Plank",
                            Value = "70 seconds"
                        }
                    },
                }
            };
        }
    }
}
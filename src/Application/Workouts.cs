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
            DateTimeOffset startDate = DateTimeOffset.Parse("08/31/2020").AddHours(3);
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
                            Name = "Sit ups",
                            Value = "10"
                        },
                        new Exercise
                        {
                            Name = "Heel taps",
                            Value = "10"
                        },
                        new Exercise
                        {
                            Name = "Flutter kicks",
                            Value = "10"
                        },
                        new Exercise
                        {
                            Name = "Side plank",
                            Value = "20 seconds"
                        }
                    },
                },
                new Workout
                {
                    Name = "Day 2",
                    WorkoutDate = startDate.AddDays(1),
                    Exercises = new List<Exercise>
                    {
                        new Exercise
                        {
                            Name = "Sit ups",
                            Value = "10"
                        },
                        new Exercise
                        {
                            Name = "Heel taps",
                            Value = "10"
                        },
                        new Exercise
                        {
                            Name = "Flutter kicks",
                            Value = "10"
                        },
                        new Exercise
                        {
                            Name = "Side plank",
                            Value = "20 seconds"
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
                            Name = "Sit ups",
                            Value = "15"
                        },
                        new Exercise
                        {
                            Name = "Heel taps",
                            Value = "15"
                        },
                        new Exercise
                        {
                            Name = "Flutter kicks",
                            Value = "15"
                        },
                        new Exercise
                        {
                            Name = "Side plank",
                            Value = "20 seconds"
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
                            Name = "Sit ups",
                            Value = "15"
                        },
                        new Exercise
                        {
                            Name = "Heel taps",
                            Value = "15"
                        },
                        new Exercise
                        {
                            Name = "Flutter kicks",
                            Value = "15"
                        },
                        new Exercise
                        {
                            Name = "Side plank",
                            Value = "30 seconds"
                        }
                    },
                },
                new Workout
                {
                    Name = "Day 6",
                    WorkoutDate = startDate.AddDays(5),
                    Exercises = new List<Exercise>
                    {
                        new Exercise
                        {
                            Name = "Sit ups",
                            Value = "20"
                        },
                        new Exercise
                        {
                            Name = "Heel taps",
                            Value = "20"
                        },
                        new Exercise
                        {
                            Name = "Flutter kicks",
                            Value = "20"
                        },
                        new Exercise
                        {
                            Name = "Side plank",
                            Value = "20 seconds"
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
                            Name = "Sit ups",
                            Value = "20"
                        },
                        new Exercise
                        {
                            Name = "Heel taps",
                            Value = "20"
                        },
                        new Exercise
                        {
                            Name = "Flutter kicks",
                            Value = "20"
                        },
                        new Exercise
                        {
                            Name = "Side plank",
                            Value = "30 seconds"
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
                            Name = "Sit ups",
                            Value = "25"
                        },
                        new Exercise
                        {
                            Name = "Heel taps",
                            Value = "25"
                        },
                        new Exercise
                        {
                            Name = "Flutter kicks",
                            Value = "25"
                        },
                        new Exercise
                        {
                            Name = "Side plank",
                            Value = "35 seconds"
                        }
                    },
                },
                new Workout
                {
                    Name = "Day 10",
                    WorkoutDate = startDate.AddDays(9),
                    Exercises = new List<Exercise>
                    {
                        new Exercise
                        {
                            Name = "Sit ups",
                            Value = "25"
                        },
                        new Exercise
                        {
                            Name = "Heel taps",
                            Value = "25"
                        },
                        new Exercise
                        {
                            Name = "Flutter kicks",
                            Value = "25"
                        },
                        new Exercise
                        {
                            Name = "Side plank",
                            Value = "35 seconds"
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
                            Name = "Sit ups",
                            Value = "30"
                        },
                        new Exercise
                        {
                            Name = "Heel taps",
                            Value = "30"
                        },
                        new Exercise
                        {
                            Name = "Flutter kicks",
                            Value = "30"
                        },
                        new Exercise
                        {
                            Name = "Side plank",
                            Value = "35 seconds"
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
                            Name = "Sit ups",
                            Value = "30"
                        },
                        new Exercise
                        {
                            Name = "Heel taps",
                            Value = "30"
                        },
                        new Exercise
                        {
                            Name = "Flutter kicks",
                            Value = "30"
                        },
                        new Exercise
                        {
                            Name = "Side plank",
                            Value = "40 seconds"
                        }
                    },
                },
                new Workout
                {
                    Name = "Day 14",
                    WorkoutDate = startDate.AddDays(13),
                    Exercises = new List<Exercise>
                    {
                        new Exercise
                        {
                            Name = "Sit ups",
                            Value = "35"
                        },
                        new Exercise
                        {
                            Name = "Heel taps",
                            Value = "35"
                        },
                        new Exercise
                        {
                            Name = "Flutter kicks",
                            Value = "35"
                        },
                        new Exercise
                        {
                            Name = "Side plank",
                            Value = "40 seconds"
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
                            Name = "Sit ups",
                            Value = "35"
                        },
                        new Exercise
                        {
                            Name = "Heel taps",
                            Value = "35"
                        },
                        new Exercise
                        {
                            Name = "Flutter kicks",
                            Value = "35"
                        },
                        new Exercise
                        {
                            Name = "Side plank",
                            Value = "40 seconds"
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
                            Name = "Sit ups",
                            Value = "40"
                        },
                        new Exercise
                        {
                            Name = "Heel taps",
                            Value = "40"
                        },
                        new Exercise
                        {
                            Name = "Flutter kicks",
                            Value = "40"
                        },
                        new Exercise
                        {
                            Name = "Side plank",
                            Value = "45 seconds"
                        }
                    },
                },
                new Workout
                {
                    Name = "Day 18",
                    WorkoutDate = startDate.AddDays(17),
                    Exercises = new List<Exercise>
                    {
                        new Exercise
                        {
                            Name = "Sit ups",
                            Value = "40"
                        },
                        new Exercise
                        {
                            Name = "Heel taps",
                            Value = "40"
                        },
                        new Exercise
                        {
                            Name = "Flutter kicks",
                            Value = "40"
                        },
                        new Exercise
                        {
                            Name = "Side plank",
                            Value = "45 seconds"
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
                            Name = "Sit ups",
                            Value = "50"
                        },
                        new Exercise
                        {
                            Name = "Heel taps",
                            Value = "50"
                        },
                        new Exercise
                        {
                            Name = "Flutter kicks",
                            Value = "50"
                        },
                        new Exercise
                        {
                            Name = "Side plank",
                            Value = "45 seconds"
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
                            Name = "Sit ups",
                            Value = "50"
                        },
                        new Exercise
                        {
                            Name = "Heel taps",
                            Value = "50"
                        },
                        new Exercise
                        {
                            Name = "Flutter kicks",
                            Value = "50"
                        },
                        new Exercise
                        {
                            Name = "Side plank",
                            Value = "50 seconds"
                        }
                    },
                },
                new Workout
                {
                    Name = "Day 22",
                    WorkoutDate = startDate.AddDays(21),
                    Exercises = new List<Exercise>
                    {
                        new Exercise
                        {
                            Name = "Sit ups",
                            Value = "55"
                        },
                        new Exercise
                        {
                            Name = "Heel taps",
                            Value = "55"
                        },
                        new Exercise
                        {
                            Name = "Flutter kicks",
                            Value = "55"
                        },
                        new Exercise
                        {
                            Name = "Side plank",
                            Value = "50 seconds"
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
                            Name = "Sit ups",
                            Value = "60"
                        },
                        new Exercise
                        {
                            Name = "Heel taps",
                            Value = "60"
                        },
                        new Exercise
                        {
                            Name = "Flutter kicks",
                            Value = "60"
                        },
                        new Exercise
                        {
                            Name = "Side plank",
                            Value = "50 seconds"
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
                            Name = "Sit ups",
                            Value = "60"
                        },
                        new Exercise
                        {
                            Name = "Heel taps",
                            Value = "60"
                        },
                        new Exercise
                        {
                            Name = "Flutter kicks",
                            Value = "60"
                        },
                        new Exercise
                        {
                            Name = "Side plank",
                            Value = "55 seconds"
                        }
                    },
                },
                new Workout
                {
                    Name = "Day 26",
                    WorkoutDate = startDate.AddDays(25),
                    Exercises = new List<Exercise>
                    {
                        new Exercise
                        {
                            Name = "Sit ups",
                            Value = "65"
                        },
                        new Exercise
                        {
                            Name = "Heel taps",
                            Value = "65"
                        },
                        new Exercise
                        {
                            Name = "Flutter kicks",
                            Value = "65"
                        },
                        new Exercise
                        {
                            Name = "Side plank",
                            Value = "55 seconds"
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
                            Name = "Sit ups",
                            Value = "70"
                        },
                        new Exercise
                        {
                            Name = "Heel taps",
                            Value = "70"
                        },
                        new Exercise
                        {
                            Name = "Flutter kicks",
                            Value = "70"
                        },
                        new Exercise
                        {
                            Name = "Side plank",
                            Value = "55 seconds"
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
                            Name = "Sit ups",
                            Value = "80"
                        },
                        new Exercise
                        {
                            Name = "Heel taps",
                            Value = "80"
                        },
                        new Exercise
                        {
                            Name = "Flutter kicks",
                            Value = "80"
                        },
                        new Exercise
                        {
                            Name = "Side plank",
                            Value = "60 seconds"
                        }
                    },
                },
                new Workout
                {
                    Name = "Day 30",
                    WorkoutDate = startDate.AddDays(29),
                    Exercises = new List<Exercise>
                    {
                        new Exercise
                        {
                            Name = "Sit ups",
                            Value = "90"
                        },
                        new Exercise
                        {
                            Name = "Heel taps",
                            Value = "90"
                        },
                        new Exercise
                        {
                            Name = "Flutter kicks",
                            Value = "90"
                        },
                        new Exercise
                        {
                            Name = "Side plank",
                            Value = "70 seconds"
                        }
                    },
                },
            };
        }
    }
}
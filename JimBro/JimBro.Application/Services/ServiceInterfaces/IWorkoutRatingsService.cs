using JimBro.Domain;

namespace JimBro.Services.ServiceInterfaces;

public interface IWorkoutRatingsService
{
    void Insert(WorkoutRating workoutRating);
    List<WorkoutRating> GetTrainersRatings(long trainerId);
}
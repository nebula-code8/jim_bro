namespace JimBro.Domain.RepositoryInterfaces;

public interface IWorkoutRatingsRepository
{
    void Insert(WorkoutRating workoutRating);
    List<WorkoutRating> GetTrainersRatings(long trainerId);
}
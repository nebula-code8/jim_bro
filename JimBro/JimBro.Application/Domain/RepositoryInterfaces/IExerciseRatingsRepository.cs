namespace JimBro.Domain.RepositoryInterfaces;

public interface IExerciseRatingsRepository
{
    void Insert(ExerciseRating exerciseRating);
    List<ExerciseRating> GetClientsRatings(long clientId, long exerciseId);
}
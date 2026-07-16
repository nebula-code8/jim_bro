using JimBro.Domain;

namespace JimBro.Services.ServiceInterfaces;

public interface IExerciseRatingsService
{
    void Insert(ExerciseRating exerciseRating);
    List<ExerciseRating> GetClientsRatings(long clientId, long exerciseId);
}
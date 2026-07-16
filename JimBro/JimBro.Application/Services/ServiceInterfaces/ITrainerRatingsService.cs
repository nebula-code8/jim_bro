using JimBro.Domain;

namespace JimBro.Services.ServiceInterfaces;

public interface ITrainerRatingsService
{
    List<TrainerRating> GetAllRatings();
    void Insert(TrainerRating trainerRating);
    List<TrainerRating> GetClientRatings(long clientId);
}
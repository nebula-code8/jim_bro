namespace JimBro.Domain.RepositoryInterfaces;

public interface ITrainerRatingsRepository
{
    List<TrainerRating> GetAllRatings();
    void Insert(TrainerRating trainerRating);
    List<TrainerRating> GetClientRatings(long clientId);
}
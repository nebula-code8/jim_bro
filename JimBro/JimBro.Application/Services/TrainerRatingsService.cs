using JimBro.Domain;
using JimBro.Domain.RepositoryInterfaces;
using JimBro.Services.ServiceInterfaces;

namespace JimBro.Services;

public class TrainerRatingsService : ITrainerRatingsService
{
    private readonly ITrainerRatingsRepository _repository;
    
    public TrainerRatingsService(ITrainerRatingsRepository repository)
    {
        _repository = repository;
    }

    public void Insert(TrainerRating trainerRating)
    {
        if(string.IsNullOrEmpty(trainerRating.Rating.ToString()))
            throw new Exception("Ocena je obavezno polje!");
        
        if(trainerRating.Rating <= 1 || trainerRating.Rating > 10)
            throw new Exception("Ocena mora biti izmedju 1 i 10!");
        _repository.Insert(trainerRating);    
    } 
    
    public List<TrainerRating> GetAllRatings() => _repository.GetAllRatings();
    public List<TrainerRating> GetClientRatings(long clientId) => _repository.GetClientRatings(clientId);
}
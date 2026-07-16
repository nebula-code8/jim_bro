using JimBro.Domain;
using JimBro.Domain.RepositoryInterfaces;
using JimBro.Services.ServiceInterfaces;

namespace JimBro.Services;

public class TrainerService : ITrainerService
{
    private readonly ITrainerRepository _trainerRepository;

    public TrainerService(ITrainerRepository trainerRepository)
    {
        _trainerRepository = trainerRepository;
    }
    
    public List<Trainer> GetAllTrainers() => _trainerRepository.GetAllTrainers();
    public List<Trainer> GetClientTrainers(long clientId) => _trainerRepository.GetClientTrainers(clientId);
    public Trainer? GetById(long id) => _trainerRepository.GetById(id);
}
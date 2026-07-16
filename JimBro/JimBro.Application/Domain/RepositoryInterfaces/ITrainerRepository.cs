namespace JimBro.Domain.RepositoryInterfaces;

public interface ITrainerRepository
{
    List<Trainer> GetAllTrainers();
    List<Trainer> GetClientTrainers(long clientId);
    Trainer? GetById(long id);
}
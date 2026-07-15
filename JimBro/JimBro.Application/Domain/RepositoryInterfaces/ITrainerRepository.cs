namespace JimBro.Domain.RepositoryInterfaces;

public interface ITrainerRepository
{
    List<Trainer> GetAllTrainers();
    Trainer? GetById(long id);
}
using JimBro.Domain;

namespace JimBro.Services.ServiceInterfaces;

public interface ITrainerService
{
    List<Trainer> GetAllTrainers();
    List<Trainer> GetClientTrainers(long clientId);
    Trainer? GetById(long id);
}
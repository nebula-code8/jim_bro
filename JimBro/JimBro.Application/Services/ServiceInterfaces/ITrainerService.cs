using JimBro.Domain;

namespace JimBro.Services.ServiceInterfaces;

public interface ITrainerService
{
    List<Trainer> GetAllTrainers();
    Trainer? GetById(long id);
}
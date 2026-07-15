using JimBro.Domain;

namespace JimBro.Services.ServiceInterfaces;

public interface IWorkoutService
{
    Workout? GetById(long id);
    List<Workout> GetWorkoutsForClient(long clientId);
    long Insert(Workout workout);
}
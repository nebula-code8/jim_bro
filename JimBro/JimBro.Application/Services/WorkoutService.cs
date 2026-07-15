using JimBro.Domain;
using JimBro.Domain.RepositoryInterfaces;
using JimBro.Services.ServiceInterfaces;

namespace JimBro.Services;

public class WorkoutService : IWorkoutService
{
    private readonly IWorkoutRepository _repository;
    
    public WorkoutService(IWorkoutRepository repository)
    {
        _repository = repository;
    }

    public Workout? GetById(long id) =>  _repository.GetById(id);
    public List<Workout> GetWorkoutsForClient(long clientId) =>  _repository.GetWorkoutsForClient(clientId);

    public void Insert(Workout workout)
    {
        _repository.Insert(workout);
    }  
}
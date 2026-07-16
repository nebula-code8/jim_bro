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

    public long Insert(Workout workout)
    {
        return _repository.Insert(workout);
    }  
    public void UpdateWorkoutStatus(long workoutId, bool completed) => _repository.UpdateWorkoutStatus(workoutId, completed);
}
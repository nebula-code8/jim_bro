namespace JimBro.Domain.RepositoryInterfaces;

public interface IWorkoutRepository
{
    Workout? GetById(long id);
    List<Workout> GetWorkoutsForClient(long clientId);
    long Insert(Workout workout);
}
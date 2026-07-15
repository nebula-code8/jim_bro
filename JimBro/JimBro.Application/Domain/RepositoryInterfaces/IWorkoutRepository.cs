namespace JimBro.Domain.RepositoryInterfaces;

public interface IWorkoutRepository
{
    Workout? GetById(long id);
    List<Workout> GetWorkoutsForClient(long clientId);
    void Insert(Workout workout);
}
namespace JimBro.Domain.RepositoryInterfaces;

public interface IExerciseRepository
{
    Exercise? GetById(long id);
    List<Exercise> GetAllExercisesForTrainer(long teacherId);
    List<Exercise> GetExercisesForTrainerAndClient(long trainerId, long clientId);
    void Insert(Exercise exercise);
}
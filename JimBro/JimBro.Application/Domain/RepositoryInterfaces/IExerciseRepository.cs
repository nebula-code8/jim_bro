namespace JimBro.Domain.RepositoryInterfaces;

public interface IExerciseRepository
{
    Exercise? GetById(long id);
    List<Exercise> GetAllExercisesForTrainer(long teacherId);
    void Insert(Exercise exercise);
}
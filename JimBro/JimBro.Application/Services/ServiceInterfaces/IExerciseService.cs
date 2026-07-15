using JimBro.Domain;

namespace JimBro.Services.ServiceInterfaces;

public interface IExerciseService
{
    Exercise? GetById(long id);
    List<Exercise> GetAllExercisesForTrainer(long trainerId);
    void CreateExercise(Exercise exercise);
}
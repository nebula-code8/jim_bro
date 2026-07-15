using JimBro.Domain;

namespace JimBro.Services.ServiceInterfaces;

public interface IExerciseService
{
    Exercise? GetById(long id);
    List<Exercise> GetAllExercisesForTrainer(long trainerId);
    List<Exercise> GetExercisesForTrainerAndClient(long trainerId, long clientId);
    void CreateExercise(Exercise exercise);
}
using JimBro.Domain;

namespace JimBro.Services.ServiceInterfaces;

public interface IWorkoutExerciseService
{
    WorkoutExercise? GetById(long id);
    List<WorkoutExercise> GetByWorkoutId(long workoutId);
    List<WorkoutExercise> GetWorkoutexercisesForClient(long clientId, long workoutId);
    void Insert(WorkoutExercise workoutExercise);
    void Delete(long id);
}
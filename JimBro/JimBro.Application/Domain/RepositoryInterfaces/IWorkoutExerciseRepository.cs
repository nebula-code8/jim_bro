namespace JimBro.Domain.RepositoryInterfaces;

public interface IWorkoutExerciseRepository
{
    WorkoutExercise? GetById(long id);
    List<WorkoutExercise> GetByWorkoutId(long workoutId);
    List<WorkoutExercise> GetWorkoutexercisesForClient(long clientId, long workoutId);
    void Insert(WorkoutExercise workoutExercise);
    void Delete(long id);
}
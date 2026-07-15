namespace JimBro.Domain.RepositoryInterfaces;

public interface IWorkoutExerciseRepository
{
    WorkoutExercise? GetById(long id);
    List<WorkoutExercise> GetByWorkoutId(long workoutId);
    void Insert(WorkoutExercise workoutExercise);
    void Delete(long id);
}
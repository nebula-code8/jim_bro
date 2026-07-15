using System.Data;
using JimBro.Domain;
using JimBro.Domain.RepositoryInterfaces;

namespace JimBro.Database.Repositories;

public class WorkoutExerciseDbRepository : BaseRepository, IWorkoutExerciseRepository
{
    private readonly IWorkoutRepository _workoutRepository;
    private readonly IExerciseRepository _exerciseRepository;
    
    public WorkoutExerciseDbRepository()
    {
        _workoutRepository = new WorkoutDbRepository(new TrainerDbRepository(), new ClientDbRepository());
        _exerciseRepository = new ExerciseDbRepository();
    }
    public WorkoutExercise? GetById(long id) 
    { 
        using IDbConnection connection = CreateConnection();
        using IDbCommand command = connection.CreateCommand();
        command.CommandText = @"SELECT we.* FROM workout_exercise we WHERE we.id = @id";
        AddParameter(command, "@id", id);
        using IDataReader reader = command.ExecuteReader();
        return reader.Read() ? MapDbRowToWorkoutExercise(reader) : null;
    }
    
    public List<WorkoutExercise> GetByWorkoutId(long workoutId) 
    { 
        using IDbConnection connection = CreateConnection();
        using IDbCommand command = connection.CreateCommand();
        command.CommandText = @"SELECT we.* FROM workout_exercise we Where we.workout_id = @workoutId";
        AddParameter(command, "@workoutId", workoutId);
        var workoutExercises = new List<WorkoutExercise>();
        using IDataReader reader = command.ExecuteReader();
        while (reader.Read())
            workoutExercises.Add(MapDbRowToWorkoutExercise(reader));
        return workoutExercises;
    }
    
    public void Insert(WorkoutExercise workoutExercise)
    {
        using IDbConnection connection = CreateConnection();
        using IDbCommand command = connection.CreateCommand();
    
        command.CommandText = @"INSERT INTO workout_exercise (sets, reps, exercise_id, workout_id) VALUES (@sets, @reps, @exercise_id, @workout_id)"; 
        AddParameter(command, "@sets", workoutExercise.Sets);
        AddParameter(command, "@reps", workoutExercise.Reps);
        AddParameter(command, "@exercise_id", workoutExercise.Exercise.Id);
        AddParameter(command, "@workout_id", workoutExercise.Workout.Id);
        command.ExecuteNonQuery();
    }
    
    public void Delete(long id)
    {
        using IDbConnection connection = CreateConnection();
        using IDbCommand command = connection.CreateCommand();
        command.CommandText = "DELETE FROM workout_exercise WHERE id = @id";
        AddParameter(command, "@id", id);
        command.ExecuteNonQuery();
    }
        
    private WorkoutExercise MapDbRowToWorkoutExercise(IDataRecord reader)
    {
        var exerciseId = Convert.ToInt64(reader["exercise_id"]);
        var exercise = _exerciseRepository.GetById(exerciseId);

        var workoutId = Convert.ToInt64(reader["workout_id"]);
        var workout = _workoutRepository.GetById(workoutId);

        return new WorkoutExercise(Convert.ToInt64(reader["id"]), Convert.ToInt32(reader["sets"]),
            Convert.ToInt32(reader["reps"]), exercise, workout);
    }
}
using System.Data;
using JimBro.Domain;
using JimBro.Domain.RepositoryInterfaces;

namespace JimBro.Database.Repositories;

public class WorkoutExerciseDbRepository : BaseRepository, IWorkoutExerciseRepository
{
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
    
    public List<WorkoutExercise> GetWorkoutexercisesForClient(long clientId, long workoutId)
    {
        using IDbConnection connection = CreateConnection();
        using IDbCommand command = connection.CreateCommand();
        command.CommandText =@"SELECT we.*, e.name as ExerciseName, e.description as ExerciseDescription, a.id as AccessoryId, a.name as AccessoryName,
       m.id as MachineId, m.name as MachineName FROM workout_exercise we  INNER JOIN workout w ON we.workout_id = w.id INNER JOIN exercises e 
           ON we.exercise_id = e.id LEFT JOIN accessories a ON e.accessory_id = a.id LEFT JOIN machines m ON e.machine_id = m.id 
                                                WHERE w.client_id = @clientId AND we.workout_id = @workoutId";
        AddParameter(command, "@clientId", clientId);
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
    
        command.CommandText = @"INSERT INTO workout_exercise (sets, reps, duration, exercise_id, workout_id) VALUES (@sets, @reps, @duration, @exercise_id, @workout_id)"; 
        AddParameter(command, "@sets", workoutExercise.Sets);
        AddParameter(command, "@reps", workoutExercise.Reps);
        AddParameter(command, "@duration", workoutExercise.Duration);
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
        var exercise = new ExerciseDbRepository().GetById(exerciseId);

        var workoutId = Convert.ToInt64(reader["workout_id"]);
        var workout = new WorkoutDbRepository().GetById(workoutId);

        return new WorkoutExercise(Convert.ToInt64(reader["id"]), Convert.ToInt32(reader["sets"]),
            Convert.ToInt32(reader["reps"]), Convert.ToInt32(reader["duration"]), exercise, workout);
    }
}
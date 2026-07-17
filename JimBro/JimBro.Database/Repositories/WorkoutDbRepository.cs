using System.Data;
using JimBro.Domain;
using JimBro.Domain.RepositoryInterfaces;
using JimBro.Services.ServiceInterfaces;

namespace JimBro.Database.Repositories;

public class WorkoutDbRepository : BaseRepository, IWorkoutRepository
{
    public Workout? GetById(long id)
    {
        using IDbConnection connection = CreateConnection();
        using IDbCommand command = connection.CreateCommand();
        command.CommandText =
            @"SELECT w.* FROM workout w WHERE w.id = @id";
        AddParameter(command, "@id", id);
        using IDataReader reader = command.ExecuteReader();
        return reader.Read() ? MapDbRowToWorkout(reader) : null;
    }
    
    public List<Workout> GetWorkoutsForClient(long clientId)
    {
        using IDbConnection connection = CreateConnection();
        using IDbCommand command = connection.CreateCommand();
        command.CommandText =
            @"SELECT w.* FROM workout w WHERE w.client_id = @clientId ORDER BY w.date DESC";
        AddParameter(command, "@clientId", clientId);
        var workouts = new List<Workout>();
        using IDataReader reader = command.ExecuteReader();
        while (reader.Read())
            workouts.Add(MapDbRowToWorkout(reader));
        return workouts;
    }
    
    public long Insert(Workout workout)
    {
        using IDbConnection connection = CreateConnection();
        using IDbCommand command = connection.CreateCommand();
        command.CommandText = "INSERT INTO workout (date, note, trainer_id, client_id) VALUES (@date, @note, @trainer_id, @client_id) RETURNING id";
        AddParameter(command, "@date", workout.Date);
        AddParameter(command, "@note", workout.Note);
        AddParameter(command, "@trainer_id", workout.Trainer.Id);
        AddParameter(command, "@client_id", workout.Client.Id);
        return Convert.ToInt64(command.ExecuteScalar());
    }
    
    public void UpdateWorkoutStatus(long workoutId, bool completed)
    {
        using IDbConnection connection = CreateConnection();
        using IDbCommand command = connection.CreateCommand();
        command.CommandText = "UPDATE workout SET completed = @completed WHERE id = @id";
        AddParameter(command, "@id", workoutId);
        AddParameter(command, "@completed", completed);
        command.ExecuteNonQuery();
    }
    
    private static Workout MapDbRowToWorkout(IDataRecord reader) 
    {
        DateOnly date;
        var dateValue = reader["date"];
        if (dateValue is DateTime dateTime)
            date = DateOnly.FromDateTime(dateTime);
        else if (dateValue is DateOnly dateOnly)
            date = dateOnly;
        else
            date = DateOnly.FromDateTime(Convert.ToDateTime(dateValue));
        
    
        var trainerId = Convert.ToInt64(reader["trainer_id"]);
        var clientId = Convert.ToInt64(reader["client_Id"]);
        
        var trainer = new TrainerDbRepository().GetById(trainerId);
        var client = new ClientDbRepository().GetById(clientId);

        var workout = new Workout(Convert.ToInt64(reader["id"]),
            date, reader["note"]?.ToString() ?? string.Empty,
            trainer, client);  
        
        if (reader["completed"] != DBNull.Value)
        {
            workout.CompletedWorkout = Convert.ToBoolean(reader["completed"]);
        }
        else
        {
            workout.CompletedWorkout = false;
        }
    
        return workout;
    }
}
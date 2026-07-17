using System.Data;
using JimBro.Domain;
using JimBro.Domain.RepositoryInterfaces;

namespace JimBro.Database.Repositories;

public class WorkoutRatingsDbRepository : BaseRepository, IWorkoutRatingsRepository
{
    
    public void Insert(WorkoutRating workoutRating)
    {
        using IDbConnection connection = CreateConnection();
        using IDbCommand command = connection.CreateCommand();
        command.CommandText = @"INSERT INTO workout_ratings (rating, comment, client_id, workout_id, completion_date) VALUES (@rating, @comment, @client_id, @workout_id, @completion_date)";
        AddParameter(command, "@rating", workoutRating.Rating);
        AddParameter(command, "@comment", workoutRating.Comment);
        AddParameter(command, "@client_id", workoutRating.Client.Id);
        AddParameter(command, "@workout_id", workoutRating.Workout.Id);
        AddParameter(command, "@completion_date", workoutRating.CompletionDate);
        command.ExecuteNonQuery();
    }
    
    public List<WorkoutRating> GetClientsRatings(long clientId)
    {
        using IDbConnection connection = CreateConnection();
        using IDbCommand command = connection.CreateCommand();
        command.CommandText = @"SELECT wr.* FROM workout_ratings wr WHERE wr.client_id = @clientId ORDER BY wr.completion_date DESC";
        AddParameter(command, "@clientId", clientId);
        var ratings = new List<WorkoutRating>();
        using IDataReader reader = command.ExecuteReader();
        while (reader.Read())
            ratings.Add(MapDbRowToWorkoutRatings(reader));
        return ratings;
    }
    
    private WorkoutRating MapDbRowToWorkoutRatings(IDataRecord reader)
    {
        DateOnly date;
        var dateValue = reader["completion_date"];
        if (dateValue is DateTime dateTime)
            date = DateOnly.FromDateTime(dateTime);
        else if (dateValue is DateOnly dateOnly)
            date = dateOnly;
        else
            date = DateOnly.FromDateTime(Convert.ToDateTime(dateValue));
        
        var clientId = Convert.ToInt64(reader["client_id"]);
        var workoutId = Convert.ToInt64(reader["workout_id"]);
        
        var client = new ClientDbRepository().GetById(clientId);
        var workout = new WorkoutDbRepository().GetById(workoutId);

        return new WorkoutRating(Convert.ToInt64(reader["id"]), Convert.ToInt32(reader["rating"]),
            reader["comment"].ToString(), date, workout, client);
    }
}
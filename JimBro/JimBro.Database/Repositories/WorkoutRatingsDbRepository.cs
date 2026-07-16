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
    
    public List<WorkoutRating> GetTrainersRatings(long trainerId)
    {
        using IDbConnection connection = CreateConnection();
        using IDbCommand command = connection.CreateCommand();
        command.CommandText = @"SELECT wr.*, c.id as ClientId, c.name as ClientName, c.surname as ClientSurname, w.id as WorkoutId, 
       FROM workout_ratings wr INNER JOIN users c ON wr.client_id = c.id INNER JOIN workout w ON wr.workout_id = w.id";
        var ratings = new List<WorkoutRating>();
        using IDataReader reader = command.ExecuteReader();
        while (reader.Read())
            ratings.Add(MapDbRowToWorkoutRatings(reader));
        return ratings;
    }
    
    private WorkoutRating MapDbRowToWorkoutRatings(IDataRecord reader)
    {
        var clientId = Convert.ToInt64(reader["client_id"]);
        var workoutId = Convert.ToInt64(reader["workout_id"]);
        
        var client = new ClientDbRepository().GetById(clientId);
        var workout = new WorkoutDbRepository().GetById(workoutId);

        return new WorkoutRating(Convert.ToInt64(reader["id"]), Convert.ToInt32(reader["rating"]),
            reader["comment"].ToString(), DateOnly.FromDateTime(DateTime.Today), workout, client);
    }
}
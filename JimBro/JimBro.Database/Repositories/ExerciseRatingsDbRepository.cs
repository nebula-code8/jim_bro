using System.Data;
using JimBro.Domain;
using JimBro.Domain.RepositoryInterfaces;

namespace JimBro.Database.Repositories;

public class ExerciseRatingsDbRepository : BaseRepository, IExerciseRatingsRepository
{
    public void Insert(ExerciseRating exerciseRating)
    {
        using IDbConnection connection = CreateConnection();
        using IDbCommand command = connection.CreateCommand();
        command.CommandText = @"INSERT INTO exercise_ratings (rating, comment, client_id, exercise_id, completion_date) VALUES (@rating, @comment, @client_id, @exercise_id, @completion_date)";
        AddParameter(command, "@rating", exerciseRating.Rating);
        AddParameter(command, "@comment", exerciseRating.Comment);
        AddParameter(command, "@client_id", exerciseRating.Client.Id);
        AddParameter(command, "@exercise_id", exerciseRating.WorkoutExercise.Id);
        AddParameter(command, "@completion_date", exerciseRating.CompletionDate);
        command.ExecuteNonQuery();
    }
    
    public List<ExerciseRating> GetClientsRatings(long clientId, long workoutId)
    {
        using IDbConnection connection = CreateConnection();
        using IDbCommand command = connection.CreateCommand();
        command.CommandText = @"SELECT er.* FROM exercise_ratings er INNER JOIN workout_exercise we ON er.exercise_id = we.id WHERE er.client_id = @clientId AND we.workout_id = @workoutId ORDER BY er.completion_date DESC";
        AddParameter(command, "@clientId", clientId);
        AddParameter(command, "@workoutId", workoutId);
        var ratings = new List<ExerciseRating>();
        using IDataReader reader = command.ExecuteReader();
        while (reader.Read())
            ratings.Add(MapDbRowToExerciseRatings(reader));
        return ratings;
    }
    
    private ExerciseRating MapDbRowToExerciseRatings(IDataRecord reader)
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
        var exerciseId = Convert.ToInt64(reader["exercise_id"]);
        
        var client = new ClientDbRepository().GetById(clientId);
        var exercise = new WorkoutExerciseDbRepository().GetById(exerciseId);

        return new ExerciseRating(Convert.ToInt64(reader["id"]), Convert.ToInt32(reader["rating"]),
            reader["comment"].ToString(), date, exercise, client);
    }
}
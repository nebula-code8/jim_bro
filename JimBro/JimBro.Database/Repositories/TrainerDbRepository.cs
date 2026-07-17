using System.Data;
using JimBro.Domain;
using JimBro.Domain.RepositoryInterfaces;

namespace JimBro.Database.Repositories;

public class TrainerDbRepository : BaseRepository, ITrainerRepository
{
    public Trainer? GetById(long id) {
        using IDbConnection connection = CreateConnection();
        using IDbCommand command = connection.CreateCommand();
        command.CommandText = "SELECT u.*, t.specialization, t.biography, t.license FROM users u INNER JOIN trainer t ON u.id = t.id WHERE u.id = @id AND u.role = @role";
        AddParameter(command, "@id", id);
        AddParameter(command, "@role", (int)Role.Trainer);
        using IDataReader reader = command.ExecuteReader();
        return reader.Read() ? MapDbRowToTrainer(reader) : null;
        
    }
    
    public List<Trainer> GetAllTrainers()
    {
        using IDbConnection connection = CreateConnection();
        using IDbCommand command = connection.CreateCommand();
        command.CommandText = @"SELECT u.*, t.specialization, t.biography, t.license FROM users u INNER JOIN trainer t ON u.id = t.id WHERE u.role = @role";
        AddParameter(command, "@role", (int)Role.Trainer);
        var trainers = new List<Trainer>();
        using IDataReader reader = command.ExecuteReader();
        while (reader.Read())
            trainers.Add(MapDbRowToTrainer(reader));
        return trainers;
    }
    
    public List<Trainer> GetClientTrainers(long clientId)
    {
        using IDbConnection connection = CreateConnection();
        using IDbCommand command = connection.CreateCommand();
        command.CommandText =
            @"SELECT u.*, t.specialization, t.biography, t.license FROM users u INNER JOIN trainer t ON u.id = t.id INNER JOIN trainer_requests tr ON t.id = tr.trainer_id 
        WHERE tr.client_id = @clientId AND u.role = @role AND tr.status = 1";
        AddParameter(command, "@role", (int)Role.Trainer);
        AddParameter(command, "@clientId", clientId);
        var trainers = new List<Trainer>();
        using IDataReader reader = command.ExecuteReader();
        while (reader.Read())
            trainers.Add(MapDbRowToTrainer(reader));
        return trainers;
    }
    
    public List<Trainer> GetAllTrainersWithRatings()
    {
        using IDbConnection connection = CreateConnection();
        using IDbCommand command = connection.CreateCommand();
        command.CommandText = @"SELECT u.*, t.specialization, t.biography, t.license, COALESCE(AVG(tr.rating), 0) as AverageRating
        FROM users u INNER JOIN trainer t ON u.id = t.id LEFT JOIN trainer_ratings tr ON t.id = tr.trainer_id WHERE u.role = @role
        GROUP BY u.id, t.id ORDER BY AverageRating DESC";
        AddParameter(command, "@role", (int)Role.Trainer);
        var trainers = new List<Trainer>();
        using IDataReader reader = command.ExecuteReader();
        while (reader.Read())
            trainers.Add(MapDbRowToTrainerWithRating(reader));
        return trainers;
    }
    
    private static Trainer MapDbRowToTrainer(IDataRecord reader)
    {
        DateOnly dateOfBirth;
        var dateOfBirthValue = reader["date_of_birth"];
        if (dateOfBirthValue is DateTime dateTime) dateOfBirth = DateOnly.FromDateTime(dateTime);
        else if (dateOfBirthValue is DateOnly dateOnly) dateOfBirth = dateOnly;
        else dateOfBirth = DateOnly.FromDateTime(Convert.ToDateTime(dateOfBirthValue));

        return new Trainer(Convert.ToInt64(reader["id"]), reader["name"].ToString(), reader["surname"].ToString(),
            (Gender)Convert.ToInt32(reader["gender"]),
            dateOfBirth, reader["phone_number"].ToString(), reader["email"].ToString(), reader["password"].ToString(),
            reader["specialization"].ToString(), reader["biography"].ToString(), reader["license"].ToString()
        );
    }
    
    private static Trainer MapDbRowToTrainerWithRating(IDataRecord reader)
    {
        var trainer = MapDbRowToTrainer(reader);
        
        if (reader["AverageRating"] != DBNull.Value)
            trainer.SetAverageRating(Convert.ToDouble(reader["AverageRating"]));
        else
            trainer.SetAverageRating(0);
        
        return trainer;
    }
}
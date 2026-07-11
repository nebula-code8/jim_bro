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
}
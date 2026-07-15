using System.Data;
using JimBro.Domain;
using JimBro.Domain.RepositoryInterfaces;

namespace JimBro.Database.Repositories;

public class TrainerRequestDbRepository : BaseRepository, ITrainerRequestRepository
{
    public List<Client> GetClientsForTrainer(long trainerId)
    {
        using IDbConnection connection = CreateConnection();
        using IDbCommand command = connection.CreateCommand();
        command.CommandText = "SELECT users.*, client.height, client.weight, client.goal, client.health_problems FROM users INNER JOIN client ON users.id = client.id INNER JOIN trainer_requests t ON t.client_id = users.id WHERE users.role = @role AND t.status = @status AND trainer_id = @trainer_id";
        AddParameter(command, "@role", (int)Role.Client);
        AddParameter(command, "status", (int)RequestStatus.Pending);
        AddParameter(command, "@trainer_id", trainerId);
        var clients = new List<Client>();
        using IDataReader reader = command.ExecuteReader();
        while (reader.Read())
            clients.Add(MapDbRowToClient(reader));
        return clients;
    }
    
    public void Insert(long clientId, long trainerId)
    {
        using IDbConnection connection = CreateConnection();
        using IDbCommand command = connection.CreateCommand();

        command.CommandText = @"INSERT INTO trainer_requests (client_id, trainer_id, status) VALUES (@client_id, @trainer_id, @status)";
        AddParameter(command, "@client_id", clientId);
        AddParameter(command, "@trainer_id", trainerId);
        AddParameter(command, "@status", (int)RequestStatus.Pending);
        command.ExecuteNonQuery();
    }

    public TrainerRequest? GetRequest(long clientId, long trainerId)
    {
        using IDbConnection connection = CreateConnection();
        using IDbCommand command = connection.CreateCommand();

        command.CommandText = @"Select * from trainer_requests WHERE client_id=@client_id AND trainer_id=@trainer_id";
        AddParameter(command, "@client_id", clientId);
        AddParameter(command, "@trainer_id", trainerId);
        using IDataReader reader = command.ExecuteReader();
        if (reader.Read())
        {
            return new TrainerRequest(
                id: Convert.ToInt64(reader["id"]),
                clientId: Convert.ToInt64(reader["client_id"]),
                trainerId: Convert.ToInt64(reader["trainer_id"]),
                status: (RequestStatus)Convert.ToInt32(reader["status"])
            );
        }
        return null;
    }
    
    public void AcceptRequest(long requestId)
    {
        using IDbConnection connection = CreateConnection();
        using IDbCommand command = connection.CreateCommand();
        command.CommandText = "UPDATE trainer_requests SET status = 1 WHERE id = @id";
        AddParameter(command, "@id", requestId);
        command.ExecuteNonQuery();
    }
    
    public void RejectRequest(long requestId)
    {
        using IDbConnection connection = CreateConnection();
        using IDbCommand command = connection.CreateCommand();
        command.CommandText = "UPDATE trainer_requests SET status = 2 WHERE id = @id";
        AddParameter(command, "@id", requestId);
        command.ExecuteNonQuery();
    }
    
    private Client MapDbRowToClient(IDataRecord reader) {
        DateOnly dateOfBirth;
        var dateOfBirthValue = reader["date_of_birth"];
        if (dateOfBirthValue is DateTime dateTime) dateOfBirth = DateOnly.FromDateTime(dateTime);
        else if (dateOfBirthValue is DateOnly dateOnly) dateOfBirth = dateOnly;
        else dateOfBirth = DateOnly.FromDateTime(Convert.ToDateTime(dateOfBirthValue));
        
        return new Client(Convert.ToInt64(reader["id"]), reader["name"].ToString(), reader["surname"].ToString(), (Gender)Convert.ToInt32(reader["gender"]),
            dateOfBirth, reader["phone_number"].ToString(), reader["email"].ToString(), reader["password"].ToString(),
            Convert.ToDouble(reader["height"]), Convert.ToDouble(reader["weight"]),  reader["goal"].ToString(), reader["health_problems"].ToString());
    }
}
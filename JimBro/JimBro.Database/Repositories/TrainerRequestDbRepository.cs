using System.Data;
using JimBro.Domain;
using JimBro.Domain.RepositoryInterfaces;

namespace JimBro.Database.Repositories;

public class TrainerRequestDbRepository : BaseRepository, ITrainerRequestRepository
{
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
}
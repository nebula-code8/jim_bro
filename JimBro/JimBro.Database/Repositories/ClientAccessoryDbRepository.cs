using System.Data;
using JimBro.Domain;
using JimBro.Domain.RepositoryInterfaces;

namespace JimBro.Database.Repositories;

public class ClientAccessoryDbRepository :  BaseRepository, IClientAccessoryRepository
{
    public void AddAccessoryToClient(long clientId, long accessoryId)
    {
        using IDbConnection connection = CreateConnection();
        using IDbCommand command = connection.CreateCommand();
        command.CommandText = "INSERT INTO client_accessories (client_id, accessory_id) VALUES (@clientId, @accessoryId)";
        AddParameter(command, "@clientId", clientId);
        AddParameter(command, "@accessoryId", accessoryId);
        command.ExecuteNonQuery();
    }
    
    public void RemoveAccessoryFromClient(long clientId, long accessoryId)
    {
        using IDbConnection connection = CreateConnection();
        using IDbCommand command = connection.CreateCommand();
        command.CommandText = "DELETE FROM client_accessories WHERE client_id = @clientId AND accessory_id = @accessoryId";
        AddParameter(command, "@clientId", clientId);
        AddParameter(command, "@accessoryId", accessoryId);
        command.ExecuteNonQuery();
    }
    
    public List<Accessory> GetAccessoriesForClient(long clientId)
    {
        using IDbConnection connection = CreateConnection();
        using IDbCommand command = connection.CreateCommand();
        command.CommandText = @"SELECT a.* FROM accessories a INNER JOIN client_accessories ca ON a.id = ca.accessory_id WHERE ca.client_id = @clientId";
        AddParameter(command, "@clientId", clientId);
        var accessories = new List<Accessory>();
        using IDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
            accessories.Add(new Accessory(Convert.ToInt64(reader["id"]), reader["name"].ToString() ?? string.Empty, reader["description"].ToString()));
        }
        return accessories;
    }
}
using System.Data;
using JimBro.Domain;
using JimBro.Domain.RepositoryInterfaces;

namespace JimBro.Database.Repositories;

public class ClientMachineDbRepository : BaseRepository, IClientMachineRepository
{
    public void AddMachineToClient(long clientId, long machineId)
    {
        using IDbConnection connection = CreateConnection();
        using IDbCommand command = connection.CreateCommand();
        command.CommandText = "INSERT INTO client_machines (client_id, machine_id) VALUES (@clientId, @machineId)";
        AddParameter(command, "@clientId", clientId);
        AddParameter(command, "@machineId", machineId);
        command.ExecuteNonQuery();
    }
    
    public void RemoveMachineFromClient(long clientId, long machineId)
    {
        using IDbConnection connection = CreateConnection();
        using IDbCommand command = connection.CreateCommand();
        command.CommandText = "DELETE FROM client_machines WHERE client_id = @clientId AND machine_id = @machineId";
        AddParameter(command, "@clientId", clientId);
        AddParameter(command, "@machineId", machineId);
        command.ExecuteNonQuery();
    }
    
    public List<Machine> GetMachinesForClient(long clientId)
    {
        using IDbConnection connection = CreateConnection();
        using IDbCommand command = connection.CreateCommand();
        command.CommandText = @"SELECT m.* FROM machines m INNER JOIN client_machines cm ON m.id = cm.machine_id WHERE cm.client_id = @clientId";
        AddParameter(command, "@clientId", clientId);
        var machines = new List<Machine>();
        using IDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
            machines.Add(new Machine(Convert.ToInt64(reader["id"]), reader["name"].ToString() ?? string.Empty, reader["description"].ToString()));
        }
        return machines;
    }
}
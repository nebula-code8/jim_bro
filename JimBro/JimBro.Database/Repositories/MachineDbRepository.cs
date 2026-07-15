using System.Data;
using JimBro.Domain;
using JimBro.Domain.RepositoryInterfaces;

namespace JimBro.Database.Repositories;

public class MachineDbRepository : BaseRepository, IMachineRepository
{
    public List<Machine> GetAllMachines() {
        using IDbConnection connection = CreateConnection();
        using IDbCommand command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM machines";
        var machines = new List<Machine>();
        using IDataReader reader = command.ExecuteReader();
        while (reader.Read())
            machines.Add(MapDbRowToMachine(reader));
        return machines;
    }
    
    private static Machine MapDbRowToMachine(IDataRecord reader)
    {
        return new Machine(Convert.ToInt64(reader["id"]), reader["name"].ToString(), reader["description"].ToString());
    }
}
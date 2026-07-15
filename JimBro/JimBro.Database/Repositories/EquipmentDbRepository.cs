using System.Data;
using JimBro.Domain;
using JimBro.Domain.RepositoryInterfaces;

namespace JimBro.Database.Repositories;

public class EquipmentDbRepository : BaseRepository, IEquipmentRepository
{
    public List<Equipment> GetAllEquipments() {
        using IDbConnection connection = CreateConnection();
        using IDbCommand command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM equipments";
        var equipment = new List<Equipment>();
        using IDataReader reader = command.ExecuteReader();
        while (reader.Read())
            equipment.Add(mapDbRowToEquipment(reader));
        return equipment;
    }
    
    private static Equipment mapDbRowToEquipment(IDataRecord reader)
    {
        return new Equipment(Convert.ToInt64(reader["id"]), reader["name"].ToString(), reader["description"].ToString());
    }
}
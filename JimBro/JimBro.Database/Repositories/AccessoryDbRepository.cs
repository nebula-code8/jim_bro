using System.Data;
using JimBro.Domain;
using JimBro.Domain.RepositoryInterfaces;

namespace JimBro.Database.Repositories;

public class AccessoryDbRepository : BaseRepository, IAccessoryRepository
{
    public List<Accessory> GetAllAccessories()
    {
        using IDbConnection connection = CreateConnection();
        using IDbCommand command = connection.CreateCommand();
        var accessories = new List<Accessory>();
        command.CommandText = "SELECT id, name, description, weight FROM accessories ORDER BY id ASC";
        using IDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
            accessories.Add(MapDbRowToAccessory(reader));
        }
        return accessories;
    }

    public long Insert(Accessory accessory)
    {
        using IDbConnection connection = CreateConnection();
        using IDbCommand command = connection.CreateCommand();
        command.CommandText = "INSERT INTO accessories (name, description, weight) VALUES (@name, @description, @weight) RETURNING id";
        AddParameter(command, "@name", accessory.Name);
        AddParameter(command, "@description", accessory.Description);
        AddParameter(command, "@weight", accessory.Weight);
        object? id = command.ExecuteScalar();
        return Convert.ToInt64(id);
    }

    public int Update(Accessory accessory)
    {
        using IDbConnection connection = CreateConnection();
        using IDbCommand command = connection.CreateCommand();
        command.CommandText = "UPDATE accessories SET name = @name, description = @description, weight = @weight WHERE id = @id";
        AddParameter(command, "@id", accessory.Id);
        AddParameter(command, "@name", accessory.Name);
        AddParameter(command, "@description", accessory.Description);
        AddParameter(command, "@weight", accessory.Weight);
        return command.ExecuteNonQuery();
    }

    public int Delete(long id)
    {
        using IDbConnection connection = CreateConnection();
        using IDbCommand command = connection.CreateCommand();
        command.CommandText = "DELETE FROM accessories WHERE id = @id";
        AddParameter(command, "@id", id);
        return command.ExecuteNonQuery();
    }

    private static Accessory MapDbRowToAccessory(IDataRecord reader)
    {
        return new Accessory(
            Convert.ToInt64(reader["id"]),
            Convert.ToString(reader["name"])!,
            Convert.ToString(reader["description"])!,
            reader["weight"] == DBNull.Value
                ? null
                : Convert.ToDouble(reader["weight"]));
    }
    
}
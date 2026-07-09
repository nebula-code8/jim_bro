using System.Data;
using JimBro.Domain;
using JimBro.Domain.RepositoryInterfaces;

namespace JimBro.Database.Repositories;

public class ClientDbRepository : BaseRepository, IClientRepository
{
    private readonly UserDbRepository _userRepository = new();
    
    public Client? GetById(long id) {
        using IDbConnection connection = CreateConnection();
        using IDbCommand command = connection.CreateCommand();
        command.CommandText = "SELECT users.*, client.height, client.weight, client.health_problems FROM users INNER JOIN client ON users.id = client.id WHERE users.id = @id AND users.role = @role";
        AddParameter(command, "@id", id);
        AddParameter(command, "@role", (int)Role.Client);
        using IDataReader reader = command.ExecuteReader();
        return reader.Read() ? MapDbRowToClient(reader) : null;
        
    }
    
    public Client? GetByEmail(string email) {
        using IDbConnection connection = CreateConnection();
        using IDbCommand command = connection.CreateCommand();
        command.CommandText = "SELECT users.*, client.height, client.weight, client.health_problems FROM users INNER JOIN client ON users.id = client.id WHERE users.email = @email AND users.role = @role";
        AddParameter(command, "@email", email);
        AddParameter(command, "@role", (int)Role.Client);
        using IDataReader reader = command.ExecuteReader();
        return reader.Read() ? MapDbRowToClient(reader) : null;
    }
    
    public List<Client> GetAllClients() {
        using IDbConnection connection = CreateConnection();
        using IDbCommand command = connection.CreateCommand();
        command.CommandText = "SELECT users.*, client.height, client.weight, client.health_problems FROM users INNER JOIN client ON users.id = client.id WHERE users.role = @role";
        AddParameter(command, "@role", (int)Role.Client);
        var clients = new List<Client>();
        using IDataReader reader = command.ExecuteReader();
        while (reader.Read())
            clients.Add(MapDbRowToClient(reader));
        return clients;
    }
    
    public long Insert(Client client)
    {
        long userId = _userRepository.Insert(client);
        using IDbConnection connection = CreateConnection();
        using IDbCommand command = connection.CreateCommand();

        command.CommandText = @"INSERT INTO client (id, height, weight, health_problems) VALUES (@id, @height, @weight, @health_problems)";
        AddParameter(command, "@id", userId);
        AddParameter(command, "@height", (int)client.Height);
        AddParameter(command, "@weight", (int)client.Weight);
        AddParameter(command, "@health_problems", client.HealthProblems);
        command.ExecuteNonQuery();
        return userId;
    }
    
    public int Update(Client client)
    {
        int userRowsAffected = _userRepository.Update(client);
        using IDbConnection connection = CreateConnection();
        using IDbCommand command = connection.CreateCommand();
        command.CommandText = @"UPDATE client SET height = @height, weight = @weight, health_problems = @health_problems WHERE id = @id";
        AddParameter(command, "@id", client.Id);
        AddParameter(command, "@height", (int)client.Height);
        AddParameter(command, "@weight", (int)client.Weight);
        AddParameter(command, "@health_problems", client.HealthProblems);
        command.ExecuteNonQuery();
        return command.ExecuteNonQuery() + userRowsAffected;
    }
    
    private static Client MapDbRowToClient(IDataRecord reader) {
        return new Client(Convert.ToInt64(reader["id"]), reader["name"].ToString(), reader["surname"].ToString(), (Gender)Convert.ToInt32(reader["gender"]),
            DateOnly.FromDateTime(Convert.ToDateTime(reader["date_of_birth"])), reader["phone_number"].ToString(), reader["email"].ToString(), reader["password"].ToString(),
            Convert.ToDouble(reader["height"]), Convert.ToDouble(reader["weight"]), reader["health_problems"].ToString());
    }
}
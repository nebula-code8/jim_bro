using System.Data;
using JimBro.Domain;
using JimBro.Domain.RepositoryInterfaces;

namespace JimBro.Database.Repositories;

public class UserDbRepository : BaseRepository, IUserRepository
{
    public (long Id, Role Role)? AuthenticateUser(string email, string password)
    {
        using IDbConnection connection = CreateConnection();
        IDbCommand command = connection.CreateCommand();
        command.CommandText = "SELECT users.id, users.role FROM users WHERE users.email = @email AND users.password = @password";
        AddParameter(command, "@email", email);
        AddParameter(command, "@password", password);
        using IDataReader reader = command.ExecuteReader();
        if (!reader.Read()) return null;
        long id = Convert.ToInt64(reader["id"]);
        Role role = (Role)Convert.ToInt32(reader["role"]);
        return (id, role);
    }
    
    public long Insert(User user)
    {
        using IDbConnection connection = CreateConnection();
        IDbCommand command = connection.CreateCommand();

        command.CommandText = @"INSERT INTO users (name,surname,gender,date_of_birth,phone_number,email,password,role)
            VALUES (@name, @surname,@gender, @date_of_birth,@phoneNumber,@email, @password,@role)
            RETURNING id;";
        AddParameter(command, "@name", user.Name);
        AddParameter(command, "@surname", user.Surname);
        AddParameter(command, "@gender", (int)user.Gender);
        AddParameter(command, "@bdate_of_birth", user.DateOfBirth);
        AddParameter(command, "@phoneNumber", user.PhoneNumber);
        AddParameter(command, "@email", user.EmailAddress);
        AddParameter(command, "@password", user.Password);
        AddParameter(command, "@role", (int)user.Role);
        return Convert.ToInt64(command.ExecuteScalar()); 
    }
    
    public int Update(User user)
    {
        using IDbConnection connection = CreateConnection();
        IDbCommand command = connection.CreateCommand();

        command.CommandText = @"UPDATE users SET name = @name,surname = @surname,gender = @gender,date_of_birth = @date_of_birth,phone_number = @phoneNumber,email = @email,password = @password,
        role = @role WHERE id = @id";

        AddParameter(command, "@name", user.Name);
        AddParameter(command, "@surname", user.Surname);
        AddParameter(command, "@gender", (int)user.Gender);
        AddParameter(command, "@date_of_birth", user.DateOfBirth);
        AddParameter(command, "@phoneNumber", user.PhoneNumber);
        AddParameter(command, "@email", user.EmailAddress);
        AddParameter(command, "@password", user.Password);
        AddParameter(command, "@role", (int)user.Role);
        AddParameter(command, "@id", user.Id);
        return command.ExecuteNonQuery(); 

    }
}
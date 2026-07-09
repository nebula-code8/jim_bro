using System.Data;
using JimBro.Domain;

namespace JimBro.Database.Repositories;

public class UserDbRepository : BaseRepository
{
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
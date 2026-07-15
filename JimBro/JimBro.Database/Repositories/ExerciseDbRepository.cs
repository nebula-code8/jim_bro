using System.Data;
using JimBro.Domain;
using JimBro.Domain.RepositoryInterfaces;

namespace JimBro.Database.Repositories;

public class ExerciseDbRepository : BaseRepository, IExerciseRepository
{
    public Exercise? GetById(long id) {
        using IDbConnection connection = CreateConnection();
        using IDbCommand command = connection.CreateCommand();
        command.CommandText = @"
            SELECT e.*, a.id as AccessoryId, a.name as accessoryName, a.description as accessoryDescription, m.id as MachineId, m.name as MachineName, m.description as MachineDescription
            FROM exercises e LEFT JOIN accessories a ON e.accessory_id = a.id LEFT JOIN machines m ON e.machine_id = m.id WHERE e.id = @id";
        AddParameter(command, "@id", id);
        using IDataReader reader = command.ExecuteReader();
        return reader.Read() ? MapDbRowToExercise(reader) : null;
    }
    
    public List<Exercise> GetAllExercisesForTrainer(long trainerId) {
        using IDbConnection connection = CreateConnection();
        using IDbCommand command = connection.CreateCommand();
        command.CommandText = @"
            SELECT e.*, a.id as AccessoryId, a.name as AccessoryName, a.description as AccessoryDescription, m.id as MachineId, m.name as MachineName, m.description as MachineDescription
            FROM exercises e LEFT JOIN accessories a ON e.accessory_id = a.id LEFT JOIN machines m ON e.machine_id = m.id WHERE e.trainer_id = @trainerId";
        AddParameter(command, "@trainerId", trainerId);
        var exercises = new List<Exercise>();
        using IDataReader reader = command.ExecuteReader();
        while (reader.Read())
            exercises.Add(MapDbRowToExercise(reader));
        return exercises;
    }
    
    public void Insert(Exercise exercise)
    {
        using IDbConnection connection = CreateConnection();
        using IDbCommand command = connection.CreateCommand();

        command.CommandText = @"INSERT INTO exercises (name, description, video_url, trainer_id, accessory_id, machine_id) VALUES (@name, @description, @video_url, @trainer_id, @accessory_id, @machine_id)";
        AddParameter(command, "@name", exercise.Name);
        AddParameter(command, "@description", exercise.Description);
        AddParameter(command, "@video_url", exercise.VideoUrl);
        AddParameter(command, "@trainer_id", exercise.TrainerId);
        AddParameter(command, "@accessory_id", exercise.Accessory?.Id ?? (object)DBNull.Value);
        AddParameter(command, "@machine_id", exercise.Machine?.Id ?? (object)DBNull.Value);
        command.ExecuteNonQuery();
    }
    
    private static Exercise MapDbRowToExercise(IDataRecord reader)
    {
        Accessory? accessory = null;
        if (reader["accessory_id"] != DBNull.Value)
            accessory = new Accessory(Convert.ToInt64(reader["accessory_id"]),
                reader["AccessoryName"]?.ToString() ?? string.Empty, reader["AccessoryDescription"]?.ToString());
        
        Machine? machine = null;
        if (reader["machine_id"] != DBNull.Value)
            machine = new Machine(Convert.ToInt64(reader["machine_id"]),
                reader["MachineName"]?.ToString() ?? string.Empty, reader["MachineDescription"]?.ToString());
        
        return new Exercise(Convert.ToInt64(reader["id"]), reader["name"].ToString() ?? "", reader["description"].ToString() ?? "",
            reader["video_url"].ToString() ?? "", Convert.ToInt64(reader["trainer_id"]), accessory, machine);
    }
}
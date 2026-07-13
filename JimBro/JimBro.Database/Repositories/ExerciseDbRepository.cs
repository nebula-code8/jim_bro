using System.Data;
using JimBro.Domain;
using JimBro.Domain.RepositoryInterfaces;

namespace JimBro.Database.Repositories;

public class ExerciseDbRepository : BaseRepository, IExerciseRepository
{
    public Exercise? GetById(long id) {
        using IDbConnection connection = CreateConnection();
        using IDbCommand command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM exercises WHERE exercises.id = @id";
        AddParameter(command, "@id", id);
        using IDataReader reader = command.ExecuteReader();
        return reader.Read() ? MapDbRowToExercise(reader) : null;
    }
    
    public List<Exercise> GetAllExercisesForTrainer(long trainerId) {
        using IDbConnection connection = CreateConnection();
        using IDbCommand command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM exercises WHERE trainer_id = @trainerId";
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

        command.CommandText = @"INSERT INTO exercises (name, description, video_url, trainer_id, equipment_id, machine_id) VALUES (@name, @description, @video_url, @trainer_id, @equipment_id, @machine_id)";
        AddParameter(command, "@name", exercise.Name);
        AddParameter(command, "@description", exercise.Description);
        AddParameter(command, "@video_url", exercise.VideoUrl);
        AddParameter(command, "@trainer_id", exercise.TrainerId);
        AddParameter(command, "@equipment_id", exercise.EquipmentId);
        AddParameter(command, "@machine_id", exercise.MachineId);
        command.ExecuteNonQuery();
    }
    
    private static Exercise MapDbRowToExercise(IDataRecord reader)
    {
        return new Exercise(Convert.ToInt64(reader["id"]), reader["name"].ToString(), reader["description"].ToString(),
            reader["video_url"].ToString(), Convert.ToInt64(reader["trainer_id"]), Convert.ToInt64(reader["equipment_id"]),
                Convert.ToInt64(reader["machine_id"]));
    }
}
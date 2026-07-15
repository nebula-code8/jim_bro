using System.Data;
using JimBro.Domain;
using JimBro.Domain.RepositoryInterfaces;
using JimBro.Services.ServiceInterfaces;

namespace JimBro.Database.Repositories;

public class WorkoutDbRepository : BaseRepository, IWorkoutRepository
{
    private readonly ITrainerRepository _trainerRepository;
    private readonly IClientRepository _clientRepository;
    
    public WorkoutDbRepository(ITrainerRepository trainerRepository, IClientRepository clientRepository)
    {
        _trainerRepository = trainerRepository;
        _clientRepository = clientRepository;
    }
    public Workout? GetById(long id)
    {
        using IDbConnection connection = CreateConnection();
        using IDbCommand command = connection.CreateCommand();
        command.CommandText =
            @"SELECT w.*, t.id as TrainerId, c.id as ClientId FROM workout w INNER JOIN users t ON w.trainer_id = t.id INNER JOIN
                users c ON w.client_id = c.id WHERE w.id = @id";
        AddParameter(command, "@id", id);
        using IDataReader reader = command.ExecuteReader();
        return reader.Read() ? MapDbRowToWorkout(reader) : null;
    }
    
    public List<Workout> GetWorkoutsForClient(long clientId)
    {
        using IDbConnection connection = CreateConnection();
        using IDbCommand command = connection.CreateCommand();
        command.CommandText =
            @"SELECT w.*, t.id as TrainerId, c.id as ClientId FROM workout w INNER JOIN users t ON w.trainer_id = t.id INNER JOIN users
                c ON w.client_id = c.id WHERE w.client_id = @clientId ORDER BY w.date DESC";
        AddParameter(command, "@clientId", clientId);
        var workouts = new List<Workout>();
        using IDataReader reader = command.ExecuteReader();
        while (reader.Read())
            workouts.Add(MapDbRowToWorkout(reader));
        return workouts;
    }
    
    public void Insert(Workout workout)
    {
        using IDbConnection connection = CreateConnection();
        using IDbCommand command = connection.CreateCommand();
        command.CommandText = "INSERT INTO workout (date, note, trainer_id, client_id) VALUES (@date, @note, @trainer_id, @client_id)";
        AddParameter(command, "@date", workout.Date);
        AddParameter(command, "@note", workout.Note);
        AddParameter(command, "@trainer_id", workout.Trainer.Id);
        AddParameter(command, "@client_id", workout.Client.Id);
        command.ExecuteNonQuery();
    }
    
    private Workout MapDbRowToWorkout(IDataRecord reader) 
    {
        var trainerId = Convert.ToInt64(reader["trainer_id"]);
        var clientIdFromDb = Convert.ToInt64(reader["client_Id"]);
            
        var trainer = _trainerRepository.GetById(trainerId);
        var client = _clientRepository.GetById(clientIdFromDb);

        return new Workout(Convert.ToInt64(reader["workout_id"]),
            DateOnly.FromDateTime(Convert.ToDateTime(reader["date"])), reader["note"]?.ToString() ?? string.Empty, trainer, client);
    }
}
using System.Data;
using JimBro.Domain;
using JimBro.Domain.RepositoryInterfaces;

namespace JimBro.Database.Repositories;

public class TrainerRatingsDbRepository : BaseRepository, ITrainerRatingsRepository
{
    public void Insert(TrainerRating trainerRating)
    {
        using IDbConnection connection = CreateConnection();
        using IDbCommand command = connection.CreateCommand();
        command.CommandText = @"INSERT INTO trainer_ratings (rating, comment, client_id, trainer_id) VALUES (@rating, @comment, @client_id, @trainer_id)";
        AddParameter(command, "@rating", trainerRating.Rating);
        AddParameter(command, "@comment", trainerRating.Comment);
        AddParameter(command, "@client_id", trainerRating.Client.Id);
        AddParameter(command, "@trainer_id", trainerRating.Trainer.Id);
        command.ExecuteNonQuery();
    }
    
    public List<TrainerRating> GetAllRatings()
    {
        using IDbConnection connection = CreateConnection();
        using IDbCommand command = connection.CreateCommand();
        command.CommandText = @"SELECT tr.*, c.id as ClientId, c.name as ClientName, c.surname as ClientSurname, t.id as TrainerId, 
       t.name as TrainerName, t.surname as TrainerSurname, tr.specialization as TrainerSpecialization FROM trainer_ratings tr
            INNER JOIN users c ON tr.client_id = c.id INNER JOIN trainer t ON tr.trainer_id = t.id INNER JOIN users u ON t.id = u.id";
        var ratings = new List<TrainerRating>();
        using IDataReader reader = command.ExecuteReader();
        while (reader.Read())
            ratings.Add(MapDbRowToTrainerRatings(reader));
        return ratings;
    }
    
    public List<TrainerRating> GetClientRatings(long clientId)
    {
        using IDbConnection connection = CreateConnection();
        using IDbCommand command = connection.CreateCommand();
        command.CommandText = @"SELECT tr.*, c.id as ClientId, c.name as ClientName, c.surname as ClientSurname, t.id as TrainerId, 
                   u.name as TrainerName, u.surname as TrainerSurname FROM trainer_ratings tr INNER JOIN users c ON tr.client_id = c.id 
            INNER JOIN trainer t ON tr.trainer_id = t.id INNER JOIN users u ON t.id = u.id WHERE tr.client_id = @clientId";
        AddParameter(command, "@clientId", clientId);
        var ratings = new List<TrainerRating>();
        using IDataReader reader = command.ExecuteReader();
        while (reader.Read())
            ratings.Add(MapDbRowToTrainerRatings(reader));
        return ratings;
    }
    
    private TrainerRating MapDbRowToTrainerRatings(IDataRecord reader)
    {
        var clientId = Convert.ToInt64(reader["ClientId"]);
        var trainerId = Convert.ToInt64(reader["TrainerId"]);
        
        var client = new ClientDbRepository().GetById(clientId);
        var trainer = new TrainerDbRepository().GetById(trainerId);

        return new TrainerRating(Convert.ToInt64(reader["id"]), Convert.ToInt32(reader["rating"]),
            reader["comment"].ToString(), trainer, client);
    }
}
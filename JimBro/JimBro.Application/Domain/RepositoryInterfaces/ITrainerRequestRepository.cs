namespace JimBro.Domain.RepositoryInterfaces;

public interface ITrainerRequestRepository
{
    void Insert(long clientId, long trainerId);
    TrainerRequest? GetRequest(long clientId, long trainerId);
    List<Client> GetClientsForTrainer(long trainerId);
    void AcceptRequest(long requestId);
    void RejectRequest(long requestId);
}
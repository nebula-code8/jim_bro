using JimBro.Domain;

namespace JimBro.Services.ServiceInterfaces;

public interface ITrainerRequestService
{
    void CreateRequest(long clientId, long trainerId);
    TrainerRequest? GetRequest(long clientId, long trainerId);
    List<Client> GetClientsForTrainer(long trainerId);
    string GetStatusText(RequestStatus? status);
    void AcceptRequest(long requestId);
    void RejectRequest(long requestId);
}
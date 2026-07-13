using JimBro.Domain;
using JimBro.Domain.RepositoryInterfaces;
using JimBro.Services.ServiceInterfaces;

namespace JimBro.Services;

public class TrainerRequestService : ITrainerRequestService
{
    private readonly ITrainerRequestRepository _trainerRequestRepository;

    public TrainerRequestService(ITrainerRequestRepository trainerRequestRepository)
    {
        _trainerRequestRepository = trainerRequestRepository;
    }

    public void CreateRequest(long clientId, long trainerId)
    {
        var existingRequest = _trainerRequestRepository.GetRequest(clientId, trainerId);
        
        if (existingRequest != null && existingRequest.Status == RequestStatus.Pending)
            throw new Exception("Već ste poslali zahtev za ovog trenera!");
        
        _trainerRequestRepository.Insert(clientId, trainerId);
    }
    public TrainerRequest? GetRequest(long clientId, long trainerId) => _trainerRequestRepository.GetRequest(clientId, trainerId);
    public List<Client> GetClientsForTrainer(long trainerId) => _trainerRequestRepository.GetClientsForTrainer(trainerId);
    public void AcceptRequest(long requestId) => _trainerRequestRepository.AcceptRequest(requestId);
    public void RejectRequest(long requestId) => _trainerRequestRepository.RejectRequest(requestId);
}
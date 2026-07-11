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
    
    public string GetStatusText(RequestStatus? status)
    {
        return status switch
        {
            RequestStatus.Pending => "Poslat zahtev",
            RequestStatus.Accepted => "Prihvaćen",
            RequestStatus.Rejected => "Odbijen",
            _ => "Nepoznat"
        };
    }
}
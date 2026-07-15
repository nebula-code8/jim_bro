using System.Collections.ObjectModel;
using JimBro.Domain;
using JimBro.Services.ServiceInterfaces;

namespace JimBro.Uix.ViewModels;

public class TrainerRequestsViewModel
{
    private readonly  ITrainerRequestService _trainerRequestService;
    private readonly Trainer _currentTrainer;
    public ObservableCollection<Client> Clients { get; private set; } = new();
    public Client? SelectedClient { get; set; }
    public long? SelectedRequestId { get; set; }
    public string ErrorMessage { get; private set; } = string.Empty;
    public bool HasError => !string.IsNullOrWhiteSpace(ErrorMessage);
    
    public TrainerRequestsViewModel(ITrainerRequestService trainerRequestService, Trainer currentTrainer)
    {
        _trainerRequestService = trainerRequestService;
        _currentTrainer = currentTrainer;
    }
    
    public void LoadRequests()
    {
        try
        {
            var clients = _trainerRequestService.GetClientsForTrainer(_currentTrainer.Id);
            Clients.Clear();
            
            foreach (var client in clients)
            {
                Clients.Add(client);
            }
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Greška pri učitavanju zahteva: {ex.Message}";
        }
    }

    public bool AcceptRequest(long requestId)
    {
        ErrorMessage = string.Empty;
        try
        {
            _trainerRequestService.AcceptRequest(requestId);
            return true;
        }
        
        catch (Exception ex)
        {
            ErrorMessage = ex.Message;
            return false;
        }
    }
    
    public bool RejectRequest(long requestId)
    {
        ErrorMessage = string.Empty;
        try
        {
            _trainerRequestService.RejectRequest(requestId);
            return true;
        }
        
        catch (Exception ex)
        {
            ErrorMessage = ex.Message;
            return false;
        }
    }
    
    public long GetRequestIdForClient(long clientId)
    {
        var request = _trainerRequestService.GetRequest(clientId, _currentTrainer.Id);
        return request.Id;
    }
}
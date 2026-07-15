using System.Collections.ObjectModel;
using JimBro.Domain;
using JimBro.Services.ServiceInterfaces;

namespace JimBro.Uix.ViewModels;

public class ClientsForTrainerViewModel
{
    private readonly  ITrainerRequestService _trainerRequestService;
    private readonly Trainer _currentTrainer;
    public ObservableCollection<Client> Clients { get; private set; } = new();
    public Client? SelectedClient { get; set; }
    public string ErrorMessage { get; private set; } = string.Empty;
    
    public ClientsForTrainerViewModel(ITrainerRequestService trainerRequestService, Trainer currentTrainer)
    {
        _trainerRequestService = trainerRequestService;
        _currentTrainer = currentTrainer;
    }
    
    public void LoadClients()
    {
        try
        {
            var clients = _trainerRequestService.GetAcceptedClientsForTrainers(_currentTrainer.Id);
            Clients.Clear();
            
            foreach (var client in clients)
            {
                Clients.Add(client);
            }
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Greška pri učitavanju klijenata: {ex.Message}";
        }
    }
}
using System.Collections.ObjectModel;
using JimBro.Domain;
using JimBro.Domain.RepositoryInterfaces;
using JimBro.Services.ServiceInterfaces;

namespace JimBro.Uix.ViewModels;

public class RequestTrainerViewModel
{
    private readonly ITrainerService _trainerService;
    private readonly  ITrainerRequestService _trainerRequestService;
    private readonly Client _currentClient;
    public ObservableCollection<Trainer> Trainers { get; private set; } = new();
    
    public string Name { get; set; } = string.Empty;
    public string Surname { get; set; } = string.Empty;
    public Gender Gender { get; set; } = Gender.Male;
    public DateTime DateOfBirth {get; set;} = DateTime.Today.AddYears(-20);
    public string PhoneNumber { get; set; }=string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Specialization { get; set; } = string.Empty;
    public string License { get; set; } = string.Empty;
    public string ErrorMessage { get; private set; } = string.Empty;
    public bool HasError => !string.IsNullOrWhiteSpace(ErrorMessage);
    
    public RequestTrainerViewModel(ITrainerService trainerService, ITrainerRequestService trainerRequestService, Client currentClient)
    {
        _trainerService = trainerService;
        _trainerRequestService = trainerRequestService;
        _currentClient = currentClient;
    }
    
    public void LoadTrainers()
    {
        try
        {
            var allTrainers = _trainerService.GetAllTrainers();
            Trainers.Clear();
            
            foreach (var trainer in allTrainers)
            {
                Trainers.Add(trainer);
            }
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Greška pri učitavanju trenera: {ex.Message}";
        }
    }

    public bool SendRequest(long clientId, long trainerId)
    {
        ErrorMessage = string.Empty;
        try
        {
            _trainerRequestService.CreateRequest(clientId, trainerId);
            return true;
        }
        
        catch (Exception ex)
        {
            ErrorMessage = ex.Message;
            return false;
        }
    }
}
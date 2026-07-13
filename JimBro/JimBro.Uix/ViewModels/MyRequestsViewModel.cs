using System.Collections.ObjectModel;
using JimBro.Domain;
using JimBro.Services.ServiceInterfaces;

namespace JimBro.Uix.ViewModels;

public class MyRequestsViewModel
{
    private readonly ITrainerService _trainerService;
    private readonly  ITrainerRequestService _trainerRequestService;
    private readonly Client _currentClient;
    public ObservableCollection<TrainerWithStatus> Trainers { get; private set; } = new();
    public string ErrorMessage { get; private set; } = string.Empty;
    
    public MyRequestsViewModel(ITrainerService trainerService, ITrainerRequestService trainerRequestService, Client currentClient)
    {
        _trainerService = trainerService;
        _trainerRequestService = trainerRequestService;
        _currentClient = currentClient;
    }
    
    public void LoadRequests()
    {
        try
        {
            var trainers = _trainerService.GetAllTrainers();
            Trainers.Clear();
            
            foreach (var trainer in trainers)
            {
                var request = _trainerRequestService.GetRequest(_currentClient.Id, trainer.Id);
                string statusText = request.Status switch
                {
                    RequestStatus.Pending => "Poslat zahtev",
                    RequestStatus.Accepted => "Prihvaćen",
                    RequestStatus.Rejected => "Odbijen",
                    _ => "Nepoznat"
                };
                Trainers.Add(new TrainerWithStatus(trainer, statusText));
            }
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Greška pri učitavanju trenera: {ex.Message}";
        }
    }
}

public class TrainerWithStatus
{
    private readonly Trainer _trainer;
    private readonly string _statusText;
    
    public TrainerWithStatus(Trainer trainer, string statusText)
    {
        _trainer = trainer;
        _statusText = statusText;
    }
    
    public long Id => _trainer.Id;
    public string Name => _trainer.Name;
    public string Surname => _trainer.Surname;
    public Gender Gender => _trainer.Gender;
    public DateOnly DateOfBirth => _trainer.DateOfBirth;
    public string PhoneNumber => _trainer.PhoneNumber;
    public string EmailAddress => _trainer.EmailAddress;
    public string Specialization => _trainer.Specialization;
    public string License => _trainer.License;
    public string Status =>  _statusText;
}
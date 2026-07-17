using System.Collections.ObjectModel;
using JimBro.Domain;
using JimBro.Services.ServiceInterfaces;

namespace JimBro.Uix.ViewModels;

public class RateTrainerViewModel
{
    private readonly ITrainerService _trainerService;
    private readonly ITrainerRatingsService _trainerRatingsService;
    private readonly Client _currentClient;
    public ObservableCollection<Trainer> Trainers { get; private set; } = new();
    public ObservableCollection<TrainerRating> TrainerRatings { get; private set; } = new();
    public Trainer? SelectedTrainer { get; set; }
    public string ErrorMessage { get; private set; } = string.Empty;

    public RateTrainerViewModel(ITrainerService trainerService, ITrainerRatingsService trainerRatingsService,
        Client currentClient)
    {
        _trainerService = trainerService;
        _trainerRatingsService = trainerRatingsService;
        _currentClient = currentClient;
    }
    public void LoadTrainers()
    {
        try
        {
            var allTrainers = _trainerService.GetClientTrainers(_currentClient.Id);
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
    public void LoadTrainerRatings()
    {
        try
        {
            var ratings = _trainerRatingsService.GetClientRatings(_currentClient.Id);
            TrainerRatings.Clear();
            
            foreach (var rating in ratings)
                TrainerRatings.Add(rating);
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Greška pri učitavanju ocena trenera: {ex.Message}";
        }
    }

    public bool AddRating(string rating, string comment)
    {
        ErrorMessage = string.Empty;
        try
        {
            var trainerRating = new TrainerRating(Convert.ToInt32(rating), comment, SelectedTrainer,  _currentClient);
            _trainerRatingsService.Insert(trainerRating);
            return true;
        }
        
        catch (Exception ex)
        {
            ErrorMessage = ex.Message;
            return false;
        }
    }
}
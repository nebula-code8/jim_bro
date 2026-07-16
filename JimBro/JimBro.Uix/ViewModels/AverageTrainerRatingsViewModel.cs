using System.Collections.ObjectModel;
using JimBro.Domain;
using JimBro.Services.ServiceInterfaces;

namespace JimBro.Uix.ViewModels;

public class AverageTrainerRatingsViewModel
{
    private readonly ITrainerService _trainerService;
    public ObservableCollection<Trainer> Trainers { get; private set; } = new();
    public string ErrorMessage { get; private set; } = string.Empty;

    public AverageTrainerRatingsViewModel(ITrainerService trainerService)
    {
        _trainerService = trainerService;
    }
    public void LoadTrainers()
    {
        try
        {
            var allTrainers = _trainerService.GetAllTrainersWithRatings();
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
}
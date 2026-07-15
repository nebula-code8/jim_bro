using System.Collections.ObjectModel;
using JimBro.Domain;
using JimBro.Services.ServiceInterfaces;

namespace JimBro.Uix.ViewModels;

public class ClientWorkoutsViewModel
{
    private readonly IWorkoutService _workoutService;
    private readonly Trainer _selectedTrainer;
    private readonly Client _currentClient;
    public ObservableCollection<Workout> Workouts { get; private set; } = new();
    public Workout? SelectedWorkout { get; set; }
    public string ErrorMessage { get; private set; } = string.Empty;

    public ClientWorkoutsViewModel(IWorkoutService workoutService, Client client)
    {
        _workoutService = workoutService;
        _currentClient = client;
    }
    
    public void LoadWorkouts()
    {
        try
        {
            var workouts = _workoutService.GetWorkoutsForClient(_currentClient.Id);
            Workouts.Clear();
            
            foreach (var workout in workouts)
                Workouts.Add(workout);
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Greška pri učitavanju treninga: {ex.Message}";
        }
    }
}
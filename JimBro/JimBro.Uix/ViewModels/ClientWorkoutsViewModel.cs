using System.Collections.ObjectModel;
using JimBro.Domain;
using JimBro.Services.ServiceInterfaces;

namespace JimBro.Uix.ViewModels;

public class ClientWorkoutsViewModel
{
    private readonly IWorkoutService _workoutService;
    private readonly IWorkoutRatingsService _workoutRatingsService;
    private readonly Trainer _selectedTrainer;
    private readonly Client _currentClient;
    public ObservableCollection<Workout> Workouts { get; private set; } = new();
    public ObservableCollection<WorkoutRating> WorkoutRatings { get; private set; } = new();
    public Workout? SelectedWorkout { get; set; }
    public string ErrorMessage { get; private set; } = string.Empty;

    public ClientWorkoutsViewModel(IWorkoutService workoutService, IWorkoutRatingsService workoutRatingsService, Client client)
    {
        _workoutService = workoutService;
        _workoutRatingsService = workoutRatingsService;
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
    
    public void LoadWorkoutRatings()
    {
        try
        {
            var workoutRatings = _workoutRatingsService.GetClientsRatings(_currentClient.Id);
            WorkoutRatings.Clear();
            
            foreach (var workoutRating in workoutRatings)
                WorkoutRatings.Add(workoutRating);
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Greška pri učitavanju ocena treninga: {ex.Message}";
        }
    }
}
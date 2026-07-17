using JimBro.Domain;
using JimBro.Services.ServiceInterfaces;

namespace JimBro.Uix.ViewModels;

public class RateWorkoutViewModel
{
    private readonly IWorkoutRatingsService _workoutRatingsService;
    private readonly Client _currentClient;
    public Workout? SelectedWorkout { get; set; }
    public string ErrorMessage { get; private set; } = string.Empty;

    public RateWorkoutViewModel(IWorkoutRatingsService workoutRatingsService, Client currentClient, Workout workout)
    {
        _workoutRatingsService = workoutRatingsService;
        _currentClient = currentClient;
        SelectedWorkout = workout;
    }
    
    public bool AddRating(string rating, string comment)
    {
        ErrorMessage = string.Empty;
        try
        {
            var workoutRating = new WorkoutRating(Convert.ToInt32(rating), comment,DateOnly.FromDateTime(DateTime.Today), SelectedWorkout,  _currentClient);
            _workoutRatingsService.Insert(workoutRating);
            return true;
        }
        
        catch (Exception ex)
        {
            ErrorMessage = ex.Message;
            return false;
        }
    }
}
using JimBro.Domain;
using JimBro.Services.ServiceInterfaces;

namespace JimBro.Uix.ViewModels;

public class RateExerciseViewModel
{
    private readonly IExerciseRatingsService _exerciseRatingsService;
    private readonly Client _currentClient;
    public WorkoutExercise? SelectedExercise { get; set; }
    public string ErrorMessage { get; private set; } = string.Empty;

    public RateExerciseViewModel(IExerciseRatingsService exerciseRatingsService, Client currentClient, WorkoutExercise workoutExercise)
    {
        _exerciseRatingsService = exerciseRatingsService;
        _currentClient = currentClient;
        SelectedExercise = workoutExercise;
    }
    
    public bool AddRating(string rating, string comment)
    {
        ErrorMessage = string.Empty;
        try
        {
            var exerciseRating = new ExerciseRating(Convert.ToInt32(rating), comment,DateOnly.FromDateTime(DateTime.Today), SelectedExercise,  _currentClient);
            _exerciseRatingsService.Insert(exerciseRating);
            return true;
        }
        
        catch (Exception ex)
        {
            ErrorMessage = ex.Message;
            return false;
        }
    }
}
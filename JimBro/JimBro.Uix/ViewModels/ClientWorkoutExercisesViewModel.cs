using System.Collections.ObjectModel;
using JimBro.Domain;
using JimBro.Services.ServiceInterfaces;

namespace JimBro.Uix.ViewModels;

public class ClientWorkoutExercisesViewModel
{
    private readonly IWorkoutExerciseService _workoutExerciseService;
    private readonly Client _currentClient;
    public ObservableCollection<WorkoutExercise> WorkoutExercises { get; private set; } = new();
    public Workout? SelectedWorkout { get; set; }
    public string ErrorMessage { get; private set; } = string.Empty;

    public ClientWorkoutExercisesViewModel(IWorkoutExerciseService workoutExerciseService, Workout workout, Client client)
    {
        _workoutExerciseService = workoutExerciseService;
        SelectedWorkout = workout;
        _currentClient = client;
    }
    
    public void LoadWorkoutExercises()
    {
        try
        {
            var workoutExercises = _workoutExerciseService.GetWorkoutexercisesForClient(_currentClient.Id, SelectedWorkout.Id);
            WorkoutExercises.Clear();
            
            foreach (var workoutExrercise in workoutExercises)
                WorkoutExercises.Add(workoutExrercise);
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Greška pri učitavanju vezbi: {ex.Message}";
        }
    }
}
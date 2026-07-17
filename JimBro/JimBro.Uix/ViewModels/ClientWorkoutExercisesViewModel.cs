using System;
using System.Collections.ObjectModel;
using JimBro.Domain;
using JimBro.Services.ServiceInterfaces;

namespace JimBro.Uix.ViewModels;

public class ClientWorkoutExercisesViewModel
{
    private readonly IWorkoutExerciseService _workoutExerciseService;
    private readonly IExerciseRatingsService _exerciseRatingsService;
    private readonly Client _currentClient;
    public ObservableCollection<WorkoutExercise> WorkoutExercises { get; private set; } = new();
    public ObservableCollection<ExerciseRating> WorkoutExerciseRatings { get; private set; } = new();
    public Workout? SelectedWorkout { get; set; }
    public WorkoutExercise? SelectedWorkoutExercise { get; set; }
    public string ErrorMessage { get; private set; } = string.Empty;

    public ClientWorkoutExercisesViewModel(IWorkoutExerciseService workoutExerciseService, IExerciseRatingsService exerciseRatingsService, Workout workout, Client client)
    {
        _workoutExerciseService = workoutExerciseService;
        _exerciseRatingsService =  exerciseRatingsService;
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
    
    public void LoadWorkoutExerciseRatings()
    {
        try
        {
            var ratings = _exerciseRatingsService.GetClientWorkoutRatings(_currentClient.Id, SelectedWorkout.Id);
            WorkoutExerciseRatings.Clear();
            
            foreach(var rating in ratings)
                WorkoutExerciseRatings.Add(rating);
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Greška pri učitavanju vezbi: {ex.Message}";
        }
    }
}
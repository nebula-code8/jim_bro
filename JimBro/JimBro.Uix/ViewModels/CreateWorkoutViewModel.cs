using System.Collections.ObjectModel;
using JimBro.Domain;
using JimBro.Services.ServiceInterfaces;

namespace JimBro.Uix.ViewModels;

public class CreateWorkoutViewModel
{
    private readonly IExerciseService _exerciseService;
    private readonly IWorkoutService _workoutService;
    private readonly IWorkoutExerciseService _workoutExerciseService;
    private readonly Trainer _currentTrainer;
    private readonly Client _selectedClient;

    public ObservableCollection<Exercise> AllExercises { get; private set; } = new();
    public ObservableCollection<WorkoutExercise> WorkoutExercises { get; private set; } = new();
    public Exercise? SelectedExercise { get; set; }
    public WorkoutExercise? SelectedWorkoutExercise { get; set; }

    public string WorkoutNote { get; set; } = string.Empty;
    public DateTime WorkoutDate { get; set; } = DateTime.Today;
    public string Sets { get; set; } = string.Empty;
    public string Reps { get; set; } = string.Empty;
    public string Duration { get; set; } = string.Empty;

    public string ErrorMessage { get; private set; } = string.Empty;

    public CreateWorkoutViewModel(IExerciseService exerciseService, IWorkoutService workoutService,
        IWorkoutExerciseService workoutExerciseService, Trainer currentTrainer, Client selectedClient)
    {
        _exerciseService = exerciseService;
        _workoutService = workoutService;
        _workoutExerciseService = workoutExerciseService;
        _currentTrainer = currentTrainer;
        _selectedClient = selectedClient;
    }

    public void LoadExercises()
    {
        try
        {
            var exercises = _exerciseService.GetExercisesForTrainerAndClient(_currentTrainer.Id, _selectedClient.Id);
            AllExercises.Clear();
            foreach (var exercise in exercises)
                AllExercises.Add(exercise);
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Greška: {ex.Message}";
        }
    }

    public bool AddExerciseToWorkout()
    {
        ErrorMessage = string.Empty;
        try
        {
            if (SelectedExercise == null)
                throw new Exception("Prvo izaberite vezbu iz liste!");
            
            foreach (var we in WorkoutExercises)
            {
                if (we.Exercise.Id == SelectedExercise.Id)
                    throw new Exception("Ova vezba je već dodata u trening!");
            }

            var workoutExercise = new WorkoutExercise(Convert.ToInt32(Sets), Convert.ToInt32(Reps), Convert.ToInt32(Duration), SelectedExercise, null);
            
            WorkoutExercises.Add(workoutExercise);
            
            Sets = string.Empty;
            Reps = string.Empty;
            Duration = string.Empty;
            SelectedExercise = null;
            return true;
        }
        catch (Exception ex)
        {
            ErrorMessage = ex.Message;
            return false;
        }
    }

    public bool RemoveExerciseFromWorkout()
    {
        ErrorMessage = string.Empty;
        try
        {
            if (SelectedWorkoutExercise == null)
                throw new Exception("Izaberite vežbu za uklanjanje!");

            WorkoutExercises.Remove(SelectedWorkoutExercise);
            SelectedWorkoutExercise = null;
            return true;
        }
        catch (Exception ex)
        {
            ErrorMessage = ex.Message;
            return false;
        }
    }
    
    public bool AddWorkout()
    {
        ErrorMessage = string.Empty;
        try
        {
            if (WorkoutExercises.Count == 0)
                throw new Exception("Dodajte bar jednu vezbu u trening!");
            
            var workout = new Workout(DateOnly.FromDateTime(WorkoutDate), WorkoutNote, _currentTrainer, _selectedClient);
            long workoutId = _workoutService.Insert(workout);
            var savedWorkout = new Workout(workoutId, workout.Date, workout.Note, workout.Trainer, workout.Client);
            
            foreach (var we in WorkoutExercises)
            {
                var workoutExercise = new WorkoutExercise(we.Sets, we.Reps, we.Duration, we.Exercise, savedWorkout);
                _workoutExerciseService.Insert(workoutExercise);
            }
            WorkoutExercises.Clear();
            return true;
        }
        catch (Exception ex)
        {
            ErrorMessage = ex.Message;
            return false;
        }
    }
}
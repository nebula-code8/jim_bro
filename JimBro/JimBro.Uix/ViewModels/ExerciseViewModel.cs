using System.Collections.ObjectModel;
using JimBro.Domain;
using JimBro.Services.ServiceInterfaces;

namespace JimBro.Uix.ViewModels;

public class ExerciseViewModel
{
    private readonly IExerciseService _exerciseService;
    private readonly IAccessoryService _accessoryService;
    private readonly IMachineService _machineService;
    private readonly Trainer _trainer;
    public ObservableCollection<Exercise> Exercises { get; private set; } = new();
    public ObservableCollection<Accessory> Accessories { get; private set; } = new();
    public ObservableCollection<Machine> Machines { get; private set; } = new();
    public Accessory? SelectedAccessory { get; set; }
    public Machine? SelectedMachine { get; set; }
    public string ErrorMessage { get; private set; } = string.Empty;

    public ExerciseViewModel(IExerciseService exerciseService, IAccessoryService accessoryService, IMachineService machineService, Trainer trainer)
    {
        _exerciseService = exerciseService;
        _accessoryService = accessoryService;
        _machineService = machineService;
        _trainer = trainer;
    }
    
    public void LoadExercises()
    {
        try
        {
            var exercises = _exerciseService.GetAllExercisesForTrainer(_trainer.Id);
            Exercises.Clear();
            
            foreach (var exercise in exercises)
            {
                Exercises.Add(exercise);
            }
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Greška pri učitavanju vezbi: {ex.Message}";
        }
    }
    
    public void LoadAccessories()
    {
        try
        {
            var accessories = _accessoryService.GetAllAccessories();
            Accessories.Clear();
            foreach (var accessory in accessories)
                Accessories.Add(accessory);
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Greška pri učitavanju rekvizita: {ex.Message}";
        }
    }
    
    public void LoadMachines()
    {
        try
        {
            var machines = _machineService.GetAllMachines();
            Machines.Clear();
            foreach (var machine in machines)
                Machines.Add(machine);
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Greška pri učitavanju sprava: {ex.Message}";
        }
    }
    
    public bool CreateExercise(string name, string description, string videoUrl)
    {
        ErrorMessage = string.Empty;
        try
        {
            var exercise = new Exercise(name, description, videoUrl, _trainer.Id, SelectedAccessory, SelectedMachine);
            _exerciseService.CreateExercise(exercise);
            return true;
        }
        catch (Exception ex)
        {
            ErrorMessage = ex.Message;
            return false;
        }
    }
}
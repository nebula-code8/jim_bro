using JimBro.Domain;
using JimBro.Domain.RepositoryInterfaces;
using JimBro.Services.ServiceInterfaces;

namespace JimBro.Services;

public class WorkoutExerciseService : IWorkoutExerciseService
{
    private  readonly IWorkoutExerciseRepository _workoutExerciseRepository;

    public WorkoutExerciseService(IWorkoutExerciseRepository workoutExerciseRepository)
    {
        _workoutExerciseRepository = workoutExerciseRepository;
    }

    public WorkoutExercise? GetById(long id) => _workoutExerciseRepository.GetById(id);
    public List<WorkoutExercise> GetByWorkoutId(long workoutId) =>  _workoutExerciseRepository.GetByWorkoutId(workoutId);
    public List<WorkoutExercise> GetWorkoutexercisesForClient(long clientId, long workoutId) => _workoutExerciseRepository.GetWorkoutexercisesForClient(clientId, workoutId);

    public void Insert(WorkoutExercise workoutExercise)
    {
        ValidateWorkoutExercise(workoutExercise);
        _workoutExerciseRepository.Insert(workoutExercise);
    } 
    public void Delete(long id) => _workoutExerciseRepository.Delete(id);
    
    public void ValidateWorkoutExercise(WorkoutExercise workoutExercise)
    {
        if (string.IsNullOrWhiteSpace(workoutExercise.Sets.ToString()))
            throw new Exception("Broj serija je obavezno polje!");
        
        if (string.IsNullOrWhiteSpace(workoutExercise.Reps.ToString()))
            throw new Exception("Broj ponavljanja je obavezno polje!");
        
        if (string.IsNullOrWhiteSpace(workoutExercise.Duration.ToString()))
            throw new Exception("Trajanje je obavezno polje!");
        
        if (workoutExercise.Exercise == null)
            throw new Exception("Izabetire vezbu!");
        
        if (workoutExercise.Sets <= 0)
            throw new Exception("Broj serija mora biti veći od 0!");
        
        if (workoutExercise.Reps <= 0)
            throw new Exception("Broj ponavljanja mora biti veći od 0!");
        
        if (workoutExercise.Duration <= 0)
            throw new Exception("Trajanje mora biti veći od 0!");
    }
}
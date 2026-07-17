using JimBro.Domain;
using JimBro.Domain.RepositoryInterfaces;
using JimBro.Services.ServiceInterfaces;

namespace JimBro.Services;

public class ExerciseService : IExerciseService
{
    private readonly IExerciseRepository _exerciseRepository;

    public ExerciseService(IExerciseRepository exerciseRepository)
    {
        _exerciseRepository = exerciseRepository;
    }
    
    public Exercise? GetById(long id) => _exerciseRepository.GetById(id);

    public List<Exercise> GetAllExercisesForTrainer(long trainerId) =>
        _exerciseRepository.GetAllExercisesForTrainer(trainerId);

    public List<Exercise> GetExercisesForTrainerAndClient(long trainerId, long clientId) => _exerciseRepository.GetExercisesForTrainerAndClient(trainerId, clientId);

    public void CreateExercise(Exercise exercise)
    {
        if (string.IsNullOrWhiteSpace(exercise.Name))
            throw new Exception("Naziv je obavezno polje!");
        _exerciseRepository.Insert(exercise);
    }
}
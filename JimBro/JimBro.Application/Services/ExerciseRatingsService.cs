using JimBro.Domain;
using JimBro.Domain.RepositoryInterfaces;
using JimBro.Services.ServiceInterfaces;

namespace JimBro.Services;

public class ExerciseRatingsService : IExerciseRatingsService
{
    private readonly  IExerciseRatingsRepository _exerciseRatingsRepository;

    public ExerciseRatingsService(IExerciseRatingsRepository repository)
    {
        _exerciseRatingsRepository = repository;
    }

    public void Insert(ExerciseRating exerciseRating)
    {
        if(string.IsNullOrEmpty(exerciseRating.Rating.ToString()))
            throw new Exception("Ocena je obavezno polje!");
        
        if(exerciseRating.Rating <= 1 || exerciseRating.Rating > 10)
            throw new Exception("Ocena mora biti izmedju 1 i 10!");
        
        var existingRatings = _exerciseRatingsRepository.GetClientsRatings(exerciseRating.Client.Id, exerciseRating.WorkoutExercise.Id);
        foreach (var rating in existingRatings)
        {
            if (rating.WorkoutExercise.Id == exerciseRating.WorkoutExercise.Id) 
                throw new Exception("Vec ste ocenili ovu vezbu!");
        }
        
        _exerciseRatingsRepository.Insert(exerciseRating);
    } 
    public List<ExerciseRating> GetClientsRatings(long clientId, long exerciseId) => _exerciseRatingsRepository.GetClientsRatings(clientId, exerciseId);
}
using System.Data;
using JimBro.Domain;
using JimBro.Domain.RepositoryInterfaces;
using JimBro.Services.ServiceInterfaces;

namespace JimBro.Services;

public class WorkoutRatingsService : IWorkoutRatingsService
{
    private readonly IWorkoutRatingsRepository _repository;
    
    public WorkoutRatingsService(IWorkoutRatingsRepository repository)
    {
        _repository = repository;
    }

    public void Insert(WorkoutRating workoutRating)
    {
        if(string.IsNullOrEmpty(workoutRating.Rating.ToString()))
            throw new Exception("Ocena je obavezno polje!");
        
        if(workoutRating.Rating <= 1 || workoutRating.Rating > 10)
            throw new Exception("Ocena mora biti izmedju 1 i 10!");
        
        var existingRatings = _repository.GetClientsRatings(workoutRating.Client.Id);
        foreach (var rating in existingRatings)
        {
            if (rating.Workout.Id == workoutRating.Workout.Id) 
                throw new Exception("Vec ste ocenili ovaj trening!");
        }
        _repository.Insert(workoutRating);
    }

    public List<WorkoutRating> GetClientsRatings(long clientId) => _repository.GetClientsRatings(clientId);
}
namespace JimBro.Domain;

public class ExerciseRating
{
    public long Id {get; private set; }
    public int Rating { get; private set; }
    public string Comment { get; private set; }
    public DateOnly CompletionDate { get; private set; }
    public WorkoutExercise WorkoutExercise { get; private set; }
    public Client Client { get; private set; }

    public ExerciseRating(long id, int rating, string comment, DateOnly completionDate, WorkoutExercise workoutExercise,
        Client client)
    {
        Id = id;
        Rating = rating;
        Comment = comment;
        CompletionDate = completionDate;
        WorkoutExercise = workoutExercise;
        Client = client;
    }
    
    public ExerciseRating(int rating, string comment, DateOnly completionDate, WorkoutExercise workoutExercise,
        Client client)
    {
        Rating = rating;
        Comment = comment;
        CompletionDate = completionDate;
        WorkoutExercise = workoutExercise;
        Client = client;
    }
}
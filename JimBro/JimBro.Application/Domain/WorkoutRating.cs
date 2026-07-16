namespace JimBro.Domain;

public class WorkoutRating
{
    public long Id {get; private set; }
    public int Rating { get; private set; }
    public string Comment { get; private set; }
    public DateOnly CompletionDate { get; private set; }
    public Workout Workout { get; private set; }
    public Client Client { get; private set; }

    public WorkoutRating(long id, int rating, string comment, DateOnly completionDate, Workout workout, Client client)
    {
        Id = id;
        Rating = rating;
        Comment = comment;
        CompletionDate = completionDate;
        Workout = workout;
        Client = client;
    }
    
    public WorkoutRating(int rating, string comment, DateOnly completionDate, Workout workout, Client client)
    {
        Rating = rating;
        Comment = comment;
        CompletionDate = completionDate;
        Workout = workout;
        Client = client;
    }
}
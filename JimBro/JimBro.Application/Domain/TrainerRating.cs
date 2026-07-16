namespace JimBro.Domain;

public class TrainerRating
{
    public long Id {get; private set; }
    public int Rating { get; private set; }
    public string Comment { get; private set; }
    public Trainer Trainer { get; private set; }
    public Client Client { get; private set; }

    public TrainerRating(long id, int rating, string comment, Trainer trainer, Client client)
    {
        Id = id;
        Rating = rating;
        Comment = comment;
        Trainer = trainer;
        Client = client;
    }
    
    public TrainerRating(int rating, string comment, Trainer trainer, Client client)
    {
        Rating = rating;
        Comment = comment;
        Trainer = trainer;
        Client = client;
    }
}
namespace JimBro.Domain;

public class Workout
{
    public long Id { get; private set; }
    public DateOnly Date { get; private set; }
    public string Note { get; private set; }
    public Trainer Trainer { get; private set; }
    public Client Client { get; private set; }

    public Workout(long id, DateOnly date, string note, Trainer trainer, Client client)
    {
        Id = id;
        Date = date;
        Note = note;
        Trainer = trainer;
        Client = client;
    }

    public Workout(DateOnly date, string note, Trainer trainer, Client client)
    {
        Date = date;
        Note = note;
        Trainer = trainer;
        Client = client;
    }
}
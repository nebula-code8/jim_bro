namespace JimBro.Domain;

public class Exercise
{
    public long Id { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public string VideoUrl { get; private set; }
    public long TrainerId { get; private set; }
    public Equipment? Equipment { get; private set; }
    public Machine? Machine { get; private set; }

    public Exercise(long id, string name, string description, string videoUrl, long trainerId, Equipment? equipment, Machine? machine)
    {
        Id = id;
        Name = name;
        Description = description;
        VideoUrl = videoUrl;
        TrainerId = trainerId;
        Equipment =  equipment;
        Machine = machine;
    }
    
    public Exercise(string name, string description, string videoUrl, long trainerId, Equipment? equipment, Machine? machine)
    {
        Name = name;
        Description = description;
        VideoUrl = videoUrl;
        TrainerId = trainerId;
        Equipment =  equipment;
        Machine = machine;
    }
}
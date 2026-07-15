namespace JimBro.Domain;

public class Exercise
{
    public long Id { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public string VideoUrl { get; private set; }
    public long TrainerId { get; private set; }
    public Accessory? Accessory { get; private set; }
    public Machine? Machine { get; private set; }

    public Exercise(long id, string name, string description, string videoUrl, long trainerId, Accessory? accessory, Machine? machine)
    {
        Id = id;
        Name = name;
        Description = description;
        VideoUrl = videoUrl;
        TrainerId = trainerId;
        Accessory =  accessory;
        Machine = machine;
    }
    
    public Exercise(string name, string description, string videoUrl, long trainerId, Accessory? accessory, Machine? machine)
    {
        Name = name;
        Description = description;
        VideoUrl = videoUrl;
        TrainerId = trainerId;
        Accessory =  accessory;
        Machine = machine;
    }
}
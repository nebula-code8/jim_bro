namespace JimBro.Domain;

public class Exercise
{
    public long Id { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public string VideoUrl { get; private set; }
    public long TrainerId { get; private set; }
    public long? EquipmentId { get; private set; }
    public long? MachineId { get; private set; }

    public Exercise(long id, string name, string description, string videoUrl, long trainerId, long? equipmentId,
        long? machineId)
    {
        Id = id;
        Name = name;
        Description = description;
        VideoUrl = videoUrl;
        TrainerId = trainerId;
        EquipmentId = equipmentId;
        MachineId = machineId;
    }
    
    public Exercise(string name, string description, string videoUrl, long trainerId, long? equipmentId,
        long? machineId)
    {
        Name = name;
        Description = description;
        VideoUrl = videoUrl;
        TrainerId = trainerId;
        EquipmentId = equipmentId;
        MachineId = machineId;
    }
}
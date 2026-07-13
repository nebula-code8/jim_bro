namespace JimBro.Domain;

public class Accessory
{
    public long Id { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public double? Weight { get; private set; }
    
    public Accessory(string name, string description, double? weight)
    {
        Name = name;
        Description = description;
        Weight = weight;
    }
    
    public Accessory(long id, string name, string description, double? weight)
    {
        Id = id;
        Name = name;
        Description = description;
        Weight = weight;
    }
}
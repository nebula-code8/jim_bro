namespace JimBro.Domain;

public class Accessory
{
    public long Id { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }

    
    public Accessory(string name, string description)
    {
        Name = name;
        Description = description;
    }
    
    public Accessory(long id, string name, string description)
    {
        Id = id;
        Name = name;
        Description = description;
    }
}